using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.Services
{
    public static class InitializationAgent
    {
        /// <summary>
        /// Placement for initialization tasks that should run prior to the starter page loading
        /// </summary>
        public static void Initialize()
        {
            GalaSoft.MvvmLight.Threading.DispatcherHelper.Initialize();

            if (Utilities.SystemInformationManager.IsTenFootExperience)
            {
                // System is an Xbox
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationViewBoundsMode"))
                {
                    var view = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
                    view.SetDesiredBoundsMode(Windows.UI.ViewManagement.ApplicationViewBoundsMode.UseCoreWindow);
                }
            }
            else
            {
                // System is a PC
                SetTitleBarColors();
            }
        }

        public static void SetTitleBarColors()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var view = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
                if ((view != null) && (view.TitleBar != null))
                {
                    // Let's not use red because it's distracting when watching videos
                    //var primaryColor = Windows.UI.Color.FromArgb(0, 179, 25, 25);
                    //var hoverColor = Windows.UI.Color.FromArgb(0, 230, 73, 73);
                    //var highlightColor = Windows.UI.Color.FromArgb(0, 232, 90, 90);

                    var primaryBackgroundColor = Windows.UI.Color.FromArgb(0, 41, 41, 41);
                    var hoverBackgroundColor = Windows.UI.Color.FromArgb(0, 62, 62, 62);
                    var highlightBackgroundColor = Windows.UI.Color.FromArgb(0, 83, 83, 83);

                    var primaryForegroundColor = Windows.UI.Color.FromArgb(0, 235, 235, 235);
                    var primaryInactiveForegroundColor = Windows.UI.Color.FromArgb(0, 171, 171, 171);

                    view.TitleBar.ButtonBackgroundColor = primaryBackgroundColor;
                    view.TitleBar.ButtonForegroundColor = primaryForegroundColor;
                    view.TitleBar.BackgroundColor = primaryBackgroundColor;
                    view.TitleBar.ForegroundColor = primaryForegroundColor;
                    view.TitleBar.ButtonHoverBackgroundColor = hoverBackgroundColor;
                    view.TitleBar.ButtonHoverForegroundColor = primaryForegroundColor;
                    view.TitleBar.ButtonPressedBackgroundColor = highlightBackgroundColor;
                    view.TitleBar.ButtonPressedForegroundColor = primaryForegroundColor;
                    view.TitleBar.ButtonInactiveBackgroundColor = primaryBackgroundColor;
                    view.TitleBar.ButtonInactiveForegroundColor = primaryInactiveForegroundColor;
                    view.TitleBar.InactiveBackgroundColor = primaryBackgroundColor;
                    view.TitleBar.InactiveForegroundColor = primaryInactiveForegroundColor;
                }
            }
        }

        /// <summary>
        /// Returns which page should be loaded first
        /// </summary>
        public static Type GetStartingPage()
        {
            Type startingPage = typeof(Views.WelcomePage);

            try
            {
                var apiKeyManager = ApiKeyManager.GetInstance();
                string storedApiKey = apiKeyManager.GetSavedApiKey();
                if (!String.IsNullOrWhiteSpace(storedApiKey))
                {
                    startingPage = typeof(Views.MainPage);
                }
            }
            catch (Exception e)
            {
                // TODO: Add logging
            }

            return startingPage;
        }
    }
}
