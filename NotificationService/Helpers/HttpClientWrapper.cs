using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotificationService.Helpers
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        public async Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> headers, StringContent content)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    if(headers != null)
                        headers.Keys.ToList().ForEach(_ => client.DefaultRequestHeaders.Add(_, headers[_]));

                    var response = await client.PostAsync(url, content);
                    return response;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }
    }
}
