using Common.Contracts.Models;

namespace Common.Contracts.DAL
{
    public interface IDiskInfoRepository : IRepository<IDiskInfo>
    {
        void Insert(IEnumerable<IDiskInfo> diskInfos);
    }
}
