using System.Text.Json.Serialization;

namespace Database.Models.Reports
{
	public class DbProcessFinishedEvent
	{
		public DbProcessFinishedEvent(string userName, string domain, string machineName, string programHash, string programPath)
		{
			UserName = userName;
			Domain = domain;
			MachineName = machineName;
			ProgramHash = programHash;
			ProgramPath = programPath;
			Timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
		}

		[JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("UserName")]
		public string UserName { get; set; } = string.Empty;
		
		[JsonPropertyName("Domain")]
		public string Domain { get; set; } = string.Empty;
		
		[JsonPropertyName("MachineName")]
		public string MachineName { get; set; } = string.Empty;

		[JsonPropertyName("ProgramHash")]
		public string ProgramHash { get; set; } = string.Empty;

		[JsonPropertyName("ProgramPath")]
		public string ProgramPath { get; set; } = string.Empty;
        
		[JsonPropertyName("Timestamp")]
		public long Timestamp { get; set; }
    }
}
