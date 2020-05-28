using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuySnackListViewCell : ViewCell
    {
        SnackServiceManager snackService = new SnackServiceManager();
        public BuySnackListViewCell()
        {
            InitializeComponent();

        }

    }
}