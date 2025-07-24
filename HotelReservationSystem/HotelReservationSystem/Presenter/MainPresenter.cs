using System;
using System.Windows.Forms;
using HotelReservationSystem.Interface;
using HotelReservationSystem.Repositories;
using HotelReservationSystem.UserControls;

namespace HotelReservationSystem.Presenter
{
    public class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnectionString;

        public MainPresenter(IMainView mainView, string sqlConnectionString)
        {
            this.mainView = mainView;
            this.sqlConnectionString = sqlConnectionString;
            this.mainView.ShowCustomerView += ShowCustomerView;
        }

        private void ShowCustomerView(object sender, EventArgs e)
        {
            var customerControl = UCCustomers.GetInstance((Form)mainView);
            var customerRepo = new CustomerRepository(sqlConnectionString);
            var presenter = new CustomerPresenter(customerControl, customerRepo);

            ICustomerView customerView = customerControl as ICustomerView;

            if (customerView != null)
            {
                mainView.LoadUserControl(customerControl);
            }
            else
            {
                throw new InvalidCastException("Unable to cast UCCustomers to ICustomerView.");
            }
        }
    }
}
