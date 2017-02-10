using GiantBombUnofficialClassic.ViewModels;
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
    public sealed partial class WelcomePage : Page
    {
        public const string PageKey = "WelcomePage";
        private WelcomePageViewModel _viewModel;

        public WelcomePage()
        {
            _viewModel = new WelcomePageViewModel();
            this.DataContext = _viewModel;
            this.InitializeComponent();
        }

        private void LinkCodeHyperlink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            var unawaitedTask = Windows.System.Launcher.LaunchUriAsync(_viewModel.LinkCodeWebsite);
        }
    }
}
