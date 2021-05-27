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
                    Error = GetError(x.Response.GetResponseStream());
                    return null;
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
        private ErrorClass GetError(Stream stream)
        {
            HttpSerializer serializer = new HttpSerializer(typeof(ErrorClass));
            try
            {
                return (ErrorClass)serializer.Deserialize(stream);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}
