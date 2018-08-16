using DACLayer.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace DACLayer.Implementations
{
    public class DACLayer : IDACLayer
    {
        public DACLayer()
        {
        }
        public async Task<string> GetDataFromUriAsync(string uri)
        {
            HttpClient client = HttpClientSingleton.GetHttpClient();
            return await client.GetStringAsync(uri);            
        }
    }
}
