using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using OpenChore.Models;
using Xamarin.Forms;

namespace OpenChore.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public ObservableRangeCollection<User> Users { get; } = new ObservableRangeCollection<User>();

        public LoginViewModel()
        {
            LoadUsersCommand = new Command(
                (async () => await ExecuteLoadUsersAsync()), () => true);
        }

        public ICommand LoadUsersCommand { get; private set; }

        public async Task ExecuteLoadUsersAsync()
        {
            try
            {
                IsBusy = true;
                var sm = StoreManager;

                var users = await StoreManager.UserStore.GetItemsAsync();
                Users.ReplaceRange(users);
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


    }
}
