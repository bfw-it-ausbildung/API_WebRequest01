using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace API_WebRequest.Model
{
    public class Data
    {
        public int id { get; set; }
        public string location { get; set; }
    }

    [DataContract]
    public class ResponseClass
    {
        [DataMember]
        public bool success { get; set; }
        [DataMember]
        public Data data { get; set; }
        [DataMember]
        // nur nötig, wenn man diese Klasse für erfolgreiche und
        // fehlerhafte Übertragungen verwendet. Sonst kann die
        // Eigenschaft hier raus und man muss eine separate 
        // Fehler-Klasse nutzen.
        public string message { get; set; }
    }
}
