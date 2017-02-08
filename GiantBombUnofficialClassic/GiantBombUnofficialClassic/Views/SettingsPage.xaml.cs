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
    public sealed partial class SettingsPage : Page
    {
        public const string PageKey = "SettingsPage";
        private ViewModels.SettingsPageViewModel _viewModel;

        public SettingsPage()
        {
            _viewModel = new ViewModels.SettingsPageViewModel();
            this.DataContext = _viewModel;
            this.InitializeComponent();
        }
    }
}
