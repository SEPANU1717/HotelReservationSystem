using System;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Domain.Interface;
using HotelReservationSystem.Infrastructure.Security;
using HotelReservationSystem.Presenter;
using HotelReservationSystem.Presenter.Common;
using HotelReservationSystem.UserControls;

namespace HotelReservationSystem.Forms
{
    public partial class ReservationSystem : Form, IMainView
    {
        private UCCustomers ucCustomers;
        private UCReservation ucReservation;
        private ReservationPresenter reservationPresenter;

        public ReservationSystem()
        {
            InitializeComponent();
            Load += ReservationSystem_Load;
            UserInfoDisplay.UpdateUserInfoDisplay(lblUsername, lblRole, pictureProfile);

            ucCustomers = UCCustomers.GetInstance(this);
            ucCustomers.CustomerSelected += UcCustomers_CustomerSelected;

            ucReservation = UCReservation.GetInstance(this);

            LoadUserControl(new UCDashboard());
        }

        #region LoadUser

        public void LoadUserControl(UserControl control)
        {
            TopHomePanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            TopHomePanel.Controls.Add(control);
        }

        private void ReservationSystem_Load(object sender, EventArgs e)
        {
            string connectionString = Properties.Settings.Default.SqlConnectionString;
            IPasswordHasher passwordHasher = new Pbkdf2PasswordHasher();
            var presenter = new MainPresenter(this, connectionString, passwordHasher);
        }

        private void UcCustomers_CustomerSelected(object sender, UCCustomers.CustomerSelectedEventArgs e)
        {
            var ucReservation = UCReservation.GetInstance(this);
            if (reservationPresenter == null)
            {
                reservationPresenter = new ReservationPresenter(
                    ucReservation,
                    new ReservationRepository(DbConfig.GetConnectionString())
                );
            }
            ucReservation.TriggerSetCustomerForReservation(e.FullName);
            LoadUserControl(ucReservation);
        }

        private void sataButton1_Click(object sender, EventArgs e) => LoadUserControl(new UCDashboard());
        private void sataButton2_Click(object sender, EventArgs e) => ShowReservationView?.Invoke(this, EventArgs.Empty);
        private void sataButton3_Click(object sender, EventArgs e) => ShowRoomView?.Invoke(this, EventArgs.Empty);
        private void sataButton4_Click(object sender, EventArgs e) => ShowCustomerView?.Invoke(this, EventArgs.Empty);
        private void sataButton5_Click(object sender, EventArgs e) => ShowBillingView?.Invoke(this, EventArgs.Empty);
        private void sataButton6_Click(object sender, EventArgs e) => ShowUserView?.Invoke(this, EventArgs.Empty);
        private void pictureBox1_Click(object sender, EventArgs e) => LoadUserControl(new UCHomepage());
        private void sataButton9_Click(object sender, EventArgs e) => ShowServiceView?.Invoke(this, EventArgs.Empty);
        private void sataButton7_Click(object sender, EventArgs e) => ShowCheckInOutView?.Invoke(this,  EventArgs.Empty);

        #endregion

        #region EventHandler

        public event EventHandler ShowCustomerView;
        public event EventHandler ShowRoomView;
        public event EventHandler ShowReservationView;
        public event EventHandler ShowBillingView;
        public event EventHandler ShowServiceView;
        public event EventHandler ShowUserView;
        public event EventHandler ShowCheckInOutView;

        #endregion

        #region LoginCleanUp

        private void sataButton8_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();

            reservationPresenter = null;

            UCBilling.ResetInstance();
            UCReservation.ResetInstance();
            UCCustomers.ResetInstance();
            UCRooms.ResetInstance();
            UCService.ResetInstance();
            UCSettings.ResetInstance();
            UCCheckINOUT.ResetInstance();
            this.Hide();
        }

        #endregion
    }
}