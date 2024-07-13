using EndpointProtector.Business.Models;
using EndpointProtector.Contracts.DAL;
using EndpointProtector.Contracts.Models;
using LiteDB;
using System.Diagnostics;
using System.Management;
using Vanara.PInvoke;

namespace EndpointProtector.BackgroundServices
{
    internal class MonitorBackgroundService : BackgroundService
    {
        private readonly IRepository<ICpuInfo> _cpuInfoRepository;

        public MonitorBackgroundService(IRepository<ICpuInfo> cpuInfoRepository)
        {
            _cpuInfoRepository = cpuInfoRepository;
        }

        private static void GetMemoryInformation()
        {
            var buff = Kernel32.MEMORYSTATUSEX.Default;
            Kernel32.GlobalMemoryStatusEx(ref buff);
            var rInfo = new RamInfo(buff.dwMemoryLoad, (long)buff.ullTotalPhys, (long)buff.ullAvailPhys);

            Console.WriteLine("MEMORY INFO");
            Console.WriteLine(rInfo);
            Console.WriteLine("\n\n");
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

            Console.WriteLine("DISK INFO");

            for (var i = 0; i < drives.Length; i++)
            {
                var drive = drives[i];

                disks[i] = new DiskInfo(drive.AvailableFreeSpace, drive.TotalSize, drive.Name, drive.DriveFormat);

                Console.WriteLine(disks[0]);
            }

            Console.WriteLine("\n\n");
        }

        private static CpuInfo GetCpuNominalInformation()
        {
            var cpu = new ManagementObjectSearcher("select * from Win32_Processor").Get().Cast<ManagementObject>().First();

            var architecture = (ushort)cpu["Architecture"];
            var name = (string)cpu["Name"];
            var manufacturer = (string)cpu["Manufacturer"];
            var caption = (string)cpu["Caption"];

            Console.WriteLine("CPU INFO");

            return new CpuInfo(name, caption, architecture, manufacturer);
        }

        private static void GetOsInformation()
        {
            var wmi = new ManagementObjectSearcher("select * from Win32_OperatingSystem").Get().Cast<ManagementObject>().First();

            var description = ((string)wmi["Caption"]).Trim();
            var version = (string)wmi["Version"];
            var architecture = (string)wmi["OSArchitecture"];
            var serialNumber = (string)wmi["SerialNumber"];
            var manufacturer = (string)wmi["Manufacturer"];
            var systemDrive = (string)wmi["SystemDrive"];

            Console.WriteLine("OS INFO");
            var osInfo = new OsInfo(description, version, architecture, serialNumber, manufacturer, systemDrive);
            Console.WriteLine(osInfo);
            Console.WriteLine("\n\n");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
           
            return Task.CompletedTask;
        }
    }
}
