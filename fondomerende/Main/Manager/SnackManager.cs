using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Manager
{
    public sealed class SnackManager
    {
        public List<object> SnackArray;
        public List<object> SnackFavArray;

        private static SnackManager _instance;


        private SnackManager()
        {

        }

        public static SnackManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SnackManager();
                }
                return _instance;
            }
        }
    }
}
