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
        private Services.VideoUriManager _videoManager;

        public SettingsPage()
        {
            _viewModel = new ViewModels.SettingsPageViewModel();
            _videoManager = Services.VideoUriManager.GetInstance();
            this.DataContext = _viewModel;
            this.InitializeComponent();
            PopulateVideoQualityComboBox();
        }

        public void PopulateVideoQualityComboBox()
        {
            var currentPreference = _videoManager.GetPreferredVideoQuality();

            this.VideoQualityComboBox.Items.Add(new ComboBoxItem()
            {
                Content = "HD",
                IsSelected = (currentPreference == GiantBombApi.Models.VideoQuality.HD)
            });

            this.VideoQualityComboBox.Items.Add(new ComboBoxItem()
            {
                Content = "High",
                IsSelected = (currentPreference == GiantBombApi.Models.VideoQuality.High)
            });
            this.VideoQualityComboBox.Items.Add(new ComboBoxItem()
            {
                Content = "Low",
                IsSelected = (currentPreference == GiantBombApi.Models.VideoQuality.Low)
            });
        }

        private void VideoQuality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedObject = VideoQualityComboBox.SelectedItem as ComboBoxItem;

            if ((selectedObject != null) && (selectedObject.Content != null))
            {
                GiantBombApi.Models.VideoQuality newSelection;
                bool success = Enum.TryParse((string)selectedObject.Content, out newSelection);

                if (success && (newSelection != _videoManager.GetPreferredVideoQuality()))
                {
                    _videoManager.SetNewQualityPreference(newSelection);
                }
            }
        }
    }
}
