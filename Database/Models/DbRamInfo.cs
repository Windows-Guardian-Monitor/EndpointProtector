using Common.Contracts.Models;
using EndpointProtector.Models.Ram;

namespace Database.Models
{
    internal class DbRamInfo : IRamNominalInfo
    {
        public int Id { get; set; }
        public Storage TotalMemory { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public uint Speed { get; set; }
    }
}

