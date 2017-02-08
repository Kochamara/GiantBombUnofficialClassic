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
        public Uri HdUri
        {
            get
            {
                return _hdUri;
            }
            set
            {
                if (value != _hdUri)
                {
                    _hdUri = value;
                    RaisePropertyChanged(() => HdUri);
                }
            }
        }
        private Uri _hdUri;

        public RelayCommand PlayVideoCommand
        {
            get
            {
                return _playVideoCommand ?? (_playVideoCommand = new RelayCommand(
                () =>
                {
                    var navigationManager = Utilities.NavigationManager.GetInstance();
                    navigationManager.Navigate("VideoPlayerPage", new Uri(this.HdUri + "?api_key=" + Services.ApiKeyManager.GetInstance().GetSavedApiKey()));
                }));
            }
        }
        private RelayCommand _playVideoCommand;
    }
}
