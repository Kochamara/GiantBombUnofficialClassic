using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace GiantBombUnofficialClassic.Utilities
{
    public class NavigationManager
    {
        private static NavigationManager _instance;
        private INavigationService _navigationService;
        private SystemNavigationManager _systemNavigationManager;

        public static NavigationManager GetInstance()
        {
            return _instance ?? (_instance = new NavigationManager());
        }

        private NavigationManager()
        {
            _navigationService = this.CreateNavigationService();
            _systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            _systemNavigationManager.BackRequested += SystemNavigationManager_BackRequested;

            UpdateBackButtonVisibility();
        }

        public void NavigateHome()
        {
            Navigate(Views.MainPage.PageKey);
        }

        public void Navigate(string pageKey)
        {
            Navigate(pageKey, null);
        }

        public void Navigate(string pageKey, object parameter)
        {
            _navigationService.NavigateTo(pageKey, parameter);
            UpdateBackButtonVisibility();
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            _navigationService.GoBack();
            UpdateBackButtonVisibility();
            e.Handled = true;
        }

        private void UpdateBackButtonVisibility()
        {
            if (_navigationService.CurrentPageKey == "-- ROOT --")
            {
                _systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
            else
            {
                _systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
        }

        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();

            navigationService.Configure(Views.MainPage.PageKey, typeof(Views.MainPage));
            navigationService.Configure(Views.VideoPlayerPage.PageKey, typeof(Views.VideoPlayerPage));
            navigationService.Configure(Views.CategoriesPage.PageKey, typeof(Views.CategoriesPage));
            navigationService.Configure(Views.ShowsPage.PageKey, typeof(Views.ShowsPage));
            navigationService.Configure(Views.SearchPage.PageKey, typeof(Views.SearchPage));
            navigationService.Configure(Views.SettingsPage.PageKey, typeof(Views.SettingsPage));
            navigationService.Configure(Views.WelcomePage.PageKey, typeof(Views.WelcomePage));
            navigationService.Configure(Views.GroupPage.PageKey, typeof(Views.GroupPage));
            navigationService.Configure(Views.TextPage.PageKey, typeof(Views.TextPage));
            navigationService.Configure(Views.PrivacyPolicyPage.PageKey, typeof(Views.PrivacyPolicyPage));

            return navigationService;
        }
    }
}