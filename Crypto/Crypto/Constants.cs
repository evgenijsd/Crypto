using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto
{
    public static class Constants
    {
        public static class Navigations
        {
            public const string MAIN = "MAIN";
            public const string COIN = "COIN";
        }

        public static class Formats
        {
            public const string SHORT_DATE_FORMAT = "{0:dd.MM.yyyy}";
            public const string DATETIME_JSON_FORMAT = "yyyy-MM-ddTHH:mm:ss.ffffffZ";
        }

        public static class API
        {
            public const string HOST_URL = "https://api.coincap.io/v2/";
            public const int REQUEST_TIMEOUT = 20;
        }
    }
}
