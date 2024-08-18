using EndpointProtector.Models.Ram;

namespace Common.Contracts.Models.Ws;

public interface IRamUsageInfo
{
    public int Id { get; set; }
    public uint PercentOfMemoryUsage { get; set; }
    public Storage UsedMemory { get; set; }
    public Storage AvailableMemory { get; set; }
}