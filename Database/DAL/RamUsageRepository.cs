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
                AvailableMemory = item.AvailableMemory,
                PercentOfMemoryUsage = item.PercentOfMemoryUsage,
                UsedMemory = item.UsedMemory
            };

            databaseContext.GetSpecificCollection<DbRamUsage>().Insert(dbRamInfo);
        }

        public IEnumerable<IRamUsageInfo> GetAll() => databaseContext.GetSpecificCollection<DbRamUsage>().FindAll();

        public void DeleteAll() => databaseContext.GetSpecificCollection<DbRamUsage>().DeleteAll();
    }
}
