using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public SettingsPageViewModel()
        {
            // Set version number text
            Package package = Package.Current;
            if ((package != null) && (package.Id != null))
            {
                var version = package.Id.Version;
                VersionNumberText = "Giant Bomb Video Player, version " + string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision) + ".";
            }
        }

        public RelayCommand ViewReleaseNotesCommand
        {
            get
            {
                return _viewReleaseNotesCommand ?? (_viewReleaseNotesCommand = new RelayCommand(
                () =>
                {
                    var navigationManager = Utilities.NavigationManager.GetInstance();
                    navigationManager.Navigate(Views.TextPage.PageKey, new Uri(@"ms-appx:///Assets//ReleaseNotes.txt"));
                }));
            }
        }
        private RelayCommand _viewReleaseNotesCommand;

        public RelayCommand ViewOpenSourceLicensesCommand
        {
            get
            {
                return _viewOpenSourceLicensesCommand ?? (_viewOpenSourceLicensesCommand = new RelayCommand(
                () =>
                {
                    var navigationManager = Utilities.NavigationManager.GetInstance();
                    navigationManager.Navigate(Views.TextPage.PageKey, new Uri(@"ms-appx:///Assets//Licenses.txt"));
                }));
            }
        }
        private RelayCommand _viewOpenSourceLicensesCommand;

        public RelayCommand ViewPrivacyPolicyCommand
        {
            get
            {
                return _viewPrivacyPolicyCommand ?? (_viewPrivacyPolicyCommand = new RelayCommand(
                () =>
                {
                    var navigationManager = Utilities.NavigationManager.GetInstance();
                    navigationManager.Navigate(Views.PrivacyPolicyPage.PageKey);
                }));
            }
        }
        private RelayCommand _viewPrivacyPolicyCommand;

        public RelayCommand ChangeApiKeyCommand
        {
            get
            {
                return _changeApiKeyCommand ?? (_changeApiKeyCommand = new RelayCommand(
                () =>
                {
                    var navigationManager = Utilities.NavigationManager.GetInstance();
                    navigationManager.Navigate(Views.WelcomePage.PageKey);
                }));
            }
        }
        private RelayCommand _changeApiKeyCommand;

        public string VersionNumberText
        {
            get
            {
                return _versionNumberText;
            }

            set
            {
                if (_versionNumberText != value)
                {
                    _versionNumberText = value;
                    RaisePropertyChanged("VersionNumberText");
                }
            }
        }
        private string _versionNumberText;

        public string OpenSourceDescription
        {
            get
            {
                if (Utilities.SystemInformationManager.IsTenFootExperience)
                {
                    return "The Giant Bomb Unofficial Classic Video Player for Your Xbox and Mine is open source, and available at";
                }
                else
                {
                    return "The Giant Bomb Unofficial Classic Video Player for Your Computer and Mine is open source, and available at";
                }
            }
        }
    }
}