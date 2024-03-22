namespace Svetlina.Services.Contracts
{


    public interface IDb<T, K> where K : IConvertible
    {
        Task CreateAsync(T item);

        Task<T> ReadAsync(K key, bool useNavigationalProperties = false);

        Task<ICollection<T>> ReadAllAsync(bool useNavigationalProperties = false);

        Task UpdateAsync(T item, bool useNavigationalProperties = false);

        Task DeleteAsync(K key);

    }
}
