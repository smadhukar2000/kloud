using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Kloud.Models;
using KloudAZFunctions.Implementations;
using KloudAZFunctions.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace KloudAZFunctions
{
    public static class DisplayOwnerCars
    {
        [FunctionName("brandwithowners")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, 
            TraceWriter log)
        {

            log.Info("C# HTTP trigger function brandwithowners.");           
            try
            {
                ITransactionBL transaction = new TransactionBL(new DACLayer.Implementations.DACLayer());
                List<CarOwner> carOwners = transaction.GetListOfModelsFromUri<CarOwner>(AppSettingsEnv.Instance.ApiUrl);
                return req.CreateResponse(HttpStatusCode.OK, transaction.GetGroupedAndOrderedData(carOwners), 
                    GenericHelper.GetJsonMediaTypeFormatter());
            }
            catch(Exception ex)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, $"Exception - {ex.Message}");
            }
        }
    }
}
