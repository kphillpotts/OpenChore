using System;
using System.Collections.Generic;
using OpenChore.Models;
using OpenChore.ViewModels;
using Xamarin.Forms;

namespace OpenChore.Pages
{
    public partial class RewardsPage : ContentPage
    {
        RewardsViewModel vm;
        public RewardsPage()
        {
            InitializeComponent();
            BindingContext = vm = new RewardsViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            vm.LoadRewardCommand.Execute(null);
        }
    }
}
