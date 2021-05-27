using System;
using System.Collections.Generic;
using System.Text;

namespace API_WebRequest
{
    public class KategorieDetailsData
    {
        public string sortingIds { get; set; }
        public bool hideSortings { get; set; }
        public string facetIds { get; set; }
        public object shops { get; set; }
        public int id { get; set; }
        public int? parentId { get; set; }
        public object streamId { get; set; }
        public string name { get; set; }
        public object position { get; set; }
        public string metaTitle { get; set; }
        public string metaKeywords { get; set; }
        public string metaDescription { get; set; }
        public string cmsHeadline { get; set; }
        public string cmsText { get; set; }
        public bool active { get; set; }
        public object template { get; set; }
        public object productBoxLayout { get; set; }
        public bool blog { get; set; }
        public string path { get; set; }
        public string external { get; set; }
        public string externalTarget { get; set; }
        public bool hideFilter { get; set; }
        public bool hideTop { get; set; }
        public DateTime changed { get; set; }
        public DateTime added { get; set; }
        public object mediaId { get; set; }
        public object attribute { get; set; }
        public List<object> emotions { get; set; }
        public object media { get; set; }
        public List<object> customerGroups { get; set; }
        public List<object> manualSorting { get; set; }
        public string childrenCount { get; set; }
        public string articleCount { get; set; }
    }

    public class KategorieDetails
    {
        public KategorieDetailsData data { get; set; }
        public bool success { get; set; }
    }


}
