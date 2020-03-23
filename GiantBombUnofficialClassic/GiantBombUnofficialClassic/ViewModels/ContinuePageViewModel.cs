using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GiantBombApi.Models;
using GiantBombUnofficialClassic.Services;
using GiantBombUnofficialClassic.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class ContinuePageViewModel : ViewModelBase
    {
        private NavigationManager _navigationManager;
        private string _apiKey;
        private int _numberOfVideosCurrentlyShown;

        private const int FINISHED = 100;
        
        public ContinuePageViewModel()
        {
            _continueVideos = new ObservableCollection<VideoViewModel>();
            _finishedVideos = new ObservableCollection<VideoViewModel>();
        }

        public async Task InitializeAsync()
        {
            _navigationManager = NavigationManager.GetInstance();
            _apiKey = ApiKeyManager.GetInstance().GetSavedApiKey();
            await LoadVideosAsync(true);
        }

        public async Task LoadVideosAsync(bool isFirstTimeLoadingVideos)
        {
            try
            {
                if (isFirstTimeLoadingVideos)
                {
                    IsLoading = true;
                    FoundVideos = false;
                    _continueVideos.Clear();
                    _finishedVideos.Clear();
                }
                else
                {
                    AreAdditionalResultsBeingLoaded = true;
                }

                // Get the saved video times and ids for the user account
                AllPlaybackPositionsResponse allSavedTimes = await GiantBombApi.Services.VideoPlaybackPositionAgent.GetAllPlaybackPositionsListAsync(_apiKey);

                foreach (PlaybackPosition savedTimne in allSavedTimes?.SavedTimes)
                {
                    // Using the id for each saved video, retrieve the video info
                    VideosResponse response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideoContinueAsync(_apiKey, savedTimne.Id);

                    if ((response != null) && (response.Status == StatusCode.OK) && (response.Results != null) && (response.Results.Count() > 0))
                    {
                        FoundVideos = true;
                        foreach (Video video in response.Results)
                        {
                            int percentComplete = GiantBombApi.Services.VideoPlaybackPositionAgent.GetPlaybackPercentageComplete(video);
                            VideoViewModel viewModel = new VideoViewModel()
                            {
                                Id = video.Id,
                                Title = video.Name,
                                Description = video.Deck,
                                Source = video,
                                PercentageComplete = percentComplete
                            };

                            if (video.Image != null)
                            {
                                if (!String.IsNullOrWhiteSpace(video.Image.SuperUrl))
                                {
                                    viewModel.ImageLocation = new Uri(video.Image.SuperUrl);
                                }
                                else if (!String.IsNullOrWhiteSpace(video.Image.MediumUrl))
                                {
                                    viewModel.ImageLocation = new Uri(video.Image.MediumUrl);
                                }
                                else if (!String.IsNullOrWhiteSpace(video.Image.SmallUrl))
                                {
                                    viewModel.ImageLocation = new Uri(video.Image.SmallUrl);
                                }
                            }

                            // Determine if the video is 100% completed and should appear in the Finished section of the Continue Videos page
                            if (percentComplete < FINISHED)
                            {
                                _continueVideos.Add(viewModel);
                            }
                            else
                            {
                                _finishedVideos.Add(viewModel);
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Serilog.Log.Information(e, "Error Loading videos");
            }

            AreAdditionalResultsBeingLoaded = false;
            IsLoading = false;
        }

        public ObservableCollection<VideoViewModel> ContinueVideos
        {
            get { return _continueVideos; }
        }
        private ObservableCollection<VideoViewModel> _continueVideos;

        public ObservableCollection<VideoViewModel> FinishedVideos
        {
            get { return _finishedVideos; }
        }
        private ObservableCollection<VideoViewModel> _finishedVideos;


        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                if(_isLoading != value)
                {
                    _isLoading = value;
                    RaisePropertyChanged("IsLoading");
                }
            }
        }
        private bool _isLoading;

        /// <summary>
        /// Whether an additional page of results is being loaded. Should not hide existing results.
        /// </summary>
        public bool AreAdditionalResultsBeingLoaded
        {
            get
            {
                return _areAdditionalResultsBeingLoaded;
            }

            set
            {
                if (_areAdditionalResultsBeingLoaded != value)
                {
                    _areAdditionalResultsBeingLoaded = value;
                    RaisePropertyChanged("AreAdditionalResultsBeingLoaded");
                }
            }
        }
        private bool _areAdditionalResultsBeingLoaded;

        public bool FoundVideos
        {
            get
            {
                return _foundVideos;
            }

            set
            {
                if (_foundVideos != value)
                {
                    _foundVideos = value;
                    RaisePropertyChanged("FoundVideos");
                }
            }
        }
        private bool _foundVideos;

        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new RelayCommand(
                () =>
                {
                    var unawaitedTask = LoadVideosAsync(true);
                }));
            }
        }
        private RelayCommand _refreshCommand;
    }
}
