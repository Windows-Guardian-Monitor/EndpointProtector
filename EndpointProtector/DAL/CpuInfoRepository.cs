using EndpointProtector.Contracts;
using EndpointProtector.Contracts.DAL;
using EndpointProtector.Contracts.Models;
using EndpointProtector.Database.Models;

namespace EndpointProtector.DAL
{
    internal class CpuInfoRepository(IDatabaseContext monitoringContext) : IRepository<ICpuInfo>
    {
        private bool _disposed = false;

        public void Delete(int id) => 
            monitoringContext.GetSpecificCollection<DbCpuInfo>().Delete(id);

        public ICpuInfo? GetFirst() =>
            monitoringContext.GetSpecificCollection<DbCpuInfo>()?.FindOne(item => item != null);

        public void Insert(ICpuInfo cpuInfo)
        {
            var dbCpuInfo = new DbCpuInfo
            {
                Architecture = cpuInfo.Architecture,
                Description = cpuInfo.Description,
                Manufacturer = cpuInfo.Manufacturer,
                Name = cpuInfo.Name,
            };

            var collection = monitoringContext.GetSpecificCollection<DbCpuInfo>();

            collection.Insert(dbCpuInfo);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed is false)
            {
                if (disposing)
                {
                    monitoringContext.Dispose();
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
