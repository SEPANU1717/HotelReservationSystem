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
using HotelReservationSystem.Repositories.Rooms;
using HotelReservationSystem.Repositories.Dashboard;


namespace HotelReservationSystem.UserControls
{
    public partial class UCDashboard : UserControl
    {
        string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        public UCDashboard()
        {
            InitializeComponent();
        }

        private void sataBarChart1_Load(object sender, EventArgs e)
        {

        }

        private void UCDashboard_Load(object sender, EventArgs e)
        {
            DasboardRepository repository = new DasboardRepository(connectionString);
            var totalRooms = repository.GetRoomCount();
            var totalAvailableRooms = repository.GetAvailableRoomCount();
            var totalOccupiedRooms = repository.GetOccupiedRoomCount();
            var totalGuestToday = repository.GetTotalGuest();

            lblTotalRoom.Text = totalRooms.ToString();
            lblAvailRoom.Text = totalAvailableRooms.ToString();
            lblOccuRooms.Text = totalOccupiedRooms.ToString();
            lblTotalGuests.Text = totalGuestToday.ToString();

        }
    }
}
