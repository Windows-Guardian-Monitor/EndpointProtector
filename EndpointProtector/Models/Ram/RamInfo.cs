using EndpointProtector.Extensions;

namespace EndpointProtector.Models.Ram;

internal record RamInfo
{
    public uint PercentOfMemoryUsage { get; private set; }
    public Storage AvailableMemory { get; private set; }
    public Storage UsedMemory { get; private set; }

    public RamInfo(uint percentOfMemoryUsage, long availableMemory, long usedMemory)
    {
        PercentOfMemoryUsage = percentOfMemoryUsage;
        AvailableMemory = availableMemory.ConvertToStorage();
        UsedMemory = usedMemory.ConvertToStorage();
    }
}
