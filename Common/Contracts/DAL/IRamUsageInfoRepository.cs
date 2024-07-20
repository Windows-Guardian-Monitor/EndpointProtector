using Common.Contracts.Models;

namespace Common.Contracts.DAL
{
    public interface IRamUsageInfoRepository : IRepository<IRamUsageInfo>
    {
        void DeleteAll();
    }
}
