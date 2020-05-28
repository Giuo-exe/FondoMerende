using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserInfoViewCell : ViewCell
    {
        public EditUserInfoViewCell()
        {

            InitializeComponent();
            SetImageColorPreferences();
            MessagingCenter.Subscribe<EditUserInfoViewCell>(this, "Refresh", async (value) =>
            {
                SetImageColorPreferences();
            });
        }

        public void SetImageColorPreferences()
        {
            SnackIcon.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
        }
    }
}