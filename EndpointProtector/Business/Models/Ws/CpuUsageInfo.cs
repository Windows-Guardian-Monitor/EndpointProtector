using Common.Contracts.Models.Ws;

namespace EndpointProtector.Business.Models.Ws
{
    internal class CpuUsageInfo : ICpuUsageInfo
    {
        public float CpuUsage { get; set; }
        public int Id { get; set; }
    }
}
