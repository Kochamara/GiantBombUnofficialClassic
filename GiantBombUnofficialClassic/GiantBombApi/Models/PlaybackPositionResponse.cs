using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombApi.Models
{
    [DataContract]
    public class PlaybackPositionResponse
    {
        /// <summary>
        /// Whether or not the request succeeded
        /// </summary>
        [DataMember(Name = "success")]
        public string Success;

        /// <summary>
        /// Saved playback position of the video, in seconds
        /// </summary>
        [DataMember(Name = "savedTime")]
        public double SavedTime;

        /// <summary>
        /// Message from the Top Men
        /// </summary>
        [DataMember(Name = "message")]
        public string Description;
    }
}