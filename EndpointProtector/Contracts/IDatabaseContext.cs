using LiteDB;

namespace EndpointProtector.Contracts
{
    internal interface IDatabaseContext : IDisposable
    {
        ILiteCollection<T> GetSpecificCollection<T>();
    }
}
