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
    public sealed partial class OpenSourceLicenses : Page
    {
        public const string PageKey = "OpenSourceLicenses";

        public OpenSourceLicenses()
        {
            this.InitializeComponent();
            var unawaitedTask = PopulateLicenseTextAsync();
        }

        // Like, yeah, I could make a view model for this class, but fuck it
        public async Task PopulateLicenseTextAsync()
        {
            var licensesTextFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets//Licenses.txt"));
            if (File.Exists(licensesTextFile.Path))
            {
                var lines = await Windows.Storage.FileIO.ReadTextAsync(licensesTextFile);
                this.LicenseTextBlock.Text = lines;
            }
        }
    }
}
