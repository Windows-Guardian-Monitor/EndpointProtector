using Common.Contracts.Providers;

namespace EndpointProtector.Rules
{
    internal class PeriodicTimerProvider : IPeriodicTimerProvider
    {
        private const int IntervalInSeconds = 1;

        public PeriodicTimer GetServicesPeriodicTimer() => new PeriodicTimer(TimeSpan.FromSeconds(IntervalInSeconds));
    }
}
