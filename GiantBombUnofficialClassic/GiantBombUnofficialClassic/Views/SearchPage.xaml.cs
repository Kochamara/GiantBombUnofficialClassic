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
        private ViewModels.SearchPageViewModel _viewModel;

        public SearchPage()
        {
            _viewModel = new ViewModels.SearchPageViewModel();
            this.DataContext = _viewModel;
            this.InitializeComponent();
        }
    }
}