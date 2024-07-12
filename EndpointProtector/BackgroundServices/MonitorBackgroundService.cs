using EndpointProtector.Models.Cpu;
using EndpointProtector.Models.Disk;
using EndpointProtector.Models.Ram;
using System.Diagnostics;
using System.Management;
using Vanara.PInvoke;

namespace EndpointProtector.BackgroundServices
{
    internal class MonitorBackgroundService : BackgroundService
    {
        private static void GetMemoryInformation()
        {
            var buff = Kernel32.MEMORYSTATUSEX.Default;
            Kernel32.GlobalMemoryStatusEx(ref buff);
            var rInfo = new RamInfo(buff.dwMemoryLoad, (long)buff.ullTotalPhys, (long)buff.ullAvailPhys);
        }

        private static async ValueTask GetCpuUsage()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            while (true)
            {
                await Console.Out.WriteLineAsync(cpuCounter.NextValue() + "%");
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        private static void GetDiskInfo()
        {
            var drives = DriveInfo.GetDrives();
            var disks = new DiskInfo[drives.Length];

            for (var i = 0; i < drives.Length; i++)
            {
                var drive = drives[i];

                disks[i] = new DiskInfo(drive.AvailableFreeSpace, drive.TotalSize, drive.Name, drive.DriveFormat);
            }
        }

        private static void GetCpuNominalInformation()
        {
            var cpu = new ManagementObjectSearcher("select * from Win32_Processor").Get().Cast<ManagementObject>().First();

            var architecture = (ushort)cpu["Architecture"];
            var name = (string)cpu["Name"];
            var manufacturer = (string)cpu["Manufacturer"];
            var caption = (string)cpu["Caption"];

            var cpuInfo = new CpuInfo(name, caption, architecture, manufacturer);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            GetMemoryInformation();

            //await GetCpuInformation();

            GetDiskInfo();

            GetCpuNominalInformation();

            return Task.CompletedTask;
        }
    }
}
