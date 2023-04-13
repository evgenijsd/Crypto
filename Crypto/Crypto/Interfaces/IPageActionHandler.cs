using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto.Interfaces
{
    public interface IPageActionsHandler
    {
        void OnAppearing();
        void OnDisappearing();
    }
}
