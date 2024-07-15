using EndpointProtector.Models.Ram;

namespace Common.Contracts.Models;

public interface IRamInfo
{
    public Storage AvailableMemory { get; set; }
    public uint PercentOfMemoryUsage { get; set; }
    public Storage UsedMemory { get; set; }
}