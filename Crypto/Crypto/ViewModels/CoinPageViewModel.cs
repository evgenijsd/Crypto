using Crypto.Models.API;
using Crypto.Models.Bindables;
using Crypto.Services.Crypto;
using Crypto.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Crypto.ViewModels
{
    public class CoinPageViewModel : BaseViewModel
    {
        ICryptoService _cryptoService;

        public CoinPageViewModel(
            ICryptoService cryptoService,
            INavigationService navigationService) 
            : base(navigationService)
        {
            _cryptoService = cryptoService;
        }

        #region -- Public properties --

        public CoinBindableModel Coin { get; set; }
        public ObservableCollection<HistoryBindableModel> History { get; set; }
        public ObservableCollection<MarketBindableModel> Markets { get; set; }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= new AsyncCommand(OnGoBackCommandAsync, allowsMultipleExecutions: false);

        private ICommand _chartCommand;
        public ICommand ChartCommand => _chartCommand ??= new AsyncCommand(OnChartCommandAsync, allowsMultipleExecutions: false);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(Constants.Navigations.COIN, out CoinBindableModel coin))
            {
                Coin = coin;
                var history = await _cryptoService.GetHistoryByIdAsync(coin.Id);

                if (history.IsSuccess)
                {
                    History = new ObservableCollection<HistoryBindableModel>(history.Result.OrderByDescending(x => x.Time));
                }

                var markets = await _cryptoService.GetMarketsByIdAsync(coin.Id);

                if (markets.IsSuccess)
                {
                    Markets = new ObservableCollection<MarketBindableModel>(markets.Result);
                }
            }
        }

        #endregion

        #region -- Private helpers --

        private Task OnGoBackCommandAsync()
        {
            return _navigationService.GoBackAsync();
        }

        private Task OnChartCommandAsync()
        {
            var parameters = new NavigationParameters() { { Constants.Navigations.CHART, Coin } };

            return _navigationService.NavigateAsync(nameof(ChartPage), parameters);
        }

        #endregion
    }
}
