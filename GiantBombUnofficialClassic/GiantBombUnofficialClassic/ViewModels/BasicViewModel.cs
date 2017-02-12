using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class BasicViewModel : ViewModelBase
    {
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    RaisePropertyChanged(() => Id);
                }
            }
        }
        private string _id;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    RaisePropertyChanged(() => Title);
                }
            }
        }
        private string _title;

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    RaisePropertyChanged(() => Description);
                }
            }
        }
        private string _description;

        public Uri ImageLocation
        {
            get
            {
                return _imageLocation;
            }
            set
            {
                if (value != _imageLocation)
                {
                    _imageLocation = value;
                    RaisePropertyChanged(() => ImageLocation);
                }
            }
        }
        private Uri _imageLocation;

        public RelayCommand Command
        {
            get
            {
                return _command;
            }
            set
            {
                if (value != _command)
                {
                    _command = value;
                    RaisePropertyChanged(() => Command);
                }
            }
        }
        private RelayCommand _command;
    }
}
