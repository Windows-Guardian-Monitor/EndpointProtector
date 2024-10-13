using System.Text.Json.Serialization;

namespace EndpointProtector.Backend.Requests
{
	public class ProgramRequest
	{
		[JsonPropertyName("Programs")]
		public List<ProgramRequestItem> Programs { get; set; }
	}
}
