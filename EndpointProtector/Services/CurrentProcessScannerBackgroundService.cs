using EndpointProtector.Operators;
using System.Diagnostics;

namespace EndpointProtector.Services
{
	internal class CurrentProcessScannerBackgroundService : BackgroundService
	{
		private readonly IProgramOperator _programOperator;

		public CurrentProcessScannerBackgroundService(IProgramOperator programOperator)
		{
			_programOperator = programOperator;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var processes = Process.GetProcesses();

            foreach (var process in processes)
            {
				try
				{
					_programOperator.HandleProgramManagement(process);
				}
				catch
				{
					//ignored
				}
            }

			return Task.CompletedTask;
        }
	}
}
