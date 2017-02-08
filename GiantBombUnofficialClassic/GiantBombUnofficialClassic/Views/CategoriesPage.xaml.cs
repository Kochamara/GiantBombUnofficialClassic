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
    public sealed partial class CategoriesPage : Page
    {
        public const string PageKey = "CategoriesPage";
        private ViewModels.CategoriesPageViewModel _viewModel;

        public CategoriesPage()
        {
            _viewModel = new ViewModels.CategoriesPageViewModel();
            this.DataContext = _viewModel;
            this.InitializeComponent();
        }
    }
}