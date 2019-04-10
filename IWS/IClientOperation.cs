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
        List<String> getContracts();

        [OperationContract]
        List<String>[] getStations(string contract);

        [OperationContract]
        int getAvailableBikes(string contract, string station);

        [OperationContract]
        string communicationTest();
    }
}
