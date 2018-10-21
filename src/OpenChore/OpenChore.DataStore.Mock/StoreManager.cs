using System;
using System.Threading.Tasks;
using OpenChore.DataStore.Abstractions;

namespace OpenChore.DataStore.Mock
{
    public class StoreManager : IStoreManager
    {
        UserStore userStore;
        ChoreStore choreStore;
        AwardStore awardStore;
        RewardStore rewardStore;

        public bool IsInitialized { get { return true; } }

        public IUserStore UserStore 
        {
            get
            {
                if (userStore == null)
                {
                    userStore = new UserStore
                    {
                        StoreManager = this
                    };
                }
                return userStore;
            }
        }

        public IChoreStore ChoreStore
        {
            get
            {
                if (choreStore == null)
                {
                    choreStore = new ChoreStore
                    {
                        StoreManager = this
                    };
                }
                return choreStore;
            }
        }

        public IAwardStore AwardStore
        {
            get
            {
                if (awardStore == null)
                {
                    awardStore = new AwardStore
                    {
                        StoreManager = this
                    };
                }
                return awardStore;
            }
        }

        public IRewardStore RewardStore
        {
            get
            {
                if (rewardStore == null)
                {
                    rewardStore = new RewardStore
                    {
                        StoreManager = this
                    };
                }
                return rewardStore;
            }
        }

        public Task InitializeAsync()
        {
            return Task.FromResult(true);
        }
    }
}
