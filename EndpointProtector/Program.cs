using Common.Contracts.DAL;
using Common.Contracts.Models;
using Common.Contracts.Providers;
using Database;
using Database.DAL;
using EndpointProtector.BackgroundServices;
using EndpointProtector.Database;
using EndpointProtector.Rules;
using Microsoft.Extensions.Logging.EventLog;

internal class Program
{
    private const string _logName = "Test";
    private const string _sourceName = "Endpoint Protection Service";

    private static void ConfigureServices(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            //services.AddSingleton<JokeService>();


            //services.AddHostedService<WmiProcessListenerBackgroundService>();
            //services.AddHostedService<EtwProcessListenerBackgroundService>();

            //services.AddHostedService<SampleWindowsBackgroundService>();

            services.AddTransient<IRamUsageInfoRepository, RamUsageRepository>();
            services.AddTransient<ICpuUsageRepository, CpuUsageInfoRepository>();
            services.AddTransient<IWindowsWorkstationRepository, WindowsWorkstationRepository>();

            services.AddTransient<IPeriodicTimerProvider, PeriodicTimerProvider>();

            services.AddSingleton<IDatabaseContext, MonitoringContext>();

            //services.AddHostedService<MonitorBackgroundService>();
            services.AddHostedService<CpuUsageBackgroundService>();
            services.AddHostedService<RamInfoBackgroundService>();
            services.AddHostedService<SynchronizationBackgroundService>();
        });
    }

    private static void ConfigureWindowService(IHostBuilder builder) =>
        builder.ConfigureServices(services => services.AddWindowsService(opt => opt.ServiceName = "Endpoint Protector Service"));

    private static void ConfigureLogging(IHostBuilder builder)
    {
        var logSettings = new EventLogSettings
        {
            LogName = _logName,
            SourceName = _sourceName
        };

        builder.ConfigureLogging(opt =>
        {
            opt.ClearProviders();
            opt.AddConsole();
            opt.AddEventLog(logSettings);
        });
    }

    private static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);

        DatabaseConfiguringService.CreateFolder();

        ConfigureWindowService(builder);

        ConfigureServices(builder);

        ConfigureLogging(builder);

        var host = builder.Build();

        host.Run();
    }
}