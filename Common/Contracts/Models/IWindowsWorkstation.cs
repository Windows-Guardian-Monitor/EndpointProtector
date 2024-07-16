namespace Common.Contracts.Models
{
    public interface IWindowsWorkstation
    {
        int Id { get; set; }
        ICpuInfo CpuInfo { get; set; }
        IEnumerable<IDiskInfo> DisksInfo { get; set; }
        IOsInfo OsInfo { get; set; }
        IRamNominalInfo RamInfo { get; set; }
    }
}
