using Common.Contracts.DAL;
using Common.Contracts.Providers;
using Database;
using Database.Contracts;
using Database.Repositories;
using EndpointProtector.Database;
using EndpointProtector.Operators;
using EndpointProtector.Operators.Contracts;
using EndpointProtector.Providers;
using EndpointProtector.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventLog;

internal class Program
{
    private const string _logName = "EPS";
    private const string _sourceName = "Endpoint Protection Service";

    private static void ConfigureServices(IHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
            
			var conn = context.Configuration.GetConnectionString("ConnectionString");
			services.AddMySql<DatabaseContext>(conn, ServerVersion.AutoDetect(conn), options =>
            {
                options.EnableStringComparisonTranslations();
            });

			//services.AddTransient<IRamUsageInfoRepository, RamUsageRepository>();
   //         services.AddTransient<ICpuUsageRepository, CpuUsageInfoRepository>();
            services.AddTransient<IWindowsWorkstationRepository, WsRepository>();
            services.AddTransient<IProgramRepository, ProgramRepository>();
            services.AddTransient<IClientRuleRepository, ClientRuleRepository>();

            services.AddTransient<IPeriodicTimerProvider, PeriodicTimerProvider>();
            services.AddTransient<IProgramOperator, ProgramOperator>();
            services.AddTransient<IProcessOperator, ProcessOperator>();

            services.AddSingleton<IDatabaseContext, MonitoringContext>();

            //simplesmente parei de user interfaces
            services.AddTransient<AllProcessesOperator>();

            services.AddHostedService<WmiProcessListenerBackgroundService>();
            services.AddHostedService<EtwProcessListenerBackgroundService>();
            services.AddHostedService<CpuUsageBackgroundService>();
            services.AddHostedService<RamUsageBackgroundService>();
            services.AddHostedService<InformationRetrieverBackgroundService>();
            services.AddHostedService<SynchronizationBackgroundService>();
            services.AddHostedService<CurrentProcessScannerBackgroundService>();
            services.AddHostedService<RuleHandlerBackgroundService>();
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

        var scope = host.Services.CreateScope();

        (scope.ServiceProvider.GetRequiredService<DatabaseContext>()).Database.EnsureCreated();

        host.Run();
    }
}