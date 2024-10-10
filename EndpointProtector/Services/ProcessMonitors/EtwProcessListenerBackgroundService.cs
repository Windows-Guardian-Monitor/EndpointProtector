using EndpointProtector.Operators.Contracts;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Session;
using System.Diagnostics;

namespace EndpointProtector.Services.ProcessMonitors
{
    internal class EtwProcessListenerBackgroundService(IProgramOperator programOperator, IProcessOperator processOperator) : BackgroundService
    {
        private readonly TraceEventSession _traceEventSession = new TraceEventSession(KernelTraceEventParser.KernelSessionName);

        private void Kernel_ProcessStart(Microsoft.Diagnostics.Tracing.Parsers.Kernel.ProcessTraceData data)
        {
            string message = $"[ETW] {data.ProcessName} started";

            Console.WriteLine(message);

            try
            {
                var process = Process.GetProcessById(data.ProcessID);
                programOperator.HandleProgramManagement(process);
                processOperator.HandleNewProcess(process);
            }
            catch (Exception e)
            {
                //ignored
            }
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
