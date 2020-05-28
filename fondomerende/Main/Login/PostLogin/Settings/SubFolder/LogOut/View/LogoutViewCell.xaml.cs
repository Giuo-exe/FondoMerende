using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.CrossPlatformTintedImage.Abstractions;
using fondomerende.Main.Login;
using fondomerende.Main.Login.LoginPages;
using fondomerende;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using MasterDetailPage = Xamarin.Forms.PlatformConfiguration.WindowsSpecific.MasterDetailPage;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.LogOut.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class LogoutViewCell : ViewCell
    {
        public LogoutViewCell()
        {
            InitializeComponent();
            SetImageColorPreferences();
            MessagingCenter.Subscribe<LogoutViewCell>(this, "Refresh", async (value) =>
            {
                SetImageColorPreferences();
            });
        }

        public async void LogoutCellTapped(object sender, EventArgs e)
        {
            
            var ans = await App.Current.MainPage.DisplayAlert("Fondo Merende", "Vuoi davvero effettuare il Log Out?", "Si", "No");
            if (ans)
            {
                LogoutServiceManager logoutService = new LogoutServiceManager();
                var response = await logoutService.LogoutAsync();
                if (response != null)
                {

                    App.Current.MainPage = new LoginPage();
                    Preferences.Clear();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Fondo Merende", "Guarda, sta cosa non ha senso", "OK");
                }
            }
            else
            {

            }
        }


        public void SetImageColorPreferences()
        {
            LogoutIcon.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000")) ;
        }
    }
}