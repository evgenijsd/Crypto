using Crypto.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Crypto.Views
{
    public class BaseContentView : ContentView
    {
        private bool _isViewLoaded = false;

        public BaseContentView()
        {
        }

        #region -- Overrides --

        #nullable enable
        protected override void OnPropertyChanging([CallerMemberName] string? propertyName = null)
        {
            base.OnPropertyChanging(propertyName);

            if (propertyName == "Renderer")
            {
                if (BindingContext is IPageActionsHandler handler)
                {
                    if (_isViewLoaded)
                    {
                        handler.OnDisappearing();
                    }
                    else
                    {
                        handler.OnAppearing();
                    }

                    _isViewLoaded = !_isViewLoaded;
                }
            }
        }

        #endregion
    }
}
