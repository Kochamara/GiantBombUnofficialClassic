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
    public class MainPageViewModel : ViewModelBase
    {
        private NavigationManager _navigationManager;
        private string _apiKey;

        public MainPageViewModel()
        {
            _videos = new ObservableCollection<BasicViewModel>();
        }
        
        public async Task InitializeAsync()
        {
            ShowJeff = true;

            _navigationManager = NavigationManager.GetInstance();
            _apiKey = ApiKeyManager.GetInstance().GetSavedApiKey();

            var response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideosAsync(_apiKey);
            if ((response != null) && (response.Status == StatusCode.OK) && (response.Results != null))
            {
                var videos = response.Results as IEnumerable<Video>;
                foreach (var video in videos)
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
