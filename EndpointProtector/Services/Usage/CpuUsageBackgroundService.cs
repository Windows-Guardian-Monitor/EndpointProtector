using Common;
using Common.Contracts.Providers;
using EndpointProtector.Backend.Responses;
using EndpointProtector.Business.Models.Performance;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace EndpointProtector.Services.Usage
{
    internal class CpuUsageBackgroundService(
        IPeriodicTimerProvider periodicTimerProvider,
        ILogger<CpuUsageBackgroundService> logger) : BackgroundService
    {
        private readonly CancellationTokenSource _tokenSource = new();
        private HttpClient _httpClient;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _httpClient = new HttpClient();

            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            var periodicTimer = periodicTimerProvider.GetServicesPeriodicTimer();

            do
            {
                try
                {
                    var usageValue = cpuCounter.NextValue();

                    if (usageValue is 0)
                    {
                        continue;
                    }

                    var cpuPerformanceModel = new CpuPerformanceModel()
                    {
                        CpuUsagePercentage = ConvertToPercentage(usageValue),
                    };

                    var url = $"{InformationHandler.GetUrl()}Performance/SendCpuPerformanceInformation";

                    var json = JsonSerializer.Serialize(cpuPerformanceModel);

                    var r = await _httpClient.PostAsJsonAsync(url, json, cancellationToken: stoppingToken);

                    json = await r.Content.ReadAsStringAsync();

                    var response = JsonSerializer.Deserialize<StandardResponse>(json);

                    if (response.Success is false)
                    {
                        logger.LogError(response.Message);
                        continue;
                    }
                }
                catch (Exception e)
                {
                    logger.LogError(e.Message);
                }
            } while (await periodicTimer.WaitForNextTickAsync() && _tokenSource.IsCancellationRequested is false);
        }

        private string ConvertToPercentage(float p)
        {
            return $"{p:0.00}%";
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _tokenSource.Cancel();
            return base.StopAsync(cancellationToken);
        }
    }
}
