using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace IWS_Monitoring
{
    public class Monitor
    {
        ObjectCache cache = MemoryCache.Default;
        private double days = 30.0;

        public void save(string method, List<string> arguments)
        {
            Dictionary<DateTime, string> args = new Dictionary<DateTime, string>();
            if (cache.Contains("monitor"))
                args = (Dictionary<DateTime, string>)cache.Get("monitor");

            args.Add(DateTime.Now, "Appel à la méthode (" + method + ") avec les paramètres (" + arguments.ToString() + ") à " + DateTime.Now.ToString());

            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddDays(days);
            cache.Add("monitor", args, cacheItemPolicy);
        }

        public Dictionary<DateTime, string> getMonitor()
        {
            Dictionary<DateTime, string> args = new Dictionary<DateTime, string>();
            if (cache.Contains("monitor"))
                args = (Dictionary<DateTime, string>)cache.Get("monitor");
            return args;
        }
    }
}
