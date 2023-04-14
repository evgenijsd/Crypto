using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto.Models.API
{
    public class HistoryModel
    {
        [JsonProperty("priceUsd")]
        public double PriceUsd { get; set; }
        [JsonProperty("time")]
        public long Time { get; set; }
    }
}
