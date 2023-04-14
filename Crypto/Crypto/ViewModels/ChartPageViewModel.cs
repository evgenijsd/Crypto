using Crypto.Models.Bindables;
using Crypto.Services.Crypto;
using Prism.Commands;
using Prism.Navigation;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Crypto.ViewModels
{
    public class ChartPageViewModel : BaseViewModel
    {
        private readonly ICryptoService _cryptoService;
        private ObservableCollection<HistoryBindableModel> _history;

        public ChartPageViewModel(
            ICryptoService cryptoService,
            INavigationService navigationService) 
            : base(navigationService)
        {
            _cryptoService = cryptoService;
            PaintCanvas = new EventHandler<SKPaintSurfaceEventArgs>(OnPaintCanvas);
        }

        #region -- Public properties --

        public double[] Data { get; set; }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= new AsyncCommand(OnGoBackCommandAsync, allowsMultipleExecutions: false);

        public EventHandler<SKPaintSurfaceEventArgs> PaintCanvas { get; private set; }

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(Constants.Navigations.CHART, out CoinBindableModel coin))
            {
                var history = await _cryptoService.GetHistoryByIdAsync(coin.Id);

                if (history.IsSuccess)
                {
                    _history = new ObservableCollection<HistoryBindableModel>(history.Result.OrderByDescending(x => x.Time));
                    Data = _history.Select(x => x.PriceUsd).ToArray();
                }
            }
        }

        #endregion

        #region -- Private helpers --

        private Task OnGoBackCommandAsync()
        {
            return _navigationService.GoBackAsync();
        }

        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs args)
        {
            
        }

        #endregion
    }
}
