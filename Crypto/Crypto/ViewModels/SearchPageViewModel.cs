using Crypto.Models.Bindables;
using Crypto.Services.Crypto;
using Crypto.Views;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Crypto.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        private readonly ICryptoService _cryptoService;

        public SearchPageViewModel(
            ICryptoService cryptoService,
            INavigationService navigationService)
            : base(navigationService)
        {
            _cryptoService = cryptoService;
        }

        #region -- Public properties --

        public ObservableCollection<CoinBindableModel> Currencies { get; set; } = new ();
        public CoinBindableModel CoinSelected { get; set; }
        public string Search { get; set; }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= new AsyncCommand(OnGoBackCommandAsync, allowsMultipleExecutions: false);

        private ICommand _selectCoinCommand;
        public ICommand SelectCoinCommand => _selectCoinCommand ??= new AsyncCommand(OnSelectCoinCommand, allowsMultipleExecutions: false);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(Constants.Navigations.SEARCH, out string search))
            {
                var result = await _cryptoService.GetSearchAsync(search);

                if (result.IsSuccess)
                {
                    Currencies = new ObservableCollection<CoinBindableModel>(result.Result);
                }
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
        }

        #endregion

        #region -- Private helpers --

        private Task OnGoBackCommandAsync()
        {
            return _navigationService.GoBackAsync();
        }

        private Task OnSelectCoinCommand()
        {
            var parameters = new NavigationParameters() { { Constants.Navigations.COIN, CoinSelected } };

            return _navigationService.NavigateAsync(nameof(CoinPage), parameters);
        }

        #endregion
    }
}
