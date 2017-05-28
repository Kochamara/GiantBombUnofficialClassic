using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class VideoGroupViewModel : BasicViewModel
    {
        public GiantBombApi.Models.VideoGrouping Source;

        public RelayCommand ViewGroupCommand
        {
            get
            {
                return new RelayCommand(
                        () =>
                        {
                            var navigationManager = Utilities.NavigationManager.GetInstance();
                            navigationManager.Navigate(Views.GroupPage.PageKey, this.Source);
                        });
            }
        }
    }
}
