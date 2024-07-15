using Common.Contracts.DAL;
using Common.Contracts.Models;
using EndpointProtector.Business.Models;
using LiteDB;
using System.Diagnostics;
using System.Management;
using Vanara.PInvoke;

namespace EndpointProtector.BackgroundServices
{
    internal class MonitorBackgroundService : BackgroundService
    {
        private readonly IRepository<ICpuInfo> _cpuInfoRepository;
        private readonly IRepository<IOsInfo> _osRepository;
        private readonly IRepository<IRamInfo> _ramRepository;
        private readonly IDiskInfoRepository _diskInfoRepository;

        public MonitorBackgroundService(
            IRepository<ICpuInfo> cpuInfoRepository, 
            IRepository<IOsInfo> osRepository, 
            IRepository<IRamInfo> ramRepository, 
            IDiskInfoRepository diskInfoRepository)
        {
            _cpuInfoRepository = cpuInfoRepository;
            _osRepository = osRepository;
            _ramRepository = ramRepository;
            _diskInfoRepository = diskInfoRepository;
        }

        private void GetMemoryInformation()
        {
            var buff = Kernel32.MEMORYSTATUSEX.Default;
            Kernel32.GlobalMemoryStatusEx(ref buff);
            var rInfo = new RamInfo(buff.dwMemoryLoad, (long)buff.ullTotalPhys, (long)buff.ullAvailPhys);

            _ramRepository.Insert(rInfo);
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

        private void GetDiskInfo()
        {
            var drives = DriveInfo.GetDrives();
            var disks = new DiskInfo[drives.Length];

            var i = 0;
            foreach (var item in drives)
            {
                disks[i++] = new DiskInfo(item.AvailableFreeSpace, item.TotalSize, item.Name, item.DriveFormat);
            }

            _diskInfoRepository.Insert(disks);
        }

        private void GetCpuNominalInformation()
        {
            var cpu = new ManagementObjectSearcher("select * from Win32_Processor").Get().Cast<ManagementObject>().First();

            var architecture = (ushort)cpu["Architecture"];
            var name = (string)cpu["Name"];
            var manufacturer = (string)cpu["Manufacturer"];
            var caption = (string)cpu["Caption"];

            var cpuInfo = new CpuInfo(name, caption, architecture, manufacturer);

            _cpuInfoRepository.Insert(cpuInfo);
        }

        private void GetOsInformation()
        {
            var wmi = new ManagementObjectSearcher("select * from Win32_OperatingSystem").Get().Cast<ManagementObject>().First();

            var description = ((string)wmi["Caption"]).Trim();
            var version = (string)wmi["Version"];
            var architecture = (string)wmi["OSArchitecture"];
            var serialNumber = (string)wmi["SerialNumber"];
            var manufacturer = (string)wmi["Manufacturer"];
            var systemDrive = (string)wmi["SystemDrive"];

            var osInfo = new OsInfo(description, version, architecture, serialNumber, manufacturer, systemDrive);

            _osRepository.Insert(osInfo);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            GetMemoryInformation();
            GetDiskInfo();
            GetCpuNominalInformation();
            GetOsInformation();
            return Task.CompletedTask;
        }
    }
}
