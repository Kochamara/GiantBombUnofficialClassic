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
        private ApplicationDataContainer _roamingSettings;

        public SettingsManager()
        {
            _roamingSettings = ApplicationData.Current.RoamingSettings;
        }

        public void SaveSetting(string key, object value)
        {
            _roamingSettings.Values[key] = value;
        }

        public object GetSetting(string key)
        {
            object value = null;

            try
            {
                value = _roamingSettings.Values[key];
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error locating setting with key " + key);
            }

            return value;
        }

        public void DeleteSetting(string key)
        {
            _roamingSettings.Values.Remove(key);
        }
    }
}
