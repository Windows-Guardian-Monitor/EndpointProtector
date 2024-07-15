using Database;
using LiteDB;

namespace EndpointProtector.Database
{
    internal class MonitoringContext : IDatabaseContext, IDisposable
    {
        private LiteDatabase _database;

        private const string FileName = "ep.db";
        private const string FolderName = "tcc";

        public MonitoringContext()
        {
            _database = new LiteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), FolderName, FileName));
        }

        public void Dispose()
        {
            _database?.Dispose();
        }

        public ILiteCollection<T> GetSpecificCollection<T>() => _database.GetCollection<T>();
    }
}
