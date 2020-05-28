using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace fondomerende.Main.Services.Models
{
    class AllSnacksDTO
    {
        public bool success { get; set; }
        public int status { get; set; }
        public string message { get; set; }
        public AllSnackDataArrayDTO data { get; set; }

        public static implicit operator AllSnacksDTO(HttpResponseMessage v)
        {
            throw new NotImplementedException();
        }
    }

}

