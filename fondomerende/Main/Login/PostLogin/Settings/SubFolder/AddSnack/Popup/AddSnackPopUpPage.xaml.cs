using fondomerende.Main.Services.RESTServices;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fondomerende.Main.Login.LoginPages;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.Page;
using fondomerende.Main.Utilities;
using Xamarin.Essentials;
using fondomerende.Main.Login.PostLogin.AllSnack.Page;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSnackPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        
        SnackServiceManager snackService = new SnackServiceManager();
        LineEntry NomeSnack;
        LineEntry PrezzoSnack;
        LineEntry SnackPerBox;
        LineEntry ExpInDays;

        string appoggioNome,  appoggioSnackPerScatola, appoggioScadenzaInGiorni, appoggioPrezzo;

        bool IsDone;

        public AddSnackPopUpPage()
        {
            InitializeComponent();
            PopupAddSnack();
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
        private void PopupAddSnack()
        {
            /*switch(Device.RuntimePlatform)
            {
                case (Device.Android):
                    double Altezza = (GetAltezzaPagina()*60)/100;
                    break;
                    
            }*/
            
            double Altezza = (GetAltezzaPagina() * 60) / 100;
            double Larghezza = GetLarghezzaPagina() - 40;
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
            };

            var stackFondoiOS = new StackLayout()  //per ios 
            {
                HeightRequest = banner,
                WidthRequest = Larghezza,
                BackgroundColor = Color.Orange,
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

            /*var stackImgBtn = new StackLayout()
            {
                HeightRequest = 50,
            };*/



            //variabili in line entry//
            NomeSnack = new LineEntry
            {
                Margin = new Thickness(0, 15, 0, 0),
                Keyboard = Keyboard.Default,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 250,
            };
            PrezzoSnack = new LineEntry
            {
                Keyboard = Keyboard.Numeric,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 250,
            };
            SnackPerBox = new LineEntry
            {
                Keyboard = Keyboard.Numeric,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 250,
            };
            ExpInDays = new LineEntry
            {
                Keyboard = Keyboard.Numeric,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 250,
            };



            var stackBody = new StackLayout  //stack principale dove è contenuto l'interno di tutto (tranne round che stonda)

            {
                VerticalOptions = LayoutOptions.StartAndExpand,
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
                    break;
                case Device.iOS:
                    stackFondoiOS.Children.Add(fondomerende);
                    stackBody.Children.Add(stackFondoiOS);
                    break;
            }


            NomeSnack.TextChanged += EntrataNome;
            PrezzoSnack.TextChanged += EntrataPrezzo;
            SnackPerBox.TextChanged += EntrataSnackPerScatola;
            ExpInDays.TextChanged += EntrataScadenzaInGiorni;

            buttonCancel.Clicked += Discard_Clicked;
            buttonConfirm.Clicked += Apply_Clicked;

            NomeSnack.Placeholder = "Nome";
            PrezzoSnack.Placeholder = "Prezzo";
            SnackPerBox.Placeholder = "Snack per Scatola";
            ExpInDays.Placeholder = "Giorni di scadenza";


            //stackBody.Children.Add(stackImgBtn);
            stackBody.Children.Add(NomeSnack);
            stackBody.Children.Add(PrezzoSnack);
            stackBody.Children.Add(SnackPerBox);
            stackBody.Children.Add(ExpInDays);

            stackBody.Children.Add(stackBottoni);
            Round.Children.Add(stackBody);

            AddSnackPopUp.Content = Round;
        }

        public void EntrataNome(object sender, TextChangedEventArgs e)
        {

           // if (NomeSnack.Text.Length > 9)
            //{
            //    string appoggio = NomeSnack.Text;
            //    NomeSnack.Text = "";
            //    NomeSnack.Text += appoggio.Substring(0, 9);
            //    NomeSnack.Text += "...";
                appoggioNome = NomeSnack.Text;
           // }
            

        }

        public void EntrataPrezzo(object sender, TextChangedEventArgs e)
        {
            if (PrezzoSnack.CursorPosition == 1 && IsDone && PrezzoSnack.Text != "")
            {
                if (PrezzoSnack.Text.Substring(1, 1) == ",")
                {
                    PrezzoSnack.MaxLength = 4;
                    IsDone = false;
                }
                else
                {
                    PrezzoSnack.MaxLength = 5;
                    PrezzoSnack.Text = PrezzoSnack.Text + ",";
                    IsDone = false;
                }
            }
            else
            {
                IsDone = true;
            }
            appoggioPrezzo = e.NewTextValue;

        }

        public void EntrataScadenzaInGiorni(object sender, TextChangedEventArgs e)
        {
            appoggioScadenzaInGiorni = e.NewTextValue;

        }

        public void EntrataSnackPerScatola(object sender, TextChangedEventArgs e)
        {
            appoggioSnackPerScatola = e.NewTextValue;

        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }



        //per applicare le modifiche//
        private async void Apply_Clicked(object sender, EventArgs e)
        {
            SnackServiceManager snackService = new SnackServiceManager();

            if (NomeSnack.Text == null || PrezzoSnack.Text == null || SnackPerBox.Text == null || ExpInDays.Text == null)
            {
                await DisplayAlert("Fondo Merende", "Compila tutti i campi", "OK");
            }
            else
            {
                var ans = await DisplayAlert("Fondo Merende", "Lo Snack è contabile?", "Si", "No");
                if (ans)
                {
                    var result = await snackService.AddSnackAsync(NomeSnack.Text, double.Parse(PrezzoSnack.Text), int.Parse(SnackPerBox.Text), int.Parse(ExpInDays.Text), true);
                    if (result != null)
                    {
                        if (result.success)
                        {

                            await DisplayAlert("Fondo Merende", "Lo snack è stato aggiunto", "Ok");
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
                            await Navigation.PopPopupAsync();
                        }
                        else
                        {
                            await DisplayAlert("Fondo Merende", "Snack già presente", "Ok");
                        }
                    }
                    else
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }
                }
                else
                {
                    var result = await snackService.AddSnackAsync(NomeSnack.Text, double.Parse(PrezzoSnack.Text), int.Parse(SnackPerBox.Text), int.Parse(ExpInDays.Text), false);
                    if (result != null)
                    {
                        if (result.success)
                        {

                            await DisplayAlert("Fondo Merende", "SnackID: " + result.data.id, "Ok");
                            await Navigation.PopPopupAsync();
                        }
                        else
                        {
                            await DisplayAlert("Fondo Merende", result.message, "Ok");
                        }
                    }
                    else
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }
                }
            }
            
        }

        private async void Discard_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
