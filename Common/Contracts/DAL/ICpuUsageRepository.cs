using Common.Contracts.Models;

namespace Common.Contracts.DAL
{
    public interface ICpuUsageRepository : IRepository<ICpuUsageInfo>
    {
        void DeleteAll();
    }
}
