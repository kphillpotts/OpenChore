using System;
using System.Collections.Generic;
using Xamarin.Forms;
using OpenChore.ViewModels;
using OpenChore.Models;
using System.Threading.Tasks;

namespace OpenChore.Pages
{
    public partial class LoginPage : ContentPage
    {
        //LoginViewModel ViewModel => vm ?? (vm = BindingContext as LoginViewModel);
        //LoginViewModel vm;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.MainViewModel;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UserList.ItemTapped += UserList_ItemTapped;
            UpdatePage();
        }

        private async void UpdatePage()
        {
            ViewModelLocator.MainViewModel.LoadUsersCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            UserList.ItemTapped -= UserList_ItemTapped;
        }

        async void UserList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // go to the page for the user
            UserViewModel user = e.Item as UserViewModel;
            if (user != null)
            {
                //App.MainViewModel.SelectedUserChores = new ChildChoreViewModel(user, DateTime.Now);
                ViewModelLocator.MainViewModel.LoadUserChoresCommand.Execute(DateTime.Now);
                //ViewModelBase.ActiveUser = user;
                this.Navigation.PushAsync(new ChildChorePage());
            }
        }

    }
}
