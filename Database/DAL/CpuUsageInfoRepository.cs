using Common.Contracts.Models.Ws;
using Database.Models;

namespace Database.DAL
{
	public class CpuUsageInfoRepository(IDatabaseContext databaseContext)
    {
        public void Delete(int id) => databaseContext.GetSpecificCollection<DbCpuUsageInfo>().Delete(id);

        public void DeleteAll() => databaseContext.GetSpecificCollection<DbCpuUsageInfo>().DeleteAll();

        public IEnumerable<ICpuUsageInfo> GetAll() => databaseContext.GetSpecificCollection<DbCpuUsageInfo>().FindAll();

        public ICpuUsageInfo? GetFirst() => databaseContext.GetSpecificCollection<DbCpuUsageInfo>()?.FindOne(item => item != null);

        public void Insert(ICpuUsageInfo item)
        {
            var dbCpuUsageInfo = new DbCpuUsageInfo()
            {
                CpuUsage = item.CpuUsage
            };

            databaseContext.GetSpecificCollection<DbCpuUsageInfo>().Insert(dbCpuUsageInfo);
        }
    }
}
