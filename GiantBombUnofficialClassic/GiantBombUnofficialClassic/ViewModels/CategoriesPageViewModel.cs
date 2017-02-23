﻿using GalaSoft.MvvmLight;
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

            var response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideoTypesAsync(_apiKey);
            if ((response != null) && (response.Status == StatusCode.OK) && (response.Results != null))
            {
                foreach (var category in response.Results)
                {
                    var imageLocation = Services.CategoryImageProvider.GetImageForCategoryName(category.Name);
                    _categories.Add(new BasicViewModel
                    {
                        Title = category.Name,
                        Description = category.Deck,
                        Id = category.Id,
                        ImageLocation = imageLocation,
                        Command = new GalaSoft.MvvmLight.Command.RelayCommand(
                        () =>
                        {
                            _navigationManager.Navigate("CategoryPage", category);
                        })
                    });
                }
            }
        }

        public ObservableCollection<BasicViewModel> Categories
        {
            get { return _categories; }
        }
        private ObservableCollection<BasicViewModel> _categories;
    }
}