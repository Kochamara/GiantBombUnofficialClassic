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

namespace GiantBombUnofficialClassic.Views.Controls
{
    public sealed partial class NavigationControl : UserControl
    {
        private ViewModels.NavigationControlViewModel _context;

        public NavigationControl()
        {
            this.InitializeComponent();
            _context = new ViewModels.NavigationControlViewModel();
            this.DataContext = _context;
        }

        public void SetRefreshCommand(GalaSoft.MvvmLight.Command.RelayCommand refreshCommand)
        {
            if (_context != null)
            {
                _context.SetRefreshCommand(refreshCommand);
            }
        }

        public void UpdateNavigationMenuForParentPage(Type parentPage)
        {
            if (_context != null)
            {
                _context.UpdateNavigationMenuForParentPage(parentPage);
            }
        }
    }
}