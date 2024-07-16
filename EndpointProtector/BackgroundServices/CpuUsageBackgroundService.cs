using Common.Contracts.DAL;
using Common.Contracts.Providers;
using EndpointProtector.Business.Models;
using System.Diagnostics;

namespace EndpointProtector.BackgroundServices
{
    internal class CpuUsageBackgroundService(
        ICpuUsageRepository cpuUsageRepository, 
        ILogger<CpuUsageBackgroundService> logger, 
        IPeriodicTimerProvider _periodicTimerProvider) : BackgroundService
    {
        private readonly CancellationTokenSource _tokenSource = new();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            var periodicTimer = _periodicTimerProvider.GetServicesPeriodicTimer();

            do
            {
                var dbCpuUsage = new CpuUsageInfo
                {
                    CpuUsage = cpuCounter.NextValue()
                };

                logger.LogWarning(dbCpuUsage.CpuUsage.ToString());

                cpuUsageRepository.Insert(dbCpuUsage);

            } while (await periodicTimer.WaitForNextTickAsync() && _tokenSource.IsCancellationRequested is false);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _tokenSource.Cancel();
            return base.StopAsync(cancellationToken);
        }
    }
}
