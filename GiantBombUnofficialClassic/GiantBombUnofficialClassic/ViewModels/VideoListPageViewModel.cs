using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GiantBombApi.Models;
using GiantBombUnofficialClassic.Services;
using GiantBombUnofficialClassic.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Media.Playback;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class VideoListPageViewModel : ViewModelBase
    {
        private NavigationManager _navigationManager;
        private string _apiKey;

        public VideoListPageViewModel()
        {
            _videos = new ObservableCollection<BasicViewModel>();
        }

        public async Task InitializeAsync()
        {
            ShowJeff = true;

            _navigationManager = NavigationManager.GetInstance();
            _apiKey = ApiKeyManager.GetInstance().GetSavedApiKey();
            VideosResponse response = null;

            if ((this.Category == null) || (String.IsNullOrWhiteSpace(this.Category.Id)))
            {
                response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideosAsync(_apiKey);
            }
            else
            {
                response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideosAsync(_apiKey, Category.Id);
            }
            
            if ((response != null) && (response.Status == StatusCode.OK) && (response.Results != null))
            {
                foreach (var video in response.Results)
                {
                    _videos.Add(new VideoViewModel
                    {
                        Title = video.Name,
                        ImageLocation = new Uri(video.Image.MediumUrl),
                        HdUri = new Uri(video.HdUrl)
                    });
                }
            }

            ShowJeff = false;
        }

        public ObservableCollection<BasicViewModel> Videos
        {
            get { return _videos; }
        }
        private ObservableCollection<BasicViewModel> _videos;

        public VideoType Category
        {
            get
            {
                return _category;
            }

            set
            {
                if (_category != value)
                {
                    _category = value;
                    RaisePropertyChanged(() => Category);
                }
            }
        }
        private VideoType _category;

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
