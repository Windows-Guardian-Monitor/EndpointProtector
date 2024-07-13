using EndpointProtector.BackgroundServices;
using EndpointProtector.Contracts;
using EndpointProtector.Contracts.DAL;
using EndpointProtector.Contracts.Models;
using EndpointProtector.DAL;
using EndpointProtector.Database;
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

            services.AddTransient<IRepository<ICpuInfo>, CpuInfoRepository>();
            services.AddTransient<IRepository<IOsInfo>, OsInfoRepository>();
            services.AddTransient<IRepository<IRamInfo>, RamInfoRepository>();
            services.AddTransient<IDiskInfoRepository, DiskInfoRepository>();

            services.AddSingleton<IDatabaseContext, MonitoringContext>();
            services.AddHostedService<MonitorBackgroundService>();
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