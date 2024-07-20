using EndpointProtector.Models.Ram;

namespace Common.Contracts.Models
{
    public interface IRamNominalInfo
    {
        Storage TotalMemory { get; set; }
        string Description { get; set; }
        string Manufacturer { get; set; }
        uint Speed { get; set; }
    }
}
