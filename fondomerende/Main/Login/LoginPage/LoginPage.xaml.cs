using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using fondomerende.Main.Utilities;
using fondomerende.Main.Login.PostLogin;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View;
using fondomerende.Main.Login.PostLogin.AllSnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Settaggio.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.LogOut.View;
using fondomerende.Main.Login.TabletMode.Tabbed;
using fondomerende.Main.Login.TabletMode.Page;
using fondomerende.Main.Manager;

namespace fondomerende.Main.Login.LoginPages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class LoginPage : ContentPage
    {
        UserServiceManager userService = new UserServiceManager();
        LoginServiceManager loginService = new LoginServiceManager();
        private string username, password, testpassword, friendly_name;
        public bool remember = true;
        private bool wait = false;
        bool clicked = true;


        public LoginPage()
        {
            InitializeComponent();
            if(Services.Services.test)
            {
                usernameEntry.Text = "Giulio1234";
                passwordEntry.Text = "1234";
            }
            NavigationPage.SetHasNavigationBar(this, false);
            Donut_Background();
            LoginFade();
            
        }

        private void RememberMeButton_Clicked(object sender, EventArgs e) //Ricorda nome utente e pw (da fixare)
        {
            if (clicked == true)
            {
                clicked = false;
                remember = false;
                RememberMe_Button.BackgroundColor = Color.Transparent;   
            }
            else
            {
                clicked = true;
                remember = true;
                RememberMe_Button.BackgroundColor = Color.WhiteSmoke;
            }

        }

        private async void Login_ClickedAsync(object sender, EventArgs e) //Effettua il Log In
        {
            wait = !wait;
            if (usernameEntry.Text == "Giulio1234" && passwordEntry.Text == "1234")
            {
                App.Current.MainPage = new MainPage();
                
                MessagingCenter.Send(new EditUserViewCell()
                {

                }, "RefreshUF");

            }

            if (!string.IsNullOrEmpty(usernameEntry.Text) && !string.IsNullOrEmpty(passwordEntry.Text))
                {
                    Loading.IsVisible = true;
                    LoadingLottie.IsVisible = true;
                    LoadingLottie.Play();
                    var response = await loginService.LoginAsync(usernameEntry.Text, passwordEntry.Text, remember);
                    LoadingLottie.IsVisible = false;
                    Loading.IsVisible = false;

               

                if (response == null)
                    {
                        await DisplayAlert("Fondo Merende", "Errore di connessione", "OK");
                    }
                    else if (response.message == "Invalid login: wrong credentials.")
                    {
                        await DisplayAlert("Fondo Merende", "Username o Password Errati", "OK");
                    }
                    else if (response.success == true)
                    {
                        await userService.GetUserData();
                        App.Current.MainPage = new MainPage();
                        MessagingCenter.Send(new EditUserViewCell()
                        {

                        }, "RefreshUF");


                    wait = true;
                    }
                    else
                    {
                        await DisplayAlert("Fondo Merende", "Errore da investigare", "OK");
                    }
                }
               
                else
                {
                    await DisplayAlert("Fondo Merende", "Username o Password mancanti", "OK");

                }
        }

        private async void TabletMode_ClickedAsync(object sender, EventArgs e) //Effettua il Log In
        {
            wait = !wait;
            Loading.IsVisible = true;
            LoadingLottie.IsVisible = true;
            LoadingLottie.Play();
            var response = await loginService.LoginAsync("@kfc", "1", false);
            LoadingLottie.IsVisible = false;
            Loading.IsVisible = false;

            if (response == null)
            {
                await DisplayAlert("Fondo Merende", "Errore di connessione", "OK");
            }
            else if (response.message == "Invalid login: wrong credentials.")
            {
                await DisplayAlert("Fondo Merende", "Username o Password Errati", "OK");
            }
            else if (response.success == true)
            {
                await userService.GetUserData();
                App.Current.MainPage = new NavigationPage(new TabletPage());
                TabletManager.Instance.tablet = true;
                MessagingCenter.Send(new EditUserViewCell()
                {

                }, "RefreshUF");


                wait = true;
            }
            else
            {
                await DisplayAlert("Fondo Merende", "Errore da investigare", "OK");
            }
        }

        private async void RegisterButton_ClickedAsync(object sender, EventArgs e) //Mostra il form di registrazione
        {
            await LoginStack.FadeTo(0, 500);
            await LoginStack.TranslateTo(0, 1000, 1);
            await RegisterStack.TranslateTo(0, 0, 1);
            await RegisterStack.FadeTo(1, 500);
        }

   

        private async void Register_ClickedAsync(object sender, EventArgs e)
        {
            //SnackServiceManager snackService = new SnackServiceManager();
            //var a = await snackService.GetSnacksAsync();
            
            if (!string.IsNullOrEmpty(usernameEntryR.Text) && !string.IsNullOrEmpty(friendlyNameEntryR.Text) && !string.IsNullOrEmpty(passwordEntryR.Text) && !string.IsNullOrEmpty(testPasswordEntryR.Text))
            {
                bool nondeveaverespazi = true;
                string[] friend = friendlyNameEntryR.Text.Split();
                foreach(var a in friend)
                {
                    if(a == "")
                    {
                        nondeveaverespazi = false;
                        break;
                    }

                }
                
                if (nondeveaverespazi)
                {
                    password = passwordEntryR.Text;
                    testpassword = testPasswordEntryR.Text;
                    username = usernameEntryR.Text;
                    friendly_name = friendlyNameEntryR.Text;

                    if (password == testpassword)
                    {
                        RegisterServiceManager registerService = new RegisterServiceManager();
                        var response = await registerService.RegisterAsync(username, password, friendly_name);
                        if (response == null)
                        {

                        }
                        else if (response.success == true && response.status == 201)
                        {
                            await userService.GetUserData();
                            App.Current.MainPage = new MainPage();
                            MessagingCenter.Send(new EditUserViewCell()
                            {

                            }, "RefreshUF");
                        }
                        else
                        {
                            if (response.status == 400)
                            {
                                await DisplayAlert("Fondo Merende", response.message, "OK");
                            }
                            else
                            {
                                await DisplayAlert("Fondo Merende", "Registrazione fallita", "OK");
                            }
                        }
                    }
                    else
                    {
                        await DisplayAlert("Fondo Merende", "Le password non combaciano", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Fondo Merende", "Il friendly name non può contenere spazi", "OK");
                }
            }
            else
            {
                await DisplayAlert("Fondo Merende", "Compila tutti i campi", "OK");
            }
        }

        private async void Cancel_ClickedAsync(object sender, EventArgs e) //Mostra il form di registrazione
        {
            await RegisterStack.FadeTo(0, 500);
            await RegisterStack.TranslateTo(0, 1000, 1);
            await LoginStack.TranslateTo(0, 0, 1);
            await LoginStack.FadeTo(1, 500);
        }
        private async void Donut_Background()
        {
            await Donut.RotateTo(10, 0);
            await Donut.ScaleTo(1.5, 0);
            await Donut.TranslateTo(20, -800, 100000);
            await Donut.TranslateTo(-20, 0, 1);
            


        }
        private async void Ciambella()
        {
            await Fondo_Merende_logo.ScaleTo(0.2, 0);
            await Fondo_Merende_logo.TranslateTo(0, 0, 500);

            await Task.WhenAny<bool>
                (
                    Fondo_Merende_logo.ScaleTo(0.8, 500),
                    Fondo_Merende_logo.RotateTo(360, 500)
                );

            await Fondo_Merende_logo.ScaleTo(1.2, 150);
            await Fondo_Merende_logo.ScaleTo(1.0, 250);
            await Fondo_Merende_logo.TranslateTo(0, 0, 1500);

        }


        private async void LoginFade()
        {
            await RegisterStack.FadeTo(0, 1);
            await RegisterStack.TranslateTo(0, 1000, 1);
        }

    }

   
}

