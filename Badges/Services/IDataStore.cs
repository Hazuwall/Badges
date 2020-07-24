using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Badges
{
    public interface IDataStore<TItem, TGroup, TFilter> : INotifyPropertyChanged
    {
        bool IsAvailable { get; }
        Task<bool> AddItemAsync(TItem item);
        Task<bool> UpdateItemAsync(TItem item);
        Task<bool> DeleteItemAsync(TItem item);
        Task<TItem> GetItemAsync(int id);
        Task<IEnumerable<TItem>> GetItemsAsync(TFilter filter, bool forceRefresh);
        IEnumerable<TGroup> GetGroups();
    }
}
