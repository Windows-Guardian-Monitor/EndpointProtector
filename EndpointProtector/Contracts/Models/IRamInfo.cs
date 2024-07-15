using EndpointProtector.Models.Ram;

namespace EndpointProtector.Contracts.Models
{
    internal interface IRamInfo
    {
        Storage AvailableMemory { get; set; }
        uint PercentOfMemoryUsage { get; set; }
        Storage UsedMemory { get; set; }
    }
}