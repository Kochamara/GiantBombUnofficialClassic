using GalaSoft.MvvmLight;
using GiantBombApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class VideoGroupsPageViewModel : ViewModelBase
    {
        private Utilities.NavigationManager _navigationManager;
        private string _apiKey;
        private GroupingType _pageType;

        public VideoGroupsPageViewModel(GroupingType pageType)
        {
            _navigationManager = Utilities.NavigationManager.GetInstance();
            _groups = new ObservableCollection<BasicViewModel>();
            _archivedGroups = new ObservableCollection<BasicViewModel>();
            _pageType = pageType;
        }

        public async Task InitializeAsync()
        {
            IsLoading = true;

            _apiKey = Services.ApiKeyManager.GetInstance().GetSavedApiKey();

            VideoGroupingsResponse response = null;

            switch (_pageType)
            {
                case GroupingType.Category:
                    response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideoCategoriesAsync(_apiKey);
                    break;
                case GroupingType.Show:
                default:
                    response = await GiantBombApi.Services.VideoRetrievalAgent.GetVideoShowsAsync(_apiKey);
                    break;
            }

            if ((response != null) && (response.Status == StatusCode.OK) && (response.Results != null))
            {
                var activeShowViewModels = new List<VideoGroupViewModel>();
                var inactiveShowViewModels = new List<VideoGroupViewModel>();

                foreach (var group in response.Results)
                {
                    Uri imageLocation = null;
                    group.GroupingType = _pageType;
                    if (group.Image == null)
                    {
                        imageLocation = Services.VideoGroupImageProvider.GetImageForGroupName(group.Title);
                    }
                    else
                    {
                        // Get the override image if we're using one. Otherwise, use the image that's provided.
                        imageLocation = Services.VideoGroupImageProvider.GetOverrideImageForGroupName(group.Title);

                        if (imageLocation == null)
                        {
                            if (!String.IsNullOrWhiteSpace(group.Image.SuperUrl))
                            {
                                imageLocation = new Uri(group.Image.SuperUrl);
                            }
                            else if (!String.IsNullOrWhiteSpace(group.Image.MediumUrl))
                            {
                                imageLocation = new Uri(group.Image.MediumUrl);
                            }
                            else if (!String.IsNullOrWhiteSpace(group.Image.SmallUrl))
                            {
                                imageLocation = new Uri(group.Image.SmallUrl);
                            }
                        }
                    }

                    var cat = new VideoGroupViewModel
                    {
                        Title = group.Title,
                        Description = group.Deck,
                        Id = group.Id,
                        ImageLocation = imageLocation,
                        Order = group.Position,
                        Source = group
                    };

                    // Active/Inactive distinction only exists for shows, not categories.
                    if ((group.GroupingType == GroupingType.Category) || (group.IsActive))
                    {
                        activeShowViewModels.Add(cat);
                    }
                    else
                    {
                        inactiveShowViewModels.Add(cat);
                    }
                }

                // Now time to order the list
                var orderedList = activeShowViewModels.OrderBy(x => x.Order);
                foreach (var group in orderedList)
                {
                    _groups.Add(group);
                }

                orderedList = inactiveShowViewModels.OrderBy(x => x.Order);
                foreach (var group in orderedList)
                {
                    _archivedGroups.Add(group);
                }
            }

            IsLoading = false;
        }

        public ObservableCollection<BasicViewModel> Groups
        {
            get { return _groups; }
        }
        private ObservableCollection<BasicViewModel> _groups;

        public ObservableCollection<BasicViewModel> ArchivedGroups
        {
            get { return _archivedGroups; }
        }
        private ObservableCollection<BasicViewModel> _archivedGroups;

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    RaisePropertyChanged("IsLoading");
                }
            }
        }
        private bool _isLoading;
    }
}