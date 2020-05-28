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

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSnackViewCell : ViewCell
    {
        public AddSnackViewCell()
        {
            InitializeComponent();
            SetImageColorPreferences();
            var LoadTint = Color.FromHex(Preferences.Get("Colore", "#000000"));
            MessagingCenter.Subscribe<AddSnackViewCell>(this, "Refresh", async (value) =>
            {
                SetImageColorPreferences();
            });
        }

        public void SetImageColorPreferences()
        {
            AddIcon.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
        }
    }
}