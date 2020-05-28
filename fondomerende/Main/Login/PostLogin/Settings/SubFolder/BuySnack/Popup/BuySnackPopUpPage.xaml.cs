using fondomerende.Main.Login.PostLogin.AllSnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View;
using fondomerende.Main.Login.TabletMode.Popup;
using fondomerende.Main.Manager;
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


namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Popup


{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuySnackPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        bool OneClick = false;
        ImageButton immagine;
        LineEntry lineAndroid, lineiOs;
        double Altezza, Larghezza;
        RoundedCornerView Round;
        StackLayout stackBody;
        LineEntry prezzoAndroid, prezzoiOs;
        LineEntry scadenzaAndroid, scadenzaiOs;
        Button buttonCancel, buttonConfirm;
        bool isDone, swap=false;
        string appoggio;
        
        SnackServiceManager snackService = new SnackServiceManager();
        public BuySnackPopUpPage()
        {
            InitializeComponent();
            PopupBuy();
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
        private void PopupBuy()
        {
            Altezza = (GetAltezzaPagina() * 30) / 100;
            Larghezza = GetLarghezzaPagina() - 80;
            double banner = 50;

            Round = new RoundedCornerView  //coso che stonda
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
            lineAndroid = new LineEntry
            {
                Placeholder = "Quanti snack vuoi acquistare?",
                WidthRequest = 250,
                Keyboard = Keyboard.Numeric,
                Margin = new Thickness(0, 5, 0, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                
            };

            prezzoAndroid = new LineEntry
            {
                Placeholder = "Inserire il prezzo",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 250,
                IsVisible = false,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(0, 5, 0, 0),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            scadenzaAndroid = new LineEntry
            {
                Placeholder = "Inserire la scadenza",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 250,
                Margin = new Thickness(0, 5, 0, 0),
                IsVisible = false,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };



            lineiOs = new LineEntry
            {
                Placeholder = "Quanti snack vuoi acquistare?",
                WidthRequest = 250,
                FontSize = 18,
                HeightRequest = 35,
                Margin = new Thickness(0,10,0,0),
                Keyboard = Keyboard.Numeric,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,

            };

            prezzoiOs = new LineEntry
            {
                Placeholder = "Inserire il prezzo",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 250,
                FontSize = 18,
                HeightRequest = 35,
                VerticalOptions = LayoutOptions.StartAndExpand,
                IsVisible = false,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            scadenzaiOs = new LineEntry
            {
                Placeholder = "Inserire la scadenza",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 250,
                FontSize = 18,
                HeightRequest = 35,
                VerticalOptions = LayoutOptions.StartAndExpand,
                IsVisible = false,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };


            


            stackBody = new StackLayout  //stack principale dove è contenuto l'interno di tutto (tranne round che stonda)

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


            immagine = new ImageButton
            {
                Source = ImageSource.FromResource("fondomerende.image.Edit_Icon_32x32.png"),
                CornerRadius = 20,
                Scale = 1.5,
                BackgroundColor = Color.Transparent,
                Margin = new Thickness (0,0,15,0),
                Aspect = Aspect.AspectFit,
            };

            

            buttonCancel = new Button
            {
                Text = "Annulla",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Transparent,
            };

             buttonConfirm = new Button
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
                    stackFondoAndroid.Children.Add(immagine);
                    stackBody.Children.Add(stackFondoAndroid);
                    immagine.Clicked += Immagine_Clicked;
                    prezzoAndroid.TextChanged += EntrataPrezzoAndroid;
                    buttonCancel.Clicked += Discard_Clicked;
                    buttonConfirm.Clicked += Apply_ClickedAndroid;
                    stackBody.Children.Add(lineAndroid);
                    stackBody.Children.Add(prezzoAndroid);
                    stackBody.Children.Add(scadenzaAndroid);


                    stackBody.Children.Add(stackBottoni);
                    Round.Children.Add(stackBody);
                    break;


                case Device.iOS:
                    stackFondoiOS.Children.Add(fondomerende);
                    stackFondoiOS.Children.Add(immagine);
                    stackBody.Children.Add(stackFondoiOS);
                    immagine.Clicked += Immagine_Clicked;
                    prezzoAndroid.TextChanged += EntrataPrezzoiOs;
                    buttonCancel.Clicked += Discard_Clicked;
                    buttonConfirm.Clicked += Apply_ClickediOs;
                    stackBody.Children.Add(lineiOs);
                    stackBody.Children.Add(prezzoiOs);
                    stackBody.Children.Add(scadenzaiOs);


                    stackBody.Children.Add(stackBottoni);
                    Round.Children.Add(stackBody);

                    break;
            }
          //  entry.TextChanged += Entrata;


            
            

            PopupBuySnack.Content = Round;
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
            MessagingCenter.Send(new BuySnackListPage()
            {

            }, "Close");
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
            base.OnBackButtonPressed();
            return base.OnBackButtonPressed();
        }
        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
 
            return base.OnBackgroundClicked();
        }


        private async void Immagine_Clicked(object sender, EventArgs e)
        {
            swap = true;
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    Altezza = (GetAltezzaPagina() * 50) / 100;
                    Round.HeightRequest = Altezza;
                    stackBody.Spacing = 20;
                    immagine.IsVisible = false;
                    prezzoAndroid.IsVisible = true;
                    scadenzaAndroid.IsVisible = true;
                    break;
                case Device.iOS:
                    Altezza = (GetAltezzaPagina() * 50) / 100;
                    Round.HeightRequest = Altezza;
                    stackBody.Spacing = 20;
                    immagine.IsVisible = false;
                    prezzoiOs.IsVisible = true;
                    scadenzaiOs.IsVisible = true;
                    break;
            }
        }
        


        public void EntrataPrezzoAndroid(object sender, TextChangedEventArgs e)
        {
            if (prezzoAndroid.CursorPosition == 1 && isDone && prezzoAndroid.Text != "")
            {
                if (prezzoAndroid.Text.Substring(1, 1) == ",")
                {
                    prezzoAndroid.MaxLength = 4;
                    isDone = false;
                }
                else
                {
                    prezzoAndroid.MaxLength = 5;
                    prezzoAndroid.Text = prezzoAndroid.Text + ",";
                    isDone = false;
                }
            }

            else
            {
                isDone = true;
            }
        }
        public void EntrataPrezzoiOs(object sender, TextChangedEventArgs e)
        {
            if (prezzoiOs.CursorPosition == 1 && isDone && prezzoiOs.Text != "")
            {
                if (prezzoiOs.Text.Substring(1, 1) == ",")
                {
                    prezzoiOs.MaxLength = 4;
                    isDone = false;
                }
                else
                {
                    prezzoiOs.MaxLength = 5;
                    prezzoiOs.Text = prezzoiOs.Text + ",";
                    isDone = false;
                }
            }

            else
            {
                isDone = true;
            }
        }



        private async void Apply_ClickedAndroid(object sender, EventArgs e)
        { 
            SnackServiceManager snackService = new SnackServiceManager();
            if (swap == false)
            {
                if (lineAndroid.Text == null || lineAndroid.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci la quantità", "OK");
                }
                else
                {
                   
                    if(lineAndroid.Text.Contains(","))
                    {
                        int aiuto = lineAndroid.Text.IndexOf(",");
                        lineAndroid.Text = lineAndroid.Text.Substring(0, aiuto);
                    }
                    if (TabletManager.Instance.tablet)
                    {
                        await Navigation.PushPopupAsync(new CodicePopup((BuySnackListPage.SelectedSnackID), "compra", Convert.ToInt32(lineAndroid.Text),swap));
                    }
                    else
                    {
                        var result = await snackService.BuySnackAsync(BuySnackListPage.SelectedSnackID, Convert.ToInt32(lineAndroid.Text));
                        if (result != null)
                        {
                            if (result.success)
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
                                await DisplayAlert("Fondo Merende", result.message, "Ok");
                            }
                        }
                    } 
                }
            }
            if(swap == true )
            {
                if (lineAndroid.Text == null || lineAndroid.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci la quantità", "OK");
                }
                if(prezzoAndroid.Text == null || prezzoAndroid.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci il prezzo", "OK");
                }
                if(scadenzaAndroid.Text == null ||  scadenzaAndroid.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci i giorni di scadenza", "OK");
                }
                else
                {
                    if (lineAndroid.Text.Contains(","))
                    {
                        int aiuto = lineAndroid.Text.IndexOf(",");
                        lineAndroid.Text = lineAndroid.Text.Substring(0, aiuto);
                    }
                    if (TabletManager.Instance.tablet)
                    {
                        await Navigation.PushPopupAsync(new CodicePopup(BuySnackListPage.SelectedSnackID, "compra", Int32.Parse(lineAndroid.Text), prezzoAndroid.Text, scadenzaAndroid.Text, true));
                    }
                    else
                    {
                        var result = await snackService.BuySnackAsync2(BuySnackListPage.SelectedSnackID, Int32.Parse(lineAndroid.Text), prezzoAndroid.Text, scadenzaAndroid.Text);
                        if (result != null)
                        {
                            if (result.success)
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
                                await PopupNavigation.Instance.PopAsync();
                            }
                            else
                            {
                                await DisplayAlert("Fondo Merende", result.message, "Ok");
                            }
                        }
                    }
                    
                }

            }
           
        }


        private async void Apply_ClickediOs(object sender, EventArgs e)
        {
            try
            {
                SnackServiceManager snackService = new SnackServiceManager();
                if (swap == false)
                {
                    if (lineiOs.Text.Contains(","))
                    {
                        int aiuto = lineiOs.Text.IndexOf(",");
                        lineiOs.Text = lineiOs.Text.Substring(0, aiuto);
                    }
                    
                    else
                    {
                        if (TabletManager.Instance.tablet)
                        {
                            await Navigation.PushPopupAsync(new CodicePopup((BuySnackListPage.SelectedSnackID), "compra", Convert.ToInt32(lineiOs.Text),null,null, swap));
                        }
                        else
                        {
                            var result = await snackService.BuySnackAsync(BuySnackListPage.SelectedSnackID, Convert.ToInt32(lineiOs.Text));
                            if (result != null)
                            {
                                if (result.success)
                                {
                                    MessagingCenter.Send(new AllSnacksPage()
                                    {

                                    }, "RefreshGetSnacks");
                                    DependencyService.Get<HapticFeedbackGen>().HapticFeedbackGenSuccessAsync();
                                    MessagingCenter.Send(new AllSnacksPage()
                                    {

                                    }, "RefreshGriglia");
                                    await PopupNavigation.Instance.PopAsync();
                                }
                                else
                                {
                                    await DisplayAlert("Fondo Merende", result.message, "Ok");
                                }
                            }
                        }
                    }
                }
                if (swap == true)
                {
                    
                    if (lineiOs.Text.Contains(","))
                    {
                        int aiuto = lineiOs.Text.IndexOf(",");
                        lineiOs.Text = lineiOs.Text.Substring(0, aiuto);
                    }
                    else
                    {
                        if (TabletManager.Instance.tablet)
                        {
                            await Navigation.PushPopupAsync(new CodicePopup(BuySnackListPage.SelectedSnackID, "compra", Int32.Parse(lineiOs.Text), prezzoiOs.Text, scadenzaiOs.Text, true));
                        }
                        else
                        {
                            var result = await snackService.BuySnackAsync2(BuySnackListPage.SelectedSnackID, Int32.Parse(lineiOs.Text), prezzoiOs.Text, scadenzaiOs.Text);
                            if (result != null)
                            {
                                if (result.success)
                                {
                                    MessagingCenter.Send(new AllSnacksPage()
                                    {

                                    }, "RefreshGetSnacks");
                                    DependencyService.Get<HapticFeedbackGen>().HapticFeedbackGenSuccessAsync();
                                    MessagingCenter.Send(new AllSnacksPage()
                                    {

                                    }, "RefreshGriglia");
                                    await PopupNavigation.Instance.PopAsync();
                                }
                                else
                                {
                                    await DisplayAlert("Fondo Merende", result.message, "Ok");
                                }
                            }
                        }
                        
                    }

                }
            }catch(Exception ex)
            {
                await DisplayAlert("Fondo Merende", "Riempire tutti i campi", "Ok");
            }

        }

        private async void Discard_Clicked(object sender, EventArgs e)
        {
            Discard.IsEnabled = false;
            await Navigation.PopAllPopupAsync();
        }
    }
}