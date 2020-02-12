using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace martinopapa_bot
{
    class StatoPS
    {
        private const string url = "https://servizi.apss.tn.it/opendata/STATOPS001.xml";

        public async Task<IEnumerable<ProntoSoccorso>> Lista()
        {
            var handler = new HttpClientHandler();
            handler.DefaultProxyCredentials = CredentialCache.DefaultNetworkCredentials;
            var httpClient = new HttpClient(handler);
            Stream stream = await httpClient.GetStreamAsync(url);
            XDocument doc = XDocument.Load(stream);

            IEnumerable<ProntoSoccorso> lista = doc.Root.Elements("PRONTO_SOCCORSO").Select(x =>
            {
                string nome = x.Element("PS").Value;
                string codice = x.Element("COD_PS_OD").Value;
                XElement attesaElement = x.Element("ATTESA");

                Attesa attesa = new Attesa()
                {
                    bianco = int.Parse(attesaElement.Element("BIANCO").Value),
                    verde = int.Parse(attesaElement.Element("VERDE").Value),
                    giallo = int.Parse(attesaElement.Element("GIALLO").Value),
                    rosso = int.Parse(attesaElement.Element("ROSSO").Value),
                };
                return new ProntoSoccorso(nome, codice, attesa);
            });
            return lista;
        }
    }
}

