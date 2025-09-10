using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Interface.Customer;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Presenter.Common;

namespace HotelReservationSystem.Presenter
{
    public class CustomerPresenter
    {
        private ICustomerView customerView;
        private ICustomerRepository repository;
        private BindingSource CustomerBindingSource;
        private IEnumerable<CustomerModel> customerList;

        public CustomerPresenter(ICustomerView customerView, ICustomerRepository repository)
        {
            CustomerBindingSource = new BindingSource();
            this.customerView = customerView;
            this.repository = repository;

            // Subscribe
            this.customerView.SearchEvent += SearchCustomer;
            this.customerView.AddNewEvent += AddNewCustomer;
            this.customerView.EditEvent += EditCustomer;
            this.customerView.DeleteEvent += DeleteCustomer;
            this.customerView.SaveEvent += SaveCustomer;
            this.customerView.CancelEvent += CancelAction;

            this.customerView.SetCustomerListBindingSource(CustomerBindingSource);
            LoadAllCustomerList();
            this.customerView.Show();
        }

        private void LoadAllCustomerList()
        {
            customerList = repository.GetAll() ?? Enumerable.Empty<CustomerModel>();
            var dtoList = customerList.Select(c => new CustomerDto
            {
                CustomerID = c.CustomerID,
                FirstName = c.FirstName,
                MiddleName = c.MiddleName,
                LastName = c.LastName,
                IDType = c.IDType,
                Contact = c.Contact,
                Address = c.Address,
                Email = c.Email,
                Nationality = c.Nationality,
            }).ToList();

            CustomerBindingSource.DataSource = dtoList;
            CustomerBindingSource.ResetBindings(false);
        }

        private void SearchCustomer(object sender, EventArgs e)
        {

            bool emptyValue = string.IsNullOrWhiteSpace(customerView.SearchValue);
            customerList = emptyValue
                ? repository.GetAll()
                : repository.GetByValue(customerView.SearchValue);

            CustomerBindingSource.DataSource = customerList;
            CustomerBindingSource.ResetBindings(false);
        }

        private void AddNewCustomer(object sender, EventArgs e) => customerView.isEdit = false;
        private void EditCustomer(object sender, EventArgs e)
        {
            var dto = (CustomerDto)CustomerBindingSource.Current;
            var customer = customerList.FirstOrDefault(c => c.CustomerID == dto.CustomerID);

            if (customer == null) return;

            customerView.CustomerID = customer.CustomerID.ToString();
            customerView.CustomerFirstName = customer.FirstName;
            customerView.CustomerMiddleName = customer.MiddleName;
            customerView.CustomerLastName = customer.LastName;
            customerView.CustomerIdType = customer.IDType;
            customerView.CustomerContact = customer.Contact;
            customerView.CustomerAddress = customer.Address;
            customerView.CustomerEmail = customer.Email;
            customerView.CustomerGender = customer.Gender;
            customerView.CustomerNationality = customer.Nationality;
            customerView.CustomerNotes = customer.Notes;
            customerView.CustomerBirthDate = customer.DateOfBirth ?? DateTime.Now;

            customerView.isEdit = true;
        }
        private void DeleteCustomer(object sender, EventArgs e) 
        {
            try
            {
                var customer = (CustomerDto)CustomerBindingSource.Current;
                repository.Delete(customer.CustomerID);
                customerView.isSuccessful = true;
                customerView.Message = "Customer deleted successfully";
                LoadAllCustomerList();
            }
            catch
            {
                customerView.isSuccessful = false;
                customerView.Message = "An error occurred, could not delete customer";
            }
        }
        private void SaveCustomer(object sender, EventArgs e)
        {
            var model = new CustomerModel
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


            try
            {
                new ModelDataValidation().Validate(model);

                if (customerView.isEdit)
                {
                    repository.Edit(model);
                    customerView.Message = "Customer edited successfully";
                }
                else
                {
                    repository.Add(model);
                    customerView.Message = "Customer added Successfully";
                }
                customerView.isSuccessful = true;
                LoadAllCustomerList();
                CleanviewFields();

            }
            catch(Exception ex) 
            {
                customerView.isSuccessful = false;
                customerView.Message = ex.Message;
            }
            
            
        }
        private void CancelAction(object sender, EventArgs e) => CleanviewFields();

        private void CleanviewFields()
        {
            customerView.CustomerID = "0";
            customerView.CustomerFirstName = "";
            customerView.CustomerMiddleName = "";
            customerView.CustomerLastName = "";
            customerView.CustomerIdType = "";
            customerView.CustomerContact = "";
            customerView.CustomerAddress = "";
            customerView.CustomerEmail = "";
            customerView.CustomerGender = "";
            customerView.CustomerNationality = "";
            customerView.CustomerNotes = "";
            customerView.CustomerBirthDate = DateTime.Now;
        }
    }
}
