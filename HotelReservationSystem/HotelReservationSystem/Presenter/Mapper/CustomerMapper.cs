using System;
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Interface.Customer;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Presenter.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerModel FromCustomerView(ICustomerView customerView)
        {
            if (customerView is null) throw new ArgumentNullException(nameof(customerView));

            return new CustomerModel()
            {
                CustomerID = int.Parse(customerView.CustomerID),
                FirstName = customerView.CustomerFirstName,
                MiddleName = customerView.CustomerMiddleName,
                LastName = customerView.CustomerLastName,
                IDType = customerView.CustomerIdType,
                Contact = customerView.CustomerContact,
                Address = customerView.CustomerAddress,
                Email = customerView.CustomerEmail,
                Gender = customerView.CustomerGender,
                Nationality = customerView.CustomerNationality,
                Notes = customerView.CustomerNotes,
                DateOfBirth = customerView.CustomerBirthDate
            };
        }

        public static CustomerDto FromCustomerModel(CustomerModel customer)
        {
            return new CustomerDto
            {
                CustomerID = customer.CustomerID,
                Fullname = customer.FullName,
                Contact = customer.Contact,
                Email = customer.Email,
                Address = customer.Address,
                IDType = customer.IDType,
                Nationality = customer.Nationality
            };
        }
    }
}
