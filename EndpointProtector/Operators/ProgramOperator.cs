using Database.Contracts;
using EndpointProtector.Business.Models;
using System.Diagnostics;
using System.Security.Cryptography;

namespace EndpointProtector.Operators
{
	public class ProgramOperator : IProgramOperator
	{
		private readonly IProgramRepository _programRepository;

		public ProgramOperator(IProgramRepository programRepository)
		{
			_programRepository = programRepository;
		}

		private string CalculateFileHash(string filePath)
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

			_programRepository.Insert(program);
		}
	}
}
