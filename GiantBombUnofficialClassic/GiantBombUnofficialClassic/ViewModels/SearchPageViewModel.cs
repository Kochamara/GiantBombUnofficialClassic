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
        private string _apiKey;

        public const string NoQueryEnteredError = "No search query entered.";
        public const string NoResultsFoundError = "No search results found.";

        public SearchPageViewModel()
        {
            _navigationManager = NavigationManager.GetInstance();
            _searchResults = new ObservableCollection<BasicViewModel>();
        }

        public async Task GetSearchResultsAsync(string query)
        {
            IsLoading = true;
            _searchResults.Clear();

            if (!String.IsNullOrWhiteSpace(query))
            {
                if (String.IsNullOrWhiteSpace(_apiKey))
                {
                    _apiKey = Services.ApiKeyManager.GetInstance().GetSavedApiKey();
                }

                var response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideoSearchResultsAsync(_apiKey, query);
                if ((response != null) && (response.Status == StatusCode.OK) && (response.Results != null) && (response.Results.Count() > 0))
                {
                    foreach (var video in response.Results)
                    {
                        _searchResults.Add(new BasicViewModel
                        {
                            Title = video.Name,
                            ImageLocation = new Uri(video.Image.MediumUrl),
                        //    HdUri = new Uri(video.HdUrl)
                        });
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

            IsLoading = false;
        }

        public RelayCommand SearchCommand
        {
            get
            {
                return _saveKeyCommand ?? (_saveKeyCommand = new RelayCommand(
                () =>
                {
                    var unawaitedTask = GetSearchResultsAsync(UserInput);
                }));
            }
        }
        private RelayCommand _saveKeyCommand;

        public ObservableCollection<BasicViewModel> SearchResults
        {
            get { return _searchResults; }
        }
        private ObservableCollection<BasicViewModel> _searchResults;

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
    }
}
