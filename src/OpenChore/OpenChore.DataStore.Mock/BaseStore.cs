using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenChore.DataStore.Abstractions;

namespace OpenChore.DataStore.Mock
{
    public class BaseStore<T> : IBaseStore<T>
    {
        public string Identifier => throw new NotImplementedException();

        public virtual IStoreManager StoreManager { get; set; }

        public virtual Task<T> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task InitializeStore()
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> InsertAsync(T item)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> RemoveAsync(T item)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> SyncAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}
