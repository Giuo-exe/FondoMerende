using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Services.Models
{
    public class ToBuyDataDTO
    {
        public int id { get; set; }

        public string friendly_name { get; set; }

        public double price { get; set; }
        [JsonProperty("snacks-per-box")]
        public int snack_per_box { get; set; }
        [JsonProperty("expiration-in-days")]
        public int expiration_in_days { get; set; }


    }

    [JsonObject("snacks")]
    public class ToBuyDataArrayDTO
    {
        [JsonProperty("fund-funds-amount")]
        public double fund_funds_amount { get; set; }
        public List<ToBuyDataDTO> snacks { get; set; }
    }
}
