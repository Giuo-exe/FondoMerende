using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.PostLoginPages;
using fondomerende.Main.Login.LoginPages;
using fondomerende.Main.Utilities;
using fondomerende.Main.Login.PostLogin;
using System.Threading;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.LogOut.View;

namespace fondomerende.PostLoginPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        UserServiceManager userService = new UserServiceManager();
        public LoadingPage()
        {
            InitializeComponent();
            LogIn();
            
            Donut_Background();
            Ciambella();



            LabelRandom c = new LabelRandom();
            String rPhrase = c.GetRandomPhrases();
            if (rPhrase == "Welcome to the wonderfully edible world of Fondo Merende")
            {
                LoadingLabel.FontSize = 14;
            }
            LoadingLabel.Text = rPhrase;

            MessagingCenter.Send(new ChronologyViewCell()
            {

            }, "Refresh");

            MessagingCenter.Send(new EditSnackViewCell()
            {

            }, "Refresh");

            MessagingCenter.Send(new AddSnackViewCell()
            {

            }, "Refresh");

            MessagingCenter.Send(new BuySnackViewCell()
            {

            }, "Refresh");

            MessagingCenter.Send(new LogoutViewCell()
            {

            }, "Refresh");
        }

        public async void LogIn()
        {
            LoginServiceManager login = new LoginServiceManager();
            var resultLogin = await login.LoginAsync(Preferences.Get("username", null), Preferences.Get("password", null), Preferences.Get("Logged",false));
         //   await userService.GetUserData();
            if(resultLogin == null)
            {
                App.Current.MainPage.DisplayAlert("Fondo Merende", "Connessione Al Fondo Merende fallita", "Ok");
            }
            else if (resultLogin.success)
            {
                await userService.GetUserData();
                App.Current.MainPage = new MainPage();
            }
            else
            {
                App.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }

        private async void Ciambella()
        {
            // await Fondo_Merende_logo.ScaleTo(0.2, 0);
            //await Fondo_Merende_logo.TranslateTo(0, 0, 500);

            //await Task.WhenAny<bool>
            //    (
            //        Fondo_Merende_logo.ScaleTo(0.8, 500),
            //        Fondo_Merende_logo.RotateTo(360, 500)
            //    );

            //await Fondo_Merende_logo.ScaleTo(1.2, 150);
            //await Fondo_Merende_logo.ScaleTo(1, 400);

            await Fondo_Merende_logo.FadeTo(0, 0);
            await Fondo_Merende_logo.FadeTo(0.4, 500);
            await Fondo_Merende_logo.FadeTo(1, 500);

            //await Task.WhenAny<bool>
            //   (
            //    Fondo_Merende_logo.FadeTo(1, 0),
            //    Fondo_Merende_logo.ScaleTo(0.2, 0)
            //   );

            //await Task.WhenAny<bool>
            //    (
            //    Fondo_Merende_logo.FadeTo(1, 200),
            //    Fondo_Merende_logo.ScaleTo(0.9, 800),
            //    Fondo_Merende_logo.RotateTo(360, 800)
            //   );

            //await Fondo_Merende_logo.ScaleTo(1.2, 200);
            //await Fondo_Merende_logo.ScaleTo(1.0, 200);
        }

        private void Donut_Background()
        {
            Task.WhenAny<bool>
            (
                Donut.RotateTo(10, 0),
                Donut.ScaleTo(1.5, 0),
                Donut.TranslateTo(20, -1000, 100000),
                Donut.TranslateTo(-20, 1000, 0),
                Donut.TranslateTo(20, -1000, 100000)
            );


        }
    }
}