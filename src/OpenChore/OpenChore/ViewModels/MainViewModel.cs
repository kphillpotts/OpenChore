using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using OpenChore.Models;
using Xamarin.Forms;

namespace OpenChore.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private UserViewModel _selectedUser;

        public ICommand LoadUsersCommand { get; private set; }
        public ICommand LoadUserChoresCommand { get; private set; }

        public ObservableRangeCollection<UserViewModel> Users { get; set; } = new ObservableRangeCollection<UserViewModel>();
        public ObservableRangeCollection<ChoreItemViewModel> SelectedUserChores { get; set; } = new ObservableRangeCollection<ChoreItemViewModel>();

        public UserViewModel SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }

        public MainViewModel()
        {
            LoadUsersCommand = new Command(
                                (async () => await ExecuteLoadUsersAsync()), () => true);
            LoadUserChoresCommand = new Command<DateTime>
                (async (dt) => await LoadUserChores(dt));
        }

        public async Task ExecuteLoadUsersAsync()
        {
            try
            {
                IsBusy = true;
                var users = await StoreManager.UserStore.GetItemsAsync();

                Users.ReplaceRange(users.Select(o => new UserViewModel(o)));

                foreach (var user in Users)
                {
                    user.CalculatePointsCommand.Execute(null);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to load users");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task LoadUserChores(DateTime dateTime)
        {
            // go get the daily chores for the user
            var chores = await this.StoreManager.ChoreStore.GetDailyChores(SelectedUser.Name, dateTime);
            var awards = await this.StoreManager.AwardStore.GetChoreAwardsForDate(SelectedUser.Name, dateTime);
            SelectedUserChores = new ObservableRangeCollection<ChoreItemViewModel>();
            foreach (var chore in chores)
            {
                // try and find an existing item
                //var loadedChore = Chores.FirstOrDefault(o => o.ChoreDefinition.Id == chore.Id);

                // no chore found so add
              //  if (loadedChore == null)
                //{
                    var award = awards.FirstOrDefault(o => o.ChoreDefinitionId == chore.Id);
                    var choreItem = new ChoreItemViewModel(SelectedUser.Name, chore, award);
                    SelectedUserChores.Add(choreItem);
                //}
                //else
                //{
                //    var award = awards.FirstOrDefault(o => o.ChoreDefinitionId == chore.Id);
                //    loadedChore.UpdateAward(award);
                //}
            }
        }


    }


    public class UserViewModel : ViewModelBase
    {
        private User _user;
        private int _points;

        public ICommand CalculatePointsCommand { get; private set; }

        public string Name
        {
            get => _user.Name;
            set => _user.Name = value;
        }

        public int Points
        {
            get => _points;
            set => SetProperty(ref _points, value);
        }


        public UserViewModel(User user)
        {
            _user = user;
            CalculatePointsCommand = new Command(async() => await CalculatePointsCommandExecute());

        }

        private async Task CalculatePointsCommandExecute()
        {
            Points = await StoreManager.UserStore.GetPoints(_user.Name);
        }
    }

}
