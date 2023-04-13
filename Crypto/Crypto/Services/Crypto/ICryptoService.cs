using Crypto.Helpers.ProcessHelpers;
using Crypto.Models.API;
using Crypto.Models.Bindables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Services.Crypto
{
    public interface ICryptoService
    {
        Task<AOResult<IEnumerable<CoinBindableModel>>> GetTopCoinsAsync(int limit);
        Task<AOResult<IEnumerable<HistoryBindableModel>>> GetHistoryByIdAsync(string id);
        Task<AOResult<IEnumerable<MarketBindableModel>>> GetMarketsByIdAsync(string id);
    }
}
