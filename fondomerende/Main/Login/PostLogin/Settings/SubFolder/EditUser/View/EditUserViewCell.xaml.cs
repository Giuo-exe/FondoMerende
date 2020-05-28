using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Services.RESTServices;
using fondomerende.Main.Manager;
using fondomerende.Main.Utilities;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.LogOut.View;
using fondomerende.Main.Services.Models;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.About_and_UserSettings.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Settaggio.View;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserViewCell : ViewCell
    {
        UserFundsServiceManager userFundsService = new UserFundsServiceManager();
        public string firstLetterIcon;
    
        public EditUserViewCell()
        {
            InitializeComponent();
            First_letter();
            GetUserFundsMethod();
            friendly_name.Text = InformationFriendlyName();


            //Da cancerllare

            friendly_name.Text = "Giulio";



            if (friendly_name.Text.Length > 9)
            {
                string appoggio = friendly_name.Text;
                friendly_name.Text = "";
                friendly_name.Text += appoggio.Substring(0, 9);
                friendly_name.Text += "...";
            }

            Cerchio.BackgroundColor = Color.FromHex(Preferences.Get("Colore", "#CCCCCC"));
            CerchioRc.FillColor = Color.FromHex(Preferences.Get("Colore", "#CCCCCC"));
            CerchioRc.BackgroundColor = Color.FromHex(Preferences.Get("Colore", "#CCCCCC"));


            MessagingCenter.Subscribe<EditUserViewCell>(this, "Refresh", async (value) =>
            {
                Cerchio.BackgroundColor = Color.FromHex(Preferences.Get("Colore", "#CCCCCC"));
                CerchioRc.FillColor = Color.FromHex(Preferences.Get("Colore", "#CCCCCC"));
                CerchioRc.BackgroundColor = Color.FromHex(Preferences.Get("Colore", "#CCCCCC"));
            });



            MessagingCenter.Subscribe<EditUserViewCell>(this, "RefreshUF", async (value) =>
            {
                await GetUserFundsMethod();
            });

        }


        public void First_letter()        //Grafica
        {
            string firstLetter = "G";
            //firstLetter = Preferences.Get("friendly-name", " ").Substring(0, 1);
            inizialeLabel.Text = firstLetter;
            inizialeLabel_iOS.Text = firstLetter;
        }

        public string InformationFriendlyName() => Preferences.Get("friendly-name", "");

        public void Bottone_ClickedAsync(object sender, EventArgs e)  // modifica i vari colori 
        {
            ColorRandom c = new ColorRandom();
            Color color = c.GetRandomColorPreferences();
            Cerchio.BackgroundColor = color;
            CerchioRc.FillColor = color;
            CerchioRc.BackgroundColor = color;
            MessagingCenter.Send(new ChronologyViewCell()
            {

            }, "Refresh");

            MessagingCenter.Send(new FondoFondoMerendeViewCell()
            {

            }, "Refresh");


            MessagingCenter.Send(new EditUserInfoViewCell()
            {

            }, "Refresh");

            MessagingCenter.Send(new EditSnackViewCell()
            {

            }, "Refresh");

            MessagingCenter.Send(new ChangeColorViewCell()
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

        public async Task<string> GetUserFundsMethod()
        {
            var result =  await userFundsService.GetUserFunds();
            if(result != null)
            {
                if (result.success)
                {
                    userFunds.Text = "€" + result.data.user_funds_amount;
                    if (float.Parse(result.data.user_funds_amount) <= 0)
                    {
                        userFunds.TextColor = Color.Red;
                    }
                    else
                    {
                        userFunds.TextColor = Color.Black;
                    }
                }
                else if (!result.success)
                {
                    userFunds.Text = "Errore";
                }
            }
            else
            {

            }

            if (userFunds.Text == "Errore")
            {
                await GetUserFundsMethod();
            }
            return null;
        }
        
    }
}

