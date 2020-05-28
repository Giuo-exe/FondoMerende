using fondomerende.Main.Services.Models;
using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSnackPage : ContentPage
    {
        SnackServiceManager snackService = new SnackServiceManager();
        public static bool clicked = false;
        public AddSnackPage()
        {
            InitializeComponent();
            switch (Device.RuntimePlatform)             //Se il dispositivo è Android non mostra la Top Bar della Navigation Page, se è iOS la mostra
            {
                default:
                    NavigationPage.SetHasNavigationBar(this, true);
                    break;
                case Device.Android:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;

            }
        }


        private async void ApplyChanges_Clicked(object sender, EventArgs e)
        {
            var ans = await DisplayAlert("Fondo Merende", "Lo Snack è contabile?", "Si", "No");
            if (ans)
            {
                var result = await snackService.AddSnackAsync(Nome.Text, double.Parse(Prezzo.Text), int.Parse(SnackPerScatola.Text), int.Parse(ScadenzaInGiorni.Text), true);
                if (result != null)
                {
                    if (result.success)
                    {
                        await DisplayAlert("Fondo Merende", "Lo Snack è stato aggiunto", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Fondo Merende", "Lo Snack non è stato aggiunto", "Ok");
                    }
                }
                else
                {

                }
            }
            else
            {
                await snackService.AddSnackAsync(Nome.Text, double.Parse(Prezzo.Text), int.Parse(SnackPerScatola.Text), int.Parse(ScadenzaInGiorni.Text), false);
            }
        }

    }
}