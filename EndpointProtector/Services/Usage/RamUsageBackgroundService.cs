using Common;
using Common.Contracts.Models.Ws;
using Common.Contracts.Providers;
using EndpointProtector.Backend.Responses;
using EndpointProtector.Business.Models.Performance;
using EndpointProtector.Business.Models.Ws;
using System.Net.Http.Json;
using System.Text.Json;
using Vanara.PInvoke;

namespace EndpointProtector.Services.Usage
{
    internal class RamUsageBackgroundService(
        //IRamUsageInfoRepository ramRepository,
        IPeriodicTimerProvider periodicTimerProvider,
        ILogger<RamUsageBackgroundService> logger) : BackgroundService
    {
        private readonly CancellationTokenSource _tokenSource = new();
        private HttpClient _httpClient;

        public IRamUsageInfo GetRamInfo()
        {
            var buff = Kernel32.MEMORYSTATUSEX.Default;
            Kernel32.GlobalMemoryStatusEx(ref buff);
            return new RamInfo(buff.dwMemoryLoad, (long)buff.ullTotalPhys, (long)buff.ullAvailPhys);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var periodicTimer = periodicTimerProvider.GetServicesPeriodicTimer();

            _httpClient = new HttpClient();

            do
            {
                try
                {
                    var info = GetRamInfo();

                    var ramPerformanceModel = new RamPerformanceModel
                    {
                        RamUsagePercentage = ConvertToPercentage(info.PercentOfMemoryUsage)
                    };

                    var url = $"{InformationHandler.GetUrl()}Performance/SendRamPerformanceInformation";

                    var json = JsonSerializer.Serialize(ramPerformanceModel);

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
                    continue;
                }
            } while (await periodicTimer.WaitForNextTickAsync(stoppingToken) && _tokenSource.IsCancellationRequested is false);
        }

        private string ConvertToPercentage(uint value) => $"{value}";

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _tokenSource.Cancel();
            return base.StopAsync(cancellationToken);
        }
    }
}
