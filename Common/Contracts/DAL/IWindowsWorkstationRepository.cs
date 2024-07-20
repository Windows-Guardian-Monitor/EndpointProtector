using Common.Contracts.Models;

namespace Common.Contracts.DAL
{
    public interface IWindowsWorkstationRepository : IRepository<IWindowsWorkstation>
    {
        void Upsert(IWindowsWorkstation item);
    }
}
