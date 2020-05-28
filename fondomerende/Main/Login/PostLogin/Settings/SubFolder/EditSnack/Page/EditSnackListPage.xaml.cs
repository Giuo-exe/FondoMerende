using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using fondomerende.Main.Services.Models;
using Xamarin.Forms.Xaml;
using fondomerende.PostLoginPages;
using Rg.Plugins.Popup.Extensions;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.PopUp;
using FormsControls.Base;
using fondomerende.Main.Manager;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSnackListPage : AnimationPage
    {
        public static int SelectedSnackLength;
        public static int SelectedSnackID;
        public static string SelectedSnackName;
        public static string SelectedSnackFriendlyName;
        public static double SelectedSnackPrice;
        public static int SelectedSnackPerBox;
        public static int SelectedSnackExpiration;
        public static int SelectedSnackQuantity;
        SnackServiceManager SnackService = new SnackServiceManager();
        public EditSnackListPage()
        {
            InitializeComponent();
            GetSnacksMethod();

            MessagingCenter.Subscribe<EditSnackListPage>(this, "Close", async (value) =>
            {
                await Navigation.PopAsync();
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
            }                                                                                //

            ListView.RefreshCommand = new Command(async () =>                                
            {                                                                                         
                await RefreshDataAsync();                                                    
                ListView.IsRefreshing = false;                                               
            });                                                                              
                                                                                             
                                                                                                 
        }                                                                                    
                                                                                              
        public async Task RefreshDataAsync()                                                 
        {                                                                                    
           await GetSnacksMethod();                                                                             
        }                                                                                    

        public async Task GetSnacksMethod()     //ottiene la lista degli snack e la applica alla ListView
        {
            var result = await SnackService.GetAllSnacksAsync();
            if (result!= null)
            {
                ListView.ItemsSource = result.data.snacks;
                SelectedSnackLength = result.data.snacks.Count;
            }
            else
            {

            }
           
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SelectedSnackID = 0;
            SelectedSnackName = null;
            SelectedSnackFriendlyName = null;
            SelectedSnackPerBox = 0;
            SelectedSnackPrice = 0;
            SelectedSnackExpiration = 0;
         
            SelectedSnackID = (e.SelectedItem as AllSnacksDataDTO).id;
            SelectedSnackName = (e.SelectedItem as AllSnacksDataDTO).name;
            SelectedSnackFriendlyName = (e.SelectedItem as AllSnacksDataDTO).friendly_name;
            SelectedSnackPerBox = (e.SelectedItem as AllSnacksDataDTO).snack_per_box;
            SelectedSnackPrice = (e.SelectedItem as AllSnacksDataDTO).price;
            SelectedSnackExpiration = (e.SelectedItem as AllSnacksDataDTO).expiration_in_days;
            await Navigation.PushPopupAsync(new EditSnackPopUpPage());
            GetSnacksMethod();
        }

    }
}



