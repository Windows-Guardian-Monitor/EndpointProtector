using System.Net;
using System.Text.Json.Serialization;

namespace EndpointProtector.Backend.Responses
{
    public class StandardResponse
    {
        [JsonPropertyName("Code")]
        public HttpStatusCode Code { get; set; }
        [JsonPropertyName("Message")]
        public string Message { get; set; }

		[JsonPropertyName("Success")]
		public bool Success { get; set; }
	}
}
