using EndpointProtector.Services;

namespace EndpointProtector.BackgroundServices;

internal class SampleWindowsBackgroundService : BackgroundService
{
    private readonly JokeService _jokeService;
    private readonly ILogger<SampleWindowsBackgroundService> _logger;

    public SampleWindowsBackgroundService(JokeService jokeService, ILogger<SampleWindowsBackgroundService> logger)
    {
        _jokeService = jokeService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string joke = _jokeService.GetJoke();

                _logger.LogInformation("{Joke}", joke);

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            //ignored
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);
            Environment.Exit(1);
        }
    }
}
