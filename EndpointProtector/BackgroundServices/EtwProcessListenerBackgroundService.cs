using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Session;

namespace EndpointProtector.BackgroundServices
{
    internal class EtwProcessListenerBackgroundService : BackgroundService
    {
        private readonly ILogger<EtwProcessListenerBackgroundService> _logger;
        private readonly TraceEventSession _traceEventSession;

        public EtwProcessListenerBackgroundService(ILogger<EtwProcessListenerBackgroundService> logger)
        {
            _logger = logger;
            _traceEventSession = new TraceEventSession(KernelTraceEventParser.KernelSessionName);
        }

        private void Kernel_ProcessStart(Microsoft.Diagnostics.Tracing.Parsers.Kernel.ProcessTraceData data)
        {
            string message = $"[ETW] {data.ProcessName} started";
            Console.WriteLine(message);
            _logger.LogInformation(message);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _traceEventSession.EnableKernelProvider(KernelTraceEventParser.Keywords.Process);

            _traceEventSession.Source.Kernel.ProcessStart += Kernel_ProcessStart;

            Task.Run(() => _traceEventSession.Source.Process());

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _traceEventSession.Source.Kernel.ProcessStart -= Kernel_ProcessStart;
            _traceEventSession.Source.StopProcessing();
            _traceEventSession.Stop();
            _traceEventSession.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
