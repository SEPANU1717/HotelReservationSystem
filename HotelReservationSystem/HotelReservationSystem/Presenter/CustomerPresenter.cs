using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Interface.Customer;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Presenter.Common;
using HotelReservationSystem.Presenter.Mapper;

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
            this.customerView = customerView ?? throw new ArgumentNullException(nameof(customerView)); ;
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));

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
            var dtoList = customerList.Select(CustomerMapper.FromCustomerModel).ToList();

            CustomerBindingSource.DataSource = dtoList;
            CustomerBindingSource.ResetBindings(false);
        }

        private void SearchCustomer(object sender, EventArgs e)
        {

            bool emptyValue = string.IsNullOrWhiteSpace(customerView.SearchValue);
            customerList = emptyValue
                ? repository.GetAll()
                : repository.GetByValue(customerView.SearchValue);

            var dtoList = (customerList ?? Enumerable.Empty<CustomerModel>())
                .Select(CustomerMapper.FromCustomerModel)
                .ToList();

            CustomerBindingSource.DataSource = dtoList;
            CustomerBindingSource.ResetBindings(false);
        }

        private void AddNewCustomer(object sender, EventArgs e) => customerView.isEdit = false;
        private void EditCustomer(object sender, EventArgs e)
        {
            var dto = (CustomerDto)CustomerBindingSource.Current;
            var customer = customerList.FirstOrDefault(c => c.CustomerID == dto.CustomerID);

            if (customer is null) return;

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
            var model = CustomerMapper.FromCustomerView(customerView);

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
                CleanViewFields();

            }
            catch(Exception ex) 
            {
                customerView.isSuccessful = false;
                customerView.Message = ex.Message;
            }
            
            
        }
        private void CancelAction(object sender, EventArgs e) => CleanViewFields();

        private void CleanViewFields() => FieldsCleaner.ClearInputs(customerView as Control);
        
    }
}
