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
        public static async Task<VideosResponse> GetVideosAsync(string apiKey)
        {
            VideosResponse response = null;

            try
            {
                var uri = new Uri("http://www.giantbomb.com/api/videos/?format=json&api_key=" + apiKey);
                response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<VideosResponse>(uri);
            }
            catch (Exception e)
            {
                // TODO: add a logger
            }

            return response;
        }

        public static async Task<VideosResponse> GetVideosAsync(string apiKey, string videoTypeId)
        {
            VideosResponse response = null;

            try
            {
                var uri = new Uri("http://www.giantbomb.com/api/videos/?format=json&api_key=" + apiKey + "&video_type=" + videoTypeId);
                response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<VideosResponse>(uri);
            }
            catch (Exception e)
            {
                // TODO: add a logger
            }

            return response;
        }

        public static async Task<TypesResponse> GetVideoTypesAsync(string apiKey)
        {
            TypesResponse response = null;

            try
            {
                var uri = new Uri("http://www.giantbomb.com/api/video_types/?format=json&api_key=" + apiKey);
                response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<TypesResponse>(uri);
            }
            catch (Exception e)
            {
                // TODO: add a logger
            }

            return response;
        }

        public static async Task<VideosResponse> GetVideoSearchResultsAsync(string apiKey, string query)
        {
            VideosResponse response = null;

            try
            {
                var uri = new Uri("http://www.giantbomb.com/api/search/?format=json&api_key=" + apiKey + "&query=" + query + "&resources=video");
                response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<VideosResponse>(uri);
            }
            catch (Exception e)
            {
                // TODO: add a logger
            }

            return response;
        }
    }
}
