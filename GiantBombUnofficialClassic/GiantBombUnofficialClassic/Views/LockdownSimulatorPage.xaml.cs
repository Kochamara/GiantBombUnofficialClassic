using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
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
        private Windows.System.Display.DisplayRequest _displayRequest;
        private MediaPlayer _mediaPlayer;

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
            PlayBackgroundAudio();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            try
            {
                _mediaPlayer.Dispose();
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
        private void PlayBackgroundAudio()
        {
            try
            {
                AudioPlayer = new MediaPlayerElement();

                MediaSource mediaSource = MediaSource.CreateFromUri(new Uri(@"ms-appx:///Assets//Lockdown.mp3"));
                MediaPlaybackItem playbackItem = new MediaPlaybackItem(mediaSource);

                _mediaPlayer = new MediaPlayer();
                _mediaPlayer.IsLoopingEnabled = true;
                _mediaPlayer.Source = playbackItem;
                AudioPlayer.SetMediaPlayer(_mediaPlayer);
                _mediaPlayer.Play();
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Error playing Lockdown audio");
            }
        }
    }
}