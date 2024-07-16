using Common.Contracts.Models;

namespace EndpointProtector.Business.Models
{
    internal class CpuUsageInfo : ICpuUsageInfo
    {
        public float CpuUsage { get; set; }
        public int Id { get; set; }
    }
}
