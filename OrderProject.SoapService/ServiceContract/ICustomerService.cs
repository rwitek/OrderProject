using OrderProject.SoapService.DataContract;
using System.ServiceModel;

namespace OrderProject.SoapService.ServiceContract
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        public string AddCustomer(Customer customer);
    }
}
