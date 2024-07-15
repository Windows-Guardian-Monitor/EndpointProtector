using Common.Contracts.Models;
using System.Runtime.InteropServices;

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
