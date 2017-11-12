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

        /// <summary>
        /// How far the user has watched this video previously. Value from 0 to 100.
        /// </summary>
        public int PercentageComplete
        {
            get
            {
                return _percentageComplete;
            }
            set
            {
                if (value != _percentageComplete)
                {
                    _percentageComplete = value;
                    RaisePropertyChanged("PercentageComplete");
                    RaisePropertyChanged("PercentageCompleteString");
                    RaisePropertyChanged("PercentageRemainingString");
                }
            }
        }
        private int _percentageComplete;

        /// <summary>
        /// Because we are showing a visual indicator in XAML, we need to convert the integer to
        /// a string that ends with a *. This is used in the column definitions.
        /// </summary>
        public string PercentageCompleteString
        {
            get
            {
                return (this.PercentageComplete + "*");
            }
        }

        public string PercentageRemainingString
        {
            get
            {
                return ((100 - this.PercentageComplete) + "*");
            }
        }

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
