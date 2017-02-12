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
            _apiKey = Services.ApiKeyManager.GetInstance().GetSavedApiKey();

            string text = string.Empty;

            var response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideoTypesAsync(_apiKey);
            if ((response != null) && (response.Status == StatusCode.OK) && (response.Results != null))
            {
                foreach (var category in response.Results)
                {
                    _categories.Add(new BasicViewModel
                    {
                        Title = category.Name,
                        Description = category.Deck,
                        Id = category.Id,
                        Command = new GalaSoft.MvvmLight.Command.RelayCommand(
                        () =>
                        {
                            _navigationManager.Navigate("CategoryPage", category);
                        })
                    });
                    text += category.Name + " " + category.Id + "\n" + category.Deck + "\n\n";
                }
            }
            text += "end";
        }

        public ObservableCollection<BasicViewModel> Categories
        {
            get { return _categories; }
        }
        private ObservableCollection<BasicViewModel> _categories;
    }
}