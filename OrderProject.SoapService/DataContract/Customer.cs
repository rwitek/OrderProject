using OrderProject.Domain.Models;
using System.Runtime.Serialization;

namespace OrderProject.SoapService.DataContract
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
