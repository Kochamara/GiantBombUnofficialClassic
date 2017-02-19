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

        public const string NoKeyEnteredError = "Uh oh! You need to enter a key to use this app.";
        public const string NoApiKeyReturnedError = "We're having trouble validating your code. Are you sure it's correct?";
        public Uri LinkCodeWebsite = new Uri("http://www.giantbomb.com/app/Giant%20Bomb%20Unofficial%20Classic/");

        public WelcomePageViewModel()
        {
            _navigationManager = NavigationManager.GetInstance();
            _apiKeyManager = ApiKeyManager.GetInstance();
        }

        public async Task ConvertLinkCodeToApiKeyAndNavigateAsync(string linkCode)
        {
            IsLoading = true;

            if (!String.IsNullOrWhiteSpace(UserInput))
            {
                var apiKey = await GiantBombApi.Services.ApiKeyRetrievalAgent.GetApiKeyFromCodeAsync(linkCode);

                if (!String.IsNullOrWhiteSpace(apiKey))
                {
                    _apiKeyManager.SaveNewApiKey(apiKey);
                    _navigationManager.Navigate("MainPage");
                }
                else
                {
                    ErrorText = NoApiKeyReturnedError;
                }
            }
            else
            {
                ErrorText = NoKeyEnteredError;
            }

            IsLoading = false;
        }

        public RelayCommand SaveKeyCommand
        {
            get
            {
                return _saveKeyCommand ?? (_saveKeyCommand = new RelayCommand(
                () =>
                {
                    var unawaitedTask = ConvertLinkCodeToApiKeyAndNavigateAsync(UserInput);
                }));
            }
        }
        private RelayCommand _saveKeyCommand;

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
