using GalaSoft.MvvmLight;
using GiantBombApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class VideoPlayerViewModel : ViewModelBase
    {
        public CancellationTokenSource PlaybackPositionReportingCancellationToken;

        public VideoPlayerViewModel()
        {
            Player = new MediaPlayer();
            PlaybackPositionReportingCancellationToken = new CancellationTokenSource();
        }

        /// <summary>
        /// This view model handles both archived videos and live streams, but initializes
        /// differently based on either scenario.
        /// </summary>
        public void Initialize()
        {
            var videoUriManager = Services.VideoUriManager.GetInstance();

            if (this.Video != null)
            {
                ShowSystemMediaTransportControls = true;

                // For archived videos, we need to determine which URI to use (HD/high/low quality) and
                // also we should handle sycning playback position with the site.
                var videoUri = videoUriManager.GetAppropriateVideoUri(Video);

                if (videoUri != null)
                {
                    MediaSource mediaSource = MediaSource.CreateFromUri(videoUri);
                    MediaPlaybackItem playbackItem = new MediaPlaybackItem(mediaSource);
                    Player.Source = playbackItem;

                    Player.MediaOpened += Player_MediaOpened;
                }
                else
                {
                    Serilog.Log.Error("Unable to start video without URI");
                }
            }
            else if (this.LiveStream != null)
            {
                ShowSystemMediaTransportControls = true;

                // For live streams, we just need to get the URI with the API key appended to the end of it.
                var videoUri = videoUriManager.GetAppropriateVideoUri(LiveStream);

                if (videoUri != null)
                {
                    MediaSource mediaSource = MediaSource.CreateFromUri(videoUri);
                    MediaPlaybackItem playbackItem = new MediaPlaybackItem(mediaSource);
                    Player.Source = playbackItem;
                }
                else
                {
                    Serilog.Log.Error("Unable to start live stream without URI");
                }
            }
            else
            {
                Serilog.Log.Error("Unable to initialize view model without valid video");
            }
        }

        /// <summary>
        /// Wait until the video is opened before we try to skip ahead to the previous playback position.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Player_MediaOpened(MediaPlayer sender, object args)
        {
            Player.MediaOpened -= Player_MediaOpened;
            var unawaitedPlaybackPositionTask = SkipAheadToPreviousPositionAsync();
            var unawaitedReportPositionTask = ReportPlaybackPositionAsync(PlaybackPositionReportingCancellationToken.Token);
        }

        #region Playback position handling
        private async Task ReportPlaybackPositionAsync(CancellationToken token)
        {
            try
            {
                for (int i = 0; i < 10000; i++)
                {
                    // Only report every 10 seconds
                    await Task.Delay(10000);
                    token.ThrowIfCancellationRequested();
                    await SaveCurrentPositionAsync();
                }
            }
            catch (OperationCanceledException)
            {
                // Expected
            }
            catch (Exception e)
            {
                Serilog.Log.Error("Exception thrown while trying to report playback position", e);
            }

            PlaybackPositionReportingCancellationToken.Dispose();
        }

        private async Task SkipAheadToPreviousPositionAsync()
        {
            var apiKey = Services.ApiKeyManager.GetInstance().GetSavedApiKey();

            // First make an API call to directly request the playback position.
            var previouslySavedPosition = await GiantBombApi.Services.VideoPlaybackPositionAgent.GetPlaybackPositionAsync(apiKey, Video.Id);

            // If nothing's returned, use the value that's bundled directly with the Video object. I believe this value isn't updated
            // as frequently, so it's less accurate. This value frequently is out of sync with the direct API call requesting the position.
            if (previouslySavedPosition <= 0)
            {
                previouslySavedPosition = GiantBombApi.Services.VideoPlaybackPositionAgent.GetValidatedPlaybackPosition(Video);
            }

            if (previouslySavedPosition > 0)
            {
                Player.PlaybackSession.Position = TimeSpan.FromSeconds(previouslySavedPosition);
            }
        }

        private async Task SaveCurrentPositionAsync()
        {
            var apiKey = Services.ApiKeyManager.GetInstance().GetSavedApiKey();
            bool success = await GiantBombApi.Services.VideoPlaybackPositionAgent.SetPlaybackPositionAsync(apiKey, Video.Id, (int)Player.PlaybackSession.Position.TotalSeconds);
        }
        #endregion

        #region Bound properties
        public Video Video
        {
            get
            {
                return _video;
            }

            set
            {
                if (_video != value)
                {
                    _video = value;
                    RaisePropertyChanged(() => Video);
                }
            }
        }
        private Video _video;

        public LiveStream LiveStream
        {
            get
            {
                return _liveStream;
            }

            set
            {
                if (_liveStream != value)
                {
                    _liveStream = value;
                    RaisePropertyChanged(() => LiveStream);
                }
            }
        }
        private LiveStream _liveStream;

        public MediaPlayer Player
        {
            get
            {
                return _player;
            }

            set
            {
                if (_player != value)
                {
                    _player = value;
                    RaisePropertyChanged(() => Player);
                }
            }
        }
        private MediaPlayer _player;

        public bool ShowSystemMediaTransportControls
        {
            get
            {
                return _showSystemMediaTransportControls;
            }

            set
            {
                if (_showSystemMediaTransportControls != value)
                {
                    _showSystemMediaTransportControls = value;
                    RaisePropertyChanged(() => ShowSystemMediaTransportControls);
                }
            }
        }
        private bool _showSystemMediaTransportControls;
        #endregion
    }
}
