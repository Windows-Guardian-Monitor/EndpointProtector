using Common.Contracts.DAL;
using Database.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Repositories
{
	internal class ProgramRepository : IRepository<DbProgram>
	{
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly DatabaseContext _databaseContext;

		public ProgramRepository(IServiceScopeFactory serviceScopeFactory)
		{
			_serviceScopeFactory = serviceScopeFactory;
			var scope = _serviceScopeFactory.CreateScope();
			_databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<DbProgram> GetAll() => _databaseContext.Programs.ToList();

		public DbProgram? GetFirst()
		{
			throw new NotImplementedException();
		}

		public void Insert(DbProgram item)
		{
			_databaseContext.Programs.Add(item);
			_databaseContext.SaveChanges();
		}
	}
}
