using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace IWS_Caching
{
    [Serializable]
    public class CachingV2 : ICaching
    {
        ObjectCache cache = MemoryCache.Default;
        private double minutes = 10.0;

        public CachingV2(double time)
        {
            this.minutes = time;    
        }

        public void setLifeTime(double time)
        {
            this.minutes = time;
        }

        public List<string> getContract()
        {
            if (cache.Contains("contract"))
                return (List<string>)cache.Get("contract");
            return null;
        }

        public void updateContracts(List<string> c)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(minutes);
            cache.Add("contract", c, cacheItemPolicy);
        }

        public void updateStations(string contract, List<string>[] s)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(minutes);
            cache.Add("stations" + contract, s, cacheItemPolicy);
        }

        public void updateAvailableBike(string contract, string station, int number)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(minutes);
            cache.Add("availableBike" + contract + station, number, cacheItemPolicy);
        }

        public void updateStationInformation(string contract, string station, string[] information)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(minutes);
            cache.Add("stationInformation" + contract + station, information, cacheItemPolicy);
        }

        public int getAvailableBike(string contract, string station)
        {
            if (cache.Contains("availableBike" + contract + station))
                return (int)cache.Get("availableBike" + contract + station);

            return -1;
        }

        public string[] getStationInformation(string contract, string station)
        {
            if (cache.Contains("stationInformation" + contract + station))
                return (string[])cache.Get("stationInformation" + contract + station);

            return null;
        }

        public List<string>[] getStations(string c)
        {
            if (cache.Contains("stations" + c))
                return (List<string>[])cache.Get("stations" + c);
            return null;
        }
    }
}
