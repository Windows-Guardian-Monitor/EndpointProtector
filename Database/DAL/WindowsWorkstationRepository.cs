using Common.Contracts.DAL;
using Common.Contracts.Models;
using Database.Models;

namespace Database.DAL
{
    public class WindowsWorkstationRepository(IDatabaseContext databaseContext) : IWindowsWorkstationRepository
    {
        public void Delete(int id) => databaseContext.GetSpecificCollection<DbWindowsWorkstation>().Delete(id);

        public IEnumerable<IWindowsWorkstation> GetAll() => databaseContext.GetSpecificCollection<DbWindowsWorkstation>().FindAll();

        public IWindowsWorkstation? GetFirst() => databaseContext.GetSpecificCollection<DbWindowsWorkstation>().FindOne(item => item != null);

        public void Insert(IWindowsWorkstation item)
        {
            var dbWindowsWorkstation = new DbWindowsWorkstation
            {
                CpuInfo = item.CpuInfo,
                DisksInfo = item.DisksInfo,
                OsInfo = item.OsInfo,
                RamInfo = item.RamInfo
            };

            databaseContext.GetSpecificCollection<DbWindowsWorkstation>().Insert(dbWindowsWorkstation);
        }

        public void Upsert(IWindowsWorkstation item)
        {
            var dbWorkstation = GetFirst();


            var dbCpuInfo = new DbCpuInfo
            {
                Architecture = item.CpuInfo.Architecture,
                Description = item.CpuInfo.Description,
                Manufacturer = item.CpuInfo.Manufacturer,
                Name = item.CpuInfo.Name
            };

            var dbOsVersion = new DbOsInfo
            {
                Architecture = item.OsInfo.Architecture,
                Description = item.OsInfo.Description,
                Manufacturer = item.OsInfo.Manufacturer,
                OsVersion = item.OsInfo.OsVersion,
                SerialNumber = item.OsInfo.SerialNumber,
                WindowsDirectory = item.OsInfo.WindowsDirectory
            };

            var dbRamInfo = new DbRamInfo
            {
                Description = item.RamInfo.Description,
                Manufacturer = item.RamInfo.Manufacturer,
                TotalMemory = item.RamInfo.TotalMemory,
                Speed = item.RamInfo.Speed
            };


            var dbDisks = new DbDiskInfo[item.DisksInfo.Count()];

            int i = 0;

            foreach (var disk in item.DisksInfo)
            {
                dbDisks[i++] = new DbDiskInfo
                {
                    AvailableSize = disk.AvailableSize,
                    DiskName = disk.DiskName,
                    DiskType = disk.DiskType,
                    TotalSize = disk.TotalSize,
                };
            }

            var windowsWorkstation = new DbWindowsWorkstation
            {
                CpuInfo = dbCpuInfo,
                DisksInfo = dbDisks,
                OsInfo = dbOsVersion,
                RamInfo = dbRamInfo,
                Uuid = item.Uuid
            };

            if (dbWorkstation is null)
            {
                Insert(windowsWorkstation);

                return;
            }

            windowsWorkstation.Id = dbWorkstation.Id;

            databaseContext.GetSpecificCollection<DbWindowsWorkstation>().Update(windowsWorkstation);
        }
    }
}
