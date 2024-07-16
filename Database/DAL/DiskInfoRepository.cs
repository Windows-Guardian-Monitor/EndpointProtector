using Common.Contracts.DAL;
using Common.Contracts.Models;
using Database.Models;

namespace Database.DAL
{
    public class DiskInfoRepository : IDiskInfoRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public DiskInfoRepository(IDatabaseContext monitoringContext)
        {
            _databaseContext = monitoringContext;
        }

        public void Delete(int id) =>
            _databaseContext.GetSpecificCollection<DbDiskInfo>().Delete(id);

        public IDiskInfo? GetFirst() =>
            _databaseContext.GetSpecificCollection<DbDiskInfo>().FindOne(disk => disk != null);

        public void Insert(IEnumerable<IDiskInfo> diskInfos)
        {
            var disks = new DbDiskInfo[diskInfos.Count()];

            var i = 0;

            foreach (var item in diskInfos)
            {
                disks[i++] = new DbDiskInfo
                {
                    TotalSize = item.TotalSize,
                    AvailableSize = item.AvailableSize,
                    DiskName = item.DiskName,
                    DiskType = item.DiskType,
                };
            }
        }

        public void Insert(IDiskInfo item)
        {
            var diskInfo = new DbDiskInfo
            {
                AvailableSize = item.AvailableSize,
                DiskName = item.DiskName,
                DiskType = item.DiskType,
                TotalSize = item.TotalSize,
            };

            _databaseContext.GetSpecificCollection<DbDiskInfo>().Insert(diskInfo);
        }

        public IEnumerable<IDiskInfo> GetAll() => _databaseContext.GetSpecificCollection<DbDiskInfo>().FindAll();
    }
}
