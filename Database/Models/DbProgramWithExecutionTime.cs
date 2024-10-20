using System.Text.Json.Serialization;

namespace Database.Models
{
	public class DbProgramWithExecutionTime
	{
		public DbProgramWithExecutionTime(string path, string name)
		{
			Path = path;
			Name = name;
		}

		[JsonPropertyName("Path")]
		public string Path { get; set; }
		
		[JsonPropertyName("Name")]
		public string Name { get; set; }

		[JsonPropertyName("ExecutionTime")]
		public DateTime ExecutionTime { get; set; } = DateTime.Now;
    }
}
