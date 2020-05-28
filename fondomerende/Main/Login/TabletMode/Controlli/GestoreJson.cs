using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Foundation;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace fondomerende.Main.Login.TabletMode.Controlli
{
    class GestoreJson
    {

        private static string filename = "Utenti.json";
        public GestoreJson()
        {
        }

        public static async Task Serializza(List<Utente> u)
        {
            string json = JsonConvert.SerializeObject(u, Formatting.Indented);
            await LetturaFile.WriteTextAllAsync(filename,json);
        }

        public static async Task<List<Utente>> deserializza()
        {
            string json = await LetturaFile.ReadAllTextAsync(filename);
            List<Utente> u = JsonConvert.DeserializeObject<List<Utente>>(json);
            return u;
        }

    }  
}
