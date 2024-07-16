using Common.Contracts.DAL;
using Common.Contracts.Models;
using Database.Models;

namespace Database.DAL
{
    public class CpuInfoRepository(IDatabaseContext databaseContext) : IRepository<ICpuInfo>
    {
        private bool _disposed = false;

        public void Delete(int id) =>
            databaseContext.GetSpecificCollection<DbCpuInfo>().Delete(id);

        public ICpuInfo? GetFirst() =>
            databaseContext.GetSpecificCollection<DbCpuInfo>()?.FindOne(item => item != null);

        public void Insert(ICpuInfo cpuInfo)
        {
            var dbCpuInfo = new DbCpuInfo
            {
                Architecture = cpuInfo.Architecture,
                Description = cpuInfo.Description,
                Manufacturer = cpuInfo.Manufacturer,
                Name = cpuInfo.Name,
            };

            var collection = databaseContext.GetSpecificCollection<DbCpuInfo>();

            collection.Insert(dbCpuInfo);
        }

        public IEnumerable<ICpuInfo> GetAll() => databaseContext.GetSpecificCollection<DbCpuInfo>().FindAll();

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed is false)
            {
                if (disposing)
                {
                    databaseContext.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }        
    }
}
