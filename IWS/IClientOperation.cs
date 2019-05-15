using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWS
{
    [ServiceContract]
    public interface IClientOperation
    {
        [OperationContract]
        List<String> getContracts(string user);

        [OperationContract]
        List<String>[] getStations(string contract, string user);

        [OperationContract]
        int getAvailableBikes(string contract, string station, string user);

        [OperationContract]
        string communicationTest();

        [OperationContract]
        String[] getStationInformation(string contract, string station, string user);

        [OperationContract]
        List<List<string>> getLogs();
    }
}
