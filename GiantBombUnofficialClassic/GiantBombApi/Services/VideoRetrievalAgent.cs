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
        /// <summary>
        /// Pulls a list of videos from Giant Bomb
        /// </summary>
        /// <param name="apiKey">API key unique to the user</param>
        /// <returns></returns>
        public static async Task<VideosResponse> GetVideosAsync(string apiKey)
        {
            var response = await GetVideosAsync(apiKey, string.Empty, 0);
            return response;
        }

        /// <summary>
        /// Pulls a list of videos from Giant Bomb
        /// </summary>
        /// <param name="apiKey">API key unique to the user</param>
        /// <param name="offset">If viewing multiple pages of videos, how many videos to skip in the query</param>
        /// <returns></returns>
        public static async Task<VideosResponse> GetVideosAsync(string apiKey, int offset)
        {
            var response = await GetVideosAsync(apiKey, string.Empty, offset);
            return response;
        }

        /// <summary>
        /// Pulls a list of videos from Giant Bomb
        /// </summary>
        /// <param name="apiKey">API key unique to the user</param>
        /// <param name="videoCategoryId">Numerical ID of which category to query</param>
        /// <returns></returns>
        public static async Task<VideosResponse> GetVideosAsync(string apiKey, string videoCategoryId)
        {
            var response = await GetVideosAsync(apiKey, videoCategoryId, 0);
            return response;
        }

        /// <summary>
        /// Pulls a list of videos from Giant Bomb
        /// </summary>
        /// <param name="apiKey">API key unique to the user</param>
        /// <param name="videoCategoryId">Numerical ID of which category to query</param>
        /// <param name="offset">If viewing multiple pages of videos, how many videos to skip in the query</param>
        /// <returns></returns>
        public static async Task<VideosResponse> GetVideosAsync(string apiKey, string videoCategoryId, int offset)
        {
            VideosResponse response = null;

            try
            {
                string categoryParameter = string.Empty;
                if (!String.IsNullOrWhiteSpace(videoCategoryId))
                {
                    // Despite "video_type" being deprecated, the filter is still "video_type"
                    categoryParameter = "&video_type=" + videoCategoryId;
                }

                string offsetParameter = string.Empty;
                if (offset > 0)
                {
                    offsetParameter = "&offset=" + offset;
                }

                var uri = new Uri("https://www.giantbomb.com/api/videos/?format=json&api_key=" + apiKey + categoryParameter + offsetParameter);
                response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<VideosResponse>(uri);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error pulling videos with category ID " + videoCategoryId + " and offset " + offset);
            }

            return response;
        }

        /// <summary>
        /// Pulls a list of video categories
        /// </summary>
        /// <param name="apiKey">API key unique to the user</param>
        /// <returns></returns>
        public static async Task<CategoriesResponse> GetVideoCategoriesAsync(string apiKey)
        {
            CategoriesResponse response = null;

            try
            {
                var uri = new Uri("https://www.giantbomb.com/api/video_categories/?format=json&api_key=" + apiKey);
                response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<CategoriesResponse>(uri);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error pulling video categories");
            }

            return response;
        }

        /// <summary>
        /// Checks to see if there's a livestream running, and returns the stream info if available
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static async Task<LiveStreamResponse> GetLiveStreamAsync(string apiKey)
        {
            LiveStreamResponse response = null;

            try
            {
                var uri = new Uri("https://www.giantbomb.com/api/video/current-live/?format=json&api_key=" + apiKey);
                response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<LiveStreamResponse>(uri);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error pulling live stream info");
            }

            return response;
        }

        /// <summary>
        /// Looks for videos matching a query
        /// </summary>
        /// <param name="apiKey">API key unique to the user</param>
        /// <param name="query">What to search for</param>
        /// <returns></returns>
        public static async Task<VideosResponse> GetVideoSearchResultsAsync(string apiKey, string query)
        {
            var response = await GetVideoSearchResultsAsync(apiKey, query, 0);
            return response;
        }

        /// <summary>
        /// Looks for videos matching a query
        /// </summary>
        /// <param name="apiKey">API key unique to the user</param>
        /// <param name="query">What to search for</param>
        /// <param name="pageNumber">Which page of results to view</param>
        /// <returns></returns>
        public static async Task<VideosResponse> GetVideoSearchResultsAsync(string apiKey, string query, int pageNumber)
        {
            VideosResponse response = null;

            string pageNumberParameter = string.Empty;
            if (pageNumber > 1)
            {
                pageNumberParameter = "&page=" + pageNumber;
            }

            try
            {
                var uri = new Uri("https://www.giantbomb.com/api/search/?format=json&api_key=" + apiKey + "&query=" + query + "&resources=video" + pageNumberParameter);
                response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<VideosResponse>(uri);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error finding search results for query " + query + " and page number " + pageNumber);
            }

            return response;
        }
    }
}