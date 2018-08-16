using Kloud.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KloudAZFunctions.Interfaces
{
    public interface ITransactionBL
    {
        Task<List<T>> GetListOfModelsFromUriAsync<T>(string uri);
        IDictionary<string, List<string>> GetGroupedAndOrderedData(List<CarOwner> models);
    }
}
    