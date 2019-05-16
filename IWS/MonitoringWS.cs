using System;
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
    public class MonitoringWS : IMonitoring
    {
        private Monitor monitor = new Monitor();

        public Dictionary<DateTime, string> getLogs()
        {
            return monitor.getMonitor();
        }

        public int getLogsCount()
        {
            return monitor.getMonitor().Count;
        }

        public List<string> getLogsForTwoLastDay()
        {
            Dictionary<DateTime, string> logs = monitor.getMonitor();
            List<string> twodaysLog = new List<string>();
            foreach(KeyValuePair<DateTime, string> date in logs)
            {
                //Console.WriteLine(DateTime.Now.AddSeconds(-60));
                //Console.WriteLine(date.Key);
                if (date.Key > DateTime.Now.AddSeconds(-10))
                {
                    twodaysLog.Add(date.Value);
                }
            }
            return twodaysLog;
        }
    }
}
