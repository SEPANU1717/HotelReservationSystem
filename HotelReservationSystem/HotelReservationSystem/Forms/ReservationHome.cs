using System;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Interface;
using HotelReservationSystem.UserControls;

namespace HotelReservationSystem.Forms
{

    public partial class ReservationSystem : Form, IMainView
    {

        public ReservationSystem()
        {
            InitializeComponent();
        }
        #region LoadUser
        public void LoadUserControl(UserControl control)
        {
            TopHomePanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            TopHomePanel.Controls.Add(control);
        }
        private void sataButton1_Click(object sender, EventArgs e) => LoadUserControl(new UCDashboard());
        private void sataButton2_Click(object sender, EventArgs e) => ShowReservationView?.Invoke(this, EventArgs.Empty);
        private void sataButton3_Click(object sender, EventArgs e) => ShowRoomView?.Invoke(this, EventArgs.Empty);
        private void sataButton4_Click(object sender, EventArgs e) => ShowCustomerView?.Invoke(this, EventArgs.Empty);
        private void sataButton5_Click(object sender, EventArgs e) => ShowBillingView?.Invoke(this, EventArgs.Empty);
        private void sataButton6_Click(object sender, EventArgs e) => LoadUserControl(new UCSettings());
        private void pictureBox1_Click(object sender, EventArgs e) => LoadUserControl(new UCHomepage());
        private void sataButton9_Click(object sender, EventArgs e) => ShowServiceView?.Invoke(this, EventArgs.Empty);
        private void sataButton7_Click(object sender, EventArgs e) => LoadUserControl(new UCINOUT());
        #endregion

        #region EventHandler
        public event EventHandler ShowCustomerView;
        public event EventHandler ShowRoomView;
        public event EventHandler ShowReservationView;
        public event EventHandler ShowLoginView;
        public event EventHandler ShowBillingView;
        public event EventHandler ShowServiceView;
        #endregion

        #region LoginCleanUp
        private void sataButton8_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();

            // Cleanup
            UCBilling.ResetInstance();
            UCReservation.ResetInstance();
            UCCustomers.ResetInstance();
            UCRooms.ResetInstance();
            UCService.ResetInstance();
            this.Hide();
        }
        #endregion




    }
}
