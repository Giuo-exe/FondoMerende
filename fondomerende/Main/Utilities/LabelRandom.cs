using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace fondomerende.Main.Utilities
{
    class LabelRandom
    {
        public String GetRandomPhrases()
        {
            Random random = new Random();
            string[] Phrase = new string[] 
            {
                "I have bean thinking about you <3",
                "Pasta la vista",
                "Allan please add details",
                "Don't complain.",
                "Good to See you, " + Preferences.Get("friendly-name",null) + "!",
                "Welcome to the wonderfully edible world of Fondo Merende",
                "Rimettendo tutti gli snack al loro posto...",
                "Compilando le merendine...",
                "",
                "These are not the snacks you're looking for",
                "Aggiungi del testo qui",
                "Powered by Apple ProStand",
                "Prendine una fetta anche tu!",
                "Amo premere f5, è così rinfrescante!",
                "Ma per codice sorgente intendi le coordinate per trovare l’acqua più pura?",
                "If at first you don't succeed call it version 1.0.",
                "Cavoletti di Bruxelles",
                "Ricordati di questa estaThè ",
                "Fà così freddo che sono arriavti i pinguì",
                "Non dimenticarti di mangiare",
                "Le macchinette conquisteranno il mondo...",
                "Eat an apple, don't miss it",
                "Tutto ebbe inizio con un aperitivo: il crodo primordiale",
                "Puoi cambiare il colore anche dalle impostazioni",
                "Effettuando il Log out perderai tutte le preferenze"
            };
            int numeroCasuale = random.Next(0, Phrase.Length);
            return Phrase[numeroCasuale];
        }
    }
}
