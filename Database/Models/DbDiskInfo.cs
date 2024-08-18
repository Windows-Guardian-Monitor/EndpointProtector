using Common.Contracts.Models.Ws;
using EndpointProtector.Models.Ram;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class DbDiskInfo : IDiskInfo
    {
		public DbDiskInfo(string? availableSize, string? diskName, string? diskType, string? totalSize)
		{
			AvailableSize = availableSize;
			DiskName = diskName;
			DiskType = diskType;
			TotalSize = totalSize;
		}

        public DbDiskInfo()
        {
            
        }

        [Key]
        public int Id { get; set; }
        public string? AvailableSize { get; set; }
        public string? DiskName { get; set; }
        public string? DiskType { get; set; }
        public string? TotalSize { get; set; }
    }
}
