using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWS
{
    [ServiceContract]
    public interface IMonitoring
    {
        [OperationContract]
        Dictionary<DateTime, string> getLogs();

        [OperationContract]
        int getLogsCount();
    }
}
