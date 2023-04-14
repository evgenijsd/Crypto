using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto.Models.Bindables
{
    public class HistoryBindableModel : BindableBase
    {
        public double PriceUsd { get; set; }
        public DateTime Time { get; set; }
    }
}
