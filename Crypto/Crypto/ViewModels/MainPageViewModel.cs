using Crypto.Models.Bindables;
using Crypto.Resources.Strings;
using Crypto.Resources.Styles;
using Crypto.Services.Crypto;
using Crypto.Views;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Crypto.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ICryptoService _cryptoService;

        private bool _isLightTheme;
        private bool _isUaLang;

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

        private ICommand _updateThemeCommand;
        public ICommand UpdateThemeCommand => _updateThemeCommand ??= new AsyncCommand(OnUpdateThemeCommand, allowsMultipleExecutions: false);

        private ICommand _updateLanguageCommand;
        public ICommand UpdateLanguageCommand => _updateLanguageCommand ??= new AsyncCommand(OnUpdateLanguageCommand, allowsMultipleExecutions: false);

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
            var parameters = new NavigationParameters() { { Constants.Navigations.SEARCH, Search } };

            return _navigationService.NavigateAsync(nameof(SearchPage), parameters);
        }

        private Task OnSelectCoinCommand()
        {
            var parameters = new NavigationParameters() { { Constants.Navigations.COIN, CoinSelected } };

            return _navigationService.NavigateAsync(nameof(CoinPage), parameters);
        }

        private Task OnUpdateThemeCommand()
        {
            if (_isLightTheme)
            {
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new DarkModeResources());
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new LightModeResources());
            }

            _isLightTheme = !_isLightTheme;

            return Task.CompletedTask;
        }

        private Task OnUpdateLanguageCommand()
        {
            if (_isUaLang)
            {
                LocalizationResourceManager.Current.CurrentCulture = new CultureInfo("en-US");
            }
            else
            {
                LocalizationResourceManager.Current.CurrentCulture = new CultureInfo("uk-UA");
            }

            _isUaLang = !_isUaLang;

            return Task.CompletedTask;
        }

        #endregion
    }
}
