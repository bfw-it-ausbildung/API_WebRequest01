using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace API_WebRequest
{
    [Serializable]
    [DataContract]
    public class KategorieDetailsData
    {
        [DataMember]
        public string sortingIds { get; set; }
        [DataMember]
        public bool hideSortings { get; set; }
        [DataMember]
        public string facetIds { get; set; }
        [DataMember]
        public object shops { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int? parentId { get; set; }
        [DataMember]
        public object streamId { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public object position { get; set; }
        [DataMember]
        public string metaTitle { get; set; }
        [DataMember]
        public string metaKeywords { get; set; }
        [DataMember]
        public string metaDescription { get; set; }
        [DataMember]
        public string cmsHeadline { get; set; }
        [DataMember]
        public string cmsText { get; set; }
        [DataMember]
        public bool active { get; set; }
        [DataMember]
        public object template { get; set; }
        [DataMember]
        public object productBoxLayout { get; set; }
        [DataMember]
        public bool blog { get; set; }
        [DataMember]
        public string path { get; set; }
        [DataMember]
        public string external { get; set; }
        [DataMember]
        public string externalTarget { get; set; }
        [DataMember]
        public bool hideFilter { get; set; }
        [DataMember]
        public bool hideTop { get; set; }
        [DataMember]
        public DateTime changed { get; set; }
        [DataMember]
        public DateTime added { get; set; }
        [DataMember]
        public object mediaId { get; set; }
        [DataMember]
        public object attribute { get; set; }
        [DataMember]
        public List<object> emotions { get; set; }
        [DataMember]
        public object media { get; set; }
        [DataMember]
        public List<object> customerGroups { get; set; }
        
        public List<object> manualSorting { get; set; }
        [DataMember]
        public string childrenCount { get; set; }
        [DataMember]
        public string articleCount { get; set; }
    }

    public class KategorieDetails
    {
        public KategorieDetailsData data { get; set; }
        public bool success { get; set; }
        /// <summary>
        /// Message wird nur benötigt, wenn man keine explizite Klasse
        /// zum Abfangen von Error-Meldungen verwenden möchte.
        /// </summary>
        public string message { get; set; }
    }


}
