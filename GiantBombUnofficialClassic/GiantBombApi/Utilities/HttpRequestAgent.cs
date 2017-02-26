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
        public const string UserAgent = "GiantBombVideoPlayer_UnofficialClassic_ForWindowsAndXbox";

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
                Serilog.Log.Error(e, "Error getting server response");
            }

            return response;
        }
    }
}