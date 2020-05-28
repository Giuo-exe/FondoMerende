using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Services.Models
{
    public class UserDataDTO
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("friendly-name")]
        public string friendly_name { get; set; }

    }

    
    public class UserDataObjectDTO
    {
        [JsonProperty("user")]
        public UserDataDTO userList { get; set; }
    }
}
