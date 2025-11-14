using System;
using System.Configuration;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Interface;
using HotelReservationSystem.Domain.Interface.UserManagement.Email_Service;
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
            chkShowPassword.CheckedChanged += delegate
            { txtPassword.PasswordChar = !chkShowPassword.Checked; };

            btnLogin.Click += delegate
            {
                LoginEvent?.Invoke(this, EventArgs.Empty);
                if (!isSuccessful)
                {
                    MessageBox.Show(Message);
                }
            };

            lnkForgotPassword.LinkClicked += (s, e) =>
            {
                using (var forgotPasswordForm = new ForgotPasswordForm())
                {
                    forgotPasswordForm.ShowDialog(this); // Shows as modal popup
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

        private async void btnTestEmail_Click(object sender, EventArgs e)
        {
            try
            {
                var emailService = new SmtpEmailService();

                bool sent = await emailService.SendPasswordResetEmailAsync(
                    "markmanalo1717@gmail.com",  // Send to yourself for testing
                    "123456",
                    "TestUser"
                );

                MessageBox.Show(
                    sent ? "✅ Email sent successfully! Check your inbox." : "❌ Failed to send email.",
                    "Email Test",
                    MessageBoxButtons.OK,
                    sent ? MessageBoxIcon.Information : MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Test Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}