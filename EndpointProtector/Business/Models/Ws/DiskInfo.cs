using Common.Contracts.Models.Ws;
using EndpointProtector.Extensions;
using EndpointProtector.Models.Ram;

namespace EndpointProtector.Business.Models.Ws
{
    internal record DiskInfo : IDiskInfo
    {
        public int Id { get; set; }
        public string? AvailableSize { get; set; }
        public string? TotalSize { get; set; }
        public string? DiskName { get; set; }
        public string? DiskType { get; set; }

        public DiskInfo(long availableSize, long totalSize, string? diskName, string? diskType)
        {
            AvailableSize = availableSize.ConvertToStorage().ToString();
            TotalSize = totalSize.ConvertToStorage().ToString();
            DiskName = diskName;
            DiskType = diskType;
        }
    }
}
