using System.Collections.Generic;
using HotelReservationSystem.Model;

namespace HotelReservationSystem.Interface.Customer
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
