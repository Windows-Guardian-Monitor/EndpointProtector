using Common.Contracts.DAL;
using Common.Contracts.Models;
using Common.Contracts.Providers;
using EndpointProtector.Business.Models;
using Vanara.PInvoke;

namespace EndpointProtector.BackgroundServices
{
    internal class RamInfoBackgroundService(
        IRamUsageInfoRepository ramRepository, 
        ILogger<RamInfoBackgroundService> logger, 
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
                var ramInfo = GetRamInfo();
                await Console.Out.WriteLineAsync("ram");
                ramRepository.Insert(ramInfo);
            } while (await periodicTimer.WaitForNextTickAsync(stoppingToken) && _tokenSource.IsCancellationRequested is false);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _tokenSource.Cancel();
            return base.StopAsync(cancellationToken);
        }
    }
}
