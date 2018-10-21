using System;
using System.Threading.Tasks;

namespace OpenChore.DataStore.Abstractions
{
    public interface IStoreManager
    {
        bool IsInitialized { get; }
        Task InitializeAsync();
        IUserStore UserStore { get; }
        IChoreStore ChoreStore { get; }
        IAwardStore AwardStore { get; }
        IRewardStore RewardStore { get; }
    }
}
