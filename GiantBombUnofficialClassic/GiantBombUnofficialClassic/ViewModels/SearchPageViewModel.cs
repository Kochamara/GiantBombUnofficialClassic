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

namespace GiantBombUnofficialClassic.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        private NavigationManager _navigationManager;
        private int _resultPageCurrentlyShown;
        private int _numberOfVideosCurrentlyShown;
        private string _apiKey;

        public const string NoQueryEnteredError = "No search query entered.";
        public const string NoResultsFoundError = "No search results found.";

        public SearchPageViewModel()
        {
            _navigationManager = NavigationManager.GetInstance();
            _searchResults = new ObservableCollection<VideoViewModel>();
        }

        public async Task GetSearchResultsAsync(string query, bool isFirstTimeLoadingVideos)
        {
            if (isFirstTimeLoadingVideos)
            {
                IsLoading = true;
                _searchResults.Clear();
                _numberOfVideosCurrentlyShown = 0;
                _resultPageCurrentlyShown = 1;
            }
            else
            {
                AreAdditionalResultsBeingLoaded = true;
                _resultPageCurrentlyShown++;
            }

            AdditionalVideosFound = false;

            if (!String.IsNullOrWhiteSpace(query))
            {
                if (String.IsNullOrWhiteSpace(_apiKey))
                {
                    _apiKey = Services.ApiKeyManager.GetInstance().GetSavedApiKey();
                }

                var response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideoSearchResultsAsync(_apiKey, query, _resultPageCurrentlyShown);
                if ((response != null) && (response.Status == StatusCode.OK) && (response.Results != null) && (response.Results.Count() > 0))
                {
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

                        _searchResults.Add(viewModel);
                    }
                }
                else
                {
                    ErrorText = NoResultsFoundError;
                }
            }
            else
            {
                ErrorText = NoQueryEnteredError;
            }

            AreAdditionalResultsBeingLoaded = false;
            IsLoading = false;
        }

        #region Bound Properties
        public ObservableCollection<VideoViewModel> SearchResults
        {
            get { return _searchResults; }
        }
        private ObservableCollection<VideoViewModel> _searchResults;

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
                    RaisePropertyChanged(() => IsLoading);
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
                    RaisePropertyChanged(() => AreAdditionalResultsBeingLoaded);
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
                    RaisePropertyChanged(() => AdditionalVideosFound);
                }
            }
        }
        private bool _additionalVideosFound;

        public string ErrorText
        {
            get
            {
                return _errorText;
            }

            set
            {
                if (_errorText != value)
                {
                    _errorText = value;
                    RaisePropertyChanged(() => ErrorText);
                }
            }
        }
        private string _errorText;

        public string UserInput { get; set; }

        public RelayCommand SearchCommand
        {
            get
            {
                return _saveKeyCommand ?? (_saveKeyCommand = new RelayCommand(
                () =>
                {
                    var unawaitedTask = GetSearchResultsAsync(UserInput, true);
                }));
            }
        }
        private RelayCommand _saveKeyCommand;

        public RelayCommand ShowMoreVideosCommand
        {
            get
            {
                return _showMoreVideosCommand ?? (_showMoreVideosCommand = new RelayCommand(
                () =>
                {
                    var unawaitedTask = GetSearchResultsAsync(UserInput, false);
                }));
            }
        }
        private RelayCommand _showMoreVideosCommand;
        #endregion
    }
}
