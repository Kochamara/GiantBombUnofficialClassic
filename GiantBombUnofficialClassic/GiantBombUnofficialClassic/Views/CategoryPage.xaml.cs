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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GiantBombUnofficialClassic.Views
{
    public sealed partial class CategoryPage : Page
    {
        public const string PageKey = "CategoryPage";
        private ViewModels.VideoListPageViewModel _viewModel;

        public CategoryPage()
        {
            _viewModel = new ViewModels.VideoListPageViewModel();
            this.DataContext = _viewModel;
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if ((e != null) && (e.Parameter != null))
            {
                var category = e.Parameter as GiantBombApi.Models.VideoType;
                _viewModel.Category = category;
                var unawaitedTask = _viewModel.InitializeAsync();
            }
        }
    }
}