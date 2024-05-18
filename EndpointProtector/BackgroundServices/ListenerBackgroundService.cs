using System.Management;

namespace EndpointProtector.BackgroundServices
{
    internal class ProcessListenerBackgroundService : BackgroundService
    {
        private ManagementEventWatcher _managementEventWatcher;
        void startWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Console.WriteLine("Process started: {0}", e.NewEvent.Properties["ProcessName"].Value);

            _managementEventWatcher.Stop();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _managementEventWatcher = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
            _managementEventWatcher.EventArrived += new EventArrivedEventHandler(startWatch_EventArrived);
            _managementEventWatcher.Start();
            return Task.CompletedTask;
        }
    }
}
 