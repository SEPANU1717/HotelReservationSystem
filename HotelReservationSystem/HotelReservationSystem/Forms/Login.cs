using System;
using System.Configuration;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Interface;
using HotelReservationSystem.Infrastructure.Repository;
using HotelReservationSystem.Infrastructure.Security;
using HotelReservationSystem.Presenter;
using HotelReservationSystem.Presenter.Common;

namespace HotelReservationSystem.Forms
{
    public partial class Login : Form, ILoginView
    {
        private LoginPresenter _presenter;
        public Login()
        {
            InitializeComponent();
            InitializePresenter();
            AssociateAndRaiseEvents();
        }

        public string UsernameOrEmail { get => txtUsername.Texts; set => txtUsername.Texts = value; }
        public string Password { get => txtPassword.Texts; set => txtPassword.Texts = value; }
        public bool RememberMe { get => false; set { } }
        public bool isSuccessful { get; set; }
        public string Message { get; set; }

        public event EventHandler LoginEvent;
        public event EventHandler CancelEvent;

        private void AssociateAndRaiseEvents()
        {
            btnLogin.Click += delegate
            {
                LoginEvent?.Invoke(this, EventArgs.Empty);
                if (!isSuccessful)
                {
                    MessageBox.Show(Message);
                }
            };
        }
        private void InitializePresenter()
        {
            string connectionString = Properties.Settings.Default.SqlConnectionString;
            IPasswordHasher passwordHasher = new Pbkdf2PasswordHasher();
            IUserRepository userRepository = new UserRepository(connectionString, passwordHasher);

            _presenter = new LoginPresenter(this, userRepository, passwordHasher);
            _presenter.LoginSuccessful += OnLoginSuccessful;
        }

        private void OnLoginSuccessful(object sender, LoginSuccessEventArgs e)
        {
            UserSession.Login(e.User);
            this.Hide();
            ReservationSystem reservationSystem = new ReservationSystem();
            reservationSystem.Show();
        }

        public void ClearFields()
        {
            UsernameOrEmail = string.Empty;
            Password = string.Empty;
        }
    }
}