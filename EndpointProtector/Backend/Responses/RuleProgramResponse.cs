using System.Text.Json.Serialization;

namespace EndpointProtector.Backend.Responses
{
	internal class RuleProgramResponse
	{
		[JsonPropertyName("Path")]
		public string Path { get; set; }

		[JsonPropertyName("Name")]
		public string Name { get; set; }

		[JsonPropertyName("Hash")]
		public string Hash { get; set; }
	}
}
