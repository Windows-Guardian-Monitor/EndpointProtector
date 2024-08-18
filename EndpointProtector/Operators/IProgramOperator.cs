using System.Diagnostics;

namespace EndpointProtector.Operators
{
	public interface IProgramOperator
	{
		void HandleProgramManagement(Process process, string name = "");
	}
}
