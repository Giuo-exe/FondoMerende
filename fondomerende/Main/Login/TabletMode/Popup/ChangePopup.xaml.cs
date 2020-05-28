
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

namespace fondomerende.Main.Login.TabletMode.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ChangePopup : PopupPage
    {
        public ChangePopup()
        {
            InitializeComponent();
            popup();
        }
    
    
        LineEntry usernameAndroid, passwordAndroid, codiceAndroid;
        LineEntry usernameiOS, passwordiOS, codiceiOS;
        private LineEntry codiceAndroidChanged;
        string userA, passA, codA, userI, passI, codI;

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
                Text = "Cambio codice",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White,
            };
            usernameAndroid = new LineEntry
            {
                Placeholder = "Username",
                WidthRequest = 250,
                Margin = new Thickness(0, 5, 0, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,

            };

            passwordAndroid = new LineEntry
            {
                Placeholder = "Password",
                WidthRequest = 250,
                IsVisible = true,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(0, 5, 0, 0),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            codiceAndroid = new LineEntry
            {
                Placeholder = "Nuovo codice univoco",
                WidthRequest = 250,
                Margin = new Thickness(0, 5, 0, 0),
                IsVisible = true,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };



            usernameiOS = new LineEntry
            {
                Placeholder = "Username",
                WidthRequest = 250,
                FontSize = 18,
                HeightRequest = 35,
                Margin = new Thickness(0, 10, 0, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,

            };

            passwordiOS = new LineEntry
            {
                Placeholder = "Password",
                WidthRequest = 250,
                FontSize = 18,
                HeightRequest = 35,
                VerticalOptions = LayoutOptions.StartAndExpand,
                IsVisible = true,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            codiceiOS = new LineEntry
            {
                Placeholder = "Nuovo codice univoco",
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


            var immagine = new ImageButton
            {
                Source = ImageSource.FromResource("fondomerende.image.Edit_Icon_32x32.png"),
                CornerRadius = 20,
                Scale = 1.5,
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0, 0, 15, 0),
                Aspect = Aspect.AspectFit,
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
                    immagine.Clicked += Immagine_Clicked;
                    buttonCancel.Clicked += Discard_Clicked;
                    buttonConfirm.Clicked += Apply_Clicked;
                    stackBody.Children.Add(usernameAndroid);
                    stackBody.Children.Add(passwordAndroid);
                    stackBody.Children.Add(codiceAndroid);
                    codiceAndroid.TextChanged += codiceAndroidCambiato;
                    usernameAndroid.TextChanged += usernameAndroidChanged;
                    passwordAndroid.TextChanged += passwordAndroidChanged;

                    stackBody.Children.Add(stackBottoni);
                    Round.Children.Add(stackBody);
                    break;


                case Device.iOS:
                    stackFondoiOS.Children.Add(fondomerende);
                    stackBody.Children.Add(stackFondoiOS);
                    immagine.Clicked += Immagine_Clicked;
                    buttonCancel.Clicked += Discard_Clicked;
                    buttonConfirm.Clicked += Apply_Clicked;
                    stackBody.Children.Add(usernameiOS);
                    stackBody.Children.Add(passwordiOS);
                    stackBody.Children.Add(codiceiOS);
                    codiceiOS.TextChanged += codiceiOSCambiato;
                    usernameiOS.TextChanged += usernameiOSChanged;
                    passwordiOS.TextChanged += passwordiOSChanged;


                    stackBody.Children.Add(stackBottoni);
                    Round.Children.Add(stackBody);

                    break;
            }

            ChangePopupuser.Content = Round;
        }

        private void passwordAndroidChanged(object sender, TextChangedEventArgs e)
        {
            passA = passwordAndroid.Text;
        }

        private void usernameAndroidChanged(object sender, TextChangedEventArgs e)
        {
            userA = usernameAndroid.Text;
        }

        private void codiceAndroidCambiato(object sender, TextChangedEventArgs e)
        {
            codA = codiceAndroid.Text;
        }

        private void passwordiOSChanged(object sender, TextChangedEventArgs e)
        {
            passI = passwordiOS.Text;
        }

        private void usernameiOSChanged(object sender, TextChangedEventArgs e)
        {
            userI = usernameiOS.Text;
        }

        private void codiceiOSCambiato(object sender, TextChangedEventArgs e)
        {
            codI = codiceiOS.Text;
        }
        private void Immagine_Clicked(object sender, EventArgs e)
        {

        }

        private async void Discard_Clicked(object sender, EventArgs e)
        {
            Discard.IsEnabled = false;
            await Navigation.PopPopupAsync();
        }

        private async void Apply_Clicked(object sender, EventArgs e)
        {
            ;
            bool result = false;

            if ((codI != "" && passI != "" && userI != "") || (codA != "" && passA != "" && userA != ""))
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        Utente u = new Utente(userA, passA, codA);
                        result = await ControlloCodice.cambiaCodice(u);
                        break;
                    case Device.iOS:
                        Utente f = new Utente(userI, passI, codI);
                        result = await ControlloCodice.cambiaCodice(f);
                        break;
                }
                if (result) await DisplayAlert("Fondomerende", "il Codice è stato cambiato con successo", "Ok");
                else await DisplayAlert("Fondomerende", "Impossibile cambiare il codice", "Ok");
            }
            else
            {
                await DisplayAlert("Fondomerende", "Inserire tutti i campi", "Ok");
            }


        }
    }

}