using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Services.Models
{
    class FundFundsDataDTO
    {
        [JsonProperty("fund-funds-amount")]
        public float fund_funds_amount { get; set; }
    }
}
