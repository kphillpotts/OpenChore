using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenChore.DataStore.Abstractions;
using OpenChore.Models;

namespace OpenChore.DataStore.Mock
{
    public class RewardStore : BaseStore<Reward>, IRewardStore
    {
        List<Reward> mockRewards = new List<Reward>();
        bool initialized;

        public async override Task InitializeStore()
        {
            if (initialized) return;

            initialized = true;

            CreateMockRewards();
        }

        private void CreateMockRewards()
        {
            mockRewards.Add(new Reward()
            {
                Name = "Ice Cream",
                Description = "Delicious frozen desert",
                RedeemValue = 20
            });

            mockRewards.Add(new Reward()
            {
                Name = "Hotwheels Car",
                Description = "Vroom vroom!",
                RedeemValue = 30
            });

            mockRewards.Add(new Reward()
            {
                Name = "Lego Set",
                Description = "Everything is awesome!",
                RedeemValue = 100
            });
            mockRewards.Add(new Reward()
            {
                Name = "Pokemon Cards",
                Description = "Collect Eevee collection!",
                RedeemValue = 30
            });
            mockRewards.Add(new Reward()
            {
                Name = "iPad Time",
                Description = "One hour of screen time!",
                RedeemValue = 10
            });
        }

        public async override Task<bool> InsertAsync(Reward item)
        {
            if (!initialized)
                await InitializeStore();

            mockRewards.Add(item);

            return true;
        }

        public async override Task<IEnumerable<Reward>> GetItemsAsync()
        {
            if (!initialized)
                await InitializeStore();
            return mockRewards;
        }
    }
}
