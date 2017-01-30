using GiantBombApi.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GiantBombApi.Services
{
    public class VideoRetrievalAgent
    {
        public static async Task<Response> GetVideoThingAsync()
        {
            Response response = null;

            try
            {
                var status = (StatusCode)1;

                Uri url = null; // No no no!

                HttpClient client = new HttpClient();

                
                //var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                //httpWebRequest.Method = "GET";

                HttpRequestHeaders headers = client.DefaultRequestHeaders;
                headers.UserAgent.ParseAdd("CECACEF1-8469-4318-A1ED-EDC4B5F3BF2A_GiantBombTestApp_UnregisteredUserAgent");

                var streamResult = await client.GetStringAsync(url);


                //var webResponse = await httpWebRequest.GetResponseAsync();
                //var streamReader = new System.IO.StreamReader(webResponse.GetResponseStream());
                //string result = streamReader.ReadToEnd();

                //    response = Serializer.Deserialize<Response>(streamResult);

                response = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(streamResult);

                //var thing = Newtonsoft.Json.JsonSerializer.Create();

                //response = thing.Deserialize<Response>(streamResult);
                //Response a = new Response();
                //string b = Serializer.Serialize<Response>(a);
            }
            catch (Exception e)
            {
                // TODO: add a logger
            }

            return response;
        }
    }
}
