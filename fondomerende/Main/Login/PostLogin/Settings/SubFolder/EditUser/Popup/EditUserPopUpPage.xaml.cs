using fondomerende.Main.Services.RESTServices;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using fondomerende.Main.Services.Models;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser;
using fondomerende.Main.Login.LoginPages;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Page;
using fondomerende.Main.Utilities;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        LogoutServiceManager logoutService = new LogoutServiceManager();
        EditUserServiceManager editUser = new EditUserServiceManager();
        SnackServiceManager snackService = new SnackServiceManager();
        private static bool click = false;
        string appoggio;


        public EditUserPopUpPage()
        {
            InitializeComponent();
            PopupEditUser();
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
        private void PopupEditUser()
        {
            double Altezza = 200;
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


            var label = new Label
            {
                Text = "Inserire la vecchia password",
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            var entry = new LineEntry
            {
                Keyboard = Keyboard.Default,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                IsPassword = true,
                WidthRequest = 200,
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

            
            entry.TextChanged += Entrata;

            buttonCancel.Clicked += Discard_Clicked;
            buttonConfirm.Clicked += Apply_Clicked;

            stackBody.Children.Add(label);
            stackBody.Children.Add(entry);
            stackBody.Children.Add(stackBottoni);
            Round.Children.Add(stackBody);

            EditUserPopUp.Content = Round;
        }

        public void Entrata(object sender, TextChangedEventArgs e)
        {
            appoggio = e.NewTextValue;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            EditUserPopUpPage.click = true;
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


        private async void Apply_Clicked(object sender, EventArgs e)
        {
            string msgError = "Invalid name: " + EditUserPage.FriendlyName + " is already present in database users table at name column.";
            string oldPAssword = Preferences.Get("password", null);
            if (oldPAssword.Equals(appoggio))
            {
                var ans = await App.Current.MainPage.DisplayAlert("Fondo Merende", "Vuoi davvero cambiare  account?", "Si", "No");
                if (ans)
                {
                    var risp = await editUser.EditUserAsync(EditUserInfoPopUp.username, EditUserInfoPopUp.FriendlyName, EditUserInfoPopUp.passwordNuova);

                    if (risp.success == true)
                    {

                        await PopupNavigation.Instance.PopAsync();
                        if (ans)
                        {
                            LogoutServiceManager logoutService = new LogoutServiceManager();
                            var response = await logoutService.LogoutAsync();
                            if (response != null)
                            {
                                if (response.success == true)
                                {
                                    await Navigation.PopAllPopupAsync();
                                    App.Current.MainPage = new LoginPage();
                                    Preferences.Clear();
                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("Fondo Merende", "Guarda, sta cosa non ha senso", "OK");
                                }
                            }
                            else
                            {

                            }
                        }


                    }
                    else if (risp.message != null)
                    {
                        await DisplayAlert("Fondo Merende",risp.message, "Ok");
                        await Navigation.PopPopupAsync();
                    }
                }
                
            }
            else
            {
                await DisplayAlert("Fondo Merende", "Password errata", "Ok");
            }



        }

        private async void Discard_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }



    }
}