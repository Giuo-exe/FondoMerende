using Flurl.Http;
using fondomerende.Main.Manager;
using fondomerende.Main.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace fondomerende.Main.Services.RESTServices
{
    class UserServiceManager
    {


        //Servizio per ricevere i dati utente
        public async System.Threading.Tasks.Task<UserDTO> GetUserData()
        {
            string urlString = Services.Concatenazione("?command-name=get-user-data");
            try
            {
                var response = await urlString
                                .WithCookie("auth-key", Services.GetAuthKey())
                                .WithCookie("user-token", UserManager.Instance.token)
                                .GetJsonAsync<UserDTO>();

                if (response.success == true)
                {
                    Preferences.Set("friendly-name", response.data.userList.friendly_name);
                }
                return response;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Connessione al server scaduta", "OK");
            }
            catch (FlurlHttpException ex)
            {
               
            }
            return null;

        }
    }
}
