using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Manager
{ 
    public sealed class TabletManager
    {
        public bool tablet;
        public int codicegiusto = 0;

        private static TabletManager _instance;


        private TabletManager()
        {

        }

        public static TabletManager Instance
        {
            get
            {
                if (_instance == null) _instance = new TabletManager();
                return _instance;
            }
        }
    }
    
}
