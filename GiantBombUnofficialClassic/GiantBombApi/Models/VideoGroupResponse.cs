using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombApi.Models
{
    [DataContract]
    public class VideoGroupResponse : Response
    {
        /// <summary>
        /// Zero or more items that match the filters specified
        /// </summary>
        [DataMember(Name = "results")]
        public IEnumerable<VideoGrouping> Results;
    }

    [DataContract]
    public class VideoGrouping
    {
        /// <summary>
        /// URL pointing to the video_category detail resource.
        /// </summary>
        [DataMember(Name = "api_detail_url")]
        public string ApiDetailUrl;

        /// <summary>
        /// Brief summary of the video_category.
        /// </summary>
        [DataMember(Name = "deck")]
        public string Deck;

        /// <summary>
        /// Unique ID of the video_category.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id;

        /// <summary>
        /// Title of the video_show.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title;

        /// <summary>
        /// Editor ordering.
        /// </summary>
        [DataMember(Name = "position")]
        public int Position;

        /// <summary>
        /// URL pointing to the video_category on Giant Bomb.
        /// </summary>
        [DataMember(Name = "site_detail_url")]
        public string SiteDetailUrl;

        /// <summary>
        /// Main image of the video_show.
        /// </summary>
        [DataMember(Name = "image")]
        public VideoImage Image;



        public GroupingType CategoryType;


        /// <summary>
        /// Name of the video_category.
        /// </summary>
        [DataMember(Name = "name")]
        public string DoNotUse_Name
        {
            get
            {
                return Title;
            }
            set
            {
                this.Title = value;
            }
        }
    }

    public enum GroupingType
    {
        None,
        Category,
        Show
    }
}