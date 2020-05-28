using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Services.Models
{
    
    public class AddSnackResponseDTO
    {
       [JsonProperty("snack-id")]
       public int id { get; set; }
        
    }
}
