using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace API_WebRequest
{
    [Serializable]
    [DataContract]
    class Version
    {
        [DataMember]
        public VersionData data { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public bool success { get; set; }
    }

    [DataContract]
    class VersionData
    {
        [DataMember]
        public string version { get; set; }
        [DataMember]
        public string revision { get; set; }
    }
}
