using System;
using OpenChore.Pages;
using OpenChore.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OpenChore
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ViewModelBase.Init();
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
