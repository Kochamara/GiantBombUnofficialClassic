using GiantBombUnofficialClassic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.Services
{
    public class ApiKeyManager
    {
        private static ApiKeyManager _instance;
        private SettingsManager _settingsManager;
        private string _currentKey;
        public const string ApiKeySettingName = "ApiKey";

        public static ApiKeyManager GetInstance()
        {
            return _instance ?? (_instance = new ApiKeyManager());
        }

        /// <summary>
        /// Handles storage and retrieval of the user-specific API key needed to show Giant Bomb content
        /// </summary>
        private ApiKeyManager()
        {
            _settingsManager = new SettingsManager();
        }

        public string GetSavedApiKey()
        {
            if (String.IsNullOrWhiteSpace(_currentKey))
            {
                _currentKey = (string)_settingsManager.GetSetting(ApiKeySettingName);
            }
            return _currentKey;
        }

        public void SaveNewApiKey(string apiKey)
        {
            // TODO: Validate the API key works before saving.
            _settingsManager.SaveSetting(ApiKeySettingName, apiKey);
            _currentKey = apiKey;
        }
    }
}
