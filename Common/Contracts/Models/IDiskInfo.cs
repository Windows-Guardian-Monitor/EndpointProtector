using EndpointProtector.Models.Ram;

namespace Common.Contracts.Models;

public interface IDiskInfo
{
    Storage? AvailableSize { get; set; }
    string? DiskName { get; set; }
    string? DiskType { get; set; }
    Storage? TotalSize { get; set; }
}