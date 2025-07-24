using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Interface;
using HotelReservationSystem.Model.Customer;

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
            this.customerView.UpdateEvent += UpdateCustomer;

            this.customerView.SetCustomerListBindingSource(CustomerBindingSource);
            LoadAllCustomerList();
            this.customerView.Show();
        }

        private void LoadAllCustomerList()
        {
            customerList = repository.GetAll();
            CustomerBindingSource.DataSource = customerList;
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
        private void EditCustomer(object sender, EventArgs e) => throw new NotImplementedException();
        private void DeleteCustomer(object sender, EventArgs e) => throw new NotImplementedException();
        private void SaveCustomer(object sender, EventArgs e) => throw new NotImplementedException();
        private void UpdateCustomer(object sender, EventArgs e) => throw new NotImplementedException();
        private void CancelAction(object sender, EventArgs e) => CleanviewFields();

        private void CleanviewFields()
        {
            customerView.CustomerID = "0";
            customerView.CustomerName = "";
            customerView.CustomerIdType = "";
            customerView.CustomerContact = "";
            customerView.CustomerAddress = "";
        }
    }
}
