using GiantBombUnofficialClassic.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.Streaming.Adaptive;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GiantBombUnofficialClassic.Views
{
    public sealed partial class MainPage : Page
    {
        public const string PageKey = "MainPage";
        private VideoListPageViewModel _context;

        public MainPage()
        {
            this.InitializeComponent();
            _context = new VideoListPageViewModel();
            this.DataContext = _context;
            var unawaitedTask = _context.InitializeAsync();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            if ((_context != null))
            {
                this.NavigationControl.UpdateNavigationMenuForParentPage(typeof(MainPage));

                if (_context.RefreshCommand != null)
                {
                    this.NavigationControl.SetRefreshCommand(_context.RefreshCommand);
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Window.Current.CoreWindow.KeyUp -= CoreWindow_KeyUp;
        }

        /// <summary>
        /// Gamepad shortcuts for navigation. The X button refreshes the page, and Y goes to the search page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (args != null)
            {
                try
                {
                    switch (args.VirtualKey)
                    {
                        case Windows.System.VirtualKey.GamepadX:
                            if ((_context != null) && (_context.RefreshCommand != null))
                            {
                                _context.RefreshCommand.Execute(null);
                            }
                            args.Handled = true;
                            break;
                        case Windows.System.VirtualKey.GamepadY:
                            if ((_context != null) && (_context.NavigateSearchPageCommand != null))
                            {
                                _context.NavigateSearchPageCommand.Execute(null);
                            }
                            args.Handled = true;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Serilog.Log.Error("Exception thrown trying to use gamepad shortcut", e);
                }
            }
        }
    }
}