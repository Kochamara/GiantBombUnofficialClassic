using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public SettingsPageViewModel()
        {

        }

        public RelayCommand ViewOpenSourceLicensesCommand
        {
            get
            {
                return _viewOpenSourceLicensesCommand ?? (_viewOpenSourceLicensesCommand = new RelayCommand(
                () =>
                {
                    var navigationManager = Utilities.NavigationManager.GetInstance();
                    navigationManager.Navigate(Views.OpenSourceLicenses.PageKey);
                }));
            }
        }
        private RelayCommand _viewOpenSourceLicensesCommand;

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