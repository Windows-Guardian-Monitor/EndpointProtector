using EndpointProtector.Contracts;
using EndpointProtector.Contracts.DAL;
using EndpointProtector.Contracts.Models;
using EndpointProtector.Database.Models;

namespace EndpointProtector.DAL
{
    internal class RamInfoRepository(IDatabaseContext databaseContext) : IRepository<IRamInfo>
    {
        public void Delete(int id) => databaseContext.GetSpecificCollection<DbRamInfo>().Delete(id);

        public IRamInfo? GetFirst() => databaseContext.GetSpecificCollection<DbRamInfo>().FindOne(item => item != null);

        public void Insert(IRamInfo item)
        {
            var dbRamInfo = new DbRamInfo
            {
                AvailableMemory = item.AvailableMemory,
                PercentOfMemoryUsage = item.PercentOfMemoryUsage,
                UsedMemory = item.UsedMemory
            };

            databaseContext.GetSpecificCollection<DbRamInfo>().Insert(dbRamInfo);
        }
    }
}
