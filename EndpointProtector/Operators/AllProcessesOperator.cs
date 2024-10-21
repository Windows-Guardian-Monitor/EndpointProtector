using EndpointProtector.Operators.Contracts;
using System.Diagnostics;

namespace EndpointProtector.Operators
{
	public class AllProcessesOperator
	{
	private readonly IProgramOperator _programOperator;
	private readonly IProcessOperator _processOperator;
		private readonly ILogger<AllProcessesOperator> _logger;

		public AllProcessesOperator(IProgramOperator programOperator, IProcessOperator processOperator, ILogger<AllProcessesOperator> logger)
		{
			_programOperator = programOperator;
			_processOperator = processOperator;
			_logger = logger;
		}

		public void AnalyzeAllRunningProcesses()
		{
			try
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
			}
			catch (Exception e)
			{
				_logger.LogError(e, "There was an error while trying to enumerate all processes");
			}
		}
	}
}
