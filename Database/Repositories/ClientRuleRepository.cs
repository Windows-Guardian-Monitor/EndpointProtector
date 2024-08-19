using Database.Contracts;
using Database.Models.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Repositories
{
	public class ClientRuleRepository : IClientRuleRepository
	{
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly DatabaseContext _databaseContext;

		public ClientRuleRepository(IServiceScopeFactory serviceScopeFactory)
		{
			_serviceScopeFactory = serviceScopeFactory;
			var scope = _serviceScopeFactory.CreateScope();
			_databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
		}

		public void DeleteAll()
		{
			_databaseContext.ClientRules.RemoveRange(_databaseContext.ClientRules);
			_databaseContext.ProgramRules.RemoveRange(_databaseContext.ProgramRules);
		}

		public void InsertMany(List<DbClientRule> rules)
		{
			_databaseContext.ClientRules.AddRange(rules);
			_databaseContext.SaveChanges();
		}

		public IList<DbClientRule> GetAll() => _databaseContext.ClientRules.Include(r => r.Programs).ToList();
	}
}
