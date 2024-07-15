namespace EndpointProtector.Business.Models
{

    public class WmiProcess
    {
        public uint ParentProcessId { get; set; }
        public uint ProcessId { get; set; }
        public uint SessionId { get; set; }
        public byte[] Sid { get; set; } = Array.Empty<byte>();
        public ulong StartDate { get; set; }
        public string ProcessName { get; set; } = string.Empty;
    }
}
