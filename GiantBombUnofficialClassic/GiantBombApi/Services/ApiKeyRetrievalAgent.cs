using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombApi.Services
{
    public class ApiKeyRetrievalAgent
    {
        /// <summary>
        /// Step One: Get a key from http://www.giantbomb.com/app/APP-NAME
        /// Step Two: Run it through this method, which will get that account's API key
        /// </summary>
        /// <param name="linkCode"></param>
        /// <returns></returns>
        public static async Task<string> GetApiKeyFromCodeAsync(string linkCode, string encodedAppName)
        {
            string apiKey = null;

            try
            {
                var uri = new Uri("http://www.giantbomb.com/app/" + encodedAppName + "/get-result?format=json&regCode=" + linkCode);
                var response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<Models.ApiKeyResponse>(uri);
                if ((response != null) && (!String.IsNullOrWhiteSpace(response.ApiKey)))
                {
                    apiKey = response.ApiKey;
                }
                else
                {
                    Serilog.Log.Information("Unable to get API key from provided link code");
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error getting API key from link code");
            }

            return apiKey;
        }
    }
}
