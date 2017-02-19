using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class VideoViewModel : BasicViewModel
    {
        public Uri VideoUri
        {
            get
            {
                return _videoUri;
            }
            set
            {
                if (value != _videoUri)
                {
                    _videoUri = value;
                    RaisePropertyChanged(() => VideoUri);
                }
            }
        }
        private Uri _videoUri;

        public RelayCommand PlayVideoCommand
        {
            get
            {
                return _playVideoCommand ?? (_playVideoCommand = new RelayCommand(
                () =>
                {
                    var navigationManager = Utilities.NavigationManager.GetInstance();
                    navigationManager.Navigate(Views.VideoPlayerPage.PageKey, VideoUri);
                }));
            }
        }
        private RelayCommand _playVideoCommand;
    }
}
