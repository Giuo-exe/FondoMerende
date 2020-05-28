using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.View;

namespace fondomerende.Main.Utilities
{
    class ColorRandom
    {
        Color[] colorLight = new Color[] 
        {
            Color.FromHex("#CE93D8"),
            Color.FromHex("#BA68C8"),
            Color.FromHex("#F48FB1"),
            Color.FromHex("F06292"),
            Color.FromHex("90CAF9"),
            Color.FromHex("#64B5F6"),
            Color.FromHex("#9FA8DA"),
            Color.FromHex("#7986CB"),
            Color.FromHex("#B39DDB"),
            Color.FromHex("#9575CD")
        };
             
        Color[] color = new Color[] 
        {
            Color.FromHex("#0886c7"), //azzurro
            //Color.FromHex("#E01F25"), //rosso bellino
            Color.FromHex("#FFBF18"),
            Color.FromHex("#38B54A"), //verde
            Color.FromHex("#0b9906"), //verde scurino
            Color.FromHex("#9B212A"), //rosso
            //Color.FromHex("#C7B299"), //grigiochiaro
            Color.FromHex("#904b93"), //viola
            Color.FromHex("#EA5454"), //rosso sbiadito
            Color.FromHex("#84830b"), //Coleottero
            Color.FromHex("#0bb6b0")  //azzurrino
        };

        public Color GetRandomColorPreferences()
        {
            Random random = new Random((int)DateTime.Now.Ticks);

            //string[] color = new string[] { "#FFFFFF", "#E01F25", "#FFBF18", "#41FF32", "#00A000", "#00C196", "#00323F" };
            int numeroCasuale = random.Next(0, color.Length);

            Preferences.Set("Colore", GetHexString(color[numeroCasuale]));
          


            return color[numeroCasuale];
        }

        public Color GetRandomColor()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int numeroCasuale = random.Next(0, color.Length);

            return color[numeroCasuale];
        }

        public Color GetRandomLightColor()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int numeroCasuale = random.Next(0, color.Length);

            return colorLight[numeroCasuale];

        }

        public static string GetHexString(Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var alpha = (int)(color.A * 255);
            var hex = $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";

            return hex;
        }
    }
}
