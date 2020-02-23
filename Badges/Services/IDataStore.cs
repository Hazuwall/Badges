using System.Collections.Generic;
using System.Threading.Tasks;

namespace Badges
{
    public interface IDataStore<T, G, F>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(T id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(F filter, bool forceRefresh);
        IEnumerable<G> GetGroups();
    }
}
