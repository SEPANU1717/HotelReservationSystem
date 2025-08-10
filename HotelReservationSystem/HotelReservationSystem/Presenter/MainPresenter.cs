using System;
using System.Windows.Forms;
using HotelReservationSystem.Interface;
using HotelReservationSystem.Interface.Billing;
using HotelReservationSystem.Interface.Reservation;
using HotelReservationSystem.Interface.Rooms;
using HotelReservationSystem.Interface.Service.Food;
using HotelReservationSystem.Interface.Service.Laundry;
using HotelReservationSystem.Presenter.Billing;
using HotelReservationSystem.Presenter.Reservation;
using HotelReservationSystem.Repositories;
using HotelReservationSystem.Repositories.Service.Food;
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

            // Event subscriptions
            this.mainView.ShowCustomerView += ShowCustomerView;
            this.mainView.ShowRoomView += ShowRoomView;
            this.mainView.ShowReservationView += ShowReservationView;
            this.mainView.ShowBillingView += ShowBillingView;
            this.mainView.ShowServiceView += ShowServiceView;
        }

        private void ShowServiceView(object sender, EventArgs e)
        {
            try
            {
                var serviceControl = UCService.GetInstance((Form)mainView);

                var foodRepo = new FoodStockRepository(sqlConnectionString);
                var foodPresenter = new ServicePresenter(serviceControl, foodRepo);

                var laundryRepo = new LaundryRepository(sqlConnectionString);
                var laundryPresenter = new LaundryPresenter(serviceControl, laundryRepo);

                var orderRepo = new FoodOrderRepository(sqlConnectionString);
                var foodOrderPresenter = new OrderFoodPresenter(serviceControl, orderRepo);
                mainView.LoadUserControl(serviceControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading service view: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowBillingView(object sender, EventArgs e)
        {
            var billingControl = UCBilling.GetInstance((Form)mainView);
            var billingRepo = new BillingRepository(sqlConnectionString);
            var presenter = new BillingPresenter(billingControl, billingRepo);

            if (billingControl is IBillingView)
            {
                mainView.LoadUserControl(billingControl);
            }
            else
            {
                throw new InvalidCastException("Unable to cast UCBilling to IBillingView.");
            }
        }

        private void ShowReservationView(object sender, EventArgs e)
        {
            var reservationControl = UCReservation.GetInstance((Form)mainView);
            var reservationRepo = new ReservationRepository(sqlConnectionString);
            var presenter = new ReservationPresenter(reservationControl, reservationRepo);

            if (reservationControl is IReservationView)
            {
                mainView.LoadUserControl(reservationControl);
            }
            else
            {
                throw new InvalidCastException("Unable to cast UCReservation to IReservationView.");
            }
        }

        private void ShowCustomerView(object sender, EventArgs e)
        {
            var customerControl = UCCustomers.GetInstance((Form)mainView);
            var customerRepo = new CustomerRepository(sqlConnectionString);
            var presenter = new CustomerPresenter(customerControl, customerRepo);

            if (customerControl is ICustomerView)
            {
                mainView.LoadUserControl(customerControl);
            }
            else
            {
                throw new InvalidCastException("Unable to cast UCCustomers to ICustomerView.");
            }
        }

        private void ShowRoomView(object sender, EventArgs e)
        {
            var roomControl = UCRooms.GetInstance((Form)mainView);
            var roomRepo = new RoomRepository(sqlConnectionString);
            var presenter = new RoomPresenter(roomControl, roomRepo);

            if (roomControl is IRoomView)
            {
                mainView.LoadUserControl(roomControl);
            }
            else
            {
                throw new InvalidCastException("Unable to cast UCRooms to IRoomView.");
            }
        }
    }
}
