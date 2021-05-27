using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace API_WebRequest
{
    class Program
    {
        // Zugangsdaten für den Shop
        const string URL = "http://www.shopware5.tld";
        const string USER = "apiUser";
        const string PASS = "zVLq9ulBdsgRi6n2hEnK4upotIJJsVk8GG9X0FQN";

        static void Main(string[] args)
        {
            // Klasse mit den Anmeldeinformationen, wird für den Request benötigt
            NetworkCredential credential = new NetworkCredential()
            {
                UserName=USER, Password=PASS
            };

            #region ohneHttpRequestKlasse
            // hier ist alles einfach runterprogrammiert und verwendet die Methoden 
            // im unteren Bereich dieser Seite
            //try
            //{
            //    Version version = 
            //        (Version)GetResponse(URL, "version", credential, typeof(Version));


            //    if (version.success)
            //    {
            //        Console.WriteLine("Abruf erfolgreich! Shopversion ist: {0}",
            //            version.data.version);
            //    }
            //    else
            //        Console.WriteLine("Abruf fehlgeschlagen! Fehler: {0}", version.message);

            //    Console.WriteLine("Taste für Kategorieliste");
            //    Console.ReadKey();

            //    Kategorien kategorien = 
            //        (Kategorien)GetResponse(URL, "categories", credential, typeof(Kategorien));

            //    if (kategorien.success)
            //    {
            //        Console.WriteLine("Kategoriename");
            //        Console.WriteLine(new String('-', 25));
            //        foreach(Kategorie kat in kategorien.data)
            //        {
            //            Console.WriteLine(kat.name);
            //        }
            //        Console.WriteLine(new String('-', 25));
            //        Console.WriteLine("insgesamt {0} Kategorien", kategorien.total);
            //    }


            //}catch(WebException x)
            //{
            //    Console.WriteLine(x.Message);
            //}catch(Exception x)
            //{
            //    Console.WriteLine(x.Message);
            //}
            #endregion

            #region VersionmitHttpRequestKlasse
            // durch Hinzufügen der Dispose Schnittstelle kann die unsig verwendet 
            // werden, was eine sofortige Freigabe der Ressource nach der abschließenden
            // geschweiften Klammer bewirkt
            using (HttpRequest http = new HttpRequest(URL, "version", credential))
            {
                // Instanzerstellung und gleichzeitige Zuweisung der Daten, durch Verwendung
                // der Methode aus der Klasse HttpRequest
                Version version1 = (Version)http.GetData(new HttpSerializer(typeof(Version)));

                // prüfen ob, der Abruf erfolgreich war, das kann man zb. indem man gegen
                // null testet. Im Anschluss kann zusätzlich die Eigenschaft success auf true
                // geprüft werden. Nur wenn beides wahr ist, befinden sich Daten in der Instanz
                if (version1 != null && version1.success)
                {
                    Console.WriteLine("Abruf erfolgreich! Shopversion ist: {0}",
                        version1.data.version);
                }
                else
                    Console.WriteLine("Abruf fehlgeschlagen! Fehler: {0}", http.Error.message);
            }
            #endregion

            Console.ReadKey();

            #region KategorienmitHttpRequestKlasse
            // wie oben
            using (HttpRequest http = new HttpRequest(URL, "categories", credential))
            {
                Kategorien categories = (Kategorien)http.GetData(new HttpSerializer(typeof(Kategorien)));

                if (categories != null && categories.success)
                {
                    Console.WriteLine("Abruf erfolgreich! {0} Kategorien verfügbar",
                        categories.total);
                    // hier werden noch zu jeder Kategorie die Details aus dem Shop abgerufen
                    // exemplarisch werden hier einige Detail-Attribute ausgegeben
                    foreach(Kategorie category in categories.data)
                    {
                        Console.WriteLine("{0}", category.name);
                        // hier beginnt der Abruf der Details, das Muster ist wie mit den anderen
                        // identisch, bis auf die Ausnahme, dass neben categories nun auch die ID
                        // der Kategorie, für die die Details abgerufen werden sollen mit angegeben
                        // werden muss
                        using(HttpRequest http1 = new HttpRequest(URL, "categories/"+category.id, credential))
                        {
                            KategorieDetails kategorieDetails = (KategorieDetails)http1.GetData(new HttpSerializer(typeof(KategorieDetails)));
                            if (kategorieDetails != null && kategorieDetails.success)
                            {
                                Console.WriteLine("Details[ID={0}]: [Meta-Beschreibung={1}]; [Meta-Schlagworte={2}]",
                                    kategorieDetails.data.id,
                                    kategorieDetails.data.metaDescription, 
                                    kategorieDetails.data.metaKeywords);
                            }
                            else
                            {
                                Console.WriteLine("Fehler beim Abruf der Detaildaten! Fehler: {0}",
                                    http1.Error.message);
                            }
                        }
                    }
                }
                else
                    Console.WriteLine("Abruf fehlgeschlagen! Fehler: {0}", http.Error.message);
            }
            #endregion
        }

        /// <summary>
        /// Methode zum Abruf von Shopinformationen
        /// </summary>
        /// <param name="url"></param>
        /// <param name="call"></param>
        /// <param name="credential"></param>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        static object GetResponse(string url, string call, 
            NetworkCredential credential, Type type, string method = "GET")
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url+call+"/");
            myReq.Method = method;
            myReq.Credentials = credential;

            try
            {
                using (WebResponse response = myReq.GetResponse())
                {
                    return JsonDeserialize(response.GetResponseStream(), type);
                }
            }catch(WebException x)
            {
                throw x;
            }
        }

        /// <summary>
        /// Methode zum Deserialisieren, der Serverantwort in den jeweils 
        /// benötigten Klassen-Typ
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        static object JsonDeserialize(Stream stream, Type type)
        {
            try
            {
                DataContractJsonSerializer serializer =
                        new DataContractJsonSerializer(type);

                return serializer.ReadObject(stream);
            }catch(Exception x)
            {
                throw x;
            }
        }
    }
}
