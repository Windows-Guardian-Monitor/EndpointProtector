using Database.Models.Rules;
using System.Text.Json.Serialization;

namespace EndpointProtector.Backend.Responses
{
	internal class AllRulesResponse
	{

		[JsonPropertyName("Rules")]
		public List<DbClientRule> Rules { get; set; }

		[JsonPropertyName("Success")]
		public bool Success { get; set; }

		[JsonPropertyName("Message")]
		public string Message { get; set; }
	}
}
