using System.Collections.Generic;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Domain.Interface.Customer
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
