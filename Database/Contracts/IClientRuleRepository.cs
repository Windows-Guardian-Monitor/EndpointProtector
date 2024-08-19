using Database.Models.Rules;

namespace Database.Contracts
{
	public interface IClientRuleRepository
    {
        void DeleteAll();
        void InsertMany(List<DbClientRule> rules);
        IList<DbClientRule> GetAll();
	}
}