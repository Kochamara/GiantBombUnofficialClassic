using GiantBombUnofficialClassic.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var unawaitedTask = PlayBackgroundAudio();
        }

        /// <summary>
        /// Plays some nice welcome music that goes "Giiiiiiiiiiaaaaaaaaaaaaaaant Booooooooooooomb"
        /// </summary>
        /// <returns></returns>
        private async Task PlayBackgroundAudio()
        {
            Windows.Storage.Streams.IRandomAccessStream stream = null;

            try
            {
                AudioPlayer = new MediaElement();
                Windows.Storage.StorageFile file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets//WelcomePageAudio.mp3"));

                if ((file != null) && File.Exists(file.Path))
                {
                    using (stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                    {
                        AudioPlayer.SetSource(stream, file.ContentType);
                        AudioPlayer.Play();
                        // Audio file is 21 seconds long
                        await Task.Delay(22000);
                    }
                    stream.Dispose();
                }
            }
            catch (Exception)
            {
                // No need to try any risky business for something as dumb as background audio
                if (stream != null)
                {
                    stream.Dispose();
                }
            }
        }

        private void LinkCodeHyperlink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            var unawaitedTask = Windows.System.Launcher.LaunchUriAsync(_viewModel.LinkCodeWebsite);
        }

        private void KeyEntryTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if ((e.Key == Windows.System.VirtualKey.Enter) && (e.KeyStatus.RepeatCount == 1))
            {
                var unawaitedTask = _viewModel.ConvertLinkCodeToApiKeyAndNavigateAsync(KeyEntryTextBox.Text);
            }
        }
    }
}
