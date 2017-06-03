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
    public sealed partial class TextPage : Page
    {
        public const string PageKey = "TextPage";

        /// <summary>
        /// Navigate to this page with a URI of a text file to open. This page will show it. What fun!
        /// </summary>
        public TextPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if ((e != null) && (e.Parameter != null))
            {
                if (e.Parameter is Uri)
                {
                    var textUri = e.Parameter as Uri;
                    var unawaitedTask = PopulateTextFieldAsync(textUri);
                }
                else
                {
                    Serilog.Log.Error("Unable to populate the TextPage's TextBox without some text");
                }
            }
        }

        //new Uri(@"ms-appx:///Assets//Licenses.txt")

        // Like, yeah, I could make a view model for this class, but fuck it
        private async Task PopulateTextFieldAsync(Uri textFileLocation)
        {
            var textFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(textFileLocation);
            if (File.Exists(textFile.Path))
            {
                var lines = await Windows.Storage.FileIO.ReadTextAsync(textFile);
                this.TextBlock.Text = lines;
            }
        }
    }
}