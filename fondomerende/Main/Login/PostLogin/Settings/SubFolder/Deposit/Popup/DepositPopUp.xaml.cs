using fondomerende.Main.Services.RESTServices;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Deposit.Popup;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View;
using fondomerende.Main.Utilities;
using fondomerende.Main.Login.PostLogin.AllSnack.Page;
using Rg.Plugins.Popup.Pages;
using Xamarin.Essentials;
using fondomerende.Main.Manager;
using fondomerende.Main.Login.TabletMode.Popup;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.Deposit.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class DepositPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        bool refresh = true;
        bool IsDone;
        LineEntry entryAndroid, entryiOs;
        string appoggio;
        private object inizialeLabel_iOS;

        public DepositPopUp()
        {
            InitializeComponent();
            PopupDeposita();
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
        private void PopupDeposita()
        {
            double Altezza = (GetAltezzaPagina()*30)/100;
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

            entryAndroid = new LineEntry
            {
                Placeholder = "Quanto vuoi depositare?",
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            entryiOs = new LineEntry
            {
                Placeholder = "Quanto vuoi depositare?",
                HeightRequest = 35,
                Margin = new Thickness(0, 15, 0, 0),
                Keyboard = Keyboard.Numeric,
                FontSize = 18,
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
                    entryAndroid.TextChanged += EntrataAndroid;
                    stackBody.Children.Add(entryAndroid);
                    buttonCancel.Clicked += Discard_Clicked;
                    buttonConfirm.Clicked += Apply_Clicked;

                    stackBody.Children.Add(stackBottoni);
                    break;
                case Device.iOS:


                    stackFondoiOS.Children.Add(fondomerende);
                    stackBody.Children.Add(stackFondoiOS);
                    entryiOs.TextChanged += EntrataiOs;
                    stackBody.Children.Add(entryiOs);
                    buttonCancel.Clicked += Discard_Clicked;
                    buttonConfirm.Clicked += Apply_Clicked;

                    stackBody.Children.Add(stackBottoni);
                    break;
            }
            
            Round.Children.Add(stackBody);

            DepositPop.Content = Round;
        }

        public void EntrataAndroid(object sender, TextChangedEventArgs e)
        {
            
            

            if (entryAndroid.CursorPosition == 1 &&  IsDone && entryAndroid.Text != "")
            {
                if (entryAndroid.Text.Substring(1, 1) == ",")
                {
                    entryAndroid.MaxLength = 4;
                    IsDone = false;
                }
                else
                {
                    entryAndroid.MaxLength = 5;
                    entryAndroid.Text = entryAndroid.Text + ",";
                    IsDone = false;
                }
            }
            
            else
            {
                IsDone = true;
            }
             
            appoggio = e.NewTextValue;
           
        }


        public void EntrataiOs(object sender, TextChangedEventArgs e)
        {



            if (entryiOs.CursorPosition == 1 && IsDone && entryiOs.Text != "")
            {
                if (entryiOs.Text.Substring(1, 1) == ",")
                {
                    entryiOs.MaxLength = 4;
                    IsDone = false;
                }
                else
                {
                    entryiOs.MaxLength = 5;
                    entryiOs.Text = entryiOs.Text + ",";
                    IsDone = false;
                }
            }
            else
            {
                IsDone = true;
            }

            appoggio = e.NewTextValue;

        }



        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        protected override void OnDisappearing()
        {
            if (refresh)
            {
                MessagingCenter.Send(new EditUserViewCell()
                {

                }, "RefreshUF");
                refresh = true;
            }
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
      

            DepositServiceManager depositService = new DepositServiceManager(); 
            if (appoggio == null || appoggio == "")
            {
                await DisplayAlert("Fondo Merende","Inserisci l'ammontare","OK");
            }
            else if (float.Parse(appoggio) <= 0)
            {
                await DisplayAlert("Fondo Merende", "L'ammontare deve essere maggiore di zero", "Ok");
            }
            else
            {
                if (TabletManager.Instance.tablet)
                {
                    await Navigation.PushPopupAsync(new CodicePopup(appoggio, "deposita",null,null,null,null));
                }
                else
                {
                    var resultDep = await depositService.DepositAsync(appoggio);
                    if (resultDep != null)
                    {
                        if (resultDep.success)
                        {
                            await DisplayAlert("Fondomerende", "Soldi depositati con successo", "Ok");
                            MessagingCenter.Send(new AllSnacksPage()
                            {

                            }, "Animation");
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
                            refresh = true;
                            await Navigation.PopPopupAsync();

                        }
                        else
                        {
                            await DisplayAlert("Fondomerende", "I soldi non sono stati depositati", "Ok");
                        };
                    }
                    else
                    {
                        refresh = false;
                        await Navigation.PopPopupAsync();
                    }
                }
            }
        }

        private async void Discard_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private void Amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            //int app = Convert.ToInt32(Amount.Text);
            //if(app < 100 && app >= 10)
            //{
            //    Amount.Text = Amount.Text + ",";
            //}
            
        }
    }
}