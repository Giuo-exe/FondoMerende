using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Services.Models
{
    class UserFundsDataDTO
    {
        [JsonProperty("user-funds-amount")]
        public string user_funds_amount { get; set; }
    }
}
