using System.Diagnostics;

namespace EndpointProtector.Operators.Contracts
{
    public interface IProgramOperator
    {
        void HandleProgramManagement(Process process, string name = "");
    }
}
