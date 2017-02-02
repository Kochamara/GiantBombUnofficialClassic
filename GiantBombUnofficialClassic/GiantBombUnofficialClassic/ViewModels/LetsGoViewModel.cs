using GalaSoft.MvvmLight;
using GiantBombApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class LetsGoViewModel : ViewModelBase
    {
        public LetsGoViewModel()
        {
            dataItems = new ObservableCollection<BasicViewModel>();
        }

        public async Task InitializeAsync()
        {
            ShowJeff = true;

            var response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideosAsync(Constants.ApiKey);
            if ((response != null) && (response.Status == StatusCode.OK) && (response.Results != null) && (response.Results.Count() > 0))
            {
                foreach (var video in response.Results)
                {
                    dataItems.Add(new BasicViewModel()
                    {
                        Title = video.Name,
                        ImageLocation = new Uri(video.Image.MediumUrl)
                    });
                }
            }

            var uri = new Uri("http://v.giantbomb.com/video/video_thing_082008_3500.mp4?api_key=" + Constants.ApiKey);

            //var _mediaPlayer = new MediaPlayer();
            VideoSource = Windows.Media.Core.MediaSource.CreateFromUri(uri);
            //_mediaPlayer.Play();
            //_mediaPlayerElement.SetMediaPlayer(_mediaPlayer);

            ShowJeff = false;
        }

        private ObservableCollection<BasicViewModel> dataItems;
        public ObservableCollection<BasicViewModel> Videos
        {
            get { return dataItems; }
        }

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

        public bool ShowJeff
        {
            get
            {
                return _showJeff;
            }

            set
            {
                if (_showJeff != value)
                {
                    _showJeff = value;
                    RaisePropertyChanged(() => ShowJeff);
                }
            }
        }
        private bool _showJeff;
    }
}
