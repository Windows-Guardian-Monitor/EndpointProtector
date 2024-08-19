using Common.Contracts.DAL;
using Database.Contracts;
using EndpointProtector.Backend.Requests;
using EndpointProtector.Backend.Responses;
using EndpointProtector.Extensions;
using System.Net.Http.Json;
using System.Text.Json;

namespace EndpointProtector.Services
{
	internal class SynchronizationBackgroundService : BackgroundService
	{
		private readonly IWindowsWorkstationRepository _windowsWorkstationRepository;
		private readonly IProgramRepository _programRepository;

		private ILogger<SynchronizationBackgroundService> _logger;

		public SynchronizationBackgroundService(
			IWindowsWorkstationRepository windowsWorkstationRepository, 
			ILogger<SynchronizationBackgroundService> logger, 
			IProgramRepository programRepository)
		{
			_windowsWorkstationRepository = windowsWorkstationRepository;
			_logger = logger;
			_programRepository = programRepository;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			Task.Run(() => SendMachineInformationToServer(stoppingToken), stoppingToken);
			Task.Run(() => SendProgramsToServer(stoppingToken), stoppingToken);
			return Task.CompletedTask;
		}

		private async ValueTask SendProgramsToServer(CancellationToken stoppingToken)
		{
			var periodicTimer = new PeriodicTimer(TimeSpan.FromHours(1));

			do
			{
				try
				{
					var firstPeriodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(1));

					await firstPeriodicTimer.WaitForNextTickAsync();

					var programs = _programRepository.GetAll();

					var httpClient = new HttpClient();

					const string url = "https://localhost:7102/Program/Send";

					var dbPrograms = programs.Select(p => new ProgramRequest(p.Path, p.Name, p.Hash));

					var response = await httpClient.PostAsJsonAsync(url, JsonSerializer.SerializeToUtf8Bytes(dbPrograms), stoppingToken);

					if (response is { IsSuccessStatusCode: false })
					{
						var standardResponse = JsonSerializer.Deserialize<StandardResponse>(await response.Content.ReadAsStreamAsync(stoppingToken));
						throw new Exception(standardResponse?.Message);
					}

					_programRepository.DeleteAll();
				}
				catch (Exception e)
				{
					_logger.LogError(e, "Error while sending programs to server");
				}

			} while (await periodicTimer.WaitForNextTickAsync());
		}

		private async ValueTask SendMachineInformationToServer(CancellationToken stoppingToken)
		{
			var periodicTimer = new PeriodicTimer(TimeSpan.FromHours(1));
			do
			{
				try
				{
					var workstation = _windowsWorkstationRepository.GetFirst();

					if (workstation is null)
					{
						continue;
					}


					var backendCpuInfo =
						new BackendCpuinfo(workstation.CpuInfo.Architecture.ToString(), workstation.CpuInfo.Description, workstation.CpuInfo.Manufacturer, workstation.CpuInfo.Name);

					var backendDisks = new BackendDisksinfo[workstation.DisksInfo.Count()];

					var i = 0;

					foreach (var item in workstation.DisksInfo)
					{
						backendDisks[i++] = new BackendDisksinfo
						{
							AvailableSize = item.AvailableSize.ToString(),
							DiskName = item.DiskName,
							DiskType = item.DiskType,
							TotalSize = item.TotalSize.ToString()
						};
					}

					var backendOsInfo = new BackendOsinfo
					{
						Architecture = workstation.OsInfo.Architecture.ToString(),
						Description = workstation.OsInfo.Description,
						OsManufacturer = workstation.OsInfo.Manufacturer,
						OsVersion = workstation.OsInfo.OsVersion.ToString(),
						SerialNumber = workstation.OsInfo.SerialNumber,
						VersionStr = workstation.OsInfo.OsVersion,
						WindowsDirectory = workstation.OsInfo.WindowsDirectory
					};

					var backendRamInfo = new BackendRaminfo
					{
						Description = workstation.RamInfo.Description,
						Manufacturer = workstation.RamInfo.Manufacturer,
						Speed = workstation.RamInfo.Speed.ToHumanizedMemorySpeed(),
						TotalMemory = workstation.RamInfo.TotalMemory.ToString()
					};

					var backendWindowsWorkstation = new BackendWindowsWorkstation
					{
						CpuInfo = backendCpuInfo,
						DisksInfo = backendDisks,
						OsInfo = backendOsInfo,
						RamInfo = backendRamInfo,
						Uuid = workstation.Uuid,
						HostName = workstation.HostName
					};

					var httpClient = new HttpClient();

					const string url = "https://localhost:7102/Information/Workstation";

					var response = await httpClient.PostAsJsonAsync(url, JsonSerializer.SerializeToUtf8Bytes(backendWindowsWorkstation), stoppingToken);

					if (response is { IsSuccessStatusCode: false })
					{
						var standardResponse = JsonSerializer.Deserialize<StandardResponse>(await response.Content.ReadAsStreamAsync(stoppingToken));
						throw new Exception(standardResponse?.Message);
					}
				}
				catch (Exception e)
				{
					_logger.LogError(e, "Error while sending machine information to server");
				}
			} while (await periodicTimer.WaitForNextTickAsync(stoppingToken));
		}
	}
}
