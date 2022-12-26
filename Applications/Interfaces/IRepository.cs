namespace Applications.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Add(T value);
        void Update(T value);

        Task<T?> GetByIdAsync(Guid Id);
        Task<IEnumerable<T>> GetAll();
    }
}