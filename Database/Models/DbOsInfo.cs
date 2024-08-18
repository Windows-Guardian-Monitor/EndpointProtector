using Common.Contracts.Models.Ws;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class DbOsInfo : IOsInfo
    {
		public DbOsInfo(string architecture, string description, string manufacturer, string serialNumber, string osVersion, string windowsDirectory)
		{
			Architecture = architecture;
			Description = description;
			Manufacturer = manufacturer;
			SerialNumber = serialNumber;
			OsVersion = osVersion;
			WindowsDirectory = windowsDirectory;
		}

        public DbOsInfo()
        {
            
        }
		
        [Key]
		public int Id { get; set; }
        public string Architecture { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string SerialNumber { get; set; }
        public string OsVersion { get; set; }
        public string WindowsDirectory { get; set; }
    }
}