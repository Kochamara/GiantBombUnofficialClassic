using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.Utilities
{
    public class SystemInformationManager
    {
        // Calculate this once and cache the result.
        private static bool _isXbox = (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Xbox");

        // For now, the 10-foot experience is enabled only on Xbox.
        public static bool IsTenFootExperience => _isXbox;

        // Just for testing. Don't forget to change this back!
        //public static bool IsTenFootExperience => true;
    }
}