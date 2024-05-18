using OrderProject.Application.Interfaces;
using OrderProject.SoapService.DataContract;

namespace OrderProject.SoapService.ServiceContract
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public string AddCustomer(Customer customer)
        {
            return $"User {customer.Name} registered!";
        }
    }


}

