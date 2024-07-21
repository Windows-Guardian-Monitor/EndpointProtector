using Common.Contracts.Models;

namespace Database.Models
{
    internal class DbOsInfo : IOsInfo
    {
        public string Architecture { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string SerialNumber { get; set; }
        public string OsVersion { get; set; }
        public string WindowsDirectory { get; set; }
    }
}