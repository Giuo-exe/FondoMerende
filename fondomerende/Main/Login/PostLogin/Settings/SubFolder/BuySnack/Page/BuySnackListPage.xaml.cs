using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Services.Models;
using Rg.Plugins.Popup.Extensions;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Popup;
using fondomerende.Main.Utilities;
using FormsControls.Base;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View;
using Lottie.Forms;
using fondomerende.Main.Manager;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuySnackListPage : AnimationPage
    {
        bool tgrBool;
        int idsnack = 0;
        public static int[] SelectedSnackIDArr;
        public static int SelectedSnackID;
        string snackName = null;
        ToBuySnackDTO result;
        public static bool Refresh = false;
        SnackServiceManager SnackService = new SnackServiceManager();
        public ObservableCollection<string> Items { get; set; }

        public BuySnackListPage()
        {
            InitializeComponent();

            GetSnacksMethod(false);

            MessagingCenter.Subscribe<BuySnackListPage>(this, "Refresh", async (value) =>
            {
                await GetSnacksMethod(true);
            });
            MessagingCenter.Subscribe<BuySnackListPage>(this, "Close", async (arg) =>
            {
                Navigation.PopAsync();
            });



            if (TabletManager.Instance.tablet)
            {
                switch (Device.RuntimePlatform)   //                                              ||\\
                {              //                                                                 || \\                                    
                               //                         ||  \\ Se il dispositivo è Android non mostra la Top Bar della Navigation Page,
                    case Device.Android: //                                             \\        ||   \\   Se è iOS invece si (perchè senza è una schifezza)
                        NavigationPage.SetHasNavigationBar(this, false);//                \\      ||    \\        \                
                        break;     //                                                      ||||||||||||||||\/\/|    |
                                   //      ||    //        /       
                    default:                                                            //        ||   //
                        NavigationPage.SetHasNavigationBar(this, true);//                         ||  //
                        break;  //                                                                || //
                }
            }                                                                              //
        }


       

        public async Task GetSnacksMethod(bool Loaded)     //ottiene la lista degli snack e la applica alla ListView
        {
           var result = await SnackService.GetToBuySnacksAsync();
           ListView.ItemsSource = result.data.snacks;
        }


        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(tgrBool)
            {
                tgrBool = false;
                SelectedSnackID = idsnack;
            }
            else
            {
                await SnackService.GetToBuySnacksAsync();
                SelectedSnackID = (e.SelectedItem as ToBuyDataDTO).id;
            }
            await Navigation.PushPopupAsync(new BuySnackPopUpPage());

        }
    }

}

