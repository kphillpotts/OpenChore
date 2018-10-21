using System;
using System.Collections.Generic;
using OpenChore.Models;
using OpenChore.ViewModels;
using Xamarin.Forms;

namespace OpenChore.Pages
{
    public partial class ChoreDetailsPage : ContentPage
    {
        private ChoreItemViewModel vm;

        public ChoreDetailsPage(ChoreItemViewModel choreItem)
        {
            InitializeComponent();
            BindingContext = vm = choreItem;
        }

    }
}
