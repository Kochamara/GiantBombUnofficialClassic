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
                    RaisePropertyChanged("Id");
                }
            }
        }
        private string _id;

        public string Title
        {
            get
            {
                return _title;
                //return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras sagittis mi pulvinar tempor commodo. Aliquam fermentum tempor velit. Nunc efficitur, tellus in volutpat bibendum, mi metus finibus lorem, eget cursus velit lorem at risus. Donec ut risus pulvinar, malesuada ex feugiat, finibus diam. Pellentesque luctus massa urna, nec mattis ipsum egestas nec. Sed sollicitudin mollis quam sit amet pulvinar. Sed aliquet facilisis rutrum. Morbi venenatis turpis id congue efficitur. Proin id feugiat mauris. Proin semper porttitor dictum. Aliquam tristique libero vel nunc fringilla pretium. Aliquam erat volutpat. Nulla rhoncus ornare mauris, non iaculis erat finibus eu.";
            }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }
        private string _title;

        public string Description
        {
            get
            {
                return _description;
                //return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras sagittis mi pulvinar tempor commodo. Aliquam fermentum tempor velit. Nunc efficitur, tellus in volutpat bibendum, mi metus finibus lorem, eget cursus velit lorem at risus. Donec ut risus pulvinar, malesuada ex feugiat, finibus diam. Pellentesque luctus massa urna, nec mattis ipsum egestas nec. Sed sollicitudin mollis quam sit amet pulvinar. Sed aliquet facilisis rutrum. Morbi venenatis turpis id congue efficitur. Proin id feugiat mauris. Proin semper porttitor dictum. Aliquam tristique libero vel nunc fringilla pretium. Aliquam erat volutpat. Nulla rhoncus ornare mauris, non iaculis erat finibus eu.";
            }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }
        private string _description;

        public int Order
        {
            get
            {
                return _order;
            }
            set
            {
                if (value != _order)
                {
                    _order = value;
                    RaisePropertyChanged("Order");
                }
            }
        }
        private int _order;

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
                    RaisePropertyChanged("ImageLocation");
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
                    RaisePropertyChanged("Command");
                }
            }
        }
        private RelayCommand _command;
    }
}
