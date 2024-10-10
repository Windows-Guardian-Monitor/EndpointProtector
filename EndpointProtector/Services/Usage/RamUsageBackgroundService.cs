using Common.Contracts.DAL;
using Common.Contracts.Models.Ws;
using Common.Contracts.Providers;
using EndpointProtector.Business.Models.Ws;
using Vanara.PInvoke;

namespace EndpointProtector.Services.Usage
{
    internal class RamUsageBackgroundService(
        //IRamUsageInfoRepository ramRepository,
        IPeriodicTimerProvider periodicTimerProvider) : BackgroundService
    {
        private readonly CancellationTokenSource _tokenSource = new();

        public IRamUsageInfo GetRamInfo()
        {
            var buff = Kernel32.MEMORYSTATUSEX.Default;
            Kernel32.GlobalMemoryStatusEx(ref buff);
            return new RamInfo(buff.dwMemoryLoad, (long)buff.ullTotalPhys, (long)buff.ullAvailPhys);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var periodicTimer = periodicTimerProvider.GetServicesPeriodicTimer();

            do
            {
                //ramRepository.Insert(GetRamInfo());
            } while (await periodicTimer.WaitForNextTickAsync(stoppingToken) && _tokenSource.IsCancellationRequested is false);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _tokenSource.Cancel();
            return base.StopAsync(cancellationToken);
        }
    }
}
