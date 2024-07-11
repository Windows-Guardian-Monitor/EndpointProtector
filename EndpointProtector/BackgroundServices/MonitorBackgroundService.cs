using EndpointProtector.Models.Ram;
using System.Diagnostics;
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

        private async ValueTask GetCpuInformation()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            while (true)
            {
                await Console.Out.WriteLineAsync(cpuCounter.NextValue() + "%");
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            GetMemoryInformation();

            await GetCpuInformation();
        }
    }
}
