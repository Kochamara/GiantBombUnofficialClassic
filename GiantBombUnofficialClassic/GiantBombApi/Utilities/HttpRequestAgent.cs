using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombApi.Utilities
{
    public class HttpRequestAgent
    {
        public const string UserAgent = "CECACEF1-8469-4318-A1ED-EDC4B5F3BF2A_GiantBombUnofficialClassic_UserAgent";

        public static async Task<T> GetDeserializedResponseAsync<T>(Uri uri)
        {
            T response = default(T);

            try
            {
                using (var client = new HttpClient())
                {
                    HttpRequestHeaders headers = client.DefaultRequestHeaders;
                    headers.UserAgent.ParseAdd(UserAgent);

                    var rawJson = await client.GetStringAsync(uri);
                    response = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(rawJson);
                }
            }
            catch (Exception e)
            {
                // TODO: add a logger
            }

            return response;
        }
    }
}