using System.Text.Json.Serialization;

namespace EndpointProtector.Business.Models.Performance
{
	public class RamPerformanceModel
	{
		[JsonPropertyName("RamUsagePercentage")]
		public string RamUsagePercentage { get; set; } = string.Empty;

		[JsonPropertyName("MachineName")]
		public string MachineName { get; set; }
	}
}
