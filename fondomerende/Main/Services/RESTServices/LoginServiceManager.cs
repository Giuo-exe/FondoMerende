using Flurl.Http;
using Flurl;
using fondomerende.Main.Manager;
using fondomerende.Main.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;
using fondomerende.Main.Login.LoginPages;
using Xamarin.Forms.Internals;

namespace fondomerende.Main.Services.RESTServices
{
    class LoginServiceManager
    {
        [Preserve(AllMembers = true)]
        public async System.Threading.Tasks.Task<LoginDTO> LoginAsync(string username, string passwordToLogin, bool remember) //Servizio di Log In
        {
            string key = Services.GetAuthKey();
            LoginDTO result = null;
            try
            {
				var data = new Dictionary<string, string>();
				{
					data.Add("command-name", "login");
					data.Add("name", username);
					data.Add("password", passwordToLogin);
				}
                try
                {
                    result = await Services.Concatenazione()
                    .WithCookie("auth-key", key)
                    .WithHeader("Content-Type", "application/x-www-form-urlencoded; param=value;charset=UTF-8")
                    .PostUrlEncodedAsync(data)
                    .ReceiveJson<LoginDTO>();
                }
                catch (NullReferenceException)
                {
                    result.success = false;
                }
                

                if (result.success == true)
                {
                    UserManager.Instance.token = result.data.token;
                    Preferences.Set("username", username);
                    Preferences.Set("password", passwordToLogin);
                    Preferences.Set("Logged", remember);
                    Preferences.Set("token", result.data.token);
                }
                else if(result == null)
                {
                    await App.Current.MainPage.DisplayAlert("Fondo Merende", "Impossibile connettersi al server", "Ok");
                }
                return result;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Connessione al server scaduta", "OK");
            }
            catch (FlurlHttpException ex)
            {
              //  await App.Current.MainPage.DisplayAlert("Login",ex.InnerException.Message, "OK");
            }
            
            return result;
        }

    }
}
