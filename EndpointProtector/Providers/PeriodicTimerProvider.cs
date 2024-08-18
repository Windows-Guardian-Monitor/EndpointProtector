using Common.Contracts.Providers;

namespace EndpointProtector.Providers
{
	internal class PeriodicTimerProvider : IPeriodicTimerProvider
	{
		private const int IntervalInSeconds = 1;

		public PeriodicTimer GetServicesPeriodicTimer() => new PeriodicTimer(TimeSpan.FromDays(IntervalInSeconds));
	}
}
