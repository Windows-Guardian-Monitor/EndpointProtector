using Cassia;
using Database.Contracts;
using Database.Models.Reports;
using Database.Repositories;
using EndpointProtector.Operators.Contracts;
using System.ComponentModel;
using System.Diagnostics;
using Vanara.PInvoke;

namespace EndpointProtector.Operators
{
	internal class ProcessOperator : IProcessOperator
	{
		private readonly IClientRuleRepository _clientRuleRepository;
		private readonly ILogger<ProcessOperator> _logger;
		private readonly ProcessFinishedRepository _processFinishedRepository;

		public ProcessOperator(IClientRuleRepository clientRuleRepository, ILogger<ProcessOperator> logger, ProcessFinishedRepository processFinishedRepository)
		{
			_clientRuleRepository = clientRuleRepository;
			_logger = logger;
			_processFinishedRepository = processFinishedRepository;
		}

		private void SaveProcessFinishedEvent(int sessionId, string fileName, string hash)
		{
			try
			{
				if (string.IsNullOrEmpty(fileName) &&
					string.IsNullOrEmpty(hash))
				{
					return;
				}

				var terminalServicesManager = new TerminalServicesManager();
				var session = terminalServicesManager.GetLocalServer().GetSession(sessionId);

				var finishedEvent = new DbProcessFinishedEvent(session.UserName, session.DomainName, Environment.MachineName, hash, fileName);

				_processFinishedRepository.Insert(finishedEvent);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Houve um erro ao tentar salvar as informações do processo finalizado");
			}
		}

		public void HandleNewProcess(Process process)
		{
			var rules = _clientRuleRepository.GetAll();
			//buscar as regras por processo aumenta bastante o uso de recursos

			var sessionId = 0;
			var fileName = string.Empty;
			var hash = string.Empty;

			try
			{
				fileName = process.MainModule.FileName;

				hash = ProgramOperator.CalculateFileHash(fileName);

				if (rules.Any(r => r.Programs.Any(p => p.Hash.Equals(hash, StringComparison.OrdinalIgnoreCase))))
				{
					sessionId = process.SessionId;
					process.Kill(true);

					const string applicationBlocked = "Aplicação Bloqueada";
					const long warningStyle = 0x00000030L;
					const long modalStyle = 0x00001000L;
					const int timeoutInSeconds = 10;

					var message = $"A aplicação de caminho \"{fileName}\" foi bloqueada pelo administrador de rede.";

					WTSApi32.WTSSendMessage(
						WTSApi32.HWTSSERVER.WTS_CURRENT_SERVER_HANDLE,
						(uint)process.SessionId,
						applicationBlocked,
						applicationBlocked.Length * 2,
						message,
						message.Length * 2,
						(uint)(warningStyle | modalStyle),
						timeoutInSeconds,
						out _,
						false);

					_logger.LogWarning(message);

					SaveProcessFinishedEvent(sessionId, fileName, hash);
				}
			}
			catch (InvalidOperationException) { /*ignored*/ }
			catch (Win32Exception) { /*ignored*/ }
			catch (Exception e)
			{
				_logger.LogError(e, "Erro ao finalizar o processo");
			}

			
		}
	}
}
