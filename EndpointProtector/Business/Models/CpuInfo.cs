using Common.Contracts.Models;
using Common.Enums;
using System.Runtime.InteropServices;

namespace EndpointProtector.Business.Models
{
    internal record CpuInfo : ICpuInfo
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public CustomProcessorArchitecture Architecture { get; set; }
        public string Manufacturer { get; set; }
        public int Id { get; set; }

        public CpuInfo(string? name, string? description, ushort architecture, string manufacturer)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = FormatName(name);
            Description = description;
            Architecture = (CustomProcessorArchitecture)architecture;
            Manufacturer = manufacturer;
        }

        private string FormatName(string name) => name.Replace("(TM)", "™")
                                                      .Replace("(tm)", "™")
                                                      .Replace("(R)", "®")
                                                      .Replace("(r)", "®")
                                                      .Replace("(C)", "©")
                                                      .Replace("(c)", "©")
                                                      .Replace("    ", " ")
                                                      .Replace("  ", " ");
    }
}
