using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kloud.Models;

namespace DACLayer.Interfaces
{
    public interface IDACLayer
    {
        Task<string> GetDataFromUriAsync(string uri);      
    }
}
