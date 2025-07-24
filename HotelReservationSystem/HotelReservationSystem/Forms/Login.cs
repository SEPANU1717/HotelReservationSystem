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
using HotelReservationSystem.Presenter;

namespace HotelReservationSystem
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
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid login!");
            }
        }

    }
}
