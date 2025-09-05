using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Domain.Interface;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Infrastructure.Security;
using HotelReservationSystem.Presenter.Common;

namespace HotelReservationSystem.Presenter
{
    public class LoginPresenter
    {
        private readonly ILoginView loginView;
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;

        public event EventHandler<LoginSuccessEventArgs> LoginSuccessful;

        public LoginPresenter(ILoginView loginView, IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            this.loginView = loginView;
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;

            loginView.LoginEvent += AttemptLogin;
            loginView.CancelEvent += CancelLogin;

            InitializeView();
        }

        private void CancelLogin(object sender, EventArgs e)
        {
            loginView.ClearFields();
            loginView.Hide();
        }

        private void AttemptLogin(object sender, EventArgs e)
        {
            try
            {
                var loginModel = new LoginModel
                {
                    UsernameOrEmail = loginView.UsernameOrEmail?.Trim(),
                    Password = loginView.Password,
                    RememberMe = loginView.RememberMe
                };

                new ModelDataValidation().Validate(loginModel);

                var authenticatedUser = userRepository.AuthenticateUser(loginModel.UsernameOrEmail, loginModel.Password);

                    if (authenticatedUser == null)
                    {
                        loginView.isSuccessful = false;
                        loginView.Message = "Invalid username/email or password.";
                        return;
                    }

                    if (!authenticatedUser.IsActive)
                    {
                        loginView.isSuccessful = false;
                        loginView.Message = "Your account is inactive. Please contact the administrator.";
                        return;
                    }
                    loginView.isSuccessful = true;
                    loginView.Message = "Login successful!";
                    LoginSuccessful?.Invoke(this, new LoginSuccessEventArgs(authenticatedUser));
                    loginView.Hide();

            }
            catch (Exception ex)
            {
                loginView.isSuccessful = false;
                loginView.Message = "Login error: " + ex.Message;
            }
        }

        public void Logout()
        {
            loginView.ClearFields();
            loginView.Show();
        }

        private void InitializeView()
        {
            loginView.ClearFields();
            loginView.Show();
        }

    }
}
