using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace IWS
{
    public class ClientOperation : IClientOperation
    {

        public int getAvailableBikes(string contract, string station)
        {
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations/"+station+"?contract="+contract+ "&apiKey=c5b41b9ed475fe8eace9aaffcde620fa313f37d6");
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            dynamic json = JsonConvert.DeserializeObject(responseFromServer);
            return json.available_bikes;
        }

        public List<string> getContracts()
        {
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/contracts?apiKey=c5b41b9ed475fe8eace9aaffcde620fa313f37d6");
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            dynamic json = JsonConvert.DeserializeObject(responseFromServer);
            List<String> contracts = new List<string>();
            foreach (var cont in json)
            {
                contracts.Add(cont.name.ToString());
                /*if (cont.cities != null)
                {
                    foreach (var prop in cont.cities)
                    {
                        contracts.Add(prop.ToString());
                    }
                }*/
            }
            return contracts;
        }

        public List<string>[] getStations(string contract)
        {
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract="+contract+ "&apiKey=c5b41b9ed475fe8eace9aaffcde620fa313f37d6");
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            dynamic json = JsonConvert.DeserializeObject(responseFromServer);
            List<String> stations = new List<string>();
            List<String> numbers = new List<string>();
            foreach (var prop in json)
            {
                stations.Add(prop.name.ToString() /*+ ". Adresse :" + prop.address.ToString()*/);
                numbers.Add(prop.number.ToString());
            }
            List<string>[] retour = { stations, numbers };
            return retour;
        }

        public string communicationTest()
        {
            return "Server access";
        }
    }
}
