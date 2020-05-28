using System;
using System.Collections.Generic;
using System.Text;
namespace fondomerende.Main.Manager
    {
        public sealed class EventManager
        {
            public int evento;

            private static EventManager _instance;


            private EventManager()
            {

            }

            public static EventManager Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new EventManager();
                    }
                    return _instance;
                }
            }
        }
    }

