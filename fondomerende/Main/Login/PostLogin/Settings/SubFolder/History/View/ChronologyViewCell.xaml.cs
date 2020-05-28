using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.Content;
using FormsControls.Base;
using fondomerende.Main.Utilities;
using Plugin.CrossPlatformTintedImage.Abstractions;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class ChronologyViewCell : ViewCell
    {
        public ChronologyViewCell()
        {
            InitializeComponent();
            Tint();

            MessagingCenter.Subscribe<ChronologyViewCell>(this, "Refresh", async (value) =>
            {
                Tint();
            });
        }
        public void Tint()
        {
            switch (Device.RuntimePlatform)                                                    
            {                                                                                                                    
               case Device.Android:
                    ChronologyIcon.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
                    break;                                                                      
                                                                                                
                default:
                    ColorRandom r = new ColorRandom();
                    Color app = Color.FromHex(Preferences.Get("Colore", "#000000"));
                    //ChronologyIcon.TintColor = UIColor.FromRGB;
                    break;                                                                      
            }
            ChronologyIcon.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
        }

        public void InizializeImage()
        {
            EmbeddedImage e = new EmbeddedImage();
            e.Resource = "fondomerende.image.History.png";

            var tinted = new TintedImage
            {
                Scale = 0.5,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Aspect = Aspect.AspectFill,
                Source = e.Resource,
            };

            tinted.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
            Griglia.Children.Add(tinted,0,0);
        }
    }
}