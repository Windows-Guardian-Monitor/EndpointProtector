using System.Text.Json.Serialization;

namespace EndpointProtector.Backend.Responses
{
	internal class BaseRuleResponse
	{

		[JsonPropertyName("Sucess")]
		public bool Sucess { get; set; }

		[JsonPropertyName("Message")]
		public string Message { get; set; }

		[JsonPropertyName("Rules")]
		public List<RuleResponse> Rules { get; set; }
	}
}
