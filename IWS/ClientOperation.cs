﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using IWS_Caching;
using IWS_Monitoring;
namespace IWS
{
    public class ClientOperation : IClientOperation
    {

        private ICaching caching;
        private Monitor monitor = new Monitor();

        private ICaching getCache()
        {
            Caching c;
            c = Binary.ReadFromBinaryFile<Caching>(System.AppDomain.CurrentDomain.BaseDirectory + "/cache") == null ? new Caching() :
                Binary.ReadFromBinaryFile<Caching>(System.AppDomain.CurrentDomain.BaseDirectory + "/cache");
            c.setLifeTime(new TimeSpan(0, 0, 30));
            return c;
        }

        private ICaching getCacheV2()
        {
            return new CachingV2(1);
        }

        public int getAvailableBikes(string contract, string station, string user)
        {
            DateTime start = DateTime.Now;
            monitor.save("getAvailableBikes", new List<string>() { contract, station });

            if (user == "admin")
                caching = getCache();
            else
                caching = getCacheV2();

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
                Console.WriteLine("Temps d'exécution : " + (DateTime.Now - start));
                return n;
            }else
            {
                Console.WriteLine("From Cache");
                Console.WriteLine("Temps d'exécution : " + (DateTime.Now - start));
                return number;
            }
            
            
        }

        public String[] getStationInformation(string contract, string station, string user)
        {
            DateTime start = DateTime.Now;
            monitor.save("getAvailableBikes", new List<string>() { contract, station });

            if (user == "admin")
                caching = getCache();
            else
                caching = getCacheV2();

            string[] information = caching.getStationInformation(contract, station);
            if (information == null)
            {
                WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations/" + station + "?contract=" + contract + "&apiKey=c5b41b9ed475fe8eace9aaffcde620fa313f37d6");
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                dynamic json = JsonConvert.DeserializeObject(responseFromServer);
                String[] info = { json.name.ToString(), json.number.ToString(), json.available_bikes.ToString(), json.bike_stands.ToString() };
                caching.updateStationInformation(contract, station, info);
                Console.WriteLine("Temps d'exécution : " + (DateTime.Now - start));
                return info;
            }
            else
            {
                Console.WriteLine("From Cache");
                Console.WriteLine("Temps d'exécution : " + (DateTime.Now - start));
                return information;
            }

        }

        public List<string> getContracts(string user)
        {
            DateTime start = DateTime.Now;
            monitor.save("getContracts", new List<string>());
            if (user == "admin")
                caching = getCache();
            else
                caching = getCacheV2();

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
                Console.WriteLine("Temps d'exécution : " + (DateTime.Now - start));
                return contracts;
            }else
            {
                Console.WriteLine("From Cache");
                Console.WriteLine("Temps d'exécution : " + (DateTime.Now - start));
                return gotContract;
            }
                
        }

        public List<string>[] getStations(string contract, string user)
        {
            DateTime start = DateTime.Now;
            monitor.save("getStations", new List<string>() { contract });
            if (user == "admin")
                caching = getCache();
            else
                caching = getCacheV2();

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
                List<String> numberofbikes = new List<string>();
                List<String> numberofstands = new List<string>();

                foreach (var prop in json)
                {
                    stations.Add(prop.name.ToString() /*+ ". Adresse :" + prop.address.ToString()*/);
                    numbers.Add(prop.number.ToString());
                    numberofbikes.Add(prop.available_bikes.ToString());
                    numberofstands.Add(prop.bike_stands.ToString());
                }
                List<string>[] retour = { stations, numbers, numberofbikes, numberofstands };
                caching.updateStations(contract, retour);
                Console.WriteLine("Temps d'exécution : " + (DateTime.Now - start));
                return retour;
            }else
            {
                Console.WriteLine("From Cache");
                Console.WriteLine("Temps d'exécution : " + (DateTime.Now - start));
                return gotStations;
            }
            
        }

        public string communicationTest()
        {
            return "Server access";
        }
    }
}
