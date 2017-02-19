using GiantBombApi.Models;
using GiantBombUnofficialClassic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.Services
{
    public class VideoUriManager
    {
        private static VideoUriManager _instance;
        private SettingsManager _settingsManager;
        private ApiKeyManager _apiKeyManager;
        private VideoQuality _currentQualityPreference;
        private bool _hasCheckedForPreviousPreference;
        public const string VideoQualitySettingName = "VideoQuality";

        public static VideoUriManager GetInstance()
        {
            return _instance ?? (_instance = new VideoUriManager());
        }

        /// <summary>
        /// Handles storage and retrieval of the user's preferred video quality. Also parses videos to provide correct URIs.
        /// </summary>
        private VideoUriManager()
        {
            _settingsManager = new SettingsManager();
            _apiKeyManager = Services.ApiKeyManager.GetInstance();
        }

        public Uri GetAppropriateVideoUri(GiantBombApi.Models.Video video)
        {
            Uri appropriateUri = null;

            if (video != null)
            {
                var apiKey = _apiKeyManager.GetSavedApiKey();
                var targetQuality = GetPreferredVideoQuality();
                switch (targetQuality)
                {
                    case VideoQuality.HD:
                        if (!String.IsNullOrWhiteSpace(video.HdUrl))
                        {
                            appropriateUri = new Uri(video.HdUrl + "?api_key=" + apiKey);
                            break;
                        }
                        goto case VideoQuality.High;
                    case VideoQuality.High:
                        if (!String.IsNullOrWhiteSpace(video.HighUrl))
                        {
                            appropriateUri = new Uri(video.HighUrl + "?api_key=" + apiKey);
                            break;
                        }
                        goto case VideoQuality.Low;
                    default:
                    case VideoQuality.Low:
                        if (!String.IsNullOrWhiteSpace(video.LowUrl))
                        {
                            appropriateUri = new Uri(video.LowUrl + "?api_key=" + apiKey);
                        }
                        break;
                }
            }

            return appropriateUri;
        }

        public VideoQuality GetPreferredVideoQuality()
        {
            try
            {
                if (!_hasCheckedForPreviousPreference)
                {
                    var setting = _settingsManager.GetSetting(VideoQualitySettingName);
                    if (setting == null)
                    {
                        SetNewQualityPreference(VideoQuality.HD);
                    }
                    else
                    {
                        Enum.TryParse((string)setting, out _currentQualityPreference);
                    }
                    _hasCheckedForPreviousPreference = true;
                }
            }
            catch (Exception e)
            {
                _currentQualityPreference = VideoQuality.HD;
            }

            return _currentQualityPreference;
        }

        public void SetNewQualityPreference(VideoQuality preferredQuality)
        {
            _settingsManager.SaveSetting(VideoQualitySettingName, preferredQuality.ToString());
            _currentQualityPreference = preferredQuality;
        }
    }
}
