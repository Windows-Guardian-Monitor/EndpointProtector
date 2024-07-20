using Common.Contracts.Models;

namespace Database.Models
{
    public class DbWindowsWorkstation : IWindowsWorkstation
    {
        public ICpuInfo CpuInfo { get; set; }
        public IEnumerable<IDiskInfo> DisksInfo { get; set; }
        public IOsInfo OsInfo { get; set; }
        public int Id { get; set; }
        public IRamNominalInfo RamInfo { get; set; }
    }
}
