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

        //public const string EncodedAppNameForPcVersion = "Giant%20Bomb%20Unofficial%20Classic";
        public const string EncodedAppNameForPcVersion = "WindowsVideoPlayer";
        public const string EncodedAppNameForXboxVersion = "XboxVideoPlayer";

        public WelcomePageViewModel()
        {
            _navigationManager = NavigationManager.GetInstance();
            _apiKeyManager = ApiKeyManager.GetInstance();
            
            if (IsTenFootExperience)
            {
                LinkCodeWebsite = new Uri("http://www.giantbomb.com/app/" + EncodedAppNameForXboxVersion);
            }
            else
            {
                LinkCodeWebsite = new Uri("http://www.giantbomb.com/app/" + EncodedAppNameForPcVersion);
            }
        }

        public async Task ConvertLinkCodeToApiKeyAndNavigateAsync(string linkCode)
        {
            IsLoading = true;

            if (!String.IsNullOrWhiteSpace(linkCode))
            {
                var apiKey = string.Empty;
                
                if (IsTenFootExperience)
                {
                    apiKey = await GiantBombApi.Services.ApiKeyRetrievalAgent.GetApiKeyFromCodeAsync(linkCode, EncodedAppNameForXboxVersion);
                }
                else
                {
                    apiKey = await GiantBombApi.Services.ApiKeyRetrievalAgent.GetApiKeyFromCodeAsync(linkCode, EncodedAppNameForPcVersion);
                }

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

        public bool IsTenFootExperience
        {
            get
            {
                return true;
            }
        }

        public Uri LinkCodeWebsite
        {
            get
            {
                return _linkCodeWebsite;
            }

            set
            {
                if (_linkCodeWebsite != value)
                {
                    _linkCodeWebsite = value;
                    RaisePropertyChanged(() => LinkCodeWebsite);
                }
            }
        }
        private Uri _linkCodeWebsite;

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
