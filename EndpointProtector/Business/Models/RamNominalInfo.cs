using Common.Contracts.Models;
using EndpointProtector.Extensions;
using EndpointProtector.Models.Ram;

namespace EndpointProtector.Business.Models
{
    internal record RamNominalInfo : IRamNominalInfo
    {
        public RamNominalInfo(long availableMemory, string description, string manufacturer, uint speed)
        {
            TotalMemory = availableMemory.ConvertToStorage();
            Description = description;
            Manufacturer = manufacturer;
            Speed = speed;
        }

        public Storage TotalMemory { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public uint Speed { get; set; }
    }
}
