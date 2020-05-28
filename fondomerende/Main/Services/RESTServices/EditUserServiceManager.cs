using System;
using System.Collections.Generic;
using System.Text;
using Flurl.Http;
using fondomerende.Main.Services.Models;

namespace fondomerende.Main.Services.RESTServices
{
    class EditUserServiceManager
    {

        public async System.Threading.Tasks.Task<EditUserDTO> EditUserAsync(string ChangeUsername, string ChangeFriendlyName, string ChangePassword)
        {
            var data = new Dictionary<string, string>();
            {
                data.Add("command-name", "edit-user");
                data.Add("name", ChangeUsername);
                data.Add("friendly-name", ChangeFriendlyName);
                data.Add("password", ChangePassword);
            }
            try
            {
                var result = await Services.Concatenazione()
                    .WithCookie("auth-key", Services.GetAuthKey())
                    .WithCookie("token", Manager.UserManager.Instance.token)
                    .PostUrlEncodedAsync(data)
                    .ReceiveJson<EditUserDTO>();

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
