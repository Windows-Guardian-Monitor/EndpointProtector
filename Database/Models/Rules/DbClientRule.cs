using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Database.Models.Rules
{
	public class DbClientRule
    {
		public DbClientRule(List<DbClientRuleProgram> programs, string name)
		{
			Programs = programs;
			Name = name;
		}

		public DbClientRule()
        {
            
        }

        [Key]
		[JsonPropertyName("RuleId")]
		public int Id { get; set; }

        [JsonPropertyName("Programs")]
        public List<DbClientRuleProgram> Programs { get; set; }

		[JsonPropertyName("Name")]
		public string Name { get; set; }
	}
}
