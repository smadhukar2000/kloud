using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KloudAZFunctions
{
    public static class GenericHelper
    {
        public static JsonMediaTypeFormatter GetJsonMediaTypeFormatter()
        {
            JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
            JsonSerializerSettings jsonSerialSettings = formatter.SerializerSettings;

            jsonSerialSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            jsonSerialSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            jsonSerialSettings.NullValueHandling = NullValueHandling.Ignore;
            jsonSerialSettings.Formatting = Formatting.Indented;
            jsonSerialSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerialSettings.Culture = new CultureInfo("en-AU");

            return formatter;
        }
    }
}
