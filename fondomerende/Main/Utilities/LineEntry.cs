using Xamarin.Forms;

namespace fondomerende.Main.Utilities
{
    public class LineEntry : Entry
    {
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create<LineEntry, Color>(p => p.BorderColor, Color.Black);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create<LineEntry, Color>(p => p.PlaceholderColor, Color.Default);

        public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }

        public static readonly BindableProperty TappebleProperty = BindableProperty.Create<LineEntry, bool>(p => p.SetTappable, true);

        public bool SetTappable
        {
            get { return (bool)GetValue(TappebleProperty); }
            set { SetValue(TappebleProperty, value); }
        }

        public static readonly BindableProperty ErrorProperty = BindableProperty.Create<LineEntry, bool>(p => p.SetError, false);

        public bool SetError
        {
            get { return (bool)GetValue(ErrorProperty); }
            set { SetValue(ErrorProperty, value); }
        }

    }
}
