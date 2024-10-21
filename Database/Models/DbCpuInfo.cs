using Common.Contracts.Models.Ws;
using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public record DbCpuInfo : ICpuInfo
    {
		public DbCpuInfo(CustomProcessorArchitecture architecture, string? description, string manufacturer, string? name)
		{
			Architecture = architecture;
			Description = description;
			Manufacturer = manufacturer;
			Name = name;
		}


        public DbCpuInfo()
        {
            
        }

        [Key]
        public int Id { get; set; }
        public CustomProcessorArchitecture Architecture { get; set; }
        public string? Description { get; set; }
        public string Manufacturer { get; set; }
        public string? Name { get; set; }
    }
}
