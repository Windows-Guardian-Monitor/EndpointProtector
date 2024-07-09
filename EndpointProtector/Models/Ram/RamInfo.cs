namespace EndpointProtector.Models.Ram;

internal record RamInfo
{
    public uint PercentOfMemoryUsage { get; private set; }
    public Ram AvailableMemory { get; private set; }
    public Ram UsedMemory { get; private set; }

    public RamInfo(uint percentOfMemoryUsage, ulong availableMemory, ulong usedMemory)
    {
        PercentOfMemoryUsage = percentOfMemoryUsage;
        AvailableMemory = FormatMemorySize(availableMemory);
        UsedMemory = FormatMemorySize(usedMemory);
    }

    private static Ram FormatMemorySize(ulong bytes)
    {
        string[] Suffix = ["B", "KB", "MB", "GB", "TB"];

        double dblSByte = bytes;
        int i;

        for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
        {
            dblSByte = bytes / 1024.0;
        }

        return new Ram(dblSByte, (RamBytes)i);
    }
}
