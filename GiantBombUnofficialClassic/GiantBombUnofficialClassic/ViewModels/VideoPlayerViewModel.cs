using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class VideoPlayerViewModel : ViewModelBase
    {
        public VideoPlayerViewModel()
        {
            Player = new MediaPlayer();
        }

        //public void SetNewVideoSource(Uri videoLocation)
        //{
        //    VideoSource = Windows.Media.Core.MediaSource.CreateFromUri(videoLocation);
        //    // PlayerElement.SetMediaPlayer(_mediaPlayer);
        //    //_mediaPlayer.Play();
        //}

        public IMediaPlaybackSource VideoSource
        {
            get
            {
                return _videoSource;
            }

            set
            {
                if (_videoSource != value)
                {
                    _videoSource = value;
                    RaisePropertyChanged(() => VideoSource);
                }
            }
        }
        private IMediaPlaybackSource _videoSource;

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
    }
}
