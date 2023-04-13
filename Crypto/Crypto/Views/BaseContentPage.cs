using Crypto.Interfaces;
using Xamarin.Forms;

namespace Crypto.Views
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        }

        #region -- Overrides --

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IPageActionsHandler actionsHandler)
            {
                actionsHandler.OnAppearing();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is IPageActionsHandler actionsHandler)
            {
                actionsHandler.OnDisappearing();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        #endregion
    }
}
