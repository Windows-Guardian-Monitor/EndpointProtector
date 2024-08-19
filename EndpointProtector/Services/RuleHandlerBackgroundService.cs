using Common.Contracts.DAL;
using Database.Contracts;
using Database.Models.Rules;
using EndpointProtector.Backend.Responses;
using System.Net.Http.Json;
using System.Text.Json;

namespace EndpointProtector.Services
{
	internal class RuleHandlerBackgroundService : BackgroundService
	{
		private readonly IClientRuleRepository _ruleRepository;
		private readonly IWindowsWorkstationRepository _windowsWorkstationRepository;

		public RuleHandlerBackgroundService(IClientRuleRepository ruleRepository, IWindowsWorkstationRepository windowsWorkstationRepository)
		{
			_ruleRepository = ruleRepository;
			_windowsWorkstationRepository = windowsWorkstationRepository;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			await RequestRules(stoppingToken);
		}

		private async ValueTask RequestRules(CancellationToken stoppingToken)
		{
			var periodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(5));

			do
			{
				try
				{
					var httpClient = new HttpClient();

					const string url = "https://localhost:7102/Rules/Acquire";

					var machine = _windowsWorkstationRepository.GetFirst();

					var response = await httpClient.PostAsJsonAsync(url, JsonSerializer.SerializeToUtf8Bytes(machine.Uuid));

					var json = await response.Content.ReadAsStringAsync();

					var ruleResponse = JsonSerializer.Deserialize<BaseRuleResponse>(json);

					_ruleRepository.DeleteAll();

					var dbRules = ruleResponse.Rules.Select(r => new DbClientRule(r.Programs.Select(p => new DbClientRuleProgram(p.Path, p.Name, p.Hash)).ToList())).ToList();

					_ruleRepository.InsertMany(dbRules);
				}
				catch (Exception e)
				{

				}

			} while (await periodicTimer.WaitForNextTickAsync(stoppingToken));
		}
	}
}
