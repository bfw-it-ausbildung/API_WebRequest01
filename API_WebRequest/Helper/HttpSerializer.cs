using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace API_WebRequest
{
    /// <summary>
    /// Die Serialisiererklasse die für die Deserialisierung der Serverantwort 
    /// benötigt wird.
    /// </summary>
    class HttpSerializer
    {
        private DataContractJsonSerializer serializer;

        /// <summary>
        /// Konstruktor, der den Typ des Serialisierers im Parameter benötigt
        /// </summary>
        /// <param name="type"></param>
        public HttpSerializer(Type type)
        {
            serializer = new DataContractJsonSerializer(type,
                // dieser Teil ist wesentlich und dient dazu, die Datumswerte korrekt
                // umzusetzen
                new DataContractJsonSerializerSettings()
                {
                    DateTimeFormat = new DateTimeFormat("yyyy-MM-ddTHH:mm:sszzz")
                });
            serializer = new DataContractJsonSerializer(type, new DataContractJsonSerializerSettings() 
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-ddTHH:mm:ssK")
            });
        }

        /// <summary>
        /// Methode zum Deserialisieren, sie liest die json Daten aus dem Datenstrom und
        /// gibt die Daten an den Aufrufer zurück. Die json Daten stammen aus der Antwort 
        /// des Shops.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public object Deserialize(Stream stream)
        {
            return serializer.ReadObject(stream);
        }
    }
}
