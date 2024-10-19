using Database.Contracts;
using EndpointProtector.Operators;
using System.Text.Json.Serialization;

namespace EndpointProtector.Services.ServerCommunication
{
	internal class RuleHandlerBackgroundService : BackgroundService
	{
		
		private readonly ILogger<RuleHandlerBackgroundService> _logger;
		private readonly RuleSynchronizer _ruleSynchronizer;

		public RuleHandlerBackgroundService(
			ILogger<RuleHandlerBackgroundService> logger, RuleSynchronizer ruleSynchronizer)
		{
			_logger = logger;
			_ruleSynchronizer = ruleSynchronizer;
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
				await _ruleSynchronizer.UpdateRules();

			} while (await periodicTimer.WaitForNextTickAsync(stoppingToken));
		}
	}

	public class GetRuleByWsRequest
	{
		[JsonPropertyName("Hostname")]
		public string Hostname { get; set; }
	}
}
