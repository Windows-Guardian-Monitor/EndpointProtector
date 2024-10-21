using System.Text.Json.Serialization;

namespace EndpointProtector.Backend.Requests
{
	public class RuleRequest
	{
		public RuleRequest(string machineUuid)
		{
			MachineUuid = machineUuid;
		}

		[JsonPropertyName("MachineUuid")]
		public string MachineUuid { get; set; }
    }
}
