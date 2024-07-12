using EndpointProtector.Enums;

namespace EndpointProtector.Models.Cpu
{
    internal record CpuInfo
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Architecture Architecture { get; set; }
        public string Manufacturer { get; set; }

        public CpuInfo(string? name, string? description, ushort architecture, string manufacturer)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = FormatName(name);
            Description = description;
            Architecture = (Architecture)architecture;
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
