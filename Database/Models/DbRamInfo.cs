using Common.Contracts.Models;
using EndpointProtector.Models.Ram;

namespace EndpointProtector.Database.Models
{
    internal class DbRamInfo : IRamInfo
    {
        public Storage AvailableMemory { get; set; }
        public uint PercentOfMemoryUsage { get; set; }
        public Storage UsedMemory { get; set; }
    }
}
