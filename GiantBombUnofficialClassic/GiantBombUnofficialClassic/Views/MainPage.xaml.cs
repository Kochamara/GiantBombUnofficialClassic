using GiantBombUnofficialClassic.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GiantBombUnofficialClassic.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private LetsGoViewModel _context;

        public MainPage()
        {
            this.InitializeComponent();

            _context = new LetsGoViewModel();
            this.DataContext = _context;

            var unawaitedTask = _context.InitializeAsync();

            var _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("http://v.giantbomb.com/2017/01/27/vf_giantbomb_bestof_115_4000.mp4"));
            _mediaPlayer.Play();
            _mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            
        }
    }
}
