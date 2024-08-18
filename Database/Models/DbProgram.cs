using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
	public class DbProgram
	{
		public DbProgram(string path, string name, string hash)
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
