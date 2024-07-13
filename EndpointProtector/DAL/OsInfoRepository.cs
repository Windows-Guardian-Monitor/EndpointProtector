using EndpointProtector.Contracts;
using EndpointProtector.Contracts.DAL;
using EndpointProtector.Contracts.Models;
using EndpointProtector.Database.Models;

namespace EndpointProtector.DAL
{
    internal class OsInfoRepository(IDatabaseContext databaseContext) : IRepository<IOsInfo>
    {
        public void Delete(int id) => databaseContext.GetSpecificCollection<DbOsInfo>().Delete(id);

        public IOsInfo? GetFirst() => databaseContext.GetSpecificCollection<DbOsInfo>().FindOne(os => os != null);

        public void Insert(IOsInfo item)
        {
            var dbOsInfo = new DbOsInfo
            {
                Architecture = item.Architecture,
                Description = item.Description,
                Manufacturer = item.Manufacturer,
                OSVersion = item.OSVersion,
                SerialNumber = item.SerialNumber,
                VersionStr = item.VersionStr,
                WindowsDirectory = item.WindowsDirectory
            };

            databaseContext.GetSpecificCollection<DbOsInfo>().Insert(dbOsInfo);
        }
    }
}
