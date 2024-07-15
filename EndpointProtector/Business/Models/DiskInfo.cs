using Common.Contracts.Models;
using EndpointProtector.Extensions;
using EndpointProtector.Models.Ram;

namespace EndpointProtector.Business.Models
{
    internal record DiskInfo : IDiskInfo
    {
        public int Id { get; set; }
        public Storage? AvailableSize { get; set; }
        public Storage? TotalSize { get; set; }
        public string? DiskName { get; set; }
        public string? DiskType { get; set; }

        public DiskInfo(long availableSize, long totalSize, string? diskName, string? diskType)
        {
            AvailableSize = availableSize.ConvertToStorage();
            TotalSize = totalSize.ConvertToStorage();
            DiskName = diskName;
            DiskType = diskType;
        }
    }
}
