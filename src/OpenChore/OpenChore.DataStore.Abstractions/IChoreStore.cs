using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenChore.Models;

namespace OpenChore.DataStore.Abstractions
{
    public interface IChoreStore : IBaseStore<ChoreDefinition>
    {
        Task<IEnumerable<ChoreDefinition>> GetDailyChores(string userId, DateTime forDate);
    }
}
