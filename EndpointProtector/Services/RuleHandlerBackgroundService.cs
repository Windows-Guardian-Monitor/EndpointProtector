using Common.Contracts.DAL;
using Database.Contracts;
using Database.Models.Rules;
using EndpointProtector.Backend.Requests;
using EndpointProtector.Backend.Responses;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EndpointProtector.Services
{
	internal class RuleHandlerBackgroundService : BackgroundService
	{
		private readonly IClientRuleRepository _ruleRepository;
		private readonly IWindowsWorkstationRepository _windowsWorkstationRepository;
		private readonly ILogger<RuleHandlerBackgroundService> _logger;

		public RuleHandlerBackgroundService(IClientRuleRepository ruleRepository, IWindowsWorkstationRepository windowsWorkstationRepository, ILogger<RuleHandlerBackgroundService> logger)
		{
			_ruleRepository = ruleRepository;
			_windowsWorkstationRepository = windowsWorkstationRepository;
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			await RequestRules(stoppingToken);
		}

		private async ValueTask RequestRules(CancellationToken stoppingToken)
		{
			var periodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(1));

			do
			{
				try
				{
					var httpClient = new HttpClient();

					const string url = "https://localhost:7102/Rules/AcquireAll";

					var machine = _windowsWorkstationRepository.GetFirst();

					var response = await httpClient.PostAsJsonAsync(url, new GetRuleByWsRequest() { MachineUuid = "radical" });

					var json = await response.Content.ReadAsStringAsync();

					var ruleResponse = JsonSerializer.Deserialize<AllRulesResponse>(json);

					if (ruleResponse.Success is false)
					{
						throw new Exception(ruleResponse.Message);
					}

					_ruleRepository.DeleteAll();

					var dbRules = ruleResponse.Rules.Select(r => new DbClientRule(r.Programs.Select(p => new DbClientRuleProgram(p.Path, p.Name, p.Hash)).ToList(),r.Name)).ToList();

					_ruleRepository.InsertMany(dbRules);

					_logger.LogWarning($"Regras cadastradas com sucesso\n{JsonSerializer.Serialize(dbRules)}");
				}
				catch (Exception e)
				{
					_logger.LogError(e, "Erro ao buscar regras");
				}

			} while (await periodicTimer.WaitForNextTickAsync(stoppingToken));
		}
	}

	public class GetRuleByWsRequest
	{
		[JsonPropertyName("MachineUuid")]
		public string MachineUuid { get; set; }
	}
}
