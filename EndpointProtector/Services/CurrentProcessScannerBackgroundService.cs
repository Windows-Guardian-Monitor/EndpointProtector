using EndpointProtector.Operators.Contracts;
using System.Diagnostics;

namespace EndpointProtector.Services
{
    internal class CurrentProcessScannerBackgroundService : BackgroundService
	{
		private readonly IProgramOperator _programOperator;
		private readonly IProcessOperator _processOperator;

		public CurrentProcessScannerBackgroundService(IProgramOperator programOperator, IProcessOperator processOperator)
		{
			_programOperator = programOperator;
			_processOperator = processOperator;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var processes = Process.GetProcesses();

			foreach (var process in processes)
			{
				try
				{
					_programOperator.HandleProgramManagement(process);
					_processOperator.HandleNewProcess(process);
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
