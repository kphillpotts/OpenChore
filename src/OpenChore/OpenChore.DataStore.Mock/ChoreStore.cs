using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenChore.DataStore.Abstractions;
using OpenChore.Models;

namespace OpenChore.DataStore.Mock
{
    public class ChoreStore : BaseStore<ChoreDefinition>, IChoreStore
    {
        List<ChoreDefinition> mockChores = new List<ChoreDefinition>();
        bool initialized;

        public async override Task InitializeStore()
        {
            if (initialized) return;

            initialized = true;

            await CreateMockChores();
        }



        public async override Task<bool> InsertAsync(ChoreDefinition item)
        {
            if (!initialized)
                await InitializeStore();

            mockChores.Add(item);

            return true;
        }

        public async Task<IEnumerable<ChoreDefinition>> GetDailyChores(string userId, DateTime forDate)
        {
            if (!initialized)
                await InitializeStore();

            List<ChoreDefinition> foundChores = new List<ChoreDefinition>();
            // get chores assigned to child
            foreach (var chore in mockChores)
            {
                foreach (var choreUser in chore.AllocatedTo)
                {
                    if (choreUser.Name == userId)
                        foundChores.Add(chore);
                }
            }
            return foundChores;
        }

        private async Task CreateMockChores()
        {
            var users = await StoreManager.UserStore.GetItemsAsync();

            // chores for everyone
            mockChores.Add(CreateDailyChore("Make your bed", "Good sleep is super important", 10, users));
            mockChores.Add(CreateDailyChore("Clean your room", "A clean room, is a clean mind", 20, users));
            mockChores.Add(CreateDailyChore("Eat a good dinner", "Good for growing bones", 10, users));
            mockChores.Add(CreateDailyChore("Get dressed for school", "Nobody wants to go to school naked", 10, users));

            var rose = users.Where(o => o.Name == "Rose");
            mockChores.Add(CreateDailyChore("Make your lunch", "Pack your own lunch", 10, rose));
            mockChores.Add(CreateDailyChore("Feed the fish", "nom. nom. nom.", 5, rose));
            mockChores.Add(CreateDailyChore("Ride to school", "Bicycle Bandits", 5, rose));
            mockChores.Add(CreateDailyChore("Read a book", "Knowledge is power", 10, rose));

            var charlieAndAlex = users.Where(o => o.Name == "Charlie" || o.Name == "Alex");
            mockChores.Add(CreateDailyChore("Participate in Soccer", "It's not if you win or lose, it's if you participate", 10, charlieAndAlex));
        }

        static int choreIncrementingId = 0;
        private ChoreDefinition CreateDailyChore(string name, string description, int points, IEnumerable<User> allocatedTo)
        {
            // increment a fake counter
            choreIncrementingId++;

            var newChore = new ChoreDefinition()
            {
                Id = choreIncrementingId,
                Name = name,
                Points = points,
                StartDateTime = DateTime.Now.AddDays(-1),
                Active = true,
                Description = description,
                ImageUrl = "",
                IsRepeating = true,
                RepeatingEvery = 1,
                RepeatingUnit = RepeatingUnitType.Day
            };

            // add allocations
            newChore.AllocatedTo = allocatedTo.ToList();

            return newChore;
        }
    }
}
