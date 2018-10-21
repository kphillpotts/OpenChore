using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenChore.Models;
using OpenChore.ViewModels;
using Xamarin.Forms;

namespace OpenChore.Pages
{
    public partial class ChildChorePage : ContentPage
    {
        private ChildChoreViewModel vm;

        public ChildChorePage(User user)
        {
            InitializeComponent();
            BindingContext = vm = new ChildChoreViewModel(user, DateTime.Now);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await vm.LoadChores();
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var choreDefinition = e.Item as ChoreItemViewModel;

            if (choreDefinition != null)
                Navigation.PushAsync(new ChoreDetailsPage(choreDefinition), true);
        }
    }
}
