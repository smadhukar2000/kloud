using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KloudAZFunctions.Interfaces
{
    public interface ITransactionBL
    {
        List<T> GetListOfModelsFromUri<T>(string uri);
    }
}
    