using GalaSoft.MvvmLight;
using GiantBombApi.Models;
using GiantBombUnofficialClassic.Models;
using System;
using System.Collections.Generic;
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
            // *ahem*
        }

        public async Task InitializeAsync()
        {
            ShowJeff = true;
            var response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideoThingAsync();
            VideoSource = new MediaEnginePlaybackSource();

        //    VideoSource = new IMediaEnginePlaybackSource(response.Results.First<Video>().HdUrl);
            ShowJeff = false;
        }

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

        public MediaEnginePlaybackSource VideoSource
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
        private MediaEnginePlaybackSource _videoSource;

    }
}
