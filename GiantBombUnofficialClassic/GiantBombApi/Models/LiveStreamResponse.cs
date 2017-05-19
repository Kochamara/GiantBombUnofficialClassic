using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombApi.Models
{
    [DataContract]
    public class LiveStreamResponse
    {
        /// <summary>
        /// Whether or not the request succeeded
        /// </summary>
        [DataMember(Name = "success")]
        public string Success;

        /// <summary>
        /// The active livestream, if it exists
        /// </summary>
        [DataMember(Name = "video")]
        public LiveStream Stream;
    }

    [DataContract]
    public class LiveStream
    {
        /// <summary>
        /// Brief summary of the video
        /// </summary>
        [DataMember(Name = "title")]
        public string Title;

        /// <summary>
        /// Promo image URL for the stream
        /// </summary>
        [DataMember(Name = "image")]
        public string Image;

        /// <summary>
        /// HLS (.m3u8) stream URL
        /// </summary>
        [DataMember(Name = "stream")]
        public string StreamSource;
    }
}
