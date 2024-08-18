using Database.Models;

namespace Common.Contracts.DAL
{
	public interface IWindowsWorkstationRepository
    {
        void Upsert(DbWindowsWorkstation item);
        DbWindowsWorkstation GetById(int id);
		IEnumerable<DbWindowsWorkstation> GetAll();
		DbWindowsWorkstation? GetFirst();
		void Insert(DbWindowsWorkstation item);
		void Delete(int id);
	}
}
