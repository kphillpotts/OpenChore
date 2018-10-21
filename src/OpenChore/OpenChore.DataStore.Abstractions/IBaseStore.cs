using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenChore.DataStore.Abstractions
{
    public interface IBaseStore<T>
    {
        Task InitializeStore();
        Task<IEnumerable<T>> GetItemsAsync();
        Task<T> GetItemAsync(string id);
        Task<bool> InsertAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> RemoveAsync(T item);
        Task<bool> SyncAsync();

        string Identifier { get; }
    }
}
