using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelReservationSystem.UserControls;

namespace HotelReservationSystem
{
    public partial class ReservationSystem : Form
    {
        public ReservationSystem()
        {
            InitializeComponent();
        }
        private void LoadUserControl(UserControl control)
        {
            TopHomePanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            TopHomePanel.Controls.Add(control);
        }

        private void sataButton1_Click(object sender, EventArgs e) => LoadUserControl(new UCDashboard());
        private void sataButton2_Click(object sender, EventArgs e) => LoadUserControl(new UCReservation());
        private void sataButton3_Click(object sender, EventArgs e) => LoadUserControl(new UCRooms());
        private void sataButton4_Click(object sender, EventArgs e) => LoadUserControl(new UCCustomers());
        private void sataButton5_Click(object sender, EventArgs e) => LoadUserControl(new UCBilling());
        private void sataButton6_Click(object sender, EventArgs e) => LoadUserControl(new UCSettings());
        private void sataButton8_Click(object sender, EventArgs e) => LoadUserControl(new UCDashboard());
    }
}
