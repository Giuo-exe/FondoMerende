using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace fondomerende.Main.Services.Models
{
    class SnackDTO
    {
        public bool success { get; set; }
        public int status { get; set; }
        public string message { get; set; }
        public SnackDataArrayDTO data { get; set; }


        public static implicit operator SnackDTO(HttpResponseMessage v)
        {
            throw new NotImplementedException();
        }
    }
}
