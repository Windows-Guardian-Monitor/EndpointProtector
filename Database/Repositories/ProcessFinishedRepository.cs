using Database.Models.Reports;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Repositories
{
	public class ProcessFinishedRepository
	{
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly DatabaseContext _databaseContext;

        public ProcessFinishedRepository(IServiceScopeFactory serviceScopeFactory)
        {
			_serviceScopeFactory = serviceScopeFactory;
			var scope = _serviceScopeFactory.CreateScope();
			_databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
		}

		public void Insert(DbProcessFinishedEvent dbProcessFinishedEvent)
		{
			_databaseContext.FinishedProcesses.Add(dbProcessFinishedEvent);
			_databaseContext.SaveChanges();
		}

		public List<DbProcessFinishedEvent> GetAll() => _databaseContext.FinishedProcesses.ToList();
	}
}
