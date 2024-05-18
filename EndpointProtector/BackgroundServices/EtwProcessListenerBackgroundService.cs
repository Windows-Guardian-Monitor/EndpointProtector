using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Session;

namespace EndpointProtector.BackgroundServices
{
    internal class EtwProcessListenerBackgroundService : BackgroundService
    {
        private readonly ILogger<EtwProcessListenerBackgroundService> _logger;

        public EtwProcessListenerBackgroundService(ILogger<EtwProcessListenerBackgroundService> logger)
        {
            _logger = logger;
        }

        private void Kernel_ProcessStart(Microsoft.Diagnostics.Tracing.Parsers.Kernel.ProcessTraceData data)
        {
            string message = $"[ETW] Process {data.ProcessName} started";
            Console.WriteLine(message);
            _logger.LogInformation(message);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var session = new TraceEventSession(KernelTraceEventParser.KernelSessionName))
            {
                session.EnableKernelProvider(KernelTraceEventParser.Keywords.Process);

                session.Source.Kernel.ProcessStart += Kernel_ProcessStart;

                session.Source.Process();
            }

            return Task.CompletedTask;
        }
    }
}
