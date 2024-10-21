using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
	public class DbWindowsWorkstation
    {
		public DbWindowsWorkstation(DbCpuInfo cpuInfo, IEnumerable<DbDiskInfo> disksInfo, DbOsInfo osInfo, DbRamInfo ramInfo, string uuid, string hostName)
		{
			CpuInfo = cpuInfo;
			DisksInfo = disksInfo.ToList();
			OsInfo = osInfo;
			RamInfo = ramInfo;
			Uuid = uuid;
			HostName = hostName;
		}

        public DbWindowsWorkstation()
        {
            
        }

        public DbCpuInfo CpuInfo { get; set; }
        public List<DbDiskInfo> DisksInfo { get; set; }
        public DbOsInfo OsInfo { get; set; }
        [Key]
        public int Id { get; set; }
        public DbRamInfo RamInfo { get; set; }
        public string Uuid { get; set; }
		public string HostName { get; set; }
	}
}
