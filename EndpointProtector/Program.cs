using EndpointProtector.BackgroundServices;
using EndpointProtector.Services;
using Microsoft.Extensions.Logging.EventLog;

internal class Program
{
    private const string _logName = "Test";
    private const string _sourceName = "Endpoint Protection Service";

    private static void ConfigureServices(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<JokeService>();
            //services.AddHostedService<SampleWindowsBackgroundService>();
            services.AddHostedService<EtwProcessListenerBackgroundService>();
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

        ConfigureWindowService(builder);

        ConfigureServices(builder);

        ConfigureLogging(builder);

        var host = builder.Build();

        host.Run();
    }
}