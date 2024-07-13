using EndpointProtector.Models.Ram;

namespace EndpointProtector.Contracts.Models
{
    internal interface IDiskInfo
    {
        public int Id { get; set; }
        Storage? AvailableSize { get; set; }
        string? DiskName { get; set; }
        string? DiskType { get; set; }
        Storage? TotalSize { get; set; }
    }
}