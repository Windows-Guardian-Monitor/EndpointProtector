using EndpointProtector.Contracts.Models;

namespace EndpointProtector.Contracts.DAL
{
    internal interface IDiskInfoRepository : IRepository<IDiskInfo>
    {
        void Insert(IEnumerable<IDiskInfo> diskInfos);
    }
}
