using System.ServiceModel;

namespace WCFRest
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Person GetData(string id);
    }
}