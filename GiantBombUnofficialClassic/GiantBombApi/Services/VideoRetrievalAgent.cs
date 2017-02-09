using GiantBombApi.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GiantBombApi.Services
{
    public class VideoRetrievalAgent
    {
        public static async Task<VideoResponse> GetVideosAsync(string apiKey)
        {
            VideoResponse response = null;

            try
            {
                var uri = new Uri("http://www.giantbomb.com/api/videos/?api_key=" + apiKey + "&format=json");
                response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<VideoResponse>(uri);
            }
            catch (Exception e)
            {
                // TODO: add a logger
            }

            return response;
        }

        public static async Task<IEnumerable<Category>> GetCategoriesAsync(string apiKey, bool onlyReturnShows)
        {
            List<Category> categories = null;

            try
            {
                var showsUri = new Uri("http://www.giantbomb.com/api/video_shows/?api_key=" + apiKey + "&format=json");
                var showsResponse = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<CategoriesResponse>(showsUri);

                if (showsResponse.Status == StatusCode.OK)
                {
                    categories = new List<Category>();
                    categories.AddRange(showsResponse.Results);

                    if (!onlyReturnShows)
                    {
                        var typesUri = new Uri("http://www.giantbomb.com/api/video_types/?api_key=" + apiKey + "&format=json");
                        var typesResponse = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<CategoriesResponse>(typesUri);

                        if (typesResponse.Status == StatusCode.OK)
                        {
                            foreach (var type in typesResponse.Results)
                            {
                                if (!String.IsNullOrWhiteSpace(type.Name) && (categories.Find(item => String.Equals(item.Name, type.Name, StringComparison.OrdinalIgnoreCase)) == null))
                                {
                                    categories.Add(type);
                                }
                            }
                        }
                    }
                }
                else
                {
                    // TODO: Add error logging
                }
            }
            catch (Exception e)
            {
                // TODO: add a logger
            }

            return categories;
        }
    }
}
