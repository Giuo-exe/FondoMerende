using Flurl.Http;
using fondomerende.Main.Login.LoginPages;
using fondomerende.Main.Manager;
using fondomerende.Main.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace fondomerende.Main.Services.RESTServices
{
    class RegisterServiceManager
    {
        public async System.Threading.Tasks.Task<RegisterDTO> RegisterAsync(string username, string passwordToLogin, string friendly_name) //Servizio di Registrazione
        {
            var data = new Dictionary<string, string>();
            data.Add("command-name", "add-user");
            data.Add("name", username);
            data.Add("friendly-name", friendly_name);
            data.Add("password", passwordToLogin);
            try
            {
                var result = await Services.Concatenazione()
               .WithCookie("auth-key", Services.GetAuthKey())
               .WithHeader("Content-Type", "application/x-www-form-urlencoded; param=value;charset=UTF-8")
               .PostUrlEncodedAsync(data)
               .ReceiveJson<RegisterDTO>();

                if (result.success = true && result.status == 201)
                {
                    App.Current.MainPage = new LoginPage();
                    UserManager.Instance.token = result.data.token;
                    Preferences.Set("username", username);
                    Preferences.Set("password", passwordToLogin);
                    Preferences.Set("friendly-name", friendly_name);
                }

                return result;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Connessione al server scaduta", "OK");
            }
            catch (FlurlHttpException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", ex.InnerException.Message, "OK");
            }
            return null;

        }
    }
}
