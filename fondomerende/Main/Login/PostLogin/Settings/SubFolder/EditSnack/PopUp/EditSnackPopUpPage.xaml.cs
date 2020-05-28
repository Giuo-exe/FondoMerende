using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.Page;
using fondomerende.Main.Services.RESTServices;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fondomerende.Main.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Login.PostLogin.AllSnack.Page;
using Xamarin.Essentials;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSnackPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        bool IsDone = false;
        ImageButton immagine;
        LineEntry NomeSnack;
        LineEntry PrezzoSnack;
        LineEntry SnackPerBox;
        LineEntry ExpInDays;
        LineEntry Qta;
        EmbeddedImage img = new EmbeddedImage();
        SnackServiceManager snackService = new SnackServiceManager();
        int snackID = EditSnackListPage.SelectedSnackID;
        string appoggioNome, appoggioQta, appoggioSnackPerScatola, appoggioScadenzaInGiorni;
        bool Swapped = true;
        Decimal appoggioPrezzo;
        int Quantity;
        public EditSnackPopUpPage()
        {
            InitializeComponent();
            PopupEditSnack();
            Qta.Placeholder = "Quantità";
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
        private void PopupEditSnack()
        {
            double Altezza = 0;
            switch (Device.RuntimePlatform)
            {

                case Device.Android:
                    Altezza = (GetAltezzaPagina() * 70) / 100;
                    break;
                case Device.iOS:
                    Altezza = (GetAltezzaPagina() * 60) / 100;
                    break;
            }
            
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

            immagine = new ImageButton
            {
                Source = ImageSource.FromResource("fondomerende.image.fill_full_256x256.png"),
                CornerRadius = 20,
                HeightRequest = 60,
                BackgroundColor = Color.White,
                Scale = 0.6,
                Margin = new Thickness(0,0,0,400),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
            };

            immagine.Clicked += Swap_Clicked;
            NomeSnack = new LineEntry
            {
                Keyboard = Keyboard.Default,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                WidthRequest = 250,
            };
            PrezzoSnack = new LineEntry
            {
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                WidthRequest = 250,
            };
            SnackPerBox = new LineEntry
            {
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                WidthRequest = 250,
            };
            ExpInDays = new LineEntry
            {
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                WidthRequest = 250,
            };
            Qta = new LineEntry
            {
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                WidthRequest = 250,
            };



            var stackBody = new StackLayout  //stack principale dove è contenuto l'interno di tutto (tranne round che stonda)

            {
                Spacing = 10,
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
            stackImgBtn.Children.Add(immagine);
            stackBody.Children.Add(stackImgBtn);
            stackBody.Children.Add(NomeSnack);
            stackBody.Children.Add(PrezzoSnack);
            stackBody.Children.Add(SnackPerBox);
            stackBody.Children.Add(ExpInDays);
            stackBody.Children.Add(Qta);
            NomeSnack.Placeholder = "Nome: " + EditSnackListPage.SelectedSnackName;
            PrezzoSnack.Placeholder = "Prezzo: " + Convert.ToString(EditSnackListPage.SelectedSnackPrice);
            SnackPerBox.Placeholder = "Snacks Per Scatola: " + Convert.ToString(EditSnackListPage.SelectedSnackPerBox);
            ExpInDays.Placeholder = "Scadenza In Giorni: " + Convert.ToString(EditSnackListPage.SelectedSnackExpiration);
            stackBody.Children.Add(stackBottoni);
            Round.Children.Add(stackBody);

            EditSnackPopUp.Content = Round;
        }

        public void EntrataNome(object sender, TextChangedEventArgs e)
        {
            appoggioNome = e.NewTextValue;

        }

        public void EntrataPrezzo(object sender, TextChangedEventArgs e)
        {
            if (PrezzoSnack.CursorPosition == 2) IsDone = false;
            if (PrezzoSnack.CursorPosition == 1 && IsDone == true && PrezzoSnack.Text != "")
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
            
        }

        public void EntrataScadenzaInGiorni(object sender, TextChangedEventArgs e)
        {
            appoggioScadenzaInGiorni = e.NewTextValue;

        }

        public void EntrataSnackPerScatola(object sender, TextChangedEventArgs e)
        {
            appoggioSnackPerScatola = e.NewTextValue;

        }

        public void EntrataQta(object sender, TextChangedEventArgs e)
        {
            appoggioQta = e.NewTextValue;

        }
        


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Send(new EditSnackListPage()
            {

            }, "Close");
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
            if (appoggioNome == null || PrezzoSnack.Text == null || appoggioSnackPerScatola == null || appoggioScadenzaInGiorni == null || Qta.Text == null)
            {
                await DisplayAlert("Fondo Merende", "Riempi tutti i campi", "Ok");
            }
            else
            {
                if (Convert.ToDouble(PrezzoSnack.Text) != 0 || float.Parse(appoggioSnackPerScatola) != 0)
                {
                    var res = await snackService.EditSnackAsync(snackID, appoggioNome, PrezzoSnack.Text, appoggioSnackPerScatola, appoggioScadenzaInGiorni,Int32.Parse(Qta.Text));
                    if (res != null)
                    {
                        if (res.success)
                        {
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
                            MessagingCenter.Send(new AllSnacksPage()
                            {

                            }, "RefreshGetSnacks");
                            MessagingCenter.Send(new EditSnackListPage()
                            {

                            }, "Close");
                            MessagingCenter.Send(new AllSnacksPage()
                            {

                            }, "RefreshGriglia");
                            await Navigation.PopPopupAsync();
                        }
                        else
                        {
                            await DisplayAlert("Fondo Merende", res.message, "Ok");
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Fondo Merende", "Il Prezzo e 'gli Snacks per scatola' devono essere maggiori di 0", "Ok");
                }
            }
           
        }

        private async void Discard_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(new EditSnackListPage()
            {

            }, "Close");
            await Navigation.PopPopupAsync();
        }

        private async void Swap_Clicked(object sender, EventArgs e)
        {
            if (Swapped == true)
            {
                
                immagine.Source = ImageSource.FromResource("fondomerende.image.fill_clear_256x256.png");
                NomeSnack.Placeholder = null;
                PrezzoSnack.Placeholder = null;
                SnackPerBox.Placeholder = null;
                ExpInDays.Placeholder = null;
                if(NomeSnack.Text == null)
                {
                    NomeSnack.Text = EditSnackListPage.SelectedSnackName;
                }
                if(PrezzoSnack.Text == null)
                {
                    PrezzoSnack.Text = Convert.ToString(EditSnackListPage.SelectedSnackPrice);
                }
                if(SnackPerBox.Text == null)
                {
                    SnackPerBox.Text = Convert.ToString(EditSnackListPage.SelectedSnackPerBox);
                }
                if(ExpInDays.Text == null)
                {
                  ExpInDays.Text = Convert.ToString(EditSnackListPage.SelectedSnackExpiration);
                }
                if(Qta.Text == null)
                {
                    Qta.Text = Convert.ToString(Quantity); 
                }
                Swapped = false;
            }
            else
            {
                immagine.Source = ImageSource.FromResource("fondomerende.image.fill_full_256x256.png");
                NomeSnack.Text = null;
                PrezzoSnack.Text = null;
                SnackPerBox.Text = null;
                ExpInDays.Text = null;
                Qta.Text = null;
                NomeSnack.Placeholder = "Nome: " + EditSnackListPage.SelectedSnackName;
                PrezzoSnack.Placeholder = "Prezzo: " + Convert.ToString(EditSnackListPage.SelectedSnackPrice);
                SnackPerBox.Placeholder = "Snacks Per Scatola: " + Convert.ToString(EditSnackListPage.SelectedSnackPerBox);
                ExpInDays.Placeholder = "Scadenza In Giorni: " + Convert.ToString(EditSnackListPage.SelectedSnackExpiration);
                Qta.Placeholder = "Quantità: " + Convert.ToString(Quantity);
                Swapped = true;
            }
        }


    }
}