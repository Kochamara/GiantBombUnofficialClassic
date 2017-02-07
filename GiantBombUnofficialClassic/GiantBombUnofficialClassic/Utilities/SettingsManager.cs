using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace GiantBombUnofficialClassic.Utilities
{
    public class SettingsManager
    {
        private ApplicationDataContainer _localSettings;
        public const string PrimaryContainerName = "Settings";

        /// <summary>
        /// feb 6: this uh, this isn't working.
        /// </summary>
        public SettingsManager()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
        }

        public void SetKey(string key, object value)
        {
            _localSettings.CreateContainer(PrimaryContainerName, Windows.Storage.ApplicationDataCreateDisposition.Always);

            if (_localSettings.Containers[PrimaryContainerName].Values.ContainsKey(key))
            {
                _localSettings.Containers[PrimaryContainerName].Values[key] = value;
            }
        }

        public object GetValue(string key)
        {
            object value = null;
            try
            {
                if (_localSettings.Containers.ContainsKey(PrimaryContainerName))
                {
                    _localSettings.Containers[PrimaryContainerName].Values.TryGetValue(key, out value);
                }
            }
            catch (Exception e)
            {
            }
            return value;
        }
    }
}
