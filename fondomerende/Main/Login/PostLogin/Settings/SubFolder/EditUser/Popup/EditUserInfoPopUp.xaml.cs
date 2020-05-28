using fondomerende.Main.Services.RESTServices;
using fondomerende.Main.Utilities;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserInfoPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static string username = "";
        public static string FriendlyName = "";
        public static string passwordNuova = "";
        string appoggio1, appoggio2, appoggio3;

        LineEntry entryUsername;
        LineEntry entryFriendlyName;
        LineEntry entryNewPassword;
        public string GetpasswordNuova()
        {
            return passwordNuova;
        }
        public EditUserInfoPopUp()
        {
            InitializeComponent();
            //entryUsername.Placeholder = ;
            //entryFriendlyName.Placeholder = ;
            PopupEditUserInfo();
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
        private void PopupEditUserInfo()
        {
            double Altezza = (GetAltezzaPagina()*45)/ 100;
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

            var stackImgBtn = new StackLayout()
            {
                HeightRequest = 50,
            };



            //variabili in line entry//
            entryUsername = new LineEntry
            {
                Margin= new Thickness(0,-40,0,0),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                WidthRequest = 250,
                IsSpellCheckEnabled = false,
                IsTextPredictionEnabled = false,
                Keyboard = Keyboard.Plain,
                HorizontalTextAlignment = TextAlignment.Center,
            };
            entryFriendlyName = new LineEntry
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 250,
                VerticalOptions = LayoutOptions.StartAndExpand,
                IsSpellCheckEnabled = false,
                IsTextPredictionEnabled = false,
                Keyboard = Keyboard.Plain,
                HorizontalTextAlignment = TextAlignment.Center,
            };
            entryNewPassword = new LineEntry
            {
                Keyboard = Keyboard.Default,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 250,
                IsPassword = true,
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
                    break;
                case Device.iOS:
                    stackFondoiOS.Children.Add(fondomerende);
                    stackBody.Children.Add(stackFondoiOS);
                    break;
            }


            entryUsername.TextChanged += EntrataUsername;
            entryFriendlyName.TextChanged += EntrataFriendlyName;
            entryNewPassword.TextChanged += EntrataPasswordNuova;

            buttonCancel.Clicked += Discard_Clicked;
            buttonConfirm.Clicked += ApplyChanges_Clicked_1;

            entryUsername.Placeholder = Preferences.Get("username", null);
            entryFriendlyName.Placeholder = Preferences.Get("friendly-name", null);
            entryNewPassword.Placeholder = "Nuova Password ";


            stackBody.Children.Add(stackImgBtn);
            stackBody.Children.Add(entryUsername);
            stackBody.Children.Add(entryFriendlyName);
            stackBody.Children.Add(entryNewPassword);

            stackBody.Children.Add(stackBottoni);
            Round.Children.Add(stackBody);

            EditUserInfoPopUpXaml.Content = Round;
        }

        public void EntrataUsername(object sender, TextChangedEventArgs e)
        {
            appoggio1 = e.NewTextValue;

        }

        public void EntrataFriendlyName(object sender, TextChangedEventArgs e)
        {
            appoggio2 = e.NewTextValue;

        }

        public void EntrataPasswordNuova(object sender, TextChangedEventArgs e)
        {
            appoggio3 = e.NewTextValue;

        }


        private void SetpasswordNuova(string value)
        {
            passwordNuova = value;
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


        private async void ApplyChanges_Clicked_1(object sender, EventArgs e)
        {
            EditUserServiceManager editUser = new EditUserServiceManager();
            if (appoggio1 != null && appoggio2 != null && appoggio3 != null)
            {

                username = appoggio1;
                FriendlyName = appoggio2;
                passwordNuova = appoggio3;
                Navigation.PushPopupAsync(new EditUserPopUpPage());
            }
            else
            {
                await DisplayAlert("Fondo Merende", "inserisci tutti i dati", "Ok");
            }
        }

        private async void Discard_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}