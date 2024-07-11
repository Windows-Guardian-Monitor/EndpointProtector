using EndpointProtector.Extensions;
using EndpointProtector.Models.Disk;
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
            var rInfo = new RamInfo(buff.dwMemoryLoad, (long)buff.ullTotalPhys, (long)buff.ullAvailPhys);
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

        private void GetDiskInfo()
        {
            var drives = DriveInfo.GetDrives();
            var disks = new DiskInfo[drives.Length];

            for (var i = 0; i < drives.Length; i++)
            {
                var drive = drives[i];

                var availableSize = drive.AvailableFreeSpace.ConvertToStorage();
                var totalSize = drive.TotalSize.ConvertToStorage();

                disks[i] = new DiskInfo
                {
                    DiskName = drive.Name,
                    AvailableSize = availableSize,
                    TotalSize = totalSize,
                    DiskType = drive.DriveFormat
                };
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            GetMemoryInformation();

            //await GetCpuInformation();

            GetDiskInfo();
        }
    }
}
