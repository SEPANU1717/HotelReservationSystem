using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Domain.Interface;
using HotelReservationSystem.Domain.Interface.UserManagement.Email_Service;
using HotelReservationSystem.Infrastructure.Repository;
using HotelReservationSystem.Infrastructure.Security;
using HotelReservationSystem.Presenter;

namespace HotelReservationSystem.Forms
{
    public partial class ForgotPasswordForm : Form, IPasswordResetView
    {
        private PasswordResetPresenter _presenter;

        public ForgotPasswordForm()
        {
            InitializeComponent();
            InitializePresenter();
            AssociateEvents();
            HideTabHeaders();
            InitializePasswordFields();
        }

        private void InitializePresenter()
        {
            string connectionString = DbConfig.GetConnectionString();
            IPasswordHasher passwordHasher = new Pbkdf2PasswordHasher();
            IUserRepository userRepository = new UserRepository(connectionString, passwordHasher);
            PasswordResetRepository resetRepository = new PasswordResetRepository(connectionString);
            IEmailService emailService = new SmtpEmailService();

            _presenter = new PasswordResetPresenter(this, userRepository, passwordHasher, resetRepository, emailService);
        }

        private void AssociateEvents()
        {
            btnRequestReset.Click += async (s, e) => await OnRequestResetClick();
            btnVerifyReset.Click += (s, e) => VerifyAndResetEvent?.Invoke(this, EventArgs.Empty);
            btnCancel.Click += (s, e) =>
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                this.Close();
            };
            btnBackToRequest.Click += (s, e) =>
            {
                ShowStep(1);
                HideSendingStatus();
            };

            chkShowNewPassword.CheckedChanged += (s, e) =>
                txtNewPassword.PasswordChar = !chkShowNewPassword.Checked;
            chkShowConfirmPassword.CheckedChanged += (s, e) =>
                txtConfirmPassword.PasswordChar = !chkShowConfirmPassword.Checked;

            txtResetToken.TextChanged += txtResetToken_TextChanged;
        }

        #region Initialization

        private void InitializePasswordFields()
        {
            txtNewPassword.PasswordChar = true;
            txtConfirmPassword.PasswordChar = true;

            chkShowNewPassword.Checked = false;
            chkShowConfirmPassword.Checked = false;
        }

        #endregion

        #region Status Label Management

        private void ShowSendingStatus(string message, Color color)
        {
            if (lblCodeSent != null)
            {
                lblCodeSent.Text = message;
                lblCodeSent.ForeColor = color;
                lblCodeSent.Visible = true;
            }
        }

        private void HideSendingStatus()
        {
            if (lblCodeSent != null)
            {
                lblCodeSent.Visible = false;
            }
        }

        private async Task OnRequestResetClick()
        {
            try
            {
                ShowSendingStatus("📧 Sending reset code to your email...", Color.FromArgb(101, 118, 255));
                btnRequestReset.Enabled = false;

                RequestResetEvent?.Invoke(this, EventArgs.Empty);

                await Task.Delay(500);
                btnRequestReset.Enabled = true;
            }
            catch (Exception ex)
            {
                ShowSendingStatus($"❌ Error: {ex.Message}", Color.Red);
                btnRequestReset.Enabled = true;
            }
        }

        #endregion

        #region Interface Implementation

        public string UsernameOrEmailRequest
        {
            get => txtUsernameRequest.Texts;
            set => txtUsernameRequest.Texts = value;
        }

        public string UsernameOrEmailVerify
        {
            get => txtUsernameVerify.Texts;
            set => txtUsernameVerify.Texts = value;
        }

        public string ResetToken
        {
            get => txtResetToken.Texts;
            set => txtResetToken.Texts = value;
        }

        public string NewPassword
        {
            get => txtNewPassword.Texts;
            set => txtNewPassword.Texts = value;
        }

        public string ConfirmPassword
        {
            get => txtConfirmPassword.Texts;
            set => txtConfirmPassword.Texts = value;
        }

        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        public event EventHandler RequestResetEvent;
        public event EventHandler VerifyAndResetEvent;
        public event EventHandler CancelEvent;

        #endregion

        #region Public Methods

        public void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);

            if (message.Contains("sent") || message.Contains("successfully"))
            {
                ShowSendingStatus("✅ " + message, Color.Green);
            }
            else if (message.Contains("error") || message.Contains("failed"))
            {
                ShowSendingStatus("❌ " + message, Color.Red);
            }
        }

        public void ShowStep(int step)
        {
            if (step == 1)
            {
                tabControl1.SelectedTab = tabPage1;
                lblTitle.Text = "Request Reset Password";
                txtUsernameRequest.Focus();
            }
            else if (step == 2)
            {
                tabControl1.SelectedTab = tabPage2;
                lblTitle.Text = "Reset Password";

                HideSendingStatus();

                txtNewPassword.PasswordChar = true;
                txtConfirmPassword.PasswordChar = true;
                chkShowNewPassword.Checked = false;
                chkShowConfirmPassword.Checked = false;

                txtResetToken.Focus();
            }
        }

        public void ClearFields()
        {
            txtUsernameRequest.Texts = string.Empty;
            txtUsernameVerify.Texts = string.Empty;
            txtResetToken.Texts = string.Empty;
            txtNewPassword.Texts = string.Empty;
            txtConfirmPassword.Texts = string.Empty;

            txtNewPassword.PasswordChar = true;
            txtConfirmPassword.PasswordChar = true;
            chkShowNewPassword.Checked = false;
            chkShowConfirmPassword.Checked = false;

            HideSendingStatus();
        }

        #endregion

        #region Private Helper Methods

        private void HideTabHeaders()
        {
            // Hide tab headers completely
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            // ✅ Remove border by enabling owner draw
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.Padding = new Point(0, 0);

            // ✅ Set background to match form
            tabControl1.BackColor = Color.White;

            tabControl1.DrawItem += TabControl1_DrawItem;
        }


        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            
        }

        private void txtResetToken_TextChanged(object sender, EventArgs e)
        {
            if (txtResetToken.Texts.Length > 6)
            {
                txtResetToken.Texts = txtResetToken.Texts.Substring(0, 6);
            }

            if (txtResetToken.Texts.Length == 6)
            {
                txtNewPassword.Focus();
            }
        }

        #endregion

        #region Event Handlers (From Designer)

        private void txtResetToken_FinishedTypingContent(object sender, EventArgs e)
        {
            string token = txtResetToken.Texts?.Trim();

            if (!string.IsNullOrEmpty(token) && token.Length != 6)
            {
                ShowMessage(
                    "Reset code must be exactly 6 digits.",
                    "Invalid Code",
                    MessageBoxIcon.Warning);
            }
        }


        #endregion

        private void ForgotPasswordForm_Load(object sender, EventArgs e)
        {
            tabControl1.DrawItem += TabControl1_DrawItem;
        }
    }
}