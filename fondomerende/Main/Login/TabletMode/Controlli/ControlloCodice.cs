using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using fondomerende.Main.Services.Models;
using Android.Content.Res;
using fondomerende.Main.Login.TabletMode.Popup;

namespace fondomerende.Main.Login.TabletMode.Controlli
{
    class ControlloCodice
    {
        static List<Utente> utenti;
        static LoginServiceManager loginService;
        public ControlloCodice()
        {
            utenti = new List<Utente>();
            riempiLista();
        }

        public static async void fineAzioni()
        {
            try
            {
                await loginService.LoginAsync("@kfc","1",false);
            }
            catch (NullReferenceException) { }
            
        }
        public static async Task<bool> checkBeforeAction(string codice)
        {
            LoginServiceManager login = new LoginServiceManager();
            riempiLista();
            foreach(var app in utenti)
            {
                if(app.Codiceunivoco == codice)
                {
                    var result = await login.LoginAsync(app.Username,app.Password,false);
                    
                    if(result.success == true)
                    {
                        return true;  
                    }
                }
            }
            return false;
        }

        private static async void riempiLista()
        {
            try
            {
                if(await LetturaFile.IsFileExistAsync("Utenti.json")) utenti = await GestoreJson.deserializza();
            }
            catch (NullReferenceException)
            {

            }
            
        }
        public static async Task<bool> AggiungiUtente(Utente u)
        {
            LoginServiceManager login = new LoginServiceManager();

            var result = await login.LoginAsync(u.Username,u.Password,false);

            try
            {
                if (result.success == true && result != null)
                {
                    if (!ControllaRegistrato(u) && !VerificaCodice(u.Codiceunivoco))
                    {

                        utenti.Add(u);
                        await GestoreJson.Serializza(utenti);
                        fineAzioni();
                        return true;
                    }
                }
            }
            catch (NullReferenceException)
            {

            }
            
            return false;
        }

        public static async Task<bool> cambiaCodice(Utente u)
        {
            LoginServiceManager login = new LoginServiceManager();

            var result = await login.LoginAsync(u.Username, u.Password, false);

            try
            {
                if (result.success == true && result != null)
                {
                    if (ControllaRegistrato(u) && !VerificaCodice(u.Codiceunivoco))
                    {
                        foreach(var app in utenti){
                            if (app.Username != u.Username || app.Password != u.Password){
                                continue;
                            }
                            app.Codiceunivoco = u.Codiceunivoco;
                            break;
                        }
                        await GestoreJson.Serializza(utenti);
                        fineAzioni();
                        return true;
                    }
                }
            }
            catch (NullReferenceException)
            {

            }

            return false;
        }

        public static bool ControllaRegistrato(Utente u)
        {
            if (!(utenti.Count == 0))
            {
                foreach (var utente in utenti)
                {
                    if (u.Username == utente.Username)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool VerificaCodice(string codice)
        {

            foreach (var u in utenti)
            {
                if (codice == u.Codiceunivoco)
                {
                    return true;
                }
            }
            return false;
        }

        public static async System.Threading.Tasks.Task CambiaCodiceAsync(string username,string password, string codice)
        {
            bool trovato = false;
            var result = await loginService.LoginAsync(username, password, false);
            if (result.success)
            {
                foreach (var app in utenti)
                {
                    if(app.Username == username && app.Password == password)
                    {
                        app.Codiceunivoco = codice;
                        trovato = true;
                    }
                    break;
                }
            }
            if (trovato) { }
        }
    }
}
