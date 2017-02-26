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
                        if ((this.Source != null) && (!String.IsNullOrWhiteSpace(this.Source.Name)))
                        {
                            Serilog.Log.Error("Unable to navigate to video page without URI for " + this.Source.Name);
                        }
                        else
                        {
                            Serilog.Log.Error("Unable to navigate to video page without source video");
                        }
                    }
                });
            }
        }
    }
}
