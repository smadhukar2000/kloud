using DACLayer.Interfaces;
using KloudAZFunctions.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KloudAZFunctions.Implementations
{
    public class TransactionBL : ITransactionBL
    {
        private IDACLayer dacLayer;       
        public TransactionBL(IDACLayer dacLayer)
        {
            this.dacLayer = dacLayer;
        }
        public List<T> GetListOfModelsFromUri<T>(string uri)
        {
            List<T> models = new List<T>();
            try
            {
                if(this.dacLayer == null)
                {
                    throw new ArgumentNullException("DACLayer object is null");
                }
                // invoke ASYNC api
                Task<string> task = dacLayer.GetDataFromUriAsync(uri);
                // wait till result is ready
                task.Wait();
                // convert json into workable models i.e. CarOwner
                models = JsonConvert.DeserializeObject<List<T>>(task.Result);
            }
            catch(Exception)
            {
                throw;
            }
            return models;
        }
    }
}
