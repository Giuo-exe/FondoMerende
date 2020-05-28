using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using fondomerende.PostLoginPages;
using fondomerende.Main.Services.RESTServices;
using fondomerende.Main.Login.LoginPages;
using fondomerende.Main.Services;
using fondomerende.Main.Utilities;

namespace fondomerende
{
    public partial class App : Application
    {
        public App()
        {
            Data d = new Data();
            InitializeComponent();

            if (Preferences.Get("Logged", false))
            {
                MainPage = new LoadingPage();
            }
            else
            {
                Preferences.Clear();
                MainPage = new NavigationPage(new LoginPage());
            }

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
