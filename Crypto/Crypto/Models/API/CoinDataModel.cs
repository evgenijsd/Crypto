using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto.Models.API
{
    public class CoinDataModel
    {
        [JsonProperty("data")]
        public IEnumerable<CoinModel> Coins { get; set; }
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
