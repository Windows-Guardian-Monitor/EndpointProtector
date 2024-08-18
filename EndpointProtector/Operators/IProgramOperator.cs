using Microsoft.Diagnostics.Tracing.Parsers.Kernel;

namespace EndpointProtector.Operators
{
	public interface IProgramOperator
	{
		ValueTask HandleProgramManagement(ProcessTraceData data);
	}
}
