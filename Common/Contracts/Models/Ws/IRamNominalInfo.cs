using EndpointProtector.Models.Ram;

namespace Common.Contracts.Models.Ws
{
    public interface IRamNominalInfo
    {
        string TotalMemory { get; set; }
        string Description { get; set; }
        string Manufacturer { get; set; }
        uint Speed { get; set; }
    }
}
