using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto.Models.API
{
    public class CoinModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("rank")]
        public int Rank { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("supply")]
        public double Supply { get; set; }
        [JsonProperty("maxSupply")]
        public double? MaxSupply { get; set; }
        [JsonProperty("marketCapUsd")]
        public double MarketCapUsd { get; set; }
        [JsonProperty("volumeUsd24Hr")]
        public double VolumeUsd24Hr { get; set; }
        [JsonProperty("priceUsd")]
        public double PriceUsd { get; set; }
        [JsonProperty("changePercent24Hr")]
        public double ChangePercent24Hr { get; set; }
    }
}
