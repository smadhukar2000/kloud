using DACLayer.Interfaces;
using Kloud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(uri);
            }
        }
    }
}
