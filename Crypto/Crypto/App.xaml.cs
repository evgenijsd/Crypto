using AutoMapper;
using Crypto.Models.API;
using Crypto.Models.Bindables;
using Crypto.Resources.Strings;
using Crypto.Services.Crypto;
using Crypto.Services.Rest;
using Crypto.ViewModels;
using Crypto.Views;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using System;
using System.Globalization;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace Crypto
{
    public partial class App : PrismApplication
    {
        #nullable enable
        public App(IPlatformInitializer? initializer = null)
            : base(initializer)
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var mapper = CreateMapper();
            containerRegistry.RegisterInstance(mapper);
            containerRegistry.RegisterSingleton<IRestService, RestService>();
            containerRegistry.RegisterSingleton<ICryptoService, CryptoService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<CoinPage, CoinPageViewModel>();
            containerRegistry.RegisterForNavigation<SearchPage, SearchPageViewModel>();
            containerRegistry.RegisterForNavigation<ChartPage, ChartPageViewModel>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            LocalizationResourceManager.Current.PropertyChanged += (sender, e) => Strings.Culture = LocalizationResourceManager.Current.CurrentCulture;
            LocalizationResourceManager.Current.Init(Strings.ResourceManager);

            LocalizationResourceManager.Current.CurrentCulture = new CultureInfo("en");
        }


        protected override async void OnStart()
        {
            var navigationParameters = new NavigationParameters();
            var navigationPath = $"{nameof(NavigationPage)}/";
            navigationParameters.Add(Constants.Navigations.MAIN, true);
            navigationPath += nameof(MainPage);

            await NavigationService.NavigateAsync(navigationPath, navigationParameters);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #region -- Private helpers --

        private IMapper CreateMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CoinModel, CoinBindableModel>();
                cfg.CreateMap<HistoryModel, HistoryBindableModel>()
                    .ForMember(x => x.Time, x => x.MapFrom(y => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(y.Time).ToLocalTime()));
                cfg.CreateMap<MarketModel, MarketBindableModel>();
            }).CreateMapper();  
        }

        #endregion
    }
}
