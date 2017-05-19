using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombApi.Models
{
    /// <summary>
    /// Collection of URLs for a given image. Not properly documented in the API.
    /// </summary>
    [DataContract]
    public class Image
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "icon_url")]
        public string IconUrl;

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "medium_url")]
        public string MediumUrl;

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "screen_url")]
        public string ScreenUrl;

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "small_url")]
        public string SmallUrl;

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "super_url")]
        public string SuperUrl;

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "thumb_url")]
        public string ThumbUrl;

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "tiny_url")]
        public string TinyUrl;
    }
}
