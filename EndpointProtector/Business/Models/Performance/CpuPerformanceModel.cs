using System.Text.Json.Serialization;

namespace EndpointProtector.Business.Models.Performance
{
	public class CpuPerformanceModel
	{
		[JsonPropertyName("CpuUsagePercentage")]
		public string CpuUsagePercentage { get; set; } = string.Empty;

		[JsonPropertyName("MachineName")]
		public string MachineName { get; set; } = Environment.MachineName;
	}
}
