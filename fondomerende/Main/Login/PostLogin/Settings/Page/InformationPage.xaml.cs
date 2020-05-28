using fondomerende.Main.Services.RESTServices;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using fondomerende.Main.Login.LoginPages;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Deposit;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.Content;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.Popup;
using Rg.Plugins.Popup.Extensions;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Deposit.Popup;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.About_and_UserSettings.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.LogOut.View;
using fondomerende.Main.Login.PostLogin.AllSnack.Page;
using ColorPicker;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Popup;

namespace fondomerende.Main.Login.PostLogin.Settings.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformationPage : ContentPage
    {
        UserServiceManager UserService = new UserServiceManager();

        private readonly ColorPickerPopup _colorPickerPopup;
        public object LoggedAs { get; }
        public string friendly_name = Preferences.Get("username", "");

        public InformationPage()
        {
            InitializeComponent();
            EditUserViewCell.BindingContext = friendly_name;
            PaoloAbilita.On = Preferences.Get("Paolo", false);
            Pts.On = Services.Services.test;
            _colorPickerPopup = new ColorPickerPopup();
            _colorPickerPopup.ColorChanged += ColorPickerPopupOnColorChanged;
            Version.Text = "Version:" + "1.0.1";

            //switch (Device.RuntimePlatform)             //Se il dispositivo è Android non mostra la Top Bar della Navigation Page, se è iOS la mostra
            //{
            //    default:
            //        NavigationPage.SetHasNavigationBar(this, true);
            //        break;
            //    case Device.Android:
            //        NavigationPage.SetHasNavigationBar(this, false);
            //       // var section = tableView.Root[0];
            //       // section.RemoveAt(1);
            //        break;

            //}


        }

        private bool DevicePlatform(bool v1, bool v2, bool v3)
        {
            throw new NotImplementedException();
        }

        private async void LogOut_button_Clicked(object sender, EventArgs e)        //effettua il Log Out
        {
            var ans = await DisplayAlert("Fondo Merende", "Sicuro di voler effettuare il Log Out?", "Si", "No");
            if(ans)
            {
                LogoutServiceManager logoutService = new LogoutServiceManager();
                var response = await logoutService.LogoutAsync();
                if (response != null)
                {
                    if (response.success == true)
                    {
                        App.Current.MainPage = new LoginPage();
                        Preferences.Clear();
                    }
                    else
                    {
                        await DisplayAlert("Fondo Merende", "Guarda, sta cosa non ha senso", "OK");
                    }
                }
                else
                {

                }
            }         
        }

      

        private async void ChangeUserSettings_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }

        private async void EditSnackViewCell_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditSnackListPage());
        }

        private async void AddSnackCell_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new AddSnackPopUpPage());
        }

        private async void ChronologyCell_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChronologyContentPage());
        }

        private async void BuySnack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BuySnackListPage());
        }

        private async void Deposit_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new DepositPopUp());
        }

        private void ColorPickerPopupOnColorChanged(object sender, ColorChangedEventArgs args)
        {
            Preferences.Set("Colore", GetHexString(args.Color));
        }
        public static string GetHexString(Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var alpha = (int)(color.A * 255);
            var hex = $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";

            return hex;
        }
        private void EditUserInfoViewCell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new EditUserInfoPopUp());
        }

        private void ChangeColorViewCell_Tapped(object sender, EventArgs e)
        {
            if (Device.RuntimePlatform == Device.Android)
                Navigation.PushPopupAsync(new ColorPickerPopup());
        }

        private async void Pts_Changed(object sender, EventArgs e)
        {
            if (Pts.On == true && Services.Services.test == false)
            {
                var Qst = await DisplayAlert("Fondo Merende", "Passare al server di test?", "Si", "No");
                if (Qst)
                {


                    var Ans = await DisplayAlert("FondoTest", "L'App passerà al server di test fino alla chiusura ed i preferiti andranno persi, sicuro di voler procedere?", "Si", "No");
                    if (Ans)
                    {
                        LogoutServiceManager logoutService = new LogoutServiceManager();
                        await logoutService.LogoutAsync();
                        await Navigation.PopToRootAsync();
                        Services.Services.test = true;
                        App.Current.MainPage = new LoginPage();
                    }
                }
            }
        }

        private async void OnPmChanged(object sender, EventArgs e)
        {
            AllSnacksPage.EnablePacman = Pm.On;
        }

        private void OnPaoloChanged(object sender, EventArgs e)
        {
            if (PaoloAbilita.On)
            {
                Preferences.Set("Paolo", true);
                MessagingCenter.Send(new AllSnacksPage()
                {

                }, "PaoloStart");

            }
            else
            {
                Preferences.Set("Paolo", false);
                MessagingCenter.Send(new AllSnacksPage()
                {

                }, "PaoloStart");
            }
        }
    }
}