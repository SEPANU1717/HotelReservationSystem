using System;
using System.Windows.Forms;
using HotelReservationSystem.Interface;
using HotelReservationSystem.Interface.Rooms;
using HotelReservationSystem.Presenter.Rooms;
using HotelReservationSystem.Repositories;
using HotelReservationSystem.Repositories.Rooms;
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
        }

        private void ShowCustomerView(object sender, EventArgs e)
        {
            var customerControl = UCCustomers.GetInstance((Form)mainView);
            var customerRepo = new CustomerRepository(sqlConnectionString);
            var presenter = new CustomerPresenter(customerControl, customerRepo);

            if (customerControl is ICustomerView customerView)
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

            if (roomControl is IRoomView roomView)
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
