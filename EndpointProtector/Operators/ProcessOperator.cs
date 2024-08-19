using Database.Contracts;
using EndpointProtector.Operators.Contracts;
using System.Diagnostics;

namespace EndpointProtector.Operators
{
    internal class ProcessOperator : IProcessOperator
	{
		private readonly IClientRuleRepository _clientRuleRepository;

		public ProcessOperator(IClientRuleRepository clientRuleRepository)
		{
			_clientRuleRepository = clientRuleRepository;
		}

		public void HandleNewProcess(Process process)
		{
			var rules = _clientRuleRepository.GetAll();

			try
			{
				var hash = ProgramOperator.CalculateFileHash(process.MainModule.FileName);

				if (rules.Any(r => r.Programs.Any(p => p.Hash.Equals(hash, StringComparison.OrdinalIgnoreCase))))
				{
					process.Kill(true);
				}
			}
			catch (Exception e)
			{

			}
		}
	}
}
