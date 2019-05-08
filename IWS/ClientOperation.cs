using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using IWS_Caching;
namespace IWS
{
    public class ClientOperation : IClientOperation
    {

        private Caching caching;

        public ClientOperation()
        {
            getCache();
        }

        private void getCache()
        {
            caching = Binary.ReadFromBinaryFile<Caching>(System.AppDomain.CurrentDomain.BaseDirectory + "/cache") == null ? new Caching() :
                Binary.ReadFromBinaryFile<Caching>(System.AppDomain.CurrentDomain.BaseDirectory + "/cache");
            this.caching.setLifeTime(new TimeSpan(0, 0, 30));
        }

        public int getAvailableBikes(string contract, string station)
        {
            int number = caching.getAvailableBike(contract, station);
            if (number == -1)
            {
                WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations/"+station+"?contract="+contract+ "&apiKey=c5b41b9ed475fe8eace9aaffcde620fa313f37d6");
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                dynamic json = JsonConvert.DeserializeObject(responseFromServer);
                int n = json.available_bikes;
                caching.updateAvailableBike(contract, station, n);
                return n;
            }else
            {
                Console.WriteLine("From Cache");
                return number;
            }
            
        }

        public List<string> getContracts()
        {
            List<string> gotContract = caching.getContract();
            if (gotContract == null)
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
                }
                caching.updateContracts(contracts);
                return contracts;
            }else
            {
                Console.WriteLine("From Cache");
                return gotContract;
            }
                
        }

        public List<string>[] getStations(string contract)
        {
            List<string>[] gotStations = caching.getStations(contract);
            if (gotStations == null)
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
                caching.updateStations(contract, retour);
                return retour;
            }else
            {
                Console.WriteLine("From Cache");
                return gotStations;
            }
            
        }

        public string communicationTest()
        {
            return "Server access";
        }
    }
}
