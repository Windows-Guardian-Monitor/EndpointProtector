using System.Text.Json.Serialization;

namespace EndpointProtector.Backend.Responses
{
	internal class RuleResponse
	{
		[JsonPropertyName("Programs")]
        public List<RuleProgramResponse> Programs { get; set; }
    }
}
