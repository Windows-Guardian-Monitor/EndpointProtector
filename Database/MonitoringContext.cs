using LiteDB;

namespace Database
{
    public class MonitoringContext : IDatabaseContext, IDisposable
    {
        private LiteDatabase _database;

        private const string FileName = "endpoint_protector.db";
        private const string FolderName = "tcc";

        public MonitoringContext()
        {
            var connectionString = new ConnectionString()
            {
                Connection = ConnectionType.Shared,
                Filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), FolderName, FileName)
            };

            _database = new LiteDatabase(connectionString);
        }

        public void Dispose()
        {
            _database?.Dispose();
        }

        public ILiteCollection<T> GetSpecificCollection<T>() => _database.GetCollection<T>();
    }
}
