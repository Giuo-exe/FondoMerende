using Flurl.Http;
using fondomerende.Main.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace fondomerende.Main.Services.RESTServices
{
    class LogoutServiceManager
    {
        public string token = Manager.UserManager.Instance.token;
        public async System.Threading.Tasks.Task<LogoutDTO> LogoutAsync()   //Servizio di Log Out
        {
            try
            {
				var data = new Dictionary<string, string>();
				{
					data.Add("command-name", "logout");
				}
				var result = await Services.Concatenazione()
                               .WithCookie("auth-key", Services.GetAuthKey())
                               .WithCookie("token", token)
                               .PostUrlEncodedAsync(data)
                               .ReceiveJson<LogoutDTO>();


                if (result.success == true)
                {
                    Preferences.Clear();
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
