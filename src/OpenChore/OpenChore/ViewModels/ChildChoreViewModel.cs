using System;
using MvvmHelpers;
using OpenChore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OpenChore.ViewModels
{
    public class ChildChoreViewModel : ViewModelBase
    {
        public User User { get; set; }
        private DateTime date { get; set; }
        public ObservableRangeCollection<ChoreItemViewModel> Chores { get; } = new ObservableRangeCollection<ChoreItemViewModel>();
        public ChildChoreViewModel(User user, DateTime forDate)
        {
            this.User = user;
            date = forDate;
        }

        public async Task LoadChores()
        {
            // go get the daily chores for the user
            var chores = await this.StoreManager.ChoreStore.GetDailyChores(this.User.Name, date);
            var awards = await this.StoreManager.AwardStore.GetChoreAwardsForDate(this.User.Name, date);

            foreach (var chore in chores)
            {
                // try and find an existing item
                var loadedChore = Chores.FirstOrDefault(o => o.ChoreDefinition.Id == chore.Id);

                // no chore found so add
                if (loadedChore == null)
                {
                    var award = awards.FirstOrDefault(o => o.ChoreDefinitionId == chore.Id);
                    var choreItem = new ChoreItemViewModel(this.User, chore, award);
                    Chores.Add(choreItem);
                }
                else
                {
                    var award = awards.FirstOrDefault(o => o.ChoreDefinitionId == chore.Id);
                    loadedChore.UpdateAward(award);
                }
            }
        }
    }
}
