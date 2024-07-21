namespace EndpointProtector.Backend.Requests
{

    public class BackendWindowsWorkstation
    {
        public int Id { get; set; }
        public BackendCpuinfo CpuInfo { get; set; }
        public BackendDisksinfo[] DisksInfo { get; set; }
        public BackendOsinfo OsInfo { get; set; }
        public BackendRaminfo RamInfo { get; set; }
        public string Uuid { get; set; }
    }

    public class BackendCpuinfo
    {
        public BackendCpuinfo(string architecture, string description, string manufacturer, string name)
        {
            Architecture = architecture;
            Description = description;
            CpuManufacturer = manufacturer;
            Name = name;
        }

        public int Id { get; set; }
        public string Architecture { get; set; }
        public string Description { get; set; }
        public string CpuManufacturer { get; set; }
        public string Name { get; set; }
    }

    public class BackendOsinfo
    {
        public int Id { get; set; }
        public string Architecture { get; set; }
        public string Description { get; set; }
        public string OsManufacturer { get; set; }
        public string OsVersion { get; set; }
        public string SerialNumber { get; set; }
        public string VersionStr { get; set; }
        public string WindowsDirectory { get; set; }
    }

    public class BackendRaminfo
    {
        public int Id { get; set; }
        public string TotalMemory { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Speed { get; set; }
    }

    public class BackendDisksinfo
    {
        public int Id { get; set; }
        public string AvailableSize { get; set; }
        public string DiskName { get; set; }
        public string DiskType { get; set; }
        public string TotalSize { get; set; }
    }

}
