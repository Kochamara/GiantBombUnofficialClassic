using GalaSoft.MvvmLight;
using GiantBombApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public async void InitializeAsync()
        {
            var videoUriManager = Services.VideoUriManager.GetInstance();
            var videoUri = videoUriManager.GetAppropriateVideoUri(Video);
            Player.Source = Windows.Media.Core.MediaSource.CreateFromUri(videoUri);

            await SkipAheadToPreviousPositionAsync();
            await ReportPlaybackPositionAsync(PlaybackPositionReportingCancellationToken.Token);
        }
        
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
            //TODO: Sometimes I think this is getting called before the video is even loaded

            var apiKey = Services.ApiKeyManager.GetInstance().GetSavedApiKey();
            var previouslySavedPosition = await GiantBombApi.Services.VideoPlaybackPositionAgent.GetPlaybackPositionAsync(apiKey, Video.Id);
            // Don't skip if the previous position was in the first 15 seconds of the video
            if (previouslySavedPosition > 15)
            {
                // Don't skip if the previous position was in the last 30 seconds of the video
                double furthestPositionInSecondsToJumpTo = (Player.PlaybackSession.NaturalDuration.TotalSeconds - 30);

                if (previouslySavedPosition < furthestPositionInSecondsToJumpTo)
                {
                    Player.PlaybackSession.Position = TimeSpan.FromSeconds(previouslySavedPosition);
                }
            }
        }

        private async Task SaveCurrentPositionAsync()
        {
            var apiKey = Services.ApiKeyManager.GetInstance().GetSavedApiKey();
            bool success = await GiantBombApi.Services.VideoPlaybackPositionAgent.SetPlaybackPositionAsync(apiKey, Video.Id, (int)Player.PlaybackSession.Position.TotalSeconds);
        }

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
                    RaisePropertyChanged("Video");
                }
            }
        }
        private Video _video;

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
                    RaisePropertyChanged("Player");
                }
            }
        }
        private MediaPlayer _player;
    }
}
