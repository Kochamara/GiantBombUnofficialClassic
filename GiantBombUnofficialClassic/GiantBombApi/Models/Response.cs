using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace GiantBombApi.Models
{
    [DataContract]
    public class Response
    {
        /// <summary>
        /// An enum indicating the result of the request. 
        /// </summary>
        [DataMember (Name = "status_code")]
        public StatusCode Status;

        /// <summary>
        /// A text string representing the status_code
        /// </summary>
        [DataMember (Name = "error")]
        public string Error;

        /// <summary>
        /// The number of total results matching the filter conditions specified
        /// </summary>
        [DataMember (Name = "number_of_total_results")]
        public int NumberOfTotalResults;

        /// <summary>
        /// The number of results on this page
        /// </summary>
        [DataMember (Name = "number_of_page_results")]
        public int NumberOfPageResults;

        /// <summary>
        /// The value of the limit filter specified, or 100 if not specified
        /// </summary>
        [DataMember (Name = "limit")]
        public int Limit;

        /// <summary>
        /// The value of the offset filter specified, or 0 if not specified
        /// </summary>
        [DataMember (Name = "offset")]
        public int Offset;

        /// <summary>
        /// Zero or more items that match the filters specified
        /// </summary>
        [DataMember(Name = "results")]
        public IEnumerable<Video> Results;
    }

    public enum StatusCode
    {
        [EnumMember(Value = "0")]
        Unknown = 0,
        [EnumMember(Value = "1")]
        OK = 1,
        [EnumMember(Value = "100")]
        InvalidApiKey = 100,
        [EnumMember(Value = "101")]
        ObjectNotFound = 101,
        [EnumMember(Value = "102")]
        ErrorInUrlFormat = 102,
        [EnumMember(Value = "103")]
        JsonpFormatRequiresJsonCallbackArg = 103,
        [EnumMember(Value = "104")]
        FilterError = 104,
        [EnumMember(Value = "105")]
        SubscriberOnlyVideo = 105
    }
}
