using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GiantBombApi.Models;
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

        public MainPageViewModel()
        {
            _videos = new ObservableCollection<BasicViewModel>();
        }
        
        public async Task InitializeAsync()
        {
            ShowJeff = true;

            _navigationManager = NavigationManager.GetInstance();

            var response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideosAsync(Constants.ApiKey);
            if ((response != null) && (response.Status == StatusCode.OK) && (response.Results != null) && (response.Results.Count() > 0))
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
