namespace EndpointProtector.Contracts.DAL
{
    internal interface IRepository<T> where T : class
    {
        void Delete(int id);

        T? GetFirst();

        void Insert(T item);
    }
}
