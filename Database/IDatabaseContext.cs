using LiteDB;

namespace Database;

public interface IDatabaseContext : IDisposable
{
    ILiteCollection<T> GetSpecificCollection<T>();
}
