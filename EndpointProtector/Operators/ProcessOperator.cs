using Database.Contracts;
using EndpointProtector.Operators.Contracts;
using System.Diagnostics;
using Vanara.PInvoke;

namespace EndpointProtector.Operators
{
    internal class ProcessOperator : IProcessOperator
    {
        private readonly IClientRuleRepository _clientRuleRepository;
        private readonly ILogger<ProcessOperator> _logger;

        public ProcessOperator(IClientRuleRepository clientRuleRepository, ILogger<ProcessOperator> logger)
        {
            _clientRuleRepository = clientRuleRepository;
            _logger = logger;
        }

        public void HandleNewProcess(Process process)
        {
            var rules = _clientRuleRepository.GetAll();

            try
            {
                var fileName = process.MainModule.FileName;

                var hash = ProgramOperator.CalculateFileHash(fileName);

                if (rules.Any(r => r.Programs.Any(p => p.Hash.Equals(hash, StringComparison.OrdinalIgnoreCase))))
                {
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
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao finalizar o processo");
            }
        }
    }
}
