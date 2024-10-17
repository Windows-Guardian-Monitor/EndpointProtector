using Common.Contracts.Models.Ws;
using EndpointProtector.Extensions;
using EndpointProtector.Models.Ram;

namespace EndpointProtector.Business.Models.Ws;

internal record RamInfo : IRamUsageInfo
{
    public uint PercentOfMemoryUsage { get; set; }
    public Storage TotalAvailableMemory { get; set; }
    public Storage AvailableMemory { get; set; }
    public int Id { get; set; }

    public RamInfo(uint percentOfMemoryUsage, long availableMemory, long usedMemory)
    {
        PercentOfMemoryUsage = percentOfMemoryUsage;
        TotalAvailableMemory = availableMemory.ConvertToStorage();
        AvailableMemory = usedMemory.ConvertToStorage();
    }
}
