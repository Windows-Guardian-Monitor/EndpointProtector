using Common.Contracts.Models.Ws;
using Database.Models;

namespace Common.Contracts.DAL
{
    public interface IWindowsWorkstationRepository : IRepository<DbWindowsWorkstation>
    {
        void Upsert(DbWindowsWorkstation item);
        DbWindowsWorkstation GetById(int id);
    }
}
