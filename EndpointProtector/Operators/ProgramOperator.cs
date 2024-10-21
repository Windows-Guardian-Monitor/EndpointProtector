using Common;
using Database.Contracts;
using Database.Models;
using EndpointProtector.Backend.Responses;
using EndpointProtector.Business.Models;
using EndpointProtector.Operators.Contracts;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json;

namespace EndpointProtector.Operators
{
	public class ProgramOperator : IProgramOperator
	{
		private readonly IProgramRepository _programRepository;
		private readonly ILogger<ProgramOperator> _logger;
		private readonly HttpClient _httpClient = new();

		public ProgramOperator(IProgramRepository programRepository, ILogger<ProgramOperator> logger)
		{
			_programRepository = programRepository;
			_logger = logger;
		}

		public static string CalculateFileHash(string filePath)
		{
			byte[] hash;
			using (var hash512 = SHA512.Create())
			{
				using (var stream = File.OpenRead(filePath))
				{
					hash = hash512.ComputeHash(stream);
				}
			}

			return Convert.ToBase64String(hash);
		}

		public void HandleProgramManagement(Process process, string name = "")
		{

			if (process is null || process.SessionId is 0)
			{
				return;
			}

			string? fileName;


			if (process.MainModule is null)
			{
				return;
			}

			fileName = process.MainModule.FileName;

			if (string.IsNullOrWhiteSpace(fileName))
			{
				return;
			}

			var fileHash = CalculateFileHash(fileName);

			if (_programRepository.Exists(fileHash))
			{
				return;
			}

			var processName = name;

			if (string.IsNullOrWhiteSpace(name))
			{
				if (string.IsNullOrEmpty(process.ProcessName))
				{
					return;
				}

				processName = process.ProcessName;
			}

			var program = new BusinessProgram(fileName, processName, fileHash);

			Task.Run(() => SendToServer(program));

			_programRepository.Insert(program);
		}

		private async Task SendToServer(DbProgramWithExecutionTime dbProgramWithExecutionTime)
		{
			try
			{
				var r = await _httpClient.PostAsJsonAsync($"{InformationHandler.GetUrl()}Program/SendProgramWithExecutionTime", JsonSerializer.Serialize(dbProgramWithExecutionTime));

				var json = await r.Content.ReadAsStringAsync();

				var standardResponse = JsonSerializer.Deserialize<StandardResponse>(json);

				if (standardResponse != null)
				{
					if (!standardResponse.Success)
					{
						throw new Exception(standardResponse.Message);
					}
				}
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
			}
		}
	}
}
