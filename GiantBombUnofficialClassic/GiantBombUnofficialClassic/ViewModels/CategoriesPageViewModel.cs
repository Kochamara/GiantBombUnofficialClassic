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

        public CategoriesPageViewModel()
        {
            _navigationManager = Utilities.NavigationManager.GetInstance();
            _categories = new ObservableCollection<BasicViewModel>();
        }

        public async Task InitializeAsync()
        {
            IsLoading = true;

            _apiKey = Services.ApiKeyManager.GetInstance().GetSavedApiKey();

            var response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideoCategoriesAsync(_apiKey);
            if ((response != null) && (response.Status == StatusCode.OK) && (response.Results != null))
            {
                foreach (var category in response.Results)
                {
                    var imageLocation = Services.CategoryImageProvider.GetImageForCategoryName(category.Name);
                    _categories.Add(new CategoryViewModel
                    {
                        Title = category.Name,
                        Description = category.Deck,
                        Id = category.Id,
                        ImageLocation = imageLocation,
                        Source = category
                    });
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