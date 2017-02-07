using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GiantBombUnofficialClassic.Services;
using GiantBombUnofficialClassic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class WelcomePageViewModel : ViewModelBase
    {
        private NavigationManager _navigationManager;
        private ApiKeyManager _apiKeyManager;

        public WelcomePageViewModel()
        {
            _navigationManager = NavigationManager.GetInstance();
            _apiKeyManager = ApiKeyManager.GetInstance();
        }

        public RelayCommand SaveKeyCommand
        {
            get
            {
                return _saveKeyCommand ?? (_saveKeyCommand = new RelayCommand(
                () =>
                {
                    if (!String.IsNullOrWhiteSpace(UserInput))
                    {
                        _apiKeyManager.SaveNewApiKey(UserInput);
                        _navigationManager.Navigate("MainPage");
                    }
                }));
            }
        }
        private RelayCommand _saveKeyCommand;

        public string UserInput { get; set; }
    }
}
