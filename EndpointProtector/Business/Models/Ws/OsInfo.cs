using Common.Contracts.Models.Ws;

namespace EndpointProtector.Business.Models.Ws
{
	internal record OsInfo : IOsInfo
    {
        public string Description { get; set; }
        public string OsVersion { get; set; }
        public string Architecture { get; set; }
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public string WindowsDirectory { get; set; }
		public int Id { get; set; }

		public OsInfo(string description, string versionStr, string architecture, string serialNumber, string manufacturer, string windowsDirectory)
        {
            Description = description;
            OsVersion = versionStr;
            Architecture = architecture;
            SerialNumber = serialNumber;
            Manufacturer = manufacturer;
            WindowsDirectory = windowsDirectory;
        }
    }
}
