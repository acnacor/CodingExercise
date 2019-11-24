using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebjetMoviesAPI.Helper.HttpResponse
{
    public static class Response
    {
        public static async Task<HttpResponseMessage> GetResponse(HttpClient inputUri, string resourceUri)
        {
                try
                {
                    var response = await inputUri.GetAsync(resourceUri);
                    response.EnsureSuccessStatusCode();
                    return response;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
        }
    }
}
