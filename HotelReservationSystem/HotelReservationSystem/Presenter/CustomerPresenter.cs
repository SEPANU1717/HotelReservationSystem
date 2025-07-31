using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Interface;
using HotelReservationSystem.Model.Customer;
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
            customerList = repository.GetAll();
            CustomerBindingSource.DataSource = customerList;
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
            var customer = (CustomerModel)CustomerBindingSource.Current;
            customerView.CustomerID = customer.CustomerID.ToString();
            customerView.CustomerFirstName = customer.FirstName;
            customerView.CustomerLastName = customer.LastName;
            customerView.CustomerIdType = customer.IDType;
            customerView.CustomerContact = customer.Contact;
            customerView.CustomerAddress = customer.Address;

            customerView.isEdit = true;
        }
        private void DeleteCustomer(object sender, EventArgs e) 
        {
            try
            {
                var pet = (CustomerModel)CustomerBindingSource.Current;
                repository.Delete(pet.CustomerID);
                customerView.isSuccessful = true;
                customerView.Message = "Customer deleted succesfully";
                LoadAllCustomerList();
            }
            catch
            {
                customerView.isSuccessful = false;
                customerView.Message = "An error ocurred, could not delete pet";
            }
        }
        private void SaveCustomer(object sender, EventArgs e)
        {
            var model = new CustomerModel();
            model.CustomerID = int.Parse(customerView.CustomerID);
            model.FirstName = customerView.CustomerFirstName;
            model.LastName = customerView.CustomerLastName;
            model.IDType = customerView.CustomerIdType;
            model.Contact = customerView.CustomerContact;
            model.Address = customerView.CustomerAddress;

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
            customerView.CustomerLastName = "";
            customerView.CustomerIdType = "";
            customerView.CustomerContact = "";
            customerView.CustomerAddress = "";
        }
    }
}
