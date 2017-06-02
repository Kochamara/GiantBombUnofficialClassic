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
            var response = await GetVideosAsync(apiKey, 0, string.Empty, GroupingType.None);
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
            var response = await GetVideosAsync(apiKey, offset, string.Empty, GroupingType.None);
            return response;
        }

        /// <summary>
        /// Pulls a list of videos from Giant Bomb
        /// </summary>
        /// <param name="apiKey">API key unique to the user</param>
        /// <param name="videoCategoryId">Numerical ID of which category to query</param>
        /// <returns></returns>
        public static async Task<VideosResponse> GetVideosAsync(string apiKey, string videoGroupingId, GroupingType grouping)
        {
            var response = await GetVideosAsync(apiKey, 0, videoGroupingId, grouping);
            return response;
        }

        /// <summary>
        /// Pulls a list of videos from Giant Bomb
        /// </summary>
        /// <param name="apiKey">API key unique to the user</param>
        /// <param name="videoGroupingId">Numerical ID of which grouping to query</param>
        /// <param name="grouping">Whether the group is a category or a show</param>
        /// <param name="offset">If viewing multiple pages of videos, how many videos to skip in the query</param>
        /// <returns></returns>
        public static async Task<VideosResponse> GetVideosAsync(string apiKey, int offset, string videoGroupingId, GroupingType grouping)
        {
            VideosResponse response = null;

            try
            {
                string categoryParameter = string.Empty;
                if (!String.IsNullOrWhiteSpace(videoGroupingId))
                {
                    switch (grouping)
                    {
                        case GroupingType.Category:
                            categoryParameter = "&filter=video_categories:" + videoGroupingId;
                            break;
                        case GroupingType.Show:
                            categoryParameter = "&filter=video_show:" + videoGroupingId;
                            break;
                        case GroupingType.None:
                            break;
                    }
                }

                string offsetParameter = string.Empty;
                if (offset > 0)
                {
                    offsetParameter = "&offset=" + offset;
                }

                var uri = new Uri("https://www.giantbomb.com/api/videos/?format=json&api_key=" + apiKey + categoryParameter + offsetParameter);
                response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<VideosResponse>(uri);
                response = RemoveInvalidVideos(response);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error pulling videos with category ID " + videoGroupingId + " and offset " + offset);
            }

            return response;
        }

        /// <summary>
        /// Pulls a list of video categories
        /// </summary>
        /// <param name="apiKey">API key unique to the user</param>
        /// <returns></returns>
        public static async Task<VideoGroupingsResponse> GetVideoCategoriesAsync(string apiKey)
        {
            VideoGroupingsResponse response = null;

            try
            {
                var uri = new Uri("https://www.giantbomb.com/api/video_categories/?format=json&api_key=" + apiKey);
                response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<VideoGroupingsResponse>(uri);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error pulling video categories");
            }

            return response;
        }

        /// <summary>
        /// Pulls a list of video shows
        /// </summary>
        /// <param name="apiKey">API key unique to the user</param>
        /// <returns></returns>
        public static async Task<VideoGroupingsResponse> GetVideoShowsAsync(string apiKey)
        {
            VideoGroupingsResponse response = null;

            try
            {
                var uri = new Uri("https://www.giantbomb.com/api/video_shows/?format=json&api_key=" + apiKey);
                response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<VideoGroupingsResponse>(uri);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error pulling video shows");
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


            // TEST - REMOVE BEFORE BUILD
            //response = new LiveStreamResponse()
            //{
            //    Success = "true",
            //    Stream = new LiveStream()
            //    {
            //        Title = "[FAKE] Bring Your B-Game: The Force Unleashed",
            //        Image = "static.giantbomb.com/uploads/original/21/211414/2941316-star-wars-force-unleashed.jpg",
            //        StreamSource = "https://giantbomb-pdl.akamaized.net/2017/05/26/mc_gbwpd_vanquish_052517_wp3gvf9t_4000.mp4"
            //    }
            //};
            

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
                response = RemoveInvalidVideos(response);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error finding search results for query " + query + " and page number " + pageNumber);
            }

            return response;
        }

        /// <summary>
        /// Removes all items where there's no video URL or name
        /// </summary>
        /// <param name="videos"></param>
        /// <returns></returns>
        public static VideosResponse RemoveInvalidVideos(VideosResponse videos)
        {
            if ((videos != null) && (videos.Results != null) && (videos.Results.Count > 0))
            {
                videos.Results.RemoveAll(item => (
                String.IsNullOrWhiteSpace(item.HdUrl + item.HighUrl + item.LowUrl) ||
                String.IsNullOrWhiteSpace(item.Name)));
            }
            return videos;
        }
    }
}