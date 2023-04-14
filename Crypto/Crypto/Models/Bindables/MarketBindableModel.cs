using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto.Models.Bindables
{
    public class MarketBindableModel : BindableBase
    {
        public string ExchangeId { get; set; }
        public string BaseId { get; set; }
        public string QuoteId { get; set; }
        public string BaseSymbol { get; set; }
        public string QuoteSymbol { get; set; }
        public double VolumeUsd24Hr { get; set; }
        public double PriceUsd { get; set; }        
        public double VolumePercent { get; set; }
    }
}
