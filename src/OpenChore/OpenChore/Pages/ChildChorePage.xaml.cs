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
        public ChildChorePage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.MainViewModel;
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var choreDefinition = e.Item as ChoreItemViewModel;

            if (choreDefinition != null)
                Navigation.PushAsync(new ChoreDetailsPage(choreDefinition), true);
        }
    }
}
