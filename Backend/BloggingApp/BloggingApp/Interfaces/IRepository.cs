namespace BloggingApp.Interfaces
{
    public interface IRepository<K, T> where T : class
    {
        public Task<T> Add(T item);
        public Task<T> Delete(K key);
        public Task<T> Update(T item);
        public Task<T> GetbyKey(K key);
        public Task<IEnumerable<T>> Get();
    }
}