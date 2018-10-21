using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenChore.DataStore.Abstractions;
using OpenChore.Models;

namespace OpenChore.DataStore.Mock
{
    public class AwardStore: BaseStore<ChoreAward>, IAwardStore
    {
        List<ChoreAward> mockChoreAwards = new List<ChoreAward>();

        bool initialized;
        public async override Task InitializeStore()
        {
            if (initialized)
                return;

            initialized = true;

            // create some awards
            var users = await StoreManager.UserStore.GetItemsAsync();

            foreach (var user in users)
            {
                DateTime awardDate = DateTime.Now.AddDays(-1);
                var chores = await StoreManager.ChoreStore.GetDailyChores(user.Name, DateTime.Now.AddDays(-1));

                foreach (var chore in chores)
                {
                    var award = new ChoreAward();
                    award.UserId = user.Name;
                    award.ChoreDefinitionId = chore.Id;
                    award.ChoreDate = awardDate;
                    award.IsApproved = true;
                    award.Points = chore.Points;
                    mockChoreAwards.Add(award);
                }
                user.Points = await StoreManager.UserStore.GetPoints(user.Name);
            }
        }

        public async Task<IEnumerable<ChoreAward>> GetChoreAwardsForDate(string userId, DateTime forDate)
        {
            if (!initialized)
                await InitializeStore();

            var awards = mockChoreAwards.Where(c => c.ChoreDate.Date == forDate.Date
                                               && c.UserId.ToString() == userId);

            return awards.ToList();
        }

        public async override Task<bool> InsertAsync(ChoreAward item)
        {
            mockChoreAwards.Add(item);
            return true;
        }

        public async Task<IEnumerable<ChoreAward>> GetItemsAsync(string userId)
        {
            if (!initialized)
                await InitializeStore();

            var awards = mockChoreAwards.Where(u => u.UserId == userId);
            return awards;
        }

        public async Task<ChoreAward> GetAwardForUserAndChore(string userId, int choreId, DateTime forDate)
        {
            if (!initialized)
                await InitializeStore();

            var choreAward = mockChoreAwards.FirstOrDefault(
                    mc => mc.UserId == userId &&
                    mc.ChoreDate.Date == forDate.Date &&
                    mc.ChoreDefinitionId == choreId);
            return choreAward;
        }

        public async override Task<bool> RemoveAsync(ChoreAward item)
        {
            mockChoreAwards.Remove(item);
            return true;
        }
    }
}
