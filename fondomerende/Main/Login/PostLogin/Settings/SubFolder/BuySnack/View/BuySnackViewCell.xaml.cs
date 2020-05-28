using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuySnackViewCell : ViewCell
    {
        public BuySnackViewCell()
        {
            InitializeComponent();
            SetImageColorPreferences();
            MessagingCenter.Subscribe<BuySnackViewCell>(this, "Refresh", async (value) =>
            {
                SetImageColorPreferences();
            });
        }

        public void SetImageColorPreferences()
        {
            BuyIcon.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
        }
    }
}