using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace OpenChore.Pages
{
    public partial class SwipeChorePage : ContentPage
    {
        public ICommand SwipedLeftCommand { get; set; }
        public ICommand SwipedRightCommand { get; set; }



        public SwipeChorePage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.MainViewModel;
            SwipeControl.Swiped += SwipeControl_Swiped;
            SwipedLeftCommand = new Command(HandleAction);
            SwipedRightCommand = new Command(HandleAction);
        }

        void HandleAction(object obj)
        {
        }

        void SwipeControl_Swiped(object sender, SwipeCards.Controls.Arguments.SwipedEventArgs e)
        {
            if (e.Item == ViewModelLocator.MainViewModel.SelectedUserChores.Last())
                SwipeControl.Setup();
        }

    }
}
