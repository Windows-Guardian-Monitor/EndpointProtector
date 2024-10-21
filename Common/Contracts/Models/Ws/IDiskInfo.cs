using EndpointProtector.Models.Ram;

namespace Common.Contracts.Models.Ws;

public interface IDiskInfo
{
    string? AvailableSize { get; set; }
    string? DiskName { get; set; }
    string? DiskType { get; set; }
    string? TotalSize { get; set; }
}