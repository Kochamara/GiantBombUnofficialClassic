using GiantBombUnofficialClassic.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input;
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
        }

        /// <summary>
        /// Gamepad shortcuts for media playback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (args != null)
            {
                switch (args.VirtualKey)
                {
                    case Windows.System.VirtualKey.GamepadMenu:
                    case Windows.System.VirtualKey.GamepadX:
                    case Windows.System.VirtualKey.GamepadY:
                        if (this.VideoContainer.MediaPlayer.PlaybackSession.PlaybackState == Windows.Media.Playback.MediaPlaybackState.Paused)
                        {
                            this.VideoContainer.MediaPlayer.Play();
                        }
                        else
                        {
                            this.VideoContainer.MediaPlayer.Pause();
                        }
                        args.Handled = true;
                        break;
                    case Windows.System.VirtualKey.GamepadDPadRight:
                    case Windows.System.VirtualKey.GamepadRightShoulder:
                    case Windows.System.VirtualKey.GamepadRightTrigger:
                        this.VideoContainer.MediaPlayer.PlaybackSession.Position += TimeSpan.FromSeconds(30);
                        args.Handled = true;
                        break;
                    case Windows.System.VirtualKey.GamepadDPadLeft:
                    case Windows.System.VirtualKey.GamepadLeftShoulder:
                    case Windows.System.VirtualKey.GamepadLeftTrigger:
                        this.VideoContainer.MediaPlayer.PlaybackSession.Position -= TimeSpan.FromSeconds(10);
                        args.Handled = true;
                        break;
                    default:
                        break;
                }
            }
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
                VideoContainer.SetMediaPlayer(_context.Player);
                _context.Video = e.Parameter as GiantBombApi.Models.Video;
                _context.InitializeAsync();

                if (Utilities.SystemInformationManager.IsTenFootExperience)
                {
                    Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
                }
                else
                {
                    VideoContainer.DoubleTapped += MediaPlayer_DoubleTapped;
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _context.PlaybackPositionReportingCancellationToken.Cancel();
            VideoContainer.MediaPlayer.Dispose();
            if (Utilities.SystemInformationManager.IsTenFootExperience)
            {
                Window.Current.CoreWindow.KeyUp -= CoreWindow_KeyUp;
            }
        }
    }
}