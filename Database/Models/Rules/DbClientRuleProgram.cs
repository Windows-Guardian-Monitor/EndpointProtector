using System.ComponentModel.DataAnnotations;

namespace Database.Models.Rules
{
	public class DbClientRuleProgram
	{
        public DbClientRuleProgram()
        {
            
        }

		public DbClientRuleProgram(string path, string name, string hash)
		{
			Path = path;
			Name = name;
			Hash = hash;
		}

		[Key]
		public int Id { get; set; }

		public string Path { get; set; }

		public string Name { get; set; }

		public string Hash { get; set; }
	}
}
