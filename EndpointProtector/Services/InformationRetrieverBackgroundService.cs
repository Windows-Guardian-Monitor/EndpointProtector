using Common.Contracts.DAL;
using EndpointProtector.Business.Models;
using System.Management;
using Vanara.PInvoke;

namespace EndpointProtector.Services
{
    internal class InformationRetrieverBackgroundService : BackgroundService
    {
        private readonly IWindowsWorkstationRepository _windowsWorkstationRepository;

        public InformationRetrieverBackgroundService(IWindowsWorkstationRepository windowsWorkstationRepository)
        {
            _windowsWorkstationRepository = windowsWorkstationRepository;
        }

        public RamNominalInfo GetRamInfo()
        {
            var buff = Kernel32.MEMORYSTATUSEX.Default;
            Kernel32.GlobalMemoryStatusEx(ref buff);

            var result = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory").Get().Cast<ManagementObject>().First();

            var description = (string)result["Description"];
            var manufacturer = (string)result["Manufacturer"];
            var speed = (uint)result["Speed"];

            return new RamNominalInfo((long)buff.ullTotalPhys, description, manufacturer, speed);
        }

        private static DiskInfo[] GetDiskInfo()
        {
            var drives = DriveInfo.GetDrives();
            var disks = new DiskInfo[drives.Length];

            var i = 0;
            foreach (var item in drives)
            {
                disks[i++] = new DiskInfo(item.AvailableFreeSpace, item.TotalSize, item.Name, item.DriveFormat);
            }

            return disks;
        }

        private static CpuInfo GetCpuNominalInformation()
        {
            var cpu = new ManagementObjectSearcher("select * from Win32_Processor").Get().Cast<ManagementObject>().First();

            var architecture = (ushort)cpu["Architecture"];
            var name = (string)cpu["Name"];
            var manufacturer = (string)cpu["Manufacturer"];
            var caption = (string)cpu["Caption"];

            return new CpuInfo(name, caption, architecture, manufacturer);
        }

        private static OsInfo GetOsInformation()
        {
            var wmi = new ManagementObjectSearcher("select * from Win32_OperatingSystem").Get().Cast<ManagementObject>().First();

            var description = ((string)wmi["Caption"]).Trim();
            var version = (string)wmi["Version"];
            var architecture = (string)wmi["OSArchitecture"];
            var serialNumber = (string)wmi["SerialNumber"];
            var manufacturer = (string)wmi["Manufacturer"];
            var systemDrive = (string)wmi["SystemDrive"];

            return new OsInfo(description, version, architecture, serialNumber, manufacturer, systemDrive);
        }

        private static string GetMachineUuid()
        {
            var wmi = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystemProduct").Get().Cast<ManagementObject>().First();

            var uuid = wmi["UUID"] as string;

            if (uuid is not null)
            {
                return uuid;
            }

            throw new InvalidOperationException("Could not obtain uuid");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var osInformation = GetOsInformation();
            var cpuInfomation = GetCpuNominalInformation();
            var disks = GetDiskInfo();
            var ramInfo = GetRamInfo();
            var uuid = GetMachineUuid();

            var workstation = new WindowsWorkstation
            {
                CpuInfo = cpuInfomation,
                DisksInfo = disks,
                OsInfo = osInformation,
                RamInfo = ramInfo,
                Uuid = uuid,
            };

            _windowsWorkstationRepository.Upsert(workstation);

            return Task.CompletedTask;
        }
    }
}
