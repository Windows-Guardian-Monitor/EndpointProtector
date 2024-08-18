using Common.Contracts.Providers;
using EndpointProtector.Business.Models.Ws;
using System.Diagnostics;

namespace EndpointProtector.Services
{
	internal class CpuUsageBackgroundService(
        //ICpuUsageRepository cpuUsageRepository,
        IPeriodicTimerProvider periodicTimerProvider) : BackgroundService
    {
        private readonly CancellationTokenSource _tokenSource = new();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            var periodicTimer = periodicTimerProvider.GetServicesPeriodicTimer();

            do
            {
                var dbCpuUsage = new CpuUsageInfo
                {
                    CpuUsage = cpuCounter.NextValue()
                };

                //cpuUsageRepository.Insert(dbCpuUsage);

            } while (await periodicTimer.WaitForNextTickAsync() && _tokenSource.IsCancellationRequested is false);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _tokenSource.Cancel();
            return base.StopAsync(cancellationToken);
        }
    }
}
