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
            SortedDictionary<string, List<Tuple<string, Car>>> mapBrands = new SortedDictionary<string, List<Tuple<string, Car>>>();

            // get grouped car with owner details and color
            foreach (CarOwner co in models)
            {
                var carGroups = co.Cars.GroupBy(car => car.Brand);
                foreach (IGrouping<string, Car> carGroup in carGroups)
                {
                    var cars = carGroup.ToList();
                    // if key not exist, create new brand entry 
                    if (!mapBrands.ContainsKey(carGroup.Key))
                    {
                        mapBrands[carGroup.Key] = new List<Tuple<string, Car>>();
                    }
                    foreach (Car c in cars)
                    {
                        mapBrands[carGroup.Key].Add(new Tuple<string, Car>(co.Name, c));
                    }
                }
            }

            IDictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            // sort owner name by car color
            foreach (var mapBrand in mapBrands)
            {
                var sortedList = mapBrand.Value.OrderBy(t => t.Item2.Colour).Select(tuple => tuple.Item1).Distinct().ToList();
                result[mapBrand.Key] = sortedList;
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
