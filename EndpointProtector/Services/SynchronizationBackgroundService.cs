﻿using Common.Contracts.DAL;
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
        private const string _url = "https://localhost:7102/Information/Workstation";

        public SynchronizationBackgroundService(IWindowsWorkstationRepository windowsWorkstationRepository)
        {
            _windowsWorkstationRepository = windowsWorkstationRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var periodicTimer = new PeriodicTimer(TimeSpan.FromHours(1));
                do
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

                    var response = await httpClient.PostAsJsonAsync(_url, JsonSerializer.SerializeToUtf8Bytes(backendWindowsWorkstation), stoppingToken);

                    if (response is { IsSuccessStatusCode: false })
                    {
                        var standardResponse = JsonSerializer.Deserialize<StandardResponse>(await response.Content.ReadAsStreamAsync(stoppingToken));
                        throw new Exception(standardResponse?.Message);
                    }

                } while (await periodicTimer.WaitForNextTickAsync(stoppingToken));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
