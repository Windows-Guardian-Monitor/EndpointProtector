using EndpointProtector.Contracts.DAL;
using EndpointProtector.Contracts.Models;
using EndpointProtector.Database;
using EndpointProtector.Database.Models;

namespace EndpointProtector.DAL
{
    internal class DiskInfoRepository : IDiskInfoRepository
    {
        private readonly MonitoringContext _monitoringContext;

        public DiskInfoRepository(MonitoringContext monitoringContext)
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
