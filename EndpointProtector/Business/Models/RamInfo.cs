using EndpointProtector.Contracts.Models;
using EndpointProtector.Extensions;
using EndpointProtector.Models.Ram;

namespace EndpointProtector.Business.Models;

internal record RamInfo : IRamInfo
{
    public uint PercentOfMemoryUsage { get; set; }
    public Storage AvailableMemory { get; set; }
    public Storage UsedMemory { get; set; }

    public RamInfo(uint percentOfMemoryUsage, long availableMemory, long usedMemory)
    {
        PercentOfMemoryUsage = percentOfMemoryUsage;
        AvailableMemory = availableMemory.ConvertToStorage();
        UsedMemory = usedMemory.ConvertToStorage();
    }
}
