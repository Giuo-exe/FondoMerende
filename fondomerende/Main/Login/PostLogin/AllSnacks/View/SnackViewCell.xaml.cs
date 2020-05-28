using Lottie.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Login.PostLogin.AllSnack.Page;
using System.Collections;
using System.Timers;

namespace fondomerende.Main.Login.PostLogin.AllSnacks.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SnackViewCell : ViewCell
    {
        bool AlmostDone;
        AnimationView pacMananimation;
        private static Timer aTimer;
        public static bool EnablePacman;
        
        public SnackViewCell()
        {
            InitializeComponent();
            
            MessagingCenter.Subscribe<SnackViewCell>(this, "Animate", async (arg) =>
            {
                if (EnablePacman)
                {
                    if (Nome.Text == AllSnacksPage.selectedItemBinding)
                    {
                        pacMananimation = new AnimationView
                        {
                            Animation = "pacman0.6.json",
                            Scale = 1.6,
                            Loop = true,
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            AutoPlay = true,
                            HardwareAcceleration = true,
                            InputTransparent = true,
                            IsVisible = false,
                            Speed = 4,
                        };

                        Grid.Children.Add(pacMananimation);
                        pacManAnimate();
                    }
                }
                else
                {
                    await QtaRefresh();
                }
            });
        }

        private async void pacManAnimate()
        {
            double pacmanwidth = pacMananimation.Width;
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo.Width;
            pacMananimation.IsVisible = true;
            await pacMananimation.TranslateTo(-mainDisplayInfo/2, 0, 0);
            await Task.WhenAny<bool>
            (
             pacMananimation.TranslateTo(((mainDisplayInfo*34)/100) , 0, 5000,Easing.Linear)
            );
            QtaRefresh();
            MessagingCenter.Send(new AllSnacksPage()
            {

            }, "RefreshGetSnacks");
            await pacMananimation.TranslateTo(((mainDisplayInfo * 66) / 100), 0, 2500);
            
        }
        async Task QtaRefresh()
        {
          qta.Text = Convert.ToString(Int32.Parse(qta.Text) - 1);
        }
        
    }
}
