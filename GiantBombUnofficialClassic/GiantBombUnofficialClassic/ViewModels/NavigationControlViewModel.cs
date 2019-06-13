using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GiantBombUnofficialClassic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class NavigationControlViewModel : ViewModelBase
    {
        private NavigationManager _navigationManager;
        private DateTime _E3StartDate = new DateTime(2020, 06, 5);
        private DateTime _E3EndDate = new DateTime(2020, 06, 12);

        public NavigationControlViewModel()
        {
            _navigationManager = NavigationManager.GetInstance();
            SetLockdownStatus();
        }

        public void SetRefreshCommand(RelayCommand refreshCommand)
        {
            this.RefreshCommand = refreshCommand;

            if (this.RefreshCommand == null)
            {
                IsRefreshButtonVisible = false;
                IsRefreshButtonPromptVisible = false;
            }
            else
            {
                if (SystemInformationManager.IsTenFootExperience)
                {
                    IsRefreshButtonVisible = false;
                    IsRefreshButtonPromptVisible = true;
                }
                else
                {
                    IsRefreshButtonVisible = true;
                    IsRefreshButtonPromptVisible = false;
                }
            }
        }

        public void UpdateNavigationMenuForParentPage(Type parentPage)
        {
            if (parentPage == typeof(Views.MainPage))
            {
                IsHomeButtonVisible = false;
                IsCategoriesButtonVisible = true;
                IsShowsButtonVisible = true;
                IsSettingsButtonVisible = true;

                if (SystemInformationManager.IsTenFootExperience)
                {
                    IsSearchButtonPromptVisible = true;
                }
                else
                {
                    IsSearchButtonVisible = true;
                }
            }
            else if (parentPage == typeof(Views.SearchPage))
            {
                IsHomeButtonVisible = true;
                IsSearchButtonVisible = false;
                IsCategoriesButtonVisible = true;
                IsShowsButtonVisible = true;
                IsSettingsButtonVisible = true;
            }
            else if (parentPage == typeof(Views.CategoriesPage))
            {
                IsHomeButtonVisible = true;
                IsSearchButtonVisible = true;
                IsCategoriesButtonVisible = false;
                IsShowsButtonVisible = true;
                IsSettingsButtonVisible = true;

                if (SystemInformationManager.IsTenFootExperience)
                {
                    IsSearchButtonPromptVisible = true;
                }
                else
                {
                    IsSearchButtonVisible = true;
                }
            }
            else if (parentPage == typeof(Views.ShowsPage))
            {
                IsHomeButtonVisible = true;
                IsSearchButtonVisible = true;
                IsCategoriesButtonVisible = true;
                IsShowsButtonVisible = false;
                IsSettingsButtonVisible = true;

                if (SystemInformationManager.IsTenFootExperience)
                {
                    IsSearchButtonPromptVisible = true;
                }
                else
                {
                    IsSearchButtonVisible = true;
                }
            }
            else if (parentPage == typeof(Views.SettingsPage))
            {
                IsHomeButtonVisible = true;
                IsSearchButtonVisible = true;
                IsCategoriesButtonVisible = true;
                IsShowsButtonVisible = true;
                IsSettingsButtonVisible = false;

                if (SystemInformationManager.IsTenFootExperience)
                {
                    IsSearchButtonPromptVisible = true;
                }
                else
                {
                    IsSearchButtonVisible = true;
                }
            }
            else
            {
                IsHomeButtonVisible = true;
                IsSearchButtonVisible = true;
                IsCategoriesButtonVisible = true;
                IsShowsButtonVisible = true;
                IsSettingsButtonVisible = true;

                if (SystemInformationManager.IsTenFootExperience)
                {
                    IsSearchButtonPromptVisible = true;
                }
                else
                {
                    IsSearchButtonVisible = true;
                }
            }
        }

        // Only show the Lockdown button if it's during E3. Duh.
        private void SetLockdownStatus()
        {
            if ((DateTime.Now >= _E3StartDate) && (DateTime.Now < _E3EndDate))
            {
                IsLockdownButtonVisible = true;
            }
            else
            {
                IsLockdownButtonVisible = false;
            }
        }

        #region Button Visibility
        public bool IsHomeButtonVisible
        {
            get
            {
                return _isHomeButtonVisible;
            }

            set
            {
                if (_isHomeButtonVisible != value)
                {
                    _isHomeButtonVisible = value;
                    RaisePropertyChanged(() => IsHomeButtonVisible);
                }
            }
        }
        private bool _isHomeButtonVisible;

        public bool IsSearchButtonVisible
        {
            get
            {
                return _isSearchButtonVisible;
            }

            set
            {
                if (_isSearchButtonVisible != value)
                {
                    _isSearchButtonVisible = value;
                    RaisePropertyChanged(() => IsSearchButtonVisible);
                }
            }
        }
        private bool _isSearchButtonVisible;

        public bool IsCategoriesButtonVisible
        {
            get
            {
                return _isCategoriesButtonVisible;
            }

            set
            {
                if (_isCategoriesButtonVisible != value)
                {
                    _isCategoriesButtonVisible = value;
                    RaisePropertyChanged(() => IsCategoriesButtonVisible);
                }
            }
        }
        private bool _isCategoriesButtonVisible;

        public bool IsShowsButtonVisible
        {
            get
            {
                return _isShowsButtonVisible;
            }

            set
            {
                if (_isShowsButtonVisible != value)
                {
                    _isShowsButtonVisible = value;
                    RaisePropertyChanged(() => IsShowsButtonVisible);
                }
            }
        }
        private bool _isShowsButtonVisible;

        public bool IsLockdownButtonVisible
        {
            get
            {
                return _isLockdownButtonVisible;
            }

            set
            {
                if (_isLockdownButtonVisible != value)
                {
                    _isLockdownButtonVisible = value;
                    RaisePropertyChanged(() => IsLockdownButtonVisible);
                }
            }
        }
        private bool _isLockdownButtonVisible;

        public bool IsSettingsButtonVisible
        {
            get
            {
                return _isSettingsButtonVisible;
            }

            set
            {
                if (_isSettingsButtonVisible != value)
                {
                    _isSettingsButtonVisible = value;
                    RaisePropertyChanged(() => IsSettingsButtonVisible);
                }
            }
        }
        private bool _isSettingsButtonVisible;

        public bool IsRefreshButtonVisible
        {
            get
            {
                return _isRefreshButtonVisible;
            }

            set
            {
                if (_isRefreshButtonVisible != value)
                {
                    _isRefreshButtonVisible = value;
                    RaisePropertyChanged(() => IsRefreshButtonVisible);
                }
            }
        }
        private bool _isRefreshButtonVisible;

        public bool IsSearchButtonPromptVisible
        {
            get
            {
                return _isSearchButtonPromptVisible;
            }

            set
            {
                if (_isSearchButtonPromptVisible != value)
                {
                    _isSearchButtonPromptVisible = value;
                    RaisePropertyChanged(() => IsSearchButtonPromptVisible);
                }
            }
        }
        private bool _isSearchButtonPromptVisible;

        public bool IsRefreshButtonPromptVisible
        {
            get
            {
                return _isRefreshButtonPromptVisible;
            }

            set
            {
                if (_isRefreshButtonPromptVisible != value)
                {
                    _isRefreshButtonPromptVisible = value;
                    RaisePropertyChanged(() => IsRefreshButtonPromptVisible);
                }
            }
        }
        private bool _isRefreshButtonPromptVisible;
        #endregion

        #region Commands
        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand;
            }

            private set
            {
                if (_refreshCommand != value)
                {
                    _refreshCommand = value;
                    RaisePropertyChanged(() => RefreshCommand);
                }
            }
        }
        private RelayCommand _refreshCommand;

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

        public RelayCommand NavigateShowsPageCommand
        {
            get
            {
                return _navigateShowsPageCommand ?? (_navigateShowsPageCommand = new RelayCommand(
                () =>
                {
                    _navigationManager.Navigate(Views.ShowsPage.PageKey);
                }));
            }
        }
        private RelayCommand _navigateShowsPageCommand;

        public RelayCommand NavigateLockdownPageCommand
        {
            get
            {
                return _navigateLockdownPageCommand ?? (_navigateLockdownPageCommand = new RelayCommand(
                () =>
                {
                    _navigationManager.Navigate(Views.LockdownSimulatorPage.PageKey);
                }));
            }
        }
        private RelayCommand _navigateLockdownPageCommand;

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

        public RelayCommand NavigateLiveStreamPageCommand
        {
            get
            {
                return _navigateLiveStreamPageCommand ?? (_navigateLiveStreamPageCommand = new RelayCommand(
                () =>
                {
                    _navigationManager.Navigate(Views.CategoriesPage.PageKey);
                }));
            }
        }
        private RelayCommand _navigateLiveStreamPageCommand;

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
