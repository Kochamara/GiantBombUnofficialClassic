using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class CategoryViewModel : BasicViewModel
    {
        public GiantBombApi.Models.VideoCategory Source;

        public RelayCommand ViewCategoryCommand
        {
            get
            {
                return new RelayCommand(
                () =>
                {
                    var navigationManager = Utilities.NavigationManager.GetInstance();
                    navigationManager.Navigate("CategoryPage", this.Source);
                });
            }
        }
    }
}
