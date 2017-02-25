using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombApi.Models
{
    [DataContract]
    public class CategoriesResponse : Response
    {
        /// <summary>
        /// Zero or more items that match the filters specified
        /// </summary>
        [DataMember(Name = "results")]
        public IEnumerable<VideoCategory> Results;
    }

    [DataContract]
    public class VideoCategory
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
        /// Name of the video_category.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name;

        /// <summary>
        /// URL pointing to the video_category on Giant Bomb.
        /// </summary>
        [DataMember(Name = "site_detail_url")]
        public string SiteDetailUrl;
    }
}