﻿namespace Common.Contracts.Models.Ws
{
    public interface IWindowsWorkstation
    {
        int Id { get; set; }
        ICpuInfo CpuInfo { get; set; }
        IEnumerable<IDiskInfo> DisksInfo { get; set; }
        IOsInfo OsInfo { get; set; }
        IRamNominalInfo RamInfo { get; set; }
        string Uuid { get; set; }
        string HostName { get; set; }
    }
}
