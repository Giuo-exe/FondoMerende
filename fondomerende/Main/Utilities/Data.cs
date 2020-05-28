using fondomerende.Main.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Utilities
{
    
    class Data
    {
        static DateTime data;
        static  DateTime Natale;
        static DateTime Halloween;
        public Data()
        {
            data = DateTime.Now;
            Natale = new DateTime(DateTime.Now.Year, 12, 25);
            Halloween = new DateTime(DateTime.Now.Year, 10, 31);
        }


        static TimeSpan DiffNat = Natale.Subtract(data);
        static TimeSpan DiffHall = Halloween.Subtract(data);

        public static void getEvent()
        {
            if (DiffNat.Days < 17)
            {
                EventManager.Instance.evento = 1;
            }
            if(DiffHall.Days < 5)
            {
                EventManager.Instance.evento = 2;
            }
            EventManager.Instance.evento = 0;
        }
        //if(DiffNat.<12){
        //    }
    }
}
