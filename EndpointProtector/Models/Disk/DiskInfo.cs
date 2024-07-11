using EndpointProtector.Models.Ram;

namespace EndpointProtector.Models.Disk
{
    internal record DiskInfo
    {
        public Storage? AvailableSize { get; set; }
        public Storage? TotalSize { get; set; }
        public string? DiskName { get; set; }
        public string? DiskType { get; set; }
    }
}
