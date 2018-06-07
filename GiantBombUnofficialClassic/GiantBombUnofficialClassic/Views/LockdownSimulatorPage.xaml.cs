using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GiantBombUnofficialClassic.Views
{
    public sealed partial class LockdownSimulatorPage : Page
    {
        public const string PageKey = "LockdownSimulatorPage";
        private Windows.Storage.Streams.IRandomAccessStream _audioStream;

        /// <summary>
        /// The worst, most necessary page in this dumb app.
        /// </summary>
        public LockdownSimulatorPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var unawaitedTask = PlayBackgroundAudio();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            try
            {
                AudioPlayer.Stop();
                _audioStream.Dispose();
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Error disposing of the random access stream");
            }

            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// It's E3 time! Loop this shit!
        /// </summary>
        /// <returns></returns>
        private async Task PlayBackgroundAudio()
        {
            _audioStream = null;

            try
            {
                AudioPlayer = new MediaElement();
                Windows.Storage.StorageFile file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets//Lockdown.mp3"));

                if ((file != null) && File.Exists(file.Path))
                {
                    _audioStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                    AudioPlayer.SetSource(_audioStream, file.ContentType);
                    AudioPlayer.IsLooping = true;
                    AudioPlayer.Play();
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error playing Lockdown audio");
            }
        }
    }
}
