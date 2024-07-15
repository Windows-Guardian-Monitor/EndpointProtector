using Common.Contracts.DAL;
using Common.Contracts.Models;
using EndpointProtector.Database.Models;

namespace Database.DAL
{
    public class DiskInfoRepository : IDiskInfoRepository
    {
        private readonly IDatabaseContext _monitoringContext;

        public DiskInfoRepository(IDatabaseContext monitoringContext)
        {
            _monitoringContext = monitoringContext;
        }

        public void Delete(int id) =>
            _monitoringContext.GetSpecificCollection<DbDiskInfo>().Delete(id);

        public IDiskInfo? GetFirst() =>
            _monitoringContext.GetSpecificCollection<DbDiskInfo>().FindOne(disk => disk != null);

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

            _monitoringContext.GetSpecificCollection<DbDiskInfo>().Insert(diskInfo);
        }
    }
}
