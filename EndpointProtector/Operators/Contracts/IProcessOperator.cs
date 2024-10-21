using System.Diagnostics;

namespace EndpointProtector.Operators.Contracts
{
    public interface IProcessOperator
    {
        void HandleNewProcess(Process process);
    }
}