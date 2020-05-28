using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Login.TabletMode.Controlli
{

    class Utente
    {
        public Utente(string username, string password, string codiceunivoco)
        {
            this.Username = username;
            this.Password = password;
            this.Codiceunivoco = codiceunivoco;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Codiceunivoco { get; set; }

    }
}
