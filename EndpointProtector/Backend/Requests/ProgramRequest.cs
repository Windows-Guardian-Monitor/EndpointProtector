using System.Text.Json.Serialization;

namespace EndpointProtector.Backend.Requests
{
	internal class ProgramRequest
	{
		public ProgramRequest(string path, string name, string hash)
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
	}
}
