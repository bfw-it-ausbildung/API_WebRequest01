using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace API_WebRequest
{
    public class Kategorie
    {
        public int id { get; set; }
        public bool active { get; set; }
        public string name { get; set; }
        public int? position { get; set; }
        public int? parentId { get; set; }
        public int? mediaId { get; set; }
        public string childrenCount { get; set; }
        public string articleCount { get; set; }
    }

    [Serializable]
    [DataContract]
    public class Kategorien
    {
        [DataMember]
        public List<Kategorie> data { get; set; }
        [DataMember]
        public int total { get; set; }
        [DataMember]
        public bool success { get; set; }
        [DataMember]
        public string message { get; set; }
    }


}
