using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace GiantBombUnofficialClassic.Utilities
{
    /// <summary>
    /// For some reason, it looks like Windows is holding onto the video files this app streams and not immediately deleting them.
    /// As Giant Bomb's videos are huge, the Windows policy of "clean up every 30 days" isn't gonna work!
    /// 
    /// Also, I don't know why, but this doesn't seem to apply for Xbox users. The Xbox is only caching images.
    /// </summary>
    public class INetCacheCleanupManager
    {
        public static void DeleteOldDatFiles(TimeSpan howLongToKeepFiles)
        {
            try
            {
                var oldestValidDateForCachedFiles = (DateTime.Now - howLongToKeepFiles);

                if (oldestValidDateForCachedFiles < DateTime.Now)
                {
                    var iNetCacheDirectory = (ApplicationData.Current.LocalFolder.Path + "\\..\\ac\\inetcache");

                    if (Directory.Exists(iNetCacheDirectory))
                    {
                        var iNetCacheSubDirectories = Directory.GetDirectories(iNetCacheDirectory);

                        foreach (string directory in iNetCacheSubDirectories)
                        {
                            string[] cachedFiles = Directory.GetFiles(directory, "*.dat");

                            foreach (string file in cachedFiles)
                            {
                                var lastAccessTime = File.GetLastAccessTime(file);
                                if (lastAccessTime < oldestValidDateForCachedFiles)
                                {
                                    try
                                    {
                                        File.Delete(file);
                                    }
                                    catch (Exception ex)
                                    {
                                        Serilog.Log.Error("Unable to delete this cached file: " + file, ex);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Serilog.Log.Error("Cache directory does not exist: " + iNetCacheDirectory);
                    }
                }
                else
                {
                    Serilog.Log.Error("Cache expiration date is not valid: " + oldestValidDateForCachedFiles);
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error cleaning up cache: " + ex);
            }
        }
    }
}