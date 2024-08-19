using Database.Contracts;
using Database.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Repositories
{
	public class ProgramRepository : IProgramRepository
	{
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly DatabaseContext _databaseContext;

		public ProgramRepository(IServiceScopeFactory serviceScopeFactory)
		{
			_serviceScopeFactory = serviceScopeFactory;
			var scope = _serviceScopeFactory.CreateScope();
			_databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
		}

		public void DeleteAll()
		{
			_databaseContext.Programs.RemoveRange(_databaseContext.Programs);
			_databaseContext.SaveChanges();
		}

		public bool Exists(string hash) => _databaseContext.Programs.Any(p => p.Hash.Equals(hash, StringComparison.OrdinalIgnoreCase));


		public IEnumerable<DbProgram> GetAll() => _databaseContext.Programs.ToList();

		public void Insert(DbProgram item)
		{
			_databaseContext.Programs.Add(item);
			_databaseContext.SaveChanges();
		}
	}
}
