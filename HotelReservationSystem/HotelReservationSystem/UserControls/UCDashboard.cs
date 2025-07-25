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


namespace HotelReservationSystem.UserControls
{
    public partial class UCDashboard : UserControl
    {
        public UCDashboard()
        {
            InitializeComponent();
        }

        private void sataBarChart1_Load(object sender, EventArgs e)
        {

        }

        private void UCDashboard_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

            RoomRepository repository = new RoomRepository(connectionString);
            int totalRooms = repository.GetRoomCount();
            lblTotalRoom.Text = totalRooms.ToString();
        }
    }
}
