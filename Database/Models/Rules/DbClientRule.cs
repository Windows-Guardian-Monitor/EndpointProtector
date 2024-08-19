using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Database.Models.Rules
{
	public class DbClientRule
    {
		public DbClientRule(List<DbClientRuleProgram> programs)
		{
			Programs = programs;
		}

        public DbClientRule()
        {
            
        }

        [Key]
        public int Id { get; set; }

        [JsonPropertyName("Programs")]
        public List<DbClientRuleProgram> Programs { get; set; }
    }
}
