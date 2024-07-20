namespace Common.Contracts.DAL
{
    public interface IRepository<T> where T : class
    {
        void Delete(int id);

        T? GetFirst();

        void Insert(T item);

        IEnumerable<T> GetAll();
    }
}
