namespace EndpointProtector.Models.OperatingSystem
{
    internal record OsInfo
    {
        public string Description { get; set; }
        public string VersionStr { get; set; }
        public Version OSVersion { get; set; }
        public string Architecture { get; set; }
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public string WindowsDirectory { get; set; }

        public OsInfo(string description, string versionStr, string architecture, string serialNumber, string manufacturer, string windowsDirectory)
        {
            if (Version.TryParse(versionStr, out var version))
            {
                OSVersion = version;
            }

            Description = description;
            VersionStr = versionStr;
            Architecture = architecture;
            SerialNumber = serialNumber;
            Manufacturer = manufacturer;
            WindowsDirectory = windowsDirectory;
        }
    }
}
