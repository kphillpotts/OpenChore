using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenChore.DataStore.Abstractions;
using OpenChore.Models;

namespace OpenChore.DataStore.Mock
{
    public class UserStore : BaseStore<User>, IUserStore
    {
        List<User> mockUsers = new List<User>();

        public override IStoreManager StoreManager { get; set; }

        private bool initialized;

        public async override Task InitializeStore()
        {
            if (initialized)
                return;

            initialized = true;

            await InsertAsync(new User() { Name = "Rose", ImageUrl = "Rose.png" });
            await InsertAsync(new User() { Name = "Charlie", ImageUrl = "Charlie.png" });
            await InsertAsync(new User() { Name = "Alex", ImageUrl = "Alex.png" });

            //foreach (var item in mockUsers)
            //{
            //    item.Points = await GetPoints(item.Name);
            //}
        }

        private async Task<User> CreateMockuser(string name, string imageUrl)
        {
            var newUser = new User()
            {
                Name = name,
                ImageUrl = imageUrl
            };

            return newUser;
        }

        public async override Task<bool> InsertAsync(User item)
        {
            if (!initialized)
                await InitializeStore();

            mockUsers.Add(item);
            return true;
        }

        public async override Task<User> GetItemAsync(string id)
        {
            if (!initialized)
                await InitializeStore();

            var user = mockUsers.FirstOrDefault(u => u.Name == id);
            //if (user != null)
                //user.Points = await StoreManager.UserStore.GetPoints(id);

            return user;
        }

        public async override Task<IEnumerable<User>> GetItemsAsync()
        {
            if (!initialized)
                await InitializeStore();

            return mockUsers;
        }

        public async Task<int> GetPoints(string userId)
        {
            if (!initialized)
                await InitializeStore();

            var awards = await StoreManager.AwardStore.GetItemsAsync(userId);
            return awards.Sum(a => a.Points);
        }
    }
}
