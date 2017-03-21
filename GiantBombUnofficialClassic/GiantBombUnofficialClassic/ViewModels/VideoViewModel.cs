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
                    var navigationManager = Utilities.NavigationManager.GetInstance();

                    if (Source != null)
                    {
                        navigationManager.Navigate(Views.VideoPlayerPage.PageKey, Source);
                    }
                    else
                    {
                        Serilog.Log.Error("Unable to navigate to video page without source video");
                    }
                });
            }
        }
    }
}
