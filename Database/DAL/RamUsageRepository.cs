using Common.Contracts.Models.Ws;
using Database.Models;

namespace Database.DAL
{
	public class RamUsageRepository(IDatabaseContext databaseContext)
    {
        public void Delete(int id) => databaseContext.GetSpecificCollection<DbRamUsage>().Delete(id);

        public IRamUsageInfo? GetFirst() => databaseContext.GetSpecificCollection<DbRamUsage>().FindOne(item => item != null);

        public void Insert(IRamUsageInfo item)
        {
            var dbRamInfo = new DbRamUsage
            {
                TotalAvailableMemory = item.TotalAvailableMemory,
                PercentOfMemoryUsage = item.PercentOfMemoryUsage,
                AvailableMemory = item.AvailableMemory
            };

            databaseContext.GetSpecificCollection<DbRamUsage>().Insert(dbRamInfo);
        }

        public IEnumerable<IRamUsageInfo> GetAll() => databaseContext.GetSpecificCollection<DbRamUsage>().FindAll();

        public void DeleteAll() => databaseContext.GetSpecificCollection<DbRamUsage>().DeleteAll();
    }
}
