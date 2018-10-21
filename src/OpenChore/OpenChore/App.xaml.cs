using System;
using OpenChore.Pages;
using OpenChore.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OpenChore
{

    public static class ViewModelLocator
    {
        static MainViewModel mainViewModel;

        public static MainViewModel MainViewModel =>
           mainViewModel ?? (mainViewModel = new MainViewModel());
    }

    public partial class App : Application
    {
        //public static MainViewModel MainViewModel { get; set; }

        public App()
        {
            InitializeComponent();
            ViewModelBase.Init();
            //MainViewModel = new MainViewModel();
            MainPage = new NavigationPage( new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
