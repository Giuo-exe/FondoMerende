using fondomerende.Main.Services.RESTServices;
using fondomerende.Main.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder;
using fondomerende.Main.Login.PostLogin.Settings.Page;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.Settaggio.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeColorPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        string appoggio;
        /*private double diametro = 40;
        private double larghezzaLinea = 3;
        private double altezzaLinea = 20;

        private double diametroMod;

        Dictionary<string, Color> colorByName = new Dictionary<string, Color>();
        Dictionary<string, double> sizeByName = new Dictionary<string, double>();
        Dictionary<string, string> dateByTime = new Dictionary<string, string>();
        Dictionary<string, string> mangione = new Dictionary<string, string>();
        Dictionary<Color, bool> colorReserved = new Dictionary<Color, bool>();
        Dictionary<string, string> traduttore = new Dictionary<string, string>();*/


        public ChangeColorPopUp()
        {
            InitializeComponent();
            PopupChangeColor();

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
        private void PopupChangeColor()
        {
            double Altezza = GetAltezzaPagina() / 1.2;
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



            /*for (int i = 2; i < strSplit.Length; i++)
            {

                if (i == 2)
                {
                    fs.Spans.Add(new Span { Text = strSplit[2], TextColor = colorByName[strSplit[2]] });
                }

                else if (traduttore.ContainsKey(strSplit[i]))
                {
                    fs.Spans.Add(new Span { Text = " " + traduttore[strSplit[i]], TextColor = Color.Black });
                }
                else
                {
                    fs.Spans.Add(new Span { Text = " " + strSplit[i], TextColor = Color.Black });
                }
            }

            var cerchio = new RoundedCornerView
            {
                HeightRequest = diametro + ((diametro  * 2),
                WidthRequest = diametro + ((diametro *  2),
                MinimumHeightRequest = diametro + ((diametro *  2),
                MinimumWidthRequest = diametro + ((diametro *  2),
                RoundedCornerRadius = diametro + ((diametro *  2),
                Margin = new Thickness(3, 0, 0, 0),
                BorderColor = Color.Black,
                BorderWidth = 3,
            };

            var cerchioiOS = new RoundedCornerView
            {
                HeightRequest = diametro + ((diametro *  2),
                WidthRequest = diametro + ((diametro *  2),
                MinimumHeightRequest = diametro + ((diametro *  2),
                MinimumWidthRequest = diametro + ((diametro * 2),
                RoundedCornerRadius = (diametro + ((diametro *  2)) / 2,
                Margin = new Thickness(3, 0, 0, 0),
                BackgroundColor = colorByName,
                BorderColor = Color.Black,
                BorderWidth = 1,
            };*/



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


            buttonCancel.Clicked += Discard_Clicked;
            buttonConfirm.Clicked += Apply_Clicked;
            stackBody.Children.Add(stackBottoni);
            Round.Children.Add(stackBody);

            ChangeColorPopUpPage.Content = Round;
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
            MessagingCenter.Send(new ChangeColorPopUp()
            {

            }, "Refresh");
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
            

        }

        private async void Discard_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}