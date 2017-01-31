using GiantBombApi.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GiantBombApi.Services
{
    public class VideoRetrievalAgent
    {
        public const string UserAgent = "CECACEF1-8469-4318-A1ED-EDC4B5F3BF2A_GiantBombTestApp_UnregisteredUserAgent";

        public static async Task<Response> GetVideosAsync(string apiKey)
        {
            Response response = null;

            try
            {
                var uri = new Uri("http://www.giantbomb.com/api/videos/?api_key=" + apiKey + "&format=json");

                using (var client = new HttpClient())
                {
                    HttpRequestHeaders headers = client.DefaultRequestHeaders;
                    headers.UserAgent.ParseAdd(UserAgent);

                    var rawJson = await client.GetStringAsync(uri);
                    response = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(rawJson);
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
