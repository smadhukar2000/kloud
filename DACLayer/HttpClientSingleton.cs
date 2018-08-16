using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DACLayer
{
    public class HttpClientSingleton
    {
        private static HttpClient httpClient = null;

        public static HttpClient GetHttpClient()
        {
            if(HttpClientSingleton.httpClient == null)
            {
                HttpClientSingleton.httpClient =  new HttpClient();
            }
            return HttpClientSingleton.httpClient;
        }
    }
}
