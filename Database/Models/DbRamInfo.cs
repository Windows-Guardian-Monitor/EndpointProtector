using Common.Contracts.Models.Ws;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
	public class DbRamInfo : IRamNominalInfo
    {
		public DbRamInfo(string totalMemory, string description, string manufacturer, uint speed)
		{
			TotalMemory = totalMemory;
			Description = description;
			Manufacturer = manufacturer;
			Speed = speed;
		}

        public DbRamInfo()
        {
            
        }
        [Key]
        public int Id { get; set; }
        public string TotalMemory { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public uint Speed { get; set; }
    }
}

