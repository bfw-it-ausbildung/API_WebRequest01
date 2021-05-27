using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace API_WebRequest
{
    [Serializable]
    [DataContract]
    class ErrorClass
    {
        [DataMember]
        public bool success { get; set; }
        [DataMember]
        public string message { get; set; }
    }
}
