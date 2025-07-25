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
using HotelReservationSystem.Interface;
using HotelReservationSystem.Interface.Rooms;
using HotelReservationSystem.Presenter;
using HotelReservationSystem.Repositories;
using HotelReservationSystem.UserControls;

namespace HotelReservationSystem
{

    public partial class ReservationSystem : Form, IMainView
    {

        public ReservationSystem()
        {
            InitializeComponent();

        }

        public event EventHandler ShowCustomerView;
        public event EventHandler ShowRoomView;

        public void LoadUserControl(UserControl control)
        {
            TopHomePanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            TopHomePanel.Controls.Add(control);
        }

        private void sataButton1_Click(object sender, EventArgs e) => LoadUserControl(new UCDashboard());
        private void sataButton2_Click(object sender, EventArgs e) => LoadUserControl(new UCReservation());
        private void sataButton3_Click(object sender, EventArgs e) => ShowRoomView?.Invoke(this, EventArgs.Empty);
        private void sataButton4_Click(object sender, EventArgs e)
        {

            ShowCustomerView?.Invoke(this, EventArgs.Empty);
        }
        private void sataButton5_Click(object sender, EventArgs e) => LoadUserControl(new UCBilling());
        private void sataButton6_Click(object sender, EventArgs e) => LoadUserControl(new UCSettings());
        private void pictureBox1_Click(object sender, EventArgs e) => LoadUserControl(new UCHomepage());
        private void sataButton8_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();

            // Cleanup
            UCCustomers.ResetInstance();
            UCRooms.ResetInstance();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void sataPictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}
