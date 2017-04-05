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

            if (!Utilities.SystemInformationManager.IsTenFootExperience)
            {
                VideoContainer.DoubleTapped += MediaPlayer_DoubleTapped;
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
            }
        }
        
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _context.PlaybackPositionReportingCancellationToken.Cancel();
            VideoContainer.MediaPlayer.Dispose();
        }
    }
}