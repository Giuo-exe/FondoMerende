using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.Settaggio.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeColorViewCell : ViewCell
    {
        public ChangeColorViewCell()
        {
            InitializeComponent();
            SetImageColorPreferences();
            MessagingCenter.Subscribe<ChangeColorViewCell>(this, "Refresh", async (value) =>
            {
                SetImageColorPreferences();
            });
        }
        public void SetImageColorPreferences()
        {
            SettingIcon.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
        }
    }
}