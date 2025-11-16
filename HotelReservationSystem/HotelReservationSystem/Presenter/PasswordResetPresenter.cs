using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.Domain.Interface;
using HotelReservationSystem.Domain.Interface.UserManagement.Email_Service;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Infrastructure.Security;

namespace HotelReservationSystem.Presenter
{
    public class PasswordResetPresenter
    {
        private readonly IPasswordResetView _view;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly PasswordResetRepository _resetRepository;
        private readonly IEmailService _emailService;

        public PasswordResetPresenter(
            IPasswordResetView view,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            PasswordResetRepository resetRepository,
            IEmailService emailService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _resetRepository = resetRepository ?? throw new ArgumentNullException(nameof(resetRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));

            SubscribeToViewEvents();
            _view.ShowStep(1);
        }

        private void SubscribeToViewEvents()
        {
            _view.RequestResetEvent += OnRequestReset;
            _view.VerifyAndResetEvent += OnVerifyAndReset;
            _view.CancelEvent += OnCancel;
        }

        private async void OnRequestReset(object sender, EventArgs e)
        {
            try
            {
                string usernameOrEmail = _view.UsernameOrEmailRequest?.Trim();

                if (string.IsNullOrEmpty(usernameOrEmail))
                {
                    _view.ShowMessage("Please enter your username or email.", "Validation Error", MessageBoxIcon.Warning);
                    return;
                }

                var user = _userRepository.GetByUsernameOrEmail(usernameOrEmail);
                if (user == null)
                {
                    _view.ShowMessage(
                        "If this account exists, a password reset code has been sent to the registered email.",
                        "Request Sent",
                        MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(user.Email))
                {
                    _view.ShowMessage(
                        "No email address is registered for this account. Please contact the administrator.",
                        "Email Not Found",
                        MessageBoxIcon.Warning);
                    return;
                }

                string token = _resetRepository.GenerateSecureToken();
                var resetToken = new PasswordResetToken
                {
                    UsernameOrEmail = usernameOrEmail,
                    Token = token,
                    ExpiryDate = DateTime.Now.AddMinutes(15),
                    IsUsed = false,
                    CreatedAt = DateTime.Now
                };

                _resetRepository.SaveToken(resetToken);

                bool emailSent = await _emailService.SendPasswordResetEmailAsync(user.Email, token, user.Username);

                if (emailSent)
                {
                    _view.ShowMessage(
                        $"A 6-digit reset code has been sent to {MaskEmail(user.Email)}.\n\n" +
                        "The code will expire in 15 minutes.",
                        "Reset Code Sent",
                        MessageBoxIcon.Information);

                    _view.UsernameOrEmailVerify = usernameOrEmail;
                    _view.ShowStep(2);
                }
                else
                {
                    _view.ShowMessage(
                        "Failed to send reset email. Please check your internet connection or contact support.",
                        "Email Error",
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _view.ShowMessage(
                    $"An error occurred: {ex.Message}",
                    "Error",
                    MessageBoxIcon.Error);
            }
        }

        private void OnVerifyAndReset(object sender, EventArgs e)
        {
            try
            {
                string usernameOrEmail = _view.UsernameOrEmailVerify?.Trim();
                string token = _view.ResetToken?.Trim();
                string newPassword = _view.NewPassword;
                string confirmPassword = _view.ConfirmPassword;

                if (string.IsNullOrEmpty(token) || token.Length != 6)
                {
                    _view.ShowMessage("Please enter the 6-digit reset code.", "Validation Error", MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 6)
                {
                    _view.ShowMessage("Password must be at least 6 characters.", "Validation Error", MessageBoxIcon.Warning);
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    _view.ShowMessage("Passwords do not match.", "Validation Error", MessageBoxIcon.Warning);
                    return;
                }

                var validToken = _resetRepository.GetValidToken(usernameOrEmail, token);
                if (validToken == null)
                {
                    _view.ShowMessage(
                        "Invalid or expired reset code. Please request a new one.",
                        "Invalid Code",
                        MessageBoxIcon.Error);
                    return;
                }

                var user = _userRepository.GetByUsernameOrEmail(usernameOrEmail);
                if (user == null)
                {
                    _view.ShowMessage("User not found.", "Error", MessageBoxIcon.Error);
                    return;
                }

                user.PasswordHash = _passwordHasher.HashPassword(newPassword);
                user.Password = newPassword;
                _userRepository.Edit(user);

                _resetRepository.MarkTokenAsUsed(validToken.TokenId);

                _view.IsSuccessful = true;
                _view.Message = "Password reset successful!";
                _view.ShowMessage(
                    "Your password has been reset successfully!\n\nYou can now login with your new password.",
                    "Success",
                    MessageBoxIcon.Information);

                _view.Close();
            }
            catch (Exception ex)
            {
                _view.IsSuccessful = false;
                _view.ShowMessage(
                    $"An error occurred: {ex.Message}",
                    "Error",
                    MessageBoxIcon.Error);
            }
        }

        private void OnCancel(object sender, EventArgs e)
        {
            _view.ClearFields();
            _view.Close();
        }

        private string MaskEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                return email;

            var parts = email.Split('@');
            var localPart = parts[0];
            var domain = parts[1];

            if (localPart.Length <= 2)
                return $"{localPart}***@{domain}";

            return $"{localPart.Substring(0, 2)}***@{domain}";
        }
    }
}