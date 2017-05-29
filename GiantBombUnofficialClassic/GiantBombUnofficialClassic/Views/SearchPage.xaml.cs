using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GiantBombUnofficialClassic.Views
{
    public sealed partial class SearchPage : Page
    {
        public const string PageKey = "SearchPage";
        private ViewModels.SearchPageViewModel _context;

        public SearchPage()
        {
            _context = new ViewModels.SearchPageViewModel();
            this.DataContext = _context;
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if ((_context != null))
            {
                this.NavigationControl.UpdateNavigationMenuForParentPage(typeof(SearchPage));
            }
        }

        private void SearchQueryTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if ((e.Key == Windows.System.VirtualKey.Enter) || (e.Key == Windows.System.VirtualKey.GamepadMenu))
            {
                if (e.KeyStatus.RepeatCount == 1)
                {
                    var unawaitedTask = _context.GetSearchResultsAsync(SearchQueryTextBox.Text, true);
                }
            }
        }
    }
}