using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fondomerende.Main.Login.TabletMode.Controlli;
using fondomerende.Main.Utilities;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using fondomerende.Main.Manager;
using fondomerende.Main.Login.PostLogin.AllSnack.Page;
using Rg.Plugins.Popup.Services;
using fondomerende.Main.Services.RESTServices;
using fondomerende.Main.Services.Models;

namespace fondomerende.Main.Login.TabletMode.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CodicePopup : PopupPage
    {
        LineEntry codiceAndroid;
        LineEntry codiceiOS;
        object index;
        string mod;
        object aggiunta,aggiunta2,aggiunta3,aggiunta4;

        public CodicePopup(object index, string mod, object aggiunta = null, object aggiunta4 = null)
        {
            this.aggiunta = aggiunta;
            this.aggiunta4 = aggiunta4;

            this.index = index;
            this.mod = mod;
            InitializeComponent();
            popup();
        }
        public CodicePopup(object index,string mod,object aggiunta = null,object aggiunta2 = null,object aggiunta3 = null, object aggiunta4 = null) 
        {
            this.aggiunta = aggiunta;
            this.aggiunta2 = aggiunta2;
            this.aggiunta3 = aggiunta3;
            this.aggiunta4 = aggiunta4;

            this.index = index;
            this.mod = mod;
            InitializeComponent();
            popup();
        }

        public static Color GetPrimaryAndroidColor()
        {
            return Color.FromHex("#f29e17");
        }

        public static double GetLarghezzaPagina()
        {
            return App.Current.MainPage.Width;
        }

        public static double GetAltezzaPagina()
        {
            return App.Current.MainPage.Height;
        }

        private void popup()
        {
            double Altezza = (GetAltezzaPagina() * 30) / 100;
            double Larghezza = GetLarghezzaPagina() - 80;
            double banner = 50;

            var Round = new RoundedCornerView  //coso che stonda
            {
                RoundedCornerRadius = 20,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = Altezza,
                WidthRequest = Larghezza,
            };

            var stackFondoAndroid = new StackLayout() //per android 
            {
                HeightRequest = banner,
                WidthRequest = Larghezza,
                BackgroundColor = GetPrimaryAndroidColor(),
                Orientation = StackOrientation.Horizontal,
            };

            var stackFondoiOS = new StackLayout()  //per ios 
            {
                HeightRequest = banner,
                WidthRequest = Larghezza,
                BackgroundColor = Color.Orange,
                Orientation = StackOrientation.Horizontal,
            };

            var fondomerende = new Label  //Label per Il titolo banner 
            {
                Text = "Fondo merende",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White,
            };

            codiceAndroid = new LineEntry
            {
                Placeholder = "Codice univoco",
                WidthRequest = 250,
                Margin = new Thickness(0, 5, 0, 0),
                IsVisible = true,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            codiceiOS = new LineEntry
            {
                Placeholder = "Codice univoco",
                WidthRequest = 250,
                FontSize = 18,
                HeightRequest = 35,
                VerticalOptions = LayoutOptions.StartAndExpand,
                IsVisible = true,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            var stackBody = new StackLayout  //stack principale dove è contenuto l'interno di tutto (tranne round che stonda)

            {
                HeightRequest = Altezza,
                WidthRequest = Larghezza,
                BackgroundColor = Color.White,
            };

            var stackBottoni = new StackLayout  //stack che contiene la gridlia dei bottoni
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = Larghezza,
                HeightRequest = banner,
                MinimumHeightRequest = banner,
            };

            var griglia = new Grid //griglia che contiene i bottoni
            {

            };



            var buttonCancel = new Button
            {
                Text = "Annulla",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Transparent,
            };

            var buttonConfirm = new Button
            {
                Text = "Conferma",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Transparent,
            };

            stackBottoni.Children.Add(griglia);
            griglia.Children.Add((buttonCancel)); //inzia nella prima colonna
            griglia.Children.Add((buttonConfirm)); //inizia seconda colonna

            Grid.SetColumn(buttonCancel, 0); //mi è toccato farlo qui
            Grid.SetColumn(buttonConfirm, 1);



            switch (Device.RuntimePlatform)
            {
                case Device.Android:

                    stackFondoAndroid.Children.Add(fondomerende);
                    stackBody.Children.Add(stackFondoAndroid);
                    buttonCancel.Clicked += Discard_Clicked;
                    buttonConfirm.Clicked += Apply_Clicked;
                    stackBody.Children.Add(codiceAndroid);


                    stackBody.Children.Add(stackBottoni);
                    Round.Children.Add(stackBody);
                    break;


                case Device.iOS:
                    stackFondoiOS.Children.Add(fondomerende);
                    stackBody.Children.Add(stackFondoiOS);
                    buttonCancel.Clicked += Discard_Clicked;
                    buttonConfirm.Clicked += Apply_Clicked;
                    stackBody.Children.Add(codiceiOS);


                    stackBody.Children.Add(stackBottoni);
                    Round.Children.Add(stackBody);

                    break;
            }
            //  entry.TextChanged += Entrata;





            PopupCodice.Content = Round;
        }


        private async void Discard_Clicked(object sender, EventArgs e)
        {
            Discard.IsEnabled = false;
            await Navigation.PopPopupAsync();
        }

        private async void Apply_Clicked(object sender, EventArgs e)
        {
            bool result = false;

            if (codiceAndroid.Text != "" || codiceAndroid.Text != null || codiceiOS.Text != "" || codiceiOS.Text != null)
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        result = await ControlloCodice.checkBeforeAction(codiceAndroid.Text);
                        break;
                    case Device.iOS:
                        
                        result = await ControlloCodice.checkBeforeAction(codiceiOS.Text);
                        break;
                }
                if (result)
                {
                    switch(mod)
                    {
                        case "mangia":
                            SnackServiceManager a = new SnackServiceManager();
                            SnackDataDTO elemento = (SnackDataDTO)index;
                            var scelta = await DisplayAlert("Fondomerende", "Sicuro di voler mangiare " + elemento.friendly_name + "?", "Si", "No");
                            if (scelta)
                            {
                                var risposta = await a.EatAsync(elemento.id, 1);
                                if (risposta.success) await DisplayAlert("Fondomerende", "Snack mangiato", "Ok");
                                else await DisplayAlert("Fondomerende", "Snack non mangiato", "Ok");
                                await PopupNavigation.Instance.PopAsync();
                            }
                            else
                            {
                                await PopupNavigation.Instance.PopAsync();
                            }
                            break;

                        //////////////////////////////////////////////////////////
                        case "deposita":
                            DepositServiceManager d = new DepositServiceManager();

                            string app = (string)index; 
                            var resultDep = await d.DepositAsync(app);
                            if (resultDep != null)
                            {
                                if (resultDep.success)
                                {
                                    await DisplayAlert("Fondomerende", "Soldi depositati con successo", "Ok");
                                    if (Device.RuntimePlatform == Device.iOS)
                                    {
                                        DependencyService.Get<HapticFeedbackGen>().HapticFeedbackGenSuccessAsync();
                                    }

                                    else
                                    {

                                        Vibration.Vibrate(40);
                                        await Task.Delay(100);
                                        Vibration.Vibrate(40);
                                    }
                                    await PopupNavigation.Instance.PopAsync();

                                }
                                else
                                {
                                    await DisplayAlert("Fondomerende", "I soldi non sono stati depositati", "Ok");
                                };
                            }
                            else
                            {
                                await PopupNavigation.Instance.PopAsync();
                            }
                            break;

                            /////////////////////////////////////////////////
                            case "compra":
                            int id = (int)index;
                            int quantita = (int)aggiunta;
                            bool swap = (bool)aggiunta4;
                            SnackServiceManager x = new SnackServiceManager();
                            if (!swap)
                            {
                                var risp = await x.BuySnackAsync(id, quantita);
                                if (risp != null)
                                {
                                    if (risp.success)
                                    {
                                        MessagingCenter.Send(new AllSnacksPage()
                                        {

                                        }, "RefreshGetSnacks");
                                        Vibration.Vibrate(40);
                                        await Task.Delay(100);
                                        Vibration.Vibrate(40);
                                        MessagingCenter.Send(new AllSnacksPage()
                                        {

                                        }, "RefreshGriglia");
                                        await DisplayAlert("Fondo Merende", "Lo snack è stato comprato", "Ok");
                                        await PopupNavigation.Instance.PopAsync();

                                    }
                                    else
                                    {
                                        await DisplayAlert("Fondo Merende", risp.message, "Ok");
                                    }
                                }
                            }
                            else
                            {

                                string prezzo = (string)aggiunta2;
                                string scadenza = (string)aggiunta3;
                                var risp = await x.BuySnackAsync2(id, quantita, prezzo, scadenza);
                                if (risp != null)
                                {
                                    if (risp.success)
                                    {
                                        MessagingCenter.Send(new AllSnacksPage()
                                        {

                                        }, "RefreshGetSnacks");
                                        Vibration.Vibrate(40);
                                        await Task.Delay(100);
                                        Vibration.Vibrate(40);
                                        MessagingCenter.Send(new AllSnacksPage()
                                        {

                                        }, "RefreshGriglia");
                                        await DisplayAlert("Fondo Merende", "Lo snack è stato comprato", "Ok");
                                        await PopupNavigation.Instance.PopAsync();
                                    }
                                    else
                                    {
                                        await DisplayAlert("Fondo Merende", risp.message, "Ok");
                                    }
                                }
                            }
                            
                            break;

                        ///////////////////////////////////////////
                    }
                }
                else
                {
                    await DisplayAlert("Fondomerende", "Codice non presente", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Fondomerende", "Inserire il codice", "Ok");
            }
            ControlloCodice.fineAzioni();
        }
    }

}