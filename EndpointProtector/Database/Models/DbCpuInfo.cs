using EndpointProtector.Contracts.Models;
using EndpointProtector.Enums;

namespace EndpointProtector.Database.Models
{
    internal class DbCpuInfo : ICpuInfo
    {
        public int Id { get; set; }
        public Architecture Architecture { get; set; }
        public string? Description { get; set; }
        public string Manufacturer { get; set; }
        public string? Name { get; set; }
    }
}
