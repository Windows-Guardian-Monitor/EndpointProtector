using EndpointProtector.Models.Ram;
using Microsoft.Diagnostics.Tracing.Parsers;
using Vanara.PInvoke;

namespace EndpointProtector.BackgroundServices
{
    internal class MonitorBackgroundService : BackgroundService
    {      
        private void GetMemoryInformation()
        {
            var buff = Kernel32.MEMORYSTATUSEX.Default;
            Kernel32.GlobalMemoryStatusEx(ref buff);
            var rInfo = new RamInfo(buff.dwMemoryLoad, buff.ullTotalPhys, buff.ullAvailPhys);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            GetMemoryInformation();


            return Task.CompletedTask;
        }
    }
}
