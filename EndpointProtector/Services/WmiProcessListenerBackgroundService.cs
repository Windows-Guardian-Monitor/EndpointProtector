using EndpointProtector.Business.Models;
using EndpointProtector.Operators;
using System.Diagnostics;
using System.Management;

namespace EndpointProtector.Services
{
	internal class WmiProcessListenerBackgroundService : BackgroundService
    {
        private ManagementEventWatcher? _managementEventWatcher;
        private readonly ILogger<WmiProcessListenerBackgroundService> _logger;
        private readonly IProgramOperator _programOperator;

		public WmiProcessListenerBackgroundService(ILogger<WmiProcessListenerBackgroundService> logger, IProgramOperator programOperator)
		{
			_logger = logger;
			_programOperator = programOperator;
		}

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
				_programOperator.HandleProgramManagement(process, wmiProcess.ProcessName);
			}
            catch
            {
                //ignored
            }

            var message = $"[WMI] Process started: {wmiProcess.ProcessName}";
            Console.WriteLine(message);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _managementEventWatcher = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
            _managementEventWatcher.EventArrived += new EventArrivedEventHandler(ProcessArrived);
            _managementEventWatcher.Start();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _managementEventWatcher?.Stop();
            return base.StopAsync(cancellationToken);
        }
    }
}
