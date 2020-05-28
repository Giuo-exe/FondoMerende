using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Services.Models
{

    public class SnackDataDTO
    {
        public int id { get; set; }
        public string name { get; set; }

        [JsonProperty("friendly-name")]
        public string friendly_name { get; set; }

        [JsonProperty("price-per-snack")]
        public double price { get; set; }
        [JsonProperty("snacks-per-box")]
        public int snack_per_box { get; set; }
        [JsonProperty("expiration-in-days")]
        public int expiration_in_days { get; set; }

        [JsonProperty("quantity")]
        public int quantity { get; set; }

    }

    [JsonObject("snacks")]
    public class SnackDataArrayDTO
    {
        public List<SnackDataDTO> snacks { get; set; }
    }
}
