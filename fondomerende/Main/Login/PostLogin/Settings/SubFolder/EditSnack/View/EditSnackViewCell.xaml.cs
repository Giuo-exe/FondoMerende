using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSnackViewCell : ViewCell
    {
        public EditSnackViewCell()
        {
            InitializeComponent();
            SetImageColorPreferences();
            MessagingCenter.Subscribe<EditSnackViewCell>(this, "Refresh", async (value) =>
            {
                SetImageColorPreferences();
            });
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            
        }

        public void SetImageColorPreferences()
        {
            SnackIcon.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
        }
    }
}