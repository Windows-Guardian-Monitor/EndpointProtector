using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
	public class DbProgram
	{
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
    }
}
