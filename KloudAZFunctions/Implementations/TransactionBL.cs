using DACLayer.Interfaces;
using Kloud.Models;
using KloudAZFunctions.Interfaces;
using Newtonsoft.Json;
using System;
using System.Linq;
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

        public IDictionary<string, List<string>> GetGroupedAndOrderedData(List<CarOwner> models)
        {
            if (models == null || models.Count == 0)
            {
                throw new ArgumentNullException("No  model data exist");
            }

            // result dictionary
            IDictionary<string, List<string>> result = new Dictionary<string, List<string>>();

            // group by brnad and order by brand name Alphabetically
            var groups = models.SelectMany(co => co.Cars, (co, car) => new { co.Name, car })
                .GroupBy(pair => pair.car.Brand).OrderBy(group => group.Key);
            // extract owner names in Alphabetically
            foreach (var group in groups)
            {
                List<string> names = group.ToList().OrderBy(c => c.car.Colour).Select(p => p.Name).Distinct().ToList<string>();
                result[group.Key] = names;
            }
          
            return result;
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
