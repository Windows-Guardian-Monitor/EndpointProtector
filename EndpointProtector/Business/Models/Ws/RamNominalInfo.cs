using Common.Contracts.Models.Ws;
using EndpointProtector.Extensions;

namespace EndpointProtector.Business.Models.Ws
{
	internal record RamNominalInfo : IRamNominalInfo
    {
        public RamNominalInfo(long availableMemory, string description, string manufacturer, uint speed)
        {
            TotalMemory = availableMemory.ConvertToStorage().ToString();
            Description = description;
            Manufacturer = manufacturer;
            Speed = speed;
        }

        public string TotalMemory { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public uint Speed { get; set; }
    }
}
