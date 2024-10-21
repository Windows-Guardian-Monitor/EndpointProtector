using Windows.UI.Composition.Interactions;

namespace Common.Contracts.Models.Ws
{
    public interface ICpuUsageInfo
    {
        int Id { get; set; }
        float CpuUsage { get; set; }
    }
}
