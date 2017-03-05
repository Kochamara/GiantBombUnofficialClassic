using GiantBombUnofficialClassic.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class VideoPlayerPage : Page
    {
        public const string PageKey = "VideoPlayerPage";
        private VideoPlayerViewModel _context;

        public VideoPlayerPage()
        {
            this.InitializeComponent();
            _context = new VideoPlayerViewModel();
            this.DataContext = _context;

          //  VideoContainer.MediaPlayer.PlaybackSession.SeekCompleted += PlaybackSession_SeekCompleted;
         //   VideoContainer.MediaPlayer.PlaybackSession. += PlaybackSession_PositionChanged;


            if (!Utilities.SystemInformationManager.IsTenFootExperience)
            {
                VideoContainer.DoubleTapped += MediaPlayer_DoubleTapped;
            }
        }

        private void PlaybackSession_PositionChanged(Windows.Media.Playback.MediaPlaybackSession sender, object args)
        {
        }

        private void PlaybackSession_SeekCompleted(Windows.Media.Playback.MediaPlaybackSession sender, object args)
        {
        }

        private void MediaPlayer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            VideoContainer.IsFullWindow = !VideoContainer.IsFullWindow;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if ((e != null) && (e.Parameter != null))
            {
                var videoSource = e.Parameter as Uri;
                _context.VideoSource = Windows.Media.Core.MediaSource.CreateFromUri(videoSource);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            VideoContainer.MediaPlayer.Dispose();
        }

        private void CustomMediaTransportControls_SkipForward30(object sender, EventArgs e)
        {
            VideoContainer.MediaPlayer.PlaybackSession.Position += TimeSpan.FromSeconds(30);
        }

        private void CustomMediaTransportControls_SkipBack10(object sender, EventArgs e)
        {
            VideoContainer.MediaPlayer.PlaybackSession.Position -= TimeSpan.FromSeconds(10);
        }

        private void CustomMediaTransportControls_SliderManipulationCompleted(object sender, EventArgs e)
        {
            VideoContainer.MediaPlayer.Play();

        }

        private void CustomMediaTransportControls_SliderManipulationStarted(object sender, EventArgs e)
        {
            if (VideoContainer.MediaPlayer.PlaybackSession.CanPause)
            {
                VideoContainer.MediaPlayer.Pause();
            }

        }
    }
}