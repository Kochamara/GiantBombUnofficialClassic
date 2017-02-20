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
        public GiantBombApi.Models.Video Source;

        public RelayCommand PlayVideoCommand
        {
            get
            {
                return new RelayCommand(
                () =>
                {
                    var videoUriManager = Services.VideoUriManager.GetInstance();
                    var navigationManager = Utilities.NavigationManager.GetInstance();
                    var videoUri = videoUriManager.GetAppropriateVideoUri(this.Source);

                    if (videoUri != null)
                    {
                        navigationManager.Navigate(Views.VideoPlayerPage.PageKey, videoUri);
                    }
                    else
                    {
                        // TODO: Add logging
                    }
                });
            }
        }
    }
}
