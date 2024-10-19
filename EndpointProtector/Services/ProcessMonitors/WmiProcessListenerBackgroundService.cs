using EndpointProtector.Business.Models;
using EndpointProtector.Operators;
using EndpointProtector.Operators.Contracts;
using System.Diagnostics;
using System.Management;

namespace EndpointProtector.Services.ProcessMonitors
{
	internal class WmiProcessListenerBackgroundService(
        ILogger<WmiProcessListenerBackgroundService> logger,
        IProgramOperator programOperator,
        IProcessOperator processOperator,
		RuleSynchronizer ruleSynchronizer) : BackgroundService
    {
        private ManagementEventWatcher? _managementEventWatcher;
        private readonly ILogger<WmiProcessListenerBackgroundService> _logger = logger;

        void ProcessArrived(object sender, EventArrivedEventArgs e)
        {
            var wmiProcess = new WmiProcess
            {
                ParentProcessId = (uint)e.NewEvent.Properties["ParentProcessID"].Value,
                ProcessId = (uint)e.NewEvent.Properties["ProcessID"].Value,
                SessionId = (uint)e.NewEvent.Properties["SessionID"].Value,
                StartDate = (ulong)e.NewEvent.Properties["TIME_CREATED"].Value,
                ProcessName = (string)e.NewEvent.Properties["ProcessName"].Value,
                Sid = (byte[])e.NewEvent.Properties["Sid"].Value
            };


            try
            {
                var process = Process.GetProcessById((int)wmiProcess.ProcessId);
                programOperator.HandleProgramManagement(process, wmiProcess.ProcessName);
                processOperator.HandleNewProcess(process);
            }
            catch
            {
                //ignored
            }

            var message = $"[WMI] Process started: {wmiProcess.ProcessName}";
            Console.WriteLine(message);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
			await ruleSynchronizer.UpdateRules();

			_managementEventWatcher = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
            _managementEventWatcher.EventArrived += new EventArrivedEventHandler(ProcessArrived);
            _managementEventWatcher.Start();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _managementEventWatcher?.Stop();
            return base.StopAsync(cancellationToken);
        }
    }
}
