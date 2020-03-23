using GiantBombApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombApi.Services
{
    public class VideoPlaybackPositionAgent
    {
        /// <summary>
        /// Syncs the playback position of a video with Giant Bomb
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="videoId"></param>
        /// <param name="playbackPositionInSeconds"></param>
        /// <returns></returns>
        public static async Task<bool> SetPlaybackPositionAsync(string apiKey, string videoId, int playbackPositionInSeconds)
        {
            bool success = false;

            try
            {
                if ((!String.IsNullOrWhiteSpace(videoId)) && (!String.IsNullOrWhiteSpace(apiKey)) && (playbackPositionInSeconds > 0))
                {
                    var uri = new Uri("https://www.giantbomb.com/api/video/save-time/?format=json&video_id=" + videoId + "&time_to_save=" + playbackPositionInSeconds + "&api_key=" + apiKey);
                    var response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<PlaybackPositionResponse>(uri);

                    success = true;
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error saving playback position");
            }

            return success;
        }


        /// <summary>
        /// Pulls the playback position (in seconds) of a given video directly from the API without validation
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="videoId"></param>
        /// <returns>The previous playback position in seconds. -1 if no value was found.</returns>
        public static async Task<double> GetPlaybackPositionAsync(string apiKey, string videoId)
        {
            double previousPosition = -1;

            try
            {
                if ((!String.IsNullOrWhiteSpace(videoId)) && (!String.IsNullOrWhiteSpace(apiKey)))
                {
                    var uri = new Uri("https://www.giantbomb.com/api/video/get-saved-time/?format=json&video_id=" + videoId + "&api_key=" + apiKey);
                    var response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<PlaybackPositionResponse>(uri);

                    if ((response != null) && (response.SavedTime > 0))
                    {
                        previousPosition = response.SavedTime;
                    }
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error retrieving playback position");
            }

            return previousPosition;
        }

        /// <summary>
        /// Validates the Video object's playback position and returns it in seconds
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public static double GetValidatedPlaybackPosition(Video video)
        {
            double position = 0;

            try
            {
                if ((video != null) && (!String.IsNullOrWhiteSpace(video.SavedTime)) && (!String.IsNullOrWhiteSpace(video.LengthInSeconds)))
                {
                    double savedTime = Convert.ToDouble(video.SavedTime);
                    double videoDuration = Convert.ToDouble(video.LengthInSeconds);
                    double furthestPositionInSecondsToJumpTo = (videoDuration - 30);

                    // Input validation
                    if ((savedTime > 0) && (videoDuration > 0) && (furthestPositionInSecondsToJumpTo > 0))
                    {
                        // Don't skip if the previous position was in the first 15 seconds or the last 30 seconds of the video
                        if ((savedTime > 15) && (savedTime < furthestPositionInSecondsToJumpTo))
                        {
                            position = savedTime;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error validating playback position");
            }

            return position;
        }

        /// <summary>
        /// Returns a percentage from 0 to 100 of how far the user is into a given Video
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public static int GetPlaybackPercentageComplete(Video video)
        {
            int percentageComplete = 0;

            try
            {
                if ((video != null) && (!String.IsNullOrWhiteSpace(video.SavedTime)) && (!String.IsNullOrWhiteSpace(video.LengthInSeconds)))
                {
                    double savedTime = Convert.ToDouble(video.SavedTime);
                    double videoDuration = Convert.ToDouble(video.LengthInSeconds);
                    double furthestPositionInSecondsToJumpTo = (videoDuration - 30);

                    if (savedTime < 30)
                    {
                        percentageComplete = 0;
                    }
                    else if ((videoDuration - savedTime) < 30)
                    {
                        percentageComplete = 100;
                    }
                    else
                    {
                        percentageComplete = (int)((savedTime / videoDuration) * 100);
                    }
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error calculating playback position");
            }
            
            return percentageComplete;
        }


        /// <summary>
        /// Get all the video saved times for the user
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static async Task<Dictionary<string, int>> GetAllPlaybackPositionsAsync(string apiKey)
        {
            var allPlaybackPositions = new Dictionary<string, int>();

            try
            {
                if (!String.IsNullOrWhiteSpace(apiKey))
                {
                    var uri = new Uri("https://www.giantbomb.com/api/video/get-all-saved-times/?format=json&api_key=" + apiKey);
                    var response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<AllPlaybackPositionsResponse>(uri);

                    if ((response != null) && (response.SavedTimes != null) && (response.SavedTimes.Count() > 0))
                    {
                        foreach (var playbackPosition in response.SavedTimes)
                        {
                            allPlaybackPositions.Add(playbackPosition.Id, (int)playbackPosition.SavedTime);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error retrieving all playback positions");
            }

            return allPlaybackPositions;
        }

        /// <summary>
        /// Get all the video saved times for the user and returns the fulll AllPlaybackPositionsResponse object.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static async Task<AllPlaybackPositionsResponse> GetAllPlaybackPositionsListAsync(string apiKey)
        {
            AllPlaybackPositionsResponse allPlaybackPositions = null;

            try
            {
                if (!String.IsNullOrWhiteSpace(apiKey))
                {
                    var uri = new Uri("https://www.giantbomb.com/api/video/get-all-saved-times/?format=json&api_key=" + apiKey);
                    allPlaybackPositions = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<AllPlaybackPositionsResponse>(uri);
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error retrieving all playback positions");
            }

            return allPlaybackPositions;
        }
    }
}