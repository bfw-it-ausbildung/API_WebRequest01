using API_WebRequest.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace API_WebRequest
{
    /// <summary>
    /// Klasse die (zunächst) nur den Abruf aus dem Shop implementiert. Sie implementiert
    /// die Schnittstelle IDisposable, damit man bei der Verwendung von Objekten die using-
    /// Anweisung benutzen kann.
    /// </summary>
    class HttpRequest : IDisposable
    {
        private HttpWebRequest request;
        private bool success = false;

        public ErrorClass Error { get; private set; } = null;

        /// <summary>
        /// Konstruktor der Klasse, der hier 3 Parameter benötigt. Neben der URL und den 
        /// Zugangsdaten ist das auch immer der benötigte API Endpunkt, welcher zum Abruf
        /// bestimmter Daten benötigt wird
        /// </summary>
        /// <param name="url"></param>
        /// <param name="endpoint"></param>
        /// <param name="credential"></param>
        public HttpRequest(string url, string endpoint, NetworkCredential credential) {
            request = WebRequest.CreateHttp(String.Format("{0}/api/{1}/", url, endpoint));
            request.Credentials = credential;
            request.ContentType = "application/json";
        }

        public void Dispose()
        {
            request = null;
        }

        /// <summary>
        /// Methode die vom Aufrufer verwendet wird, um einen bestimmten API-Aufruf
        /// auszulösen (der Aufruf wird schon im Konstruktor festgelegt), anschließend
        /// mit Hilfe des übergebenen Serialisierers auch die Deserilisation auslöst und
        /// letztlich das Ergebnis an den Aufrufer zurück gibt.
        /// </summary>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public object GetData(HttpSerializer serializer)
        {
            try
            {
                return serializer.Deserialize(request.GetResponse().GetResponseStream());
            }
            catch (WebException x)
            {
                try
                {
                    Error = (ErrorClass)GetError(x.Response.GetResponseStream(), typeof(ErrorClass));
                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Methode zum Senden von Daten an den Shop. Je nach Übergabe der "method" können
        /// Daten angelegt (POST) oder geändert (PUT) werden.
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="method"></param>
        /// <param name="data">Objekt mit Nutzdaten</param>
        /// <returns></returns>
        public object SendData(HttpSerializer serializer, string method, object data)
        {
            // setzt die Methode für den Request
            request.Method = method;
            try
            {
                // holen den Datenstromes für die Anfrage und speichern in einem Stream
                using (Stream stream = request.GetRequestStream())
                {
                    // Das Nutzobjekt in diesen Stream "Hinein serialisieren"
                    serializer.Serialize(stream, data);
                    
                    // Anlage eines neuen Serializer Objektes mit dem Typ der jeweiligen
                    // Antwort-Klasse (hier heißt ResponseClass). Objekte dieser Klasse
                    // besitzen ein Data Objekt mit der ID (laufende Nummer aus Shopware)
                    // und der URL unter der das angelegte Objekt im Shop erreichbar ist.
                    HttpSerializer serializer1 = new HttpSerializer(typeof(ResponseClass));
                    // Die Serverantwort in diesen Typ deserialisieren und zurückgeben
                    return serializer1.Deserialize(request.GetResponse().GetResponseStream());
                }
            }
            catch (WebException x)
            {
                try
                {
                    // Hier ein Beispiel, falls man auch die Fehlerklasse mit in die
                    // ResponseClass deserialisieren möchte. Zusätzlich muss dann die
                    // Eigenschaft "message" in die ResponseClass aufgenommen werden.
                    // Ggf. liegt der Vorteil darin, dass man im Aufrufer das success
                    // auswerten kann und nicht den Typ der Rückgabe prüfen muss.
                    return GetError(x.Response.GetResponseStream(), typeof(ResponseClass));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Diese Methode wird immer dann aufgerufen, wenn es beim Abruf zu einem
        /// Fehlermeldung aus der API kommt. Man benötigt diese Methode, damit man
        /// die originale Shop Meldung ausgeben kann.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private object GetError(Stream stream, Type type)
        {
            HttpSerializer serializer = new HttpSerializer(type);
            try
            {
                return serializer.Deserialize(stream);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}
