using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace fondomerende.Main.Manager
{
    public sealed class UserManager
    {
        public string token;
    
        private static UserManager _instance;

    
        private UserManager()
        {
            
        }
       
        public static UserManager Instance
        {
            get
            {
                if (_instance == null) _instance = new UserManager();
                return _instance;
            }
        }
    }
}
