using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Interface.Customer;
using HotelReservationSystem.Domain.Interface.Reservation;
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
        private static CustomerPresenter _lastPresenterInstance;

        public CustomerPresenter(ICustomerView customerView, ICustomerRepository repository)
        {
            CustomerBindingSource = new BindingSource();
            this.customerView = customerView ?? throw new ArgumentNullException(nameof(customerView)); ;
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));

            if (_lastPresenterInstance != null)
                _lastPresenterInstance.UnsubscribeFromViewEvents();

            SubscribeToViewEvents();
            _lastPresenterInstance = this;

            this.customerView.SetCustomerListBindingSource(CustomerBindingSource);
            LoadAllCustomerList();
            this.customerView.Show();
        }

        private void SubscribeToViewEvents()
        {
            this.customerView.SearchEvent += SearchCustomer;
            this.customerView.AddNewEvent += AddNewCustomer;
            this.customerView.EditEvent += EditCustomer;
            this.customerView.DeleteEvent += DeleteCustomer;
            this.customerView.SaveEvent += SaveCustomer;
            this.customerView.CancelEvent += CancelAction;
        }

        private void UnsubscribeFromViewEvents()
        {
            this.customerView.SearchEvent -= SearchCustomer;
            this.customerView.AddNewEvent -= AddNewCustomer;
            this.customerView.EditEvent -= EditCustomer;
            this.customerView.DeleteEvent -= DeleteCustomer;
            this.customerView.SaveEvent -= SaveCustomer;
            this.customerView.CancelEvent -= CancelAction;
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
                // Business Rule Validation - Age Check (18+)
                if (model.DateOfBirth.HasValue)
                {
                    var age = DateTime.Now.Year - model.DateOfBirth.Value.Year;
                    if (model.DateOfBirth.Value.Date > DateTime.Now.AddYears(-age))
                    {
                        age--;
                    }

                    if (age < 18)
                    {
                        customerView.isSuccessful = false;
                        customerView.Message = $"Customer must be at least 18 years old to make a reservation.\n\n" +
                                              $"Current age: {age} years old\n" +
                                              $"Date of birth: {model.DateOfBirth.Value:MM/dd/yyyy}\n\n" +
                                              "Customers under 18 are not permitted to book rooms.";
                        MessageBox.Show(customerView.Message, "Age Requirement Not Met", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Validate phone number format
                if (!string.IsNullOrEmpty(model.Contact))
                {
                    var digitsOnly = new string(model.Contact.Where(char.IsDigit).ToArray());
                    if (digitsOnly.Length < 10)
                    {
                        customerView.isSuccessful = false;
                        customerView.Message = "Contact number must contain at least 10 digits.";
                        MessageBox.Show(customerView.Message, "Invalid Contact Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Validate email uniqueness for new customers
                if (!customerView.isEdit)
                {
                    var existingCustomers = repository.GetAll();
                    if (existingCustomers.Any(c => c.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase)))
                    {
                        customerView.isSuccessful = false;
                        customerView.Message = $"Email '{model.Email}' is already registered. Please use a different email address.";
                        MessageBox.Show(customerView.Message, "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Standard model validation (data annotations)
                new ModelDataValidation().Validate(model);

                if (customerView.isEdit)
                {
                    repository.Edit(model);
                    customerView.Message = "Customer updated successfully";
                }
                else
                {
                    repository.Add(model);
                    customerView.Message = "Customer added successfully";
                }
                
                customerView.isSuccessful = true;
                LoadAllCustomerList();
                CleanViewFields();
                MessageBox.Show(customerView.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex) 
            {
                customerView.isSuccessful = false;
                customerView.Message = ex.Message;
                MessageBox.Show(customerView.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CancelAction(object sender, EventArgs e) => CleanViewFields();

        private void CleanViewFields() => FieldsCleaner.ClearInputs(customerView as Control);
        
    }
}
