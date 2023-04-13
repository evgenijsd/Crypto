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
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ICryptoService _cryptoService;

        public MainPageViewModel(
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
        public int TopQuantity { get; set; } = 100;

        private ICommand _topCommand;
        public ICommand TopCommand => _topCommand ??= new AsyncCommand(OnTopCommand, allowsMultipleExecutions: false);

        private ICommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ??= new AsyncCommand(OnSearchCommand, allowsMultipleExecutions: false);

        private ICommand _selectCoinCommand;
        public ICommand SelectCoinCommand => _selectCoinCommand ??= new AsyncCommand(OnSelectCoinCommand, allowsMultipleExecutions: false);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var result = await _cryptoService.GetTopCoinsAsync(TopQuantity);

            if (result.IsSuccess)
            {
                Currencies = new ObservableCollection<CoinBindableModel>(result.Result);
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
        }

        #endregion

        #region -- Private helpers --

        private async Task OnTopCommand()
        {
            var result = await _cryptoService.GetTopCoinsAsync(TopQuantity);

            if (result.IsSuccess)
            {
                Currencies = new ObservableCollection<CoinBindableModel>(result.Result);
            }
        }

        private Task OnSearchCommand()
        {
            return Task.CompletedTask;
        }

        private Task OnSelectCoinCommand()
        {
            var parameters = new NavigationParameters() { { Constants.Navigations.COIN, CoinSelected } };

            return _navigationService.NavigateAsync(nameof(CoinPage), parameters);
        }

        #endregion
    }
}
