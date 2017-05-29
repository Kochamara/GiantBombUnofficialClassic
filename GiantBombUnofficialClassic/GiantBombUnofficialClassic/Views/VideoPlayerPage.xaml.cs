using GiantBombApi.Models;
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
        private Windows.System.Display.DisplayRequest _displayRequest;

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
                try
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
                catch (Exception e)
                {
                    Serilog.Log.Error("Exception thrown trying to use gamepad shortcut", e);
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

                if (e.Parameter is Video)
                {
                    _context.Video = e.Parameter as Video;
                }
                else if (e.Parameter is LiveStream)
                {
                    _context.LiveStream = e.Parameter as LiveStream;
                }

                _context.InitializeAsync();

                if (Utilities.SystemInformationManager.IsTenFootExperience)
                {
                    Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
                }
                else
                {
                    VideoContainer.DoubleTapped += MediaPlayer_DoubleTapped;
                    ActivateDisplay();
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
            else
            {
                ReleaseDisplay();
            }
        }

        #region Screen Locking/Sleeping Handling
        // More information on this behavior is documented here: https://msdn.microsoft.com/en-us/library/windows/apps/xaml/jj152728.aspx

        /// <summary>
        /// To prevent screens going to sleep or locking during video playback, send an activation request.
        /// </summary>
        public void ActivateDisplay()
        {
            try
            {
                // Create the request instance if needed
                if (_displayRequest == null)
                {
                    _displayRequest = new Windows.System.Display.DisplayRequest();
                }

                // Make request to put in active state
                _displayRequest.RequestActive();
            }
            catch (Exception e)
            {
                Serilog.Log.Information(e, "Error sending display request");
            }
        }

        /// <summary>
        /// Once video playback has ended, release the display so Windows can handle the display state normally.
        /// </summary>
        public void ReleaseDisplay()
        {
            try
            {
                if (_displayRequest != null)
                {
                    _displayRequest.RequestRelease();
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Information(e, "Error releasing display");
            }
        }
        #endregion
    }
}