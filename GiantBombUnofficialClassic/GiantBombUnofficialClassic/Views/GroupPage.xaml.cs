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
    public sealed partial class GroupPage : Page
    {
        public const string PageKey = "GroupPage";
        private ViewModels.VideoListPageViewModel _viewModel;

        public GroupPage()
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
                var group = e.Parameter as GiantBombApi.Models.VideoGrouping;
                _viewModel.Group = group;
                var unawaitedTask = _viewModel.InitializeAsync();
            }
        }
    }
}