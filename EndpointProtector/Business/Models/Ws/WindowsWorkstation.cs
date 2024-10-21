using Common.Contracts.Models.Ws;
using Database.Models;

namespace EndpointProtector.Business.Models.Ws
{
	internal class WindowsWorkstation : IWindowsWorkstation
    {
        public int Id { get; set; }
        public ICpuInfo CpuInfo { get; set; }
        public IEnumerable<IDiskInfo> DisksInfo { get; set; }
        public IOsInfo OsInfo { get; set; }
        public IRamNominalInfo RamInfo { get; set; }
        public string Uuid { get; set; }
        public string HostName { get; set; }

		public static implicit operator DbWindowsWorkstation(WindowsWorkstation windowsWorkstation)
		{
            var dbCpu = new DbCpuInfo(windowsWorkstation.CpuInfo.Architecture, windowsWorkstation.CpuInfo.Description, windowsWorkstation.CpuInfo.Manufacturer, windowsWorkstation.CpuInfo.Name);

            var dbDisks = windowsWorkstation.DisksInfo.Select(d => new DbDiskInfo(d.AvailableSize, d.DiskName, d.DiskType, d.TotalSize));

            var dbOsInfo = new DbOsInfo(windowsWorkstation.OsInfo.Architecture, windowsWorkstation.OsInfo.Description, windowsWorkstation.OsInfo.Manufacturer, windowsWorkstation.OsInfo.SerialNumber, windowsWorkstation.OsInfo.OsVersion, windowsWorkstation.OsInfo.WindowsDirectory);

            var dbRamInfo = new DbRamInfo(windowsWorkstation.RamInfo.TotalMemory, windowsWorkstation.RamInfo.Description, windowsWorkstation.RamInfo.Manufacturer, windowsWorkstation.RamInfo.Speed);

			return new DbWindowsWorkstation(dbCpu, dbDisks, dbOsInfo, dbRamInfo, windowsWorkstation.Uuid, windowsWorkstation.HostName);
		}
	}
}