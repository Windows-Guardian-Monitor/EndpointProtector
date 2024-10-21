using Common.Contracts.Models.Ws;

namespace Database.Models
{
    internal class DbCpuUsageInfo : ICpuUsageInfo
    {
        public int Id { get; set; }
        public float CpuUsage { get; set; }
    }
}
