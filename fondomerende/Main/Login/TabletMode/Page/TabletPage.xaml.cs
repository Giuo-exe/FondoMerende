using fondomerende.Main.Login.LoginPages;
using fondomerende.Main.Login.PostLogin.AllSnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.Popup;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Deposit.Popup;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.Content;
using fondomerende.Main.Login.TabletMode.Controlli;
using fondomerende.Main.Login.TabletMode.Popup;
using fondomerende.Main.Manager;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.TabletMode.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabletPage : ContentPage
    {
        public TabletPage()
        {
            ControlloCodice c = new ControlloCodice();
            InitializeComponent();
            switch (Device.RuntimePlatform)   //                                              ||\\
            {              //                                                                 || \\                                    
                           //                         ||  \\ Se il dispositivo è Android non mostra la Top Bar della Navigation Page,
                case Device.Android: //                                             \\        ||   \\   Se è iOS invece si (perchè senza è una schifezza)
                    NavigationPage.SetHasNavigationBar(this, false);
                    //                \\      ||    \\        \                
                    break;     //                                                      ||||||||||||||||\/\/|    |
                               //                                                       ||    //        /       
                default:                                                            //        ||   //
                    NavigationPage.SetHasNavigationBar(this, false);//                        ||  //
                    break;  //                                                                || //
            }
        }
        private async void BackCliccato(object sender,EventArgs e)
        {
            TabletManager.Instance.tablet = false;
            App.Current.MainPage = new LoginPage();
        }

        private async void EatClicked(object sender,EventArgs e)
        {
            await Navigation.PushAsync(new AllSnacksPage());
        }
        private async void DepositaCliccato(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new DepositPopUp());
        }

        private async void CronologiaCliccato(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChronologyContentPage());
        }

        private async void AddSnackCliccato(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new AddSnackPopUpPage());
        }

        private async void BuySnackCliccato(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BuySnackListPage());
        }

        private async void EditSnackCliccato(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditSnackListPage());
        }

        private async void AdduserCliccato(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new AddUserPopup());
        }

        private async void ChangedCliccato(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new ChangePopup());
        }
    }
}