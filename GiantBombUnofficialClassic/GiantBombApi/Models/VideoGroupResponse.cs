using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombApi.Models
{
    /// <summary>
    /// Giant Bomb groups videos together three different ways:
    /// 
    /// Show - A series of videos with the same premise, like Mario Party Party, Jar Time w/ Jeff, or VRodeo
    /// Category - A traditional, generic category, like Reviews, Latest, or Events
    /// Type - Deprecated. Don't use this.
    /// 
    /// For shows and categories (both used in this app), their responses look mostly similar, so this VideoGroupingsResponse
    /// handles both of them. The primary differences are as follows:
    /// 
    /// * Categories don't use images
    /// * Shows use "name" instead of "title"
    ///
    /// </summary>
    [DataContract]
    public class VideoGroupingsResponse : Response
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
        /// Main image of the video_show. Not used for categories.
        /// </summary>
        [DataMember(Name = "image")]
        public Image Image;
        
        /// <summary>
        /// Whether it's a show or a category
        /// </summary>
        public GroupingType GroupingType;
        
        /// <summary>
        /// Name of the video_category. Not used by shows. Instead of referencing this, use Title.
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