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
        private const int NumberOfSubHeadersOnMainPage = 2;
        private const int NumberOfSubHeadersOnCategoryPage = 4;

        public VideoListPageViewModel()
        {
            _videos = new ObservableCollection<VideoViewModel>();
            _subHeaderVideos = new ObservableCollection<VideoViewModel>();
            _headerVideos = new ObservableCollection<VideoViewModel>();
        }

        public async Task InitializeAsync()
        {
            IsLoading = true;
            bool isCategoryPage = false;

            try
            {
                _navigationManager = NavigationManager.GetInstance();
                _apiKey = ApiKeyManager.GetInstance().GetSavedApiKey();
                VideosResponse response = null;

                if ((this.Category == null) || (String.IsNullOrWhiteSpace(this.Category.Id)))
                {
                    response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideosAsync(_apiKey);
                }
                else
                {
                    isCategoryPage = true;
                    CategoryTitle = Category.Name;
                    CategoryDescription = Category.Deck;
                    response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideosAsync(_apiKey, Category.Id);
                }

                if ((response != null) && (response.Status == StatusCode.OK) && (response.Results != null))
                {
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
                            else
                            {
                                // TODO: Add default image
                            }
                        }

                        _videos.Add(viewModel);
                    }
                }

                // Add in the header and subheader view models
                if (isCategoryPage)
                {
                    // Category page style, no header. Don't show anything unless we have a full
                    // second row (3 wide) of videos.
                    if (_videos.Count > (NumberOfSubHeadersOnCategoryPage + 3))
                    {
                        var subHeaderViewModels = _videos.Take(NumberOfSubHeadersOnCategoryPage);
                        foreach (var video in subHeaderViewModels)
                        {
                            _subHeaderVideos.Add(video);
                        }

                        for (int i = 0; i < NumberOfSubHeadersOnCategoryPage; i++)
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
                        _headerVideos.Add(_videos.First());
                        _videos.RemoveAt(0);

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
            catch (Exception e)
            {
                // TODO: Add a logger
            }

            IsLoading = false;
        }

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


        public VideoCategory Category
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
        private VideoCategory _category;

        public string CategoryTitle
        {
            get
            {
                return _categoryTitle;
            }

            set
            {
                if (_categoryTitle != value)
                {
                    _categoryTitle = value;
                    RaisePropertyChanged(() => CategoryTitle);
                }
            }
        }
        private string _categoryTitle;

        public string CategoryDescription
        {
            get
            {
                return _categoryDescription;
            }

            set
            {
                if (_categoryDescription != value)
                {
                    _categoryDescription = value;
                    RaisePropertyChanged(() => CategoryDescription);
                }
            }
        }
        private string _categoryDescription;

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

        #region Navigation
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

        public RelayCommand NavigateQuickLooksPageCommand
        {
            get
            {
                return _navigateQuickLooksPageCommand ?? (_navigateQuickLooksPageCommand = new RelayCommand(
                () =>
                {
                    // So yeah, we're hard coding this ID because there's no explicit API request to get this specific page.
                    // It's not ideal.
                    _navigationManager.Navigate("CategoryPage", new GiantBombApi.Models.VideoCategory()
                    {
                        Name = "Quick Looks",
                        Deck = "Our editors provide commentary as they play through 20 minutes or more of uninterrupted gameplay.",
                        Id = "3",
                    });
                }));
            }
        }
        private RelayCommand _navigateQuickLooksPageCommand;

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
