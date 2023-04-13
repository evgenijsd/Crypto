using AutoMapper;
using Crypto.Helpers.ProcessHelpers;
using Crypto.Models.API;
using Crypto.Models.Bindables;
using Crypto.Resources.Strings;
using Crypto.Services.Rest;
using Newtonsoft.Json;
using System;
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
                var currencies = await _restService.RequestAsync<CoinDataModel>(HttpMethod.Get, query);

                if (currencies is not null)
                {
                    var bindableCurrencies = _mapper.Map<IEnumerable<CoinBindableModel>>(currencies.Coins);
                    result.SetSuccess(bindableCurrencies);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetTopCoinsAsync)}", Strings.NotFound, ex);
            }

            return result;
        }

        public async Task<AOResult<CoinModel>> GetCurrencyByIdAsync(string id)
        {
            var result = new AOResult<CoinModel>();

            try
            {
                var query = $"{Constants.API.HOST_URL}coins/{id}";
                var currency = await _restService.RequestAsync<CoinModel>(HttpMethod.Get, query);

                if (currency is not null)
                {
                    result.SetSuccess(currency);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetCurrencyByIdAsync)}", Strings.NotFound, ex);
            }

            return result;
        }
    }
}
