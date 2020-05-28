using fondomerende.Main.Manager;
using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using fondomerende.Main.Utilities;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.LogOut.View;
using fondomerende.Main.Login.PostLogin.AllSnack.Page;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : TabbedPage
    {
        
        public string GetSnackName;
        public MainPage()
        {
            Preferences.Set("username", "Giulio");
            InitializeComponent();
            SnacksNavPage.IconImageSource = ImageSource.FromResource("fondomerende.image.vm_icon_64x64.png");
            SettingsNavPage.IconImageSource = ImageSource.FromResource("fondomerende.image.settings_icon_64x64.png");
            DepositNavPage.IconImageSource = ImageSource.FromResource("fondomerende.image.Deposit_icon_64x64.png");
            
        }
    }
}

