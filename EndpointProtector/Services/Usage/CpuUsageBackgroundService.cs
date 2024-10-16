using Common;
using Common.Contracts.Providers;
using EndpointProtector.Business.Models.Performance;
using System.Diagnostics;
using System.Net.Http.Json;

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
                //var dbCpuUsage = new CpuUsageInfo
                //{
                //    CpuUsage = cpuCounter.NextValue()
                //};

                try
                {
					var cpuPerformanceModel = new CpuPerformanceModel()
					{
						CpuUsagePercentage = ConvertToPercentage(cpuCounter.NextValue()),
					};

                    var url = $"{InformationHandler.GetUrl()}Performance/SendCpuPerformanceInformation";

                    //TODO CONTINUAR DAQUI, VER SE É POSSÍVEL ENVIAR A INFO DO PROCESSADOR P API

					var r = await _httpClient.PostAsJsonAsync(url, cpuPerformanceModel, cancellationToken: stoppingToken);
				}
                catch (Exception e)
                {
                    logger.LogError(e.Message);
				}
			} while (await periodicTimer.WaitForNextTickAsync() && _tokenSource.IsCancellationRequested is false);
        }

		private string ConvertToPercentage(float p)
		{
			return $"{p}%";
		}

		public override Task StopAsync(CancellationToken cancellationToken)
        {
            _tokenSource.Cancel();
            return base.StopAsync(cancellationToken);
        }
    }
}
