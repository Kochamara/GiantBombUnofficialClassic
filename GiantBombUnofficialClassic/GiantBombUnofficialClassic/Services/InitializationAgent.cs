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

            //PC customization
            // TODO: Set correct colors
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = Windows.UI.Colors.DarkMagenta;
                    titleBar.ButtonForegroundColor = Windows.UI.Colors.White;
                    titleBar.BackgroundColor = Windows.UI.Colors.Magenta;
                    titleBar.ForegroundColor = Windows.UI.Colors.White;
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
