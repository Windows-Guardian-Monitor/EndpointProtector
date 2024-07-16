using Common.Contracts.DAL;
using Common.Contracts.Models;
using Database.Models;

namespace Database.DAL
{
    public class RamInfoRepository(IDatabaseContext databaseContext) : IRepository<IRamNominalInfo>
    {
        public void Delete(int id) => databaseContext.GetSpecificCollection<DbRamInfo>().Delete(id);

        public IRamNominalInfo? GetFirst() => databaseContext.GetSpecificCollection<DbRamInfo>().FindOne(item => item != null);

        public void Insert(IRamNominalInfo item)
        {
            var dbRamInfo = new DbRamInfo
            {
                TotalMemory = item.TotalMemory,
                Speed = item.Speed,
                Description = item.Description,
                Manufacturer = item.Manufacturer
            };

            databaseContext.GetSpecificCollection<DbRamInfo>().Insert(dbRamInfo);
        }

        public IEnumerable<IRamNominalInfo> GetAll() => databaseContext.GetSpecificCollection<DbRamInfo>().FindAll();
    }
}
