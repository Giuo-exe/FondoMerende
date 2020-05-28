using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Services.Models
{
    
    public class EditSnackDataDTO
    {
        [JsonObject("snack")]
        public class SnackDTO 
        {
            public int id { get; set; }
            public string name { get; set; }

            [JsonProperty("friendly-name")]
            public string friendly_name { get; set; }

            [JsonProperty("price")]
            public double price { get; set; }
            [JsonProperty("snacks-per-box")]
            public int snack_per_box { get; set; }
            [JsonProperty("expiration-in-days")]
            public int expiration_in_days { get; set; }
        }
       
        public SnackDTO snack { get; set; }
    }

}
