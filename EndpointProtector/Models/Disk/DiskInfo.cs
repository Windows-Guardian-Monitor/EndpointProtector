using EndpointProtector.Extensions;
using EndpointProtector.Models.Ram;

namespace EndpointProtector.Models.Disk
{
    internal record DiskInfo
    {
        public DiskInfo(long availableSize, long totalSize, string? diskName, string? diskType)
        {
            AvailableSize = availableSize.ConvertToStorage();
            TotalSize = totalSize.ConvertToStorage();
            DiskName = diskName;
            DiskType = diskType;
        }

        public Storage? AvailableSize { get; set; }
        public Storage? TotalSize { get; set; }
        public string? DiskName { get; set; }
        public string? DiskType { get; set; }
    }
}
