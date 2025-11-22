using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Presenter;
using HotelReservationSystem.Presenter.Common;


namespace HotelReservationSystem.UserControls
{
    public partial class UCDashboard : UserControl
    {
        private RoomRepository roomRepo;
        public UCDashboard()
        {
            InitializeComponent();
            roomRepo = new RoomRepository(DbConfig.GetConnectionString());
            UserInfoDisplay.UpdateUserInfoDisplay(lblUsername, lblRole, pictureProfile);
        }

        private void sataBarChart1_Load(object sender, EventArgs e)
        {

        }

        private void UCDashboard_Load(object sender, EventArgs e)
        {
            DasboardRepository repository = new DasboardRepository(DbConfig.GetConnectionString());
            var totalRooms = repository.GetRoomCount();
            var totalAvailableRooms = repository.GetAvailableRoomCount();
            var totalOccupiedRooms = repository.GetOccupiedRoomCount();
            var totalReservedRooms = repository.GetReservedRoomCount();

            lblTotalRoom.Text = totalRooms.ToString();
            lblAvailRoom.Text = totalAvailableRooms.ToString();
            lblOccuRooms.Text = totalOccupiedRooms.ToString();
            // Show number of reserved rooms on dashboard instead of total guests
            lblTotalGuests.Text = totalReservedRooms.ToString();


            var reservations = ReservationPresenter.GetReservationDtoList().ToList();
            dataGridReservationDash.DataSource = reservations;
        }

    }
}
