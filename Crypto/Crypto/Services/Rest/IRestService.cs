using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Services.Rest
{
#nullable enable
    public interface IRestService
    {
        Task<T?> RequestAsync<T>(HttpMethod method, string resource, Dictionary<string, string>? additioalHeaders = null, bool isIgnoreRefreshToken = false);

        Task<T?> RequestAsync<T>(HttpMethod method, string resource, object requestBody, Dictionary<string, string>? additioalHeaders = null, bool isIgnoreRefreshToken = false);
    }
}
