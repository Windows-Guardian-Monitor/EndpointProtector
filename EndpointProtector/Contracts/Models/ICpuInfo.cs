using EndpointProtector.Enums;

namespace EndpointProtector.Contracts.Models
{
    internal interface ICpuInfo
    {
        public int Id { get; set; }
        Architecture Architecture { get; set; }
        string? Description { get; set; }
        string Manufacturer { get; set; }
        string? Name { get; set; }
    }
}