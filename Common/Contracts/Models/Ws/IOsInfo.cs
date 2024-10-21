namespace Common.Contracts.Models.Ws;

public interface IOsInfo
{
    string Architecture { get; set; }
    string Description { get; set; }
    string Manufacturer { get; set; }
    string SerialNumber { get; set; }
    string OsVersion { get; set; }
    string WindowsDirectory { get; set; }
	int Id { get; set; }
}