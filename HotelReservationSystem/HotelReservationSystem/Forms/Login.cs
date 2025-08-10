using System;
using System.Configuration;
using System.Windows.Forms;
using HotelReservationSystem.Presenter;

namespace HotelReservationSystem.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool loginSuccess = true;
            if (loginSuccess)
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

                var mainView = new ReservationSystem();
                var presenter = new MainPresenter(mainView, sqlConnectionString);
                mainView.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Invalid login!");
            }
        }

    }
}
