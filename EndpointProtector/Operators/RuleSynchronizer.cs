using Common.Contracts.DAL;
using Database.Contracts;
using Database.Models.Rules;
using EndpointProtector.Backend.Responses;
using EndpointProtector.Services.ServerCommunication;
using System.Net.Http.Json;
using System.Text.Json;

namespace EndpointProtector.Operators
{
	public class RuleSynchronizer
	{
		private readonly IWindowsWorkstationRepository _windowsWorkstationRepository;
		private readonly ILogger<RuleSynchronizer> _logger;
		private readonly IClientRuleRepository _ruleRepository;
		private readonly AllProcessesOperator _allProcessesOperator;

		public RuleSynchronizer(
			IWindowsWorkstationRepository windowsWorkstationRepository, 
			IClientRuleRepository ruleRepository, 
			ILogger<RuleSynchronizer> logger, 
			AllProcessesOperator allProcessesOperator)
		{
			_windowsWorkstationRepository = windowsWorkstationRepository;
			_ruleRepository = ruleRepository;
			_logger = logger;
			_allProcessesOperator = allProcessesOperator;
		}

		public async ValueTask UpdateRules()
		{
			try
			{
				var httpClient = new HttpClient();

				const string url = "https://localhost:7102/Rules/AcquireAll";

				var machine = _windowsWorkstationRepository.GetFirst();

				var response = await httpClient.PostAsJsonAsync(url, new GetRuleByWsRequest() { Hostname = Environment.MachineName });

				var json = await response.Content.ReadAsStringAsync();

				var ruleResponse = JsonSerializer.Deserialize<AllRulesResponse>(json);

				if (ruleResponse.Success is false)
				{
					throw new Exception(ruleResponse.Message);
				}

				_ruleRepository.DeleteAll();

				var dbRules = ruleResponse.Rules.Select(r => new DbClientRule(r.Programs.Select(p => new DbClientRuleProgram(p.Path, p.Name, p.Hash)).ToList(), r.Name)).ToList();

				_ruleRepository.InsertMany(dbRules);

				_logger.LogWarning($"Regras cadastradas com sucesso\n{JsonSerializer.Serialize(dbRules)}");

				_allProcessesOperator.AnalyzeAllRunningProcesses();
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Erro ao buscar regras");
			}
		}
	}
}
