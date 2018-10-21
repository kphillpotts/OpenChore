using System;
using System.Collections.Generic;
using OpenChore.Models;
using OpenChore.Pages;
using OpenChore.ViewModels;
using Xamarin.Forms;

namespace OpenChore.Views
{
    public partial class ChildHeaderView : ContentView
    {
        public ChildHeaderView()
        {
            InitializeComponent();
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            this.Navigation.PopToRootAsync();
        }

        void Handle_Tapped_1(object sender, System.EventArgs e)
        {
            this.Navigation.PushAsync(new RewardsPage());
        }
    }
}
