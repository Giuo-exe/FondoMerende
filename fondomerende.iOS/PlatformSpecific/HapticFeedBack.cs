using System;
using System.Threading.Tasks;
using fondomerende.iOS.PlatformSpecific;
using fondomerende.Main.Utilities;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(HapticFeedBack))]
namespace fondomerende.iOS.PlatformSpecific
{
    public class HapticFeedBack : HapticFeedbackGen
    {
        public HapticFeedBack()
        {
        }

        public void HapticFeedbackGenSuccessAsync()
        {
            var feedback = new UINotificationFeedbackGenerator();
            feedback.Prepare();
            switch ((UIDevice.CurrentDevice.ValueForKey(new Foundation.NSString("_feedbackSupportLevel")) as Foundation.NSNumber).Int16Value)
            {
                case 1:
                    using (var feedbackLow = new UISelectionFeedbackGenerator())
                    {
                        feedbackLow.SelectionChanged();
                    }
                    break;
                case 2:
                    {
                        feedback.NotificationOccurred(UINotificationFeedbackType.Success);
                    }
                    break;
                default:
                    Vibration.Vibrate(40);
                    Task.Delay(20);
                    Vibration.Vibrate(40);
                    break;
            }
        }
         public void HapticFeedbackGenErrorAsync()
            {
                var feedback = new UINotificationFeedbackGenerator();
                feedback.Prepare();
                switch ((UIDevice.CurrentDevice.ValueForKey(new Foundation.NSString("_feedbackSupportLevel")) as Foundation.NSNumber).Int16Value)
                {
                    case 1:
                        using (var feedbackLow = new UISelectionFeedbackGenerator())
                        {
                            feedbackLow.SelectionChanged();
                        }
                        break;
                    case 2:
                        {
                            feedback.NotificationOccurred(UINotificationFeedbackType.Error);
                        }
                        break;

                    default:
                        Vibration.Vibrate(40);
                        Task.Delay(20);
                        Vibration.Vibrate(40);
                        break;
                }
        }
    }
}
