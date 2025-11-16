using System;
using System.Drawing;
using System.Windows.Forms;

namespace HotelReservationSystem.Presenter.Billing
{
    public class EmailConfigForm : Form
    {
        private TextBox txtSmtpServer;
        private TextBox txtPort;
        private TextBox txtSenderEmail;
        private TextBox txtPassword;
        private CheckBox chkEnableSsl;
        private Button btnOk;
        private Button btnCancel;

        public string SmtpServer => txtSmtpServer.Text;
        public int SmtpPort => int.TryParse(txtPort.Text, out int port) ? port : 587;
        public string SenderEmail => txtSenderEmail.Text;
        public string SenderPassword => txtPassword.Text;
        public bool EnableSsl => chkEnableSsl.Checked;

        public EmailConfigForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Email Configuration";
            this.Size = new Size(450, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblTitle = new Label
            {
                Text = "Configure Email Settings",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(400, 25),
                ForeColor = Color.FromArgb(52, 152, 219)
            };

            Label lblSmtp = new Label
            {
                Text = "SMTP Server:",
                Location = new Point(20, 60),
                Size = new Size(120, 20)
            };

            txtSmtpServer = new TextBox
            {
                Location = new Point(150, 58),
                Size = new Size(260, 25),
                Text = "smtp.gmail.com"
            };

            Label lblPort = new Label
            {
                Text = "Port:",
                Location = new Point(20, 95),
                Size = new Size(120, 20)
            };

            txtPort = new TextBox
            {
                Location = new Point(150, 93),
                Size = new Size(260, 25),
                Text = "587"
            };

            Label lblEmail = new Label
            {
                Text = "Sender Email:",
                Location = new Point(20, 130),
                Size = new Size(120, 20)
            };

            txtSenderEmail = new TextBox
            {
                Location = new Point(150, 128),
                Size = new Size(260, 25)
            };

            Label lblPassword = new Label
            {
                Text = "Password:",
                Location = new Point(20, 165),
                Size = new Size(120, 20)
            };

            txtPassword = new TextBox
            {
                Location = new Point(150, 163),
                Size = new Size(260, 25),
                UseSystemPasswordChar = true
            };

            chkEnableSsl = new CheckBox
            {
                Text = "Enable SSL",
                Location = new Point(150, 200),
                Size = new Size(120, 20),
                Checked = true
            };

            Label lblNote = new Label
            {
                Text = "Note: For Gmail, use App Password instead of your account password.\nGenerate at: Google Account > Security > 2-Step Verification > App passwords",
                Location = new Point(20, 230),
                Size = new Size(400, 40),
                ForeColor = Color.Gray,
                Font = new Font("Arial", 8, FontStyle.Italic)
            };

            btnOk = new Button
            {
                Text = "Send Email",
                Location = new Point(230, 280),
                Size = new Size(90, 30),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Click += BtnOk_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(330, 280),
                Size = new Size(80, 30),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblSmtp);
            this.Controls.Add(txtSmtpServer);
            this.Controls.Add(lblPort);
            this.Controls.Add(txtPort);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtSenderEmail);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(chkEnableSsl);
            this.Controls.Add(lblNote);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSmtpServer.Text))
            {
                MessageBox.Show("SMTP Server is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSenderEmail.Text))
            {
                MessageBox.Show("Sender Email is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
