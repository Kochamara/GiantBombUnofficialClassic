using GiantBombApi.Models;
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
    public sealed partial class ContinuePage : Page
    {
        public const string PageKey = "ContinuePage";
        private ViewModels.ContinuePageViewModel _viewModel;

        public ContinuePage()
        {
            _viewModel = new ViewModels.ContinuePageViewModel();
            this.DataContext = _viewModel;
            this.InitializeComponent();
            var unawaitedTask = _viewModel.InitializeAsync();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if ((e != null) && (e.Parameter != null))
            {
                var unawaitedTask = _viewModel.InitializeAsync();
            }
        }
    }
}
