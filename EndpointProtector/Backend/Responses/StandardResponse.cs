using System.Net;
using System.Text.Json.Serialization;

namespace EndpointProtector.Backend.Responses
{
    public class StandardResponse
    {
        [JsonPropertyName("code")]
        public HttpStatusCode Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
