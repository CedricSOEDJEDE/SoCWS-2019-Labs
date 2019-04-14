using System.ServiceModel;

namespace WCFSoap
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Person GetData(string id);
    }
}