using EndpointProtector.Operators;

namespace EndpointProtector.Services.ProcessMonitors
{
	internal class CurrentProcessScannerBackgroundService : BackgroundService
    {
        private readonly AllProcessesOperator _allProcessesOperator;
		private readonly RuleSynchronizer _ruleSynchronizer;

		public CurrentProcessScannerBackgroundService(AllProcessesOperator allProcessesOperator, RuleSynchronizer ruleSynchronizer)
		{
			_allProcessesOperator = allProcessesOperator;
			_ruleSynchronizer = ruleSynchronizer;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var periodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(5));

            await _ruleSynchronizer.UpdateRules();

			do
            {
                _allProcessesOperator.AnalyzeAllRunningProcesses();
            } while (await periodicTimer.WaitForNextTickAsync());
        }
    }
}
