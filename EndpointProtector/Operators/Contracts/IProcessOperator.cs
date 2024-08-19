using System.Diagnostics;

namespace EndpointProtector.Operators.Contracts
{
    internal interface IProcessOperator
    {
        void HandleNewProcess(Process process);
    }
}