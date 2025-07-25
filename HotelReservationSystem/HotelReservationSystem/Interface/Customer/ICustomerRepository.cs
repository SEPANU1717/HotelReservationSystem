using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Model.Customer;

namespace HotelReservationSystem.Interface
{
    public interface ICustomerRepository
    {
        void Add(CustomerModel customer);
        void Edit(CustomerModel customer);
        void Delete(int id);
        IEnumerable<CustomerModel> GetAll();
        IEnumerable<CustomerModel> GetByValue(string value);




    }
}
