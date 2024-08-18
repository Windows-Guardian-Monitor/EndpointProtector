using Common.Contracts.Models.Ws;
using EndpointProtector.Models.Ram;

namespace Database.Models
{
    internal class DbRamUsage : IRamUsageInfo
    {
        public int Id { get; set; }
        public uint PercentOfMemoryUsage { get; set; }
        public Storage UsedMemory { get; set; }
        public Storage AvailableMemory { get; set; }
    }
}
