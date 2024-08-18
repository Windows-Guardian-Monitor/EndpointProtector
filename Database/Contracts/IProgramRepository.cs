using Database.Models;

namespace Database.Contracts
{
	public interface IProgramRepository
	{
		void Insert(DbProgram item);
		IEnumerable<DbProgram> GetAll();
		void DeleteAll();
		bool Exists(string hash);
	}
}
