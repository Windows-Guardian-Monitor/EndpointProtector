using Common.Enums;

namespace Common.Contracts.Models.Ws
{
    public interface ICpuInfo
    {
        public int Id { get; set; }
        CustomProcessorArchitecture Architecture { get; set; }
        string? Description { get; set; }
        string Manufacturer { get; set; }
        string? Name { get; set; }
    }
}