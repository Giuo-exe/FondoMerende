using Flurl.Http;
using fondomerende.Main.Manager;
using fondomerende.Main.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Services.RESTServices
{
    class LastActionServiceManager
    {
        public async System.Threading.Tasks.Task<LastActionDTO> GetLastActions()
        {
            string urlString = Services.Concatenazione("?command-name=get-actions&limit=30");
            try
            {
                var response = await urlString
                                    .WithCookie("auth-key", Services.GetAuthKey())
                                    .WithCookie("user-token", UserManager.Instance.token)
                                    .GetJsonAsync<LastActionDTO>();
                return response;
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
