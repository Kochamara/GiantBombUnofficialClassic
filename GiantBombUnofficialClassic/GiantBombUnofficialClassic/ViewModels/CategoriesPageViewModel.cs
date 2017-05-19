using GalaSoft.MvvmLight;
using GiantBombApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class CategoriesPageViewModel : ViewModelBase
    {
        private Utilities.NavigationManager _navigationManager;
        private string _apiKey;
        private GroupingType _pageType;

        public CategoriesPageViewModel(GroupingType pageType)
        {
            _navigationManager = Utilities.NavigationManager.GetInstance();
            _categories = new ObservableCollection<BasicViewModel>();
            _pageType = pageType;
        }

        public async Task InitializeAsync()
        {
            IsLoading = true;

            _apiKey = Services.ApiKeyManager.GetInstance().GetSavedApiKey();

            VideoGroupingsResponse response = null;

            switch (_pageType)
            {
                case GroupingType.Category:
                    response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideoCategoriesAsync(_apiKey);
                    break;
                case GroupingType.Show:
                default:
                    response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideoShowsAsync(_apiKey);
                    break;
            }

            if ((response != null) && (response.Status == StatusCode.OK) && (response.Results != null))
            {
                foreach (var category in response.Results)
                {
                    Uri imageLocation = null;
                    category.GroupingType = _pageType;
                    if (category.Image == null)
                    {
                        imageLocation = Services.CategoryImageProvider.GetImageForCategoryName(category.Title);
                    }
                    else
                    {
                        if (category.Image != null)
                        {
                            if (!String.IsNullOrWhiteSpace(category.Image.SuperUrl))
                            {
                                imageLocation = new Uri(category.Image.SuperUrl);
                            }
                            else if (!String.IsNullOrWhiteSpace(category.Image.MediumUrl))
                            {
                                imageLocation = new Uri(category.Image.MediumUrl);
                            }
                            else if (!String.IsNullOrWhiteSpace(category.Image.SmallUrl))
                            {
                                imageLocation = new Uri(category.Image.SmallUrl);
                            }
                        }
                    }

                    var cat = new VideoGroupViewModel
                    {
                        Title = category.Title,
                        Description = category.Deck,
                        Id = category.Id,
                        ImageLocation = imageLocation,
                        Source = category
                    };
                    
                    _categories.Add(cat);
                }
            }

            IsLoading = false;
        }

        public ObservableCollection<BasicViewModel> Categories
        {
            get { return _categories; }
        }
        private ObservableCollection<BasicViewModel> _categories;

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
    }
}