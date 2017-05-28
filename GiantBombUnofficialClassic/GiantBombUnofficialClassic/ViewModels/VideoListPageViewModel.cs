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
        private int _numberOfVideosCurrentlyShown;
        private const int NumberOfSubHeadersOnMainPage = 2;
        private const int NumberOfSubHeadersOnGroupPage = 4;

        public VideoListPageViewModel()
        {
            _videos = new ObservableCollection<VideoViewModel>();
            _subHeaderVideos = new ObservableCollection<VideoViewModel>();
            _headerVideos = new ObservableCollection<VideoViewModel>();
            _liveBroadcasts = new ObservableCollection<LiveStreamViewModel>();
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
                    _numberOfVideosCurrentlyShown = 0;
                    _videos.Clear();
                    _subHeaderVideos.Clear();
                    _headerVideos.Clear();
                    _liveBroadcasts.Clear();
                }
                else
                {
                    AreAdditionalResultsBeingLoaded = true;
                }

                AdditionalVideosFound = false;
                VideosResponse response = null;
                bool isGroupPage = false;

                if ((this.Group == null) || (String.IsNullOrWhiteSpace(this.Group.Id)))
                {
                    await CheckForLiveStreamAsync();
                    response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideosAsync(_apiKey, _numberOfVideosCurrentlyShown);
                }
                else
                {
                    isGroupPage = true;
                    GroupTitle = Group.Title;
                    GroupDescription = Group.Deck;
                    response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideosAsync(_apiKey, _numberOfVideosCurrentlyShown, Group.Id, Group.GroupingType);
                }

                if ((response != null) && (response.Status == StatusCode.OK) && (response.Results != null) && ((response.Results.Count() > 0)))
                {
                    FoundVideos = true;
                    _numberOfVideosCurrentlyShown += response.Results.Count();

                    if (response.NumberOfTotalResults > _numberOfVideosCurrentlyShown)
                    {
                        AdditionalVideosFound = true;
                    }
                    else
                    {
                        AdditionalVideosFound = false;
                    }

                    foreach (var video in response.Results)
                    {
                        var viewModel = new VideoViewModel()
                        {
                            Id = video.Id,
                            Title = video.Name,
                            Description = video.Deck,
                            Source = video
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

                        _videos.Add(viewModel);
                    }
                }

                // Add in the header and subheader view models
                if (isFirstTimeLoadingVideos)
                {
                    if (isGroupPage)
                    {
                        // Group page style, no header. Don't show anything unless we have a full
                        // second row (3 wide) of videos.
                        if (_videos.Count > (NumberOfSubHeadersOnGroupPage + 3))
                        {
                            var subHeaderViewModels = _videos.Take(NumberOfSubHeadersOnGroupPage);
                            foreach (var video in subHeaderViewModels)
                            {
                                _subHeaderVideos.Add(video);
                            }

                            for (int i = 0; i < NumberOfSubHeadersOnGroupPage; i++)
                            {
                                _videos.RemoveAt(0);
                            }
                        }
                    }
                    else
                    {
                        // Main page style, includes header. Don't show anything unless we have a full
                        // third row (3 wide) of videos.
                        if (_videos.Count > (NumberOfSubHeadersOnMainPage + 4))
                        {
                            if ((LiveBroadcasts == null) || (LiveBroadcasts.Count == 0))
                            {
                                _headerVideos.Add(_videos.First());
                                _videos.RemoveAt(0);
                            }

                            var subHeaderViewModels = _videos.Take(NumberOfSubHeadersOnMainPage);
                            foreach (var video in subHeaderViewModels)
                            {
                                _subHeaderVideos.Add(video);
                            }

                            for (int i = 0; i < NumberOfSubHeadersOnMainPage; i++)
                            {
                                _videos.RemoveAt(0);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Information(e, "Error loading videos");
            }

            AreAdditionalResultsBeingLoaded = false;
            IsLoading = false;
        }

        private async Task<bool> CheckForLiveStreamAsync()
        {
            bool success = false;

            try
            {
                var liveStream = await GiantBombApi.Services.VideoRetrievalAgent.GetLiveStreamAsync(_apiKey);
                if ((liveStream != null) && (liveStream.Stream != null) && (!String.IsNullOrWhiteSpace(liveStream.Stream.StreamSource)))
                {
                    var liveBroadcast = new LiveStreamViewModel()
                    {
                        Source = liveStream.Stream,
                        Title = liveStream.Stream.Title,
                    };

                    if (!String.IsNullOrWhiteSpace(liveStream.Stream.Image))
                    {
                        var imageLocation = liveStream.Stream.Image;
                        imageLocation = imageLocation.Trim();

                        if (!imageLocation.StartsWith("http"))
                        {
                            // The image is sometimes returned as "static.giantbomb.com/something.jpg", which 
                            // confuses the URI, so we need to manually add the "https://"
                            imageLocation = "https://" + imageLocation;
                        }

                        liveBroadcast.ImageLocation = new Uri(imageLocation);

                        _liveBroadcasts.Add(liveBroadcast);
                    }

                    success = true;
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Information(e, "Error loading live stream");
            }

            return success;
        }

        #region Bound Properties
        public ObservableCollection<LiveStreamViewModel> LiveBroadcasts
        {
            get { return _liveBroadcasts; }
        }
        private ObservableCollection<LiveStreamViewModel> _liveBroadcasts;

        public ObservableCollection<VideoViewModel> Videos
        {
            get { return _videos; }
        }
        private ObservableCollection<VideoViewModel> _videos;

        public ObservableCollection<VideoViewModel> SubHeaderVideos
        {
            get { return _subHeaderVideos; }
        }
        private ObservableCollection<VideoViewModel> _subHeaderVideos;

        public ObservableCollection<VideoViewModel> HeaderVideos
        {
            get { return _headerVideos; }
        }
        private ObservableCollection<VideoViewModel> _headerVideos;

        public VideoGrouping Group
        {
            get
            {
                return _group;
            }

            set
            {
                if (_group != value)
                {
                    _group = value;
                    RaisePropertyChanged("Group");
                }
            }
        }
        private VideoGrouping _group;

        public string GroupTitle
        {
            get
            {
                return _groupTitle;
            }

            set
            {
                if (_groupTitle != value)
                {
                    _groupTitle = value;
                    RaisePropertyChanged("GroupTitle");
                }
            }
        }
        private string _groupTitle;

        public string GroupDescription
        {
            get
            {
                return _groupDescription;
            }

            set
            {
                if (_groupDescription != value)
                {
                    _groupDescription = value;
                    RaisePropertyChanged("GroupDescription");
                }
            }
        }
        private string _groupDescription;

        /// <summary>
        /// Whether the page is loading or not. Should completely hide the page behind a loading indicator.
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                if (_isLoading != value)
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


        public bool AdditionalVideosFound
        {
            get
            {
                return _additionalVideosFound;
            }

            set
            {
                if (_additionalVideosFound != value)
                {
                    _additionalVideosFound = value;
                    RaisePropertyChanged("AdditionalVideosFound");
                }
            }
        }
        private bool _additionalVideosFound;

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

        public RelayCommand ShowMoreVideosCommand
        {
            get
            {
                return _showMoreVideosCommand ?? (_showMoreVideosCommand = new RelayCommand(
                () =>
                {
                    var unawaitedTask = LoadVideosAsync(false);
                }));
            }
        }
        private RelayCommand _showMoreVideosCommand;

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
        #endregion

        #region Commands
        public RelayCommand RefreshPageCommand
        {
            get
            {
                return _refreshPageCommand ?? (_refreshPageCommand = new RelayCommand(
                () =>
                {
                    var unawaitedTask = LoadVideosAsync(true);
                }));
            }
        }
        private RelayCommand _refreshPageCommand;

        public RelayCommand NavigateSearchPageCommand
        {
            get
            {
                return _navigateSearchPageCommand ?? (_navigateSearchPageCommand = new RelayCommand(
                () =>
                {
                    _navigationManager.Navigate(Views.SearchPage.PageKey);
                }));
            }
        }
        private RelayCommand _navigateSearchPageCommand;

        public RelayCommand NavigateCategoriesPageCommand
        {
            get
            {
                return _navigateCategoriesPageCommand ?? (_navigateCategoriesPageCommand = new RelayCommand(
                () =>
                {
                    _navigationManager.Navigate(Views.CategoriesPage.PageKey);
                }));
            }
        }
        private RelayCommand _navigateCategoriesPageCommand;

        public RelayCommand NavigateShowsPageCommand
        {
            get
            {
                return _navigateShowsPageCommand ?? (_navigateShowsPageCommand = new RelayCommand(
                () =>
                {
                    _navigationManager.Navigate(Views.ShowsPage.PageKey);
                }));
            }
        }
        private RelayCommand _navigateShowsPageCommand;

        public RelayCommand NavigateSettingsPageCommand
        {
            get
            {
                return _navigateSettingsPageCommand ?? (_navigateSettingsPageCommand = new RelayCommand(
                () =>
                {
                    _navigationManager.Navigate(Views.SettingsPage.PageKey);
                }));
            }
        }
        private RelayCommand _navigateSettingsPageCommand;

        public RelayCommand NavigateLiveStreamPageCommand
        {
            get
            {
                return _navigateLiveStreamPageCommand ?? (_navigateLiveStreamPageCommand = new RelayCommand(
                () =>
                {
                    _navigationManager.Navigate(Views.CategoriesPage.PageKey);
                }));
            }
        }
        private RelayCommand _navigateLiveStreamPageCommand;

        public RelayCommand NavigateHomeCommand
        {
            get
            {
                return _navigateHomeCommand ?? (_navigateHomeCommand = new RelayCommand(
                () =>
                {
                    _navigationManager.NavigateHome();
                }));
            }
        }
        private RelayCommand _navigateHomeCommand;
        #endregion
    }
}
