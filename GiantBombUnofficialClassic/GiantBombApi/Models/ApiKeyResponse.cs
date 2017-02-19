using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombApi.Models
{
    [DataContract]
    public class ApiKeyResponse
    {
        /// <summary>
        /// Your API key!
        /// </summary>
        [DataMember(Name = "regToken")]
        public string ApiKey;
    }
}