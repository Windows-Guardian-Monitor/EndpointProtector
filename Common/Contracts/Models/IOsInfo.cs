namespace Common.Contracts.Models;

public interface IOsInfo
{
    string Architecture { get; set; }
    string Description { get; set; }
    string Manufacturer { get; set; }
    Version OSVersion { get; set; }
    string SerialNumber { get; set; }
    string VersionStr { get; set; }
    string WindowsDirectory { get; set; }
}