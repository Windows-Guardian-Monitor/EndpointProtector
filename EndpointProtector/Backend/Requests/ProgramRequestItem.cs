using System.Text.Json.Serialization;

namespace EndpointProtector.Backend.Requests
{
	public class ProgramRequestItem
	{
		public ProgramRequestItem(string path, string name, string hash)
		{
			Path = path;
			Name = name;
			Hash = hash;
		}

		[JsonPropertyName("Path")]
		public string Path { get; set; }
		
		[JsonPropertyName("Name")]
		public string Name { get; set; }
		
		[JsonPropertyName("Hash")]
		public string Hash { get; set; }

		[JsonPropertyName("Hostname")]
		public string Hostname => Environment.MachineName;
	}
}
