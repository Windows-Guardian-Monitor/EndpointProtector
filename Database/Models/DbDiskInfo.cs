using Common.Contracts.Models;
using EndpointProtector.Models.Ram;

namespace EndpointProtector.Database.Models
{
    internal class DbDiskInfo : IDiskInfo
    {
        public int Id { get; set; }
        public Storage? AvailableSize { get; set; }
        public string? DiskName { get; set; }
        public string? DiskType { get; set; }
        public Storage? TotalSize { get; set; }
    }
}
