using AutoMapper;
using Crypto.Helpers.ProcessHelpers;
using Crypto.Models.API;
using Crypto.Models.Bindables;
using Crypto.Resources.Strings;
using Crypto.Services.Rest;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crypto.Services.Crypto
{
    public class CryptoService : ICryptoService
    {
        private readonly IRestService _restService;
        private readonly IMapper _mapper;

        public CryptoService(
            IMapper mapper,
            IRestService restService)
        {
            _restService = restService;
            _mapper = mapper;
        }

        public async Task<AOResult<IEnumerable<CoinBindableModel>>> GetTopCoinsAsync(int limit)
        {
            var result = new AOResult<IEnumerable<CoinBindableModel>>();

            try
            {
                var query = $"{Constants.API.HOST_URL}assets?limit={limit}";
                var currencies = await _restService.RequestAsync<CoinDataModel<CoinModel>>(HttpMethod.Get, query);

                if (currencies is not null)
                {
                    var bindableCurrencies = _mapper.Map<IEnumerable<CoinBindableModel>>(currencies.Data);
                    result.SetSuccess(bindableCurrencies);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetTopCoinsAsync)}", Strings.NotFound, ex);
            }

            return result;
        }

        public async Task<AOResult<IEnumerable<HistoryBindableModel>>> GetHistoryByIdAsync(string id)
        {
            var result = new AOResult<IEnumerable<HistoryBindableModel>>();

            try
            {
                var query = $"{Constants.API.HOST_URL}assets/{id}/history?interval=d1";
                var history = await _restService.RequestAsync<CoinDataModel<HistoryModel>>(HttpMethod.Get, query);

                if (history is not null)
                {
                    var bindableHistory = _mapper.Map<IEnumerable<HistoryBindableModel>>(history.Data);
                    result.SetSuccess(bindableHistory);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetHistoryByIdAsync)}", Strings.NotFound, ex);
            }

            return result;
        }

        public async Task<AOResult<IEnumerable<MarketBindableModel>>> GetMarketsByIdAsync(string id)
        {
            var result = new AOResult<IEnumerable<MarketBindableModel>>();

            try
            {
                var query = $"{Constants.API.HOST_URL}assets/{id}/markets";
                var markets = await _restService.RequestAsync<CoinDataModel<MarketModel>>(HttpMethod.Get, query);

                if (markets is not null)
                {
                    var bindableMarkets = _mapper.Map<IEnumerable<MarketBindableModel>>(markets.Data);
                    result.SetSuccess(bindableMarkets);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetMarketsByIdAsync)}", Strings.NotFound, ex);
            }

            return result;
        }
    }
}
