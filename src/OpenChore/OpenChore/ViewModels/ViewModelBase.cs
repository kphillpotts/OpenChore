using System;
using System.Windows.Input;
using MvvmHelpers;
using OpenChore.DataStore;
using OpenChore.DataStore.Abstractions;
using OpenChore.Models;
using OpenChore.Pages;
using Xamarin.Forms;

namespace OpenChore.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        public ICommand SwitchUserCommand { get; set; }
        public static User ActiveUser { get; set; }
        public ViewModelBase()
        {
            SwitchUserCommand = new Command(SwitchUser);
        }

        public static void Init()
        {
            DependencyService.Register<IStoreManager, DataStore.Mock.StoreManager>();
        }

        private void SwitchUser(object obj)
        {
            
            Application.Current.MainPage = new LoginPage();
        }

        public IStoreManager StoreManager { get; } = DependencyService.Get<IStoreManager>();
    }
}
