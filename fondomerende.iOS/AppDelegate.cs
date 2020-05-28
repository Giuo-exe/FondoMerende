using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Plugin.CrossPlatformTintedImage.iOS;
using UIKit;
using Lottie.Forms.iOS.Renderers;
using Xamarin.Forms;

namespace fondomerende.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            Forms.Init();
            AnimationViewRenderer.Init();
            TintedImageRenderer.Init();
            FormsControls.Touch.Main.Init();
            LoadApplication(new App());
            IQKeyboardManager.IQKeyboardManager.SharedManager.Enable = true;

            return base.FinishedLaunching(app, options);
            
        }
    }
}
