using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWS_Caching
{
    [Serializable]
    public class Caching : ICaching
    {
        private DateTime contractLastUpdate;
        private List<string> contracts;

        private IDictionary<string, Tuple<DateTime, List<string>[]>> stations;

        private IDictionary<string, Tuple<DateTime, int>> availablesBikes;

        private IDictionary<string, Tuple<DateTime, string[]>> stationInformation;

        private TimeSpan lifeTime;

        public Caching()
        {
            contracts = new List<string>();
            stations = new Dictionary<string, Tuple<DateTime, List<string>[]>>();
            availablesBikes = new Dictionary<string, Tuple<DateTime, int>>();
            stationInformation = new Dictionary<string, Tuple<DateTime, string[]>>();
            lifeTime = new TimeSpan(0, 10, 0);
        }

        public void setLifeTime(TimeSpan time)
        {
            this.lifeTime = time;
        }

        public List<string> getContract()
        {
            if (contractLastUpdate == null)
                return null;

            if ((DateTime.Now - contractLastUpdate < lifeTime) && (contracts.Count != 0))
            {
                return contracts;
            }
            contracts = null;
            return null;
        }

        public void updateContracts(List<string> c)
        {
            contractLastUpdate = DateTime.Now;
            contracts = c;
            Binary.WriteToBinaryFile<Caching>(System.AppDomain.CurrentDomain.BaseDirectory + "/cache", this);
        }

        public void updateStations(string contract, List<string>[] s)
        {
            stations.Add(contract, new Tuple<DateTime, List<string>[]>(DateTime.Now, s));
            Binary.WriteToBinaryFile<Caching>(System.AppDomain.CurrentDomain.BaseDirectory + "/cache", this);
        }

        public void updateAvailableBike(string contract, string station, int number)
        {
            availablesBikes.Add(contract + " " + station, new Tuple<DateTime, int>(DateTime.Now, number));
            Binary.WriteToBinaryFile<Caching>(System.AppDomain.CurrentDomain.BaseDirectory + "/cache", this);
        }

        public void updateStationInformation(string contract, string station, string[] information)
        {
            stationInformation.Add(contract + " " + station, new Tuple<DateTime, string[]>(DateTime.Now, information));
            Binary.WriteToBinaryFile<Caching>(System.AppDomain.CurrentDomain.BaseDirectory + "/cache", this);
        }

        public int getAvailableBike(string contract, string station)
        {
            Tuple<DateTime, int> number;
            if (!availablesBikes.TryGetValue(contract + " " + station, out number))
            {
                return -1;
            }

            if (DateTime.Now - number.Item1 < lifeTime)
            {
                return number.Item2;
            }else
            {
                availablesBikes.Remove(contract + " " + station);
            }
            return -1;
        }

        public string[] getStationInformation(string contract, string station)
        {
            Tuple<DateTime, string[]> information;
            if (!stationInformation.TryGetValue(contract + " " + station, out information))
            {
                return null;
            }

            if (DateTime.Now - information.Item1 < lifeTime)
            {
                return information.Item2;
            }
            else
            {
                stationInformation.Remove(contract + " " + station);
            }
            return null;
        }

        public List<string>[] getStations(string c)
        {
            Tuple<DateTime, List<string>[]> res;
            if (!stations.TryGetValue(c, out res))
            {
                return null;
            }

            if (DateTime.Now - res.Item1 < lifeTime)
            {
                return res.Item2;
            }else
            {
                stations.Remove(c);
            }
            return null;
        }

    }
}
