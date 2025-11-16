using System;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.Data.Repositories.CheckInOut;
using HotelReservationSystem.Data.Repositories.Service;
using HotelReservationSystem.Domain.Interface;
using HotelReservationSystem.Domain.Interface.Billing;
using HotelReservationSystem.Domain.Interface.Customer;
using HotelReservationSystem.Domain.Interface.Reservation;
using HotelReservationSystem.Domain.Interface.Rooms;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Infrastructure.Repository;
using HotelReservationSystem.Infrastructure.Security;
using HotelReservationSystem.Presenter.Billing;
using HotelReservationSystem.UserControls;

namespace HotelReservationSystem.Presenter
{
    public class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnectionString;
        private readonly IPasswordHasher PasswordHasher;

        public MainPresenter(IMainView mainView, string sqlConnectionString, IPasswordHasher passwordHasher, UserModel loggedInUser = null)
        {
            this.mainView = mainView;
            this.sqlConnectionString = sqlConnectionString;
            this.PasswordHasher = passwordHasher;

            // Event subscriptions
            this.mainView.ShowCustomerView += ShowCustomerView;
            this.mainView.ShowRoomView += ShowRoomView;
            this.mainView.ShowReservationView += ShowReservationView;
            this.mainView.ShowBillingView += ShowBillingView;
            this.mainView.ShowServiceView += ShowServiceView;
            this.mainView.ShowUserView += ShowUserView;
            this.mainView.ShowCheckInOutView += ShowCheckInOutView;

        }

        private void ShowUserView(object sender, EventArgs e)
        {
            try
            {
                var userControl = UCSettings.GetInstance(mainView as Form);
                var userRepo = new UserRepository(sqlConnectionString, PasswordHasher);
                var presenter = new UserPresenter(userControl, userRepo, PasswordHasher);

                mainView.LoadUserControl(userControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user management: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowServiceView(object sender, EventArgs e)
        {
            try
            {
                var serviceControl = UCService.GetInstance((Form)mainView);

                var foodRepo = new FoodStockRepository(sqlConnectionString);
                var foodPresenter = new ServicePresenter(serviceControl, foodRepo);

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
            try
            {
                var billingControl = UCBilling.GetInstance((Form)mainView);
                var billingRepo = new BillingRepository(sqlConnectionString);
                var presenter = new BillingPresenter(billingControl, billingRepo);

                mainView.LoadUserControl(billingControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error loading billing view: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ShowCheckInOutView(object sender, EventArgs e)
        {
            try
            {
                var checkInOutControl = UCCheckINOUT.GetInstance((Form)mainView);
                var checkInRepo = new CheckInOutRepository(sqlConnectionString);
                var presenter = new UCINOUTPresenter(checkInOutControl, checkInRepo, sqlConnectionString);

                mainView.LoadUserControl(checkInOutControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Check-In/Out view: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
