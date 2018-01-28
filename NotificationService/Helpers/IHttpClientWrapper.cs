using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotificationService.Helpers
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> headers, StringContent content); 
    }
}
