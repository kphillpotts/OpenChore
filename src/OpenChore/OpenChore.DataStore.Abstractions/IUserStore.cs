using System;
using System.Threading.Tasks;
using OpenChore.Models;

namespace OpenChore.DataStore.Abstractions
{
    public interface IUserStore : IBaseStore<User>
    {

        Task<int> GetPoints(string userId);

    }
}
