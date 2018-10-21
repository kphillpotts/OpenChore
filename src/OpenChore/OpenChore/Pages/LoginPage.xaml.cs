using System;
using System.Collections.Generic;
using Xamarin.Forms;
using OpenChore.ViewModels;
using OpenChore.Models;

namespace OpenChore.Pages
{
    public partial class LoginPage : ContentPage
    {
        LoginViewModel ViewModel => vm ?? (vm = BindingContext as LoginViewModel);
        LoginViewModel vm;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UserList.ItemTapped += UserList_ItemTapped;
            UpdatePage();
        }

        private async void UpdatePage()
        {
            ViewModel.LoadUsersCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            UserList.ItemTapped -= UserList_ItemTapped;
        }

        void UserList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // go to the page for the user
            User user = e.Item as User;
            if (user != null)
            {
                ViewModelBase.ActiveUser = user;
                this.Navigation.PushAsync(new ChildChorePage(user));
            }
        }

    }
}
