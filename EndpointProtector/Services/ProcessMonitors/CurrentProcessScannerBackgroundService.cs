using EndpointProtector.Operators;
using EndpointProtector.Operators.Contracts;
using System.Diagnostics;

namespace EndpointProtector.Services.ProcessMonitors
{
    internal class CurrentProcessScannerBackgroundService : BackgroundService
    {
        private readonly AllProcessesOperator _allProcessesOperator;

        public CurrentProcessScannerBackgroundService(AllProcessesOperator allProcessesOperator)
        {
            _allProcessesOperator = allProcessesOperator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var periodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(5));

            do
            {
                _allProcessesOperator.AnalyzeAllRunningProcesses();
            } while (await periodicTimer.WaitForNextTickAsync());
        }
    }
}
