using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenChore.Models;

namespace OpenChore.DataStore.Abstractions
{
    public interface IAwardStore : IBaseStore<ChoreAward>
    {

        Task<IEnumerable<ChoreAward>> GetChoreAwardsForDate (string userId, DateTime forDate);
        Task<ChoreAward> GetAwardForUserAndChore(string userId, int choreId, DateTime forDate);
        Task<IEnumerable<ChoreAward>> GetItemsAsync(string userId);
    }
}
