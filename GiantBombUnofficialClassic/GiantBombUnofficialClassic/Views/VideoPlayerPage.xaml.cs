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
                this.MediaPlayer.DoubleTapped += MediaPlayer_DoubleTapped;
            }
        }

        private void MediaPlayer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            MediaPlayer.IsFullWindow = !MediaPlayer.IsFullWindow;
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
            MediaPlayer.MediaPlayer.Dispose();
        }
    }
}