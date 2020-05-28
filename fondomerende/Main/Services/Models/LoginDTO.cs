using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace fondomerende.Main.Services.Models
{
    class LoginDTO
    {
        public bool success { get; set; }
        public int status { get; set; }
        public string message { get; set; }
        public LoginDataDTO data { get; set; }
    }
}
