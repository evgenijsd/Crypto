using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto.Models.API
{
    public class CoinDataModel<T>
    {
        [JsonProperty("data")]
        public IEnumerable<T> Data { get; set; }
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
