using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Kloud.Models;
using KloudAZFunctions.DI;
using KloudAZFunctions.Implementations;
using KloudAZFunctions.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace KloudAZFunctions
{
    public static class CarsApi
    {
        [FunctionName("cars")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, 
            TraceWriter log, [Inject(typeof(ITransactionBL))]ITransactionBL transaction)
        {
            List<CarOwner> carOwners = new List<CarOwner>();
            try
            {
                carOwners = transaction.GetListOfModelsFromUri<CarOwner>(AppSettingsEnv.Instance.ApiUrl);
                return req.CreateResponse(HttpStatusCode.OK, carOwners, GenericHelper.GetJsonMediaTypeFormatter());
            }
            catch(Exception ex)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, $"Exception - {ex.Message}");
            }
        }
    }
}
