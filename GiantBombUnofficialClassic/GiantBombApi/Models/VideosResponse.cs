﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GiantBombApi.Models
{
    [DataContract]
    public class VideosResponse : Response
    {
        /// <summary>
        /// Zero or more items that match the filters specified
        /// </summary>
        [DataMember(Name = "results")]
        public List<Video> Results;
    }

    [DataContract]
    public class Video
    {
        /// <summary>
        /// URL pointing to the video detail resource.
        /// </summary>
        [DataMember(Name = "api_detail_url")]
        public string ApiDetailUrl;

        /// <summary>
        /// Brief summary of the video.
        /// </summary>
        [DataMember(Name = "deck")]
        public string Deck;

        /// <summary>
        /// URL to the HD version of the video.
        /// </summary>
        [DataMember(Name = "hd_url")]
        public string HdUrl;

        /// <summary>
        /// URL to the High Res version of the video.
        /// </summary>
        [DataMember(Name = "high_url")]
        public string HighUrl;

        /// <summary>
        /// URL to the Low Res version of the video.
        /// </summary>
        [DataMember(Name = "low_url")]
        public string LowUrl;

        /// <summary>
        /// URL for video embed player. To be inserted into an iFrame.
        /// </summary>
        [DataMember(Name = "embed_player")]
        public string EmbedPlayer;

        /// <summary>
        /// Unique ID of the video.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id;

        /// <summary>
        /// Main image of the video.
        /// </summary>
        [DataMember(Name = "image")]
        public Image Image;

        /// <summary>
        /// Length (in seconds) of the video.
        /// </summary>
        [DataMember(Name = "length_seconds")]
        public string LengthInSeconds;

        /// <summary>
        /// Name of the video.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name;

        /// <summary>
        /// Date the video was published on Giant Bomb.
        /// </summary>
        [DataMember(Name = "publish_date")]
        public string PublishDate;

        /// <summary>
        /// URL pointing to the video on Giant Bomb.
        /// </summary>
        [DataMember(Name = "site_detail_url")]
        public string SiteDetailUrl;

        /// <summary>
        /// The video's filename.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url;

        /// <summary>
        /// Author of the video.
        /// </summary>
        [DataMember(Name = "user")]
        public string User;

        /// <summary>
        /// YouTube ID for the video.
        /// </summary>
        [DataMember(Name = "youtube_id")]
        public string YouTubeId;

        /// <summary>
        /// The time where the user left off watching this video.
        /// </summary>
        [DataMember(Name = "saved_time")]
        public string SavedTime;
    }
}
