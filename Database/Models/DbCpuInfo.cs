using Common.Contracts.Models;
using Common.Enums;

namespace Database.Models
{
    public record DbCpuInfo : ICpuInfo
    {
        public int Id { get; set; }
        public CustomProcessorArchitecture Architecture { get; set; }
        public string? Description { get; set; }
        public string Manufacturer { get; set; }
        public string? Name { get; set; }
    }
}
