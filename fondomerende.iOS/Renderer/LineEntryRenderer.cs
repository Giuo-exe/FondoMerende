using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using System.ComponentModel;
using CoreAnimation;
using Foundation;
using moosaicox.iOS.Utils;
using CoreGraphics;
using System;
using ObjCRuntime;
using fondomerende;
using fondomerende.Main.Utilities;

[assembly: ExportRenderer(typeof(fondomerende.Main.Utilities.LineEntry), typeof(LineEntryRenderer))]
namespace moosaicox.iOS.Utils
{
    public class LineEntryRenderer : EntryRenderer
    {
        private CALayer _borderLayer;

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            if (Control != null)
            {
                var nativeTextField = (UITextField)Control;
                nativeTextField.EditingDidEnd -= NativeTextField_EditingDidEnd;
                nativeTextField.EditingDidBegin -= NativeTextField_EditingDidBegin;

                nativeTextField.EditingDidBegin += NativeTextField_EditingDidBegin;
                nativeTextField.EditingDidEnd += NativeTextField_EditingDidEnd;
            }
        }

        public override bool CanPerform(Selector action, NSObject withSender)
        {
            var element = Element as LineEntry;
            if (element == null)
                return false;
            if (element.SetTappable)
            {
                return base.CanPerform(action, withSender);
            }
            else
            {
                return false;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
                return;

            Control.BorderStyle = UITextBorderStyle.None;

            var element = Element as LineEntry;
            if (element == null)
                return;

            if (element.SetError)
            {
                DrawBorder(Color.Red.ToCGColor());
            }
            else
            {
                DrawBorder(element.BorderColor.ToCGColor());
            }
            if (!element.SetTappable)
            {
                Control.InputView = new UIView(CGRect.Empty);
                Control.InputAssistantItem.LeadingBarButtonGroups = new UIBarButtonItemGroup[0];
                Control.InputAssistantItem.TrailingBarButtonGroups = new UIBarButtonItemGroup[0];
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var element = Element as LineEntry;
            if (element == null)
                return;

            if (e.PropertyName == nameof(LineEntry.BorderColor))
            {
                DrawBorder(element.BorderColor.ToCGColor());
            }
            if (e.PropertyName == nameof(LineEntry.SetError))
            {
                DrawBorder(Color.Red.ToCGColor());
            }
            if (e.PropertyName == nameof(LineEntry.SetTappable))
            {
                if (!element.SetTappable)
                {
                    Control.InputView = new UIView(CGRect.Empty);
                    Control.InputAssistantItem.LeadingBarButtonGroups = new UIBarButtonItemGroup[0];
                    Control.InputAssistantItem.TrailingBarButtonGroups = new UIBarButtonItemGroup[0];
                }
            }
        }

        void NativeTextField_EditingDidBegin(object sender, EventArgs e)
        {
            var element = Element as LineEntry;
            if (element == null)
                return;
            if (element.SetTappable)
            {
                DrawBorderSelected();
            }
        }

        void NativeTextField_EditingDidEnd(object sender, EventArgs e)
        {
            var element = Element as LineEntry;
            if (element == null)
                return;
            element.SetError = false;
            DrawBorder(element.BorderColor.ToCGColor());
        }


        public override CGRect Frame
        {
            get { return base.Frame; }
            set
            {
                base.Frame = value;

                var element = Element as LineEntry;
                if (element == null)
                    return;

                DrawBorder(element.BorderColor.ToCGColor());
            }
        }

        private void DrawBorder(CGColor borderColor)
        {
            if (Control != null)
            {
                if (Frame.Height <= 0 || Frame.Width <= 0)
                    return;

                if (_borderLayer == null)
                {
                    _borderLayer = new CALayer
                    {
                        MasksToBounds = true,
                        Frame = new CGRect(0f, Frame.Height - 1, Frame.Width, 1f),
                        BorderColor = borderColor,
                        BorderWidth = 1.0f
                    };

                    Control.Layer.AddSublayer(_borderLayer);
                    Control.BorderStyle = UITextBorderStyle.None;
                }
                else
                {
                    _borderLayer.BorderColor = borderColor;
                    _borderLayer.Frame = new CGRect(0f, Frame.Height - 1, Frame.Width, 1f);
                }
            }
        }

        private void DrawBorderSelected()
        {
            if (Control != null)
            {
                if (Frame.Height <= 0 || Frame.Width <= 0)
                    return;

                if (_borderLayer == null)
                {
                    _borderLayer = new CALayer
                    {
                        MasksToBounds = true,
                        Frame = new CGRect(0f, Frame.Height - 1, Frame.Width, 1f),
                        BorderColor = Color.FromHex("#65C3FF").ToCGColor(),
                        BorderWidth = 1.5f
                    };

                    Control.Layer.AddSublayer(_borderLayer);
                    Control.BorderStyle = UITextBorderStyle.None;
                }
                else
                {
                    _borderLayer.BorderColor = Color.FromHex("#65C3FF").ToCGColor();
                    _borderLayer.Frame = new CGRect(0f, Frame.Height - 1, Frame.Width, 1.5f);
                }
            }
        }
    }
}
