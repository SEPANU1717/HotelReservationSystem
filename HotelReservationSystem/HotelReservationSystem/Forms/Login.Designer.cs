namespace HotelReservationSystem.Forms
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SATAUiFramework.BorderRadius borderRadius1 = new SATAUiFramework.BorderRadius();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.sataEllipseControl1 = new SATAUiFramework.Controls.SATAEllipseControl();
            this.sataPanel1 = new SATAUiFramework.SATAPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnLogin = new FrameworkTest.SATAButton();
            this.chkShowPassword = new CuoreUI.Controls.cuiCheckbox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new SATATextBox();
            this.txtUsername = new SATATextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sataPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // sataEllipseControl1
            // 
            this.sataEllipseControl1.CornerRadius = 36;
            this.sataEllipseControl1.TargetControl = this;
            // 
            // sataPanel1
            // 
            this.sataPanel1.BackColor = System.Drawing.Color.IndianRed;
            this.sataPanel1.BackColor2 = System.Drawing.Color.Transparent;
            this.sataPanel1.BorderColor = System.Drawing.Color.Black;
            borderRadius1.BottomLeft = 10;
            borderRadius1.BottomRight = 10;
            borderRadius1.TopLeft = 10;
            borderRadius1.TopRight = 10;
            this.sataPanel1.BorderRadius = borderRadius1;
            this.sataPanel1.BorderThickness = 0;
            this.sataPanel1.Controls.Add(this.pictureBox1);
            this.sataPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.sataPanel1.Location = new System.Drawing.Point(709, 0);
            this.sataPanel1.Name = "sataPanel1";
            this.sataPanel1.Size = new System.Drawing.Size(768, 800);
            this.sataPanel1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(768, 800);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(87, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 30);
            this.label7.TabIndex = 14;
            this.label7.Text = "Lodgix";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(19, 21);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(90, 68);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // btnLogin
            // 
            this.btnLogin.ButtonText = "Login";
            this.btnLogin.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnLogin.CheckedForeColor = System.Drawing.Color.White;
            this.btnLogin.CheckedImageTint = System.Drawing.Color.White;
            this.btnLogin.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnLogin.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(90)))), ((int)(((byte)(240)))));
            this.btnLogin.HoverForeColor = System.Drawing.Color.White;
            this.btnLogin.HoverImage = null;
            this.btnLogin.HoverImageTint = System.Drawing.Color.White;
            this.btnLogin.HoverOutline = System.Drawing.Color.Empty;
            this.btnLogin.Image = null;
            this.btnLogin.ImageAutoCenter = true;
            this.btnLogin.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnLogin.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnLogin.ImageTint = System.Drawing.Color.White;
            this.btnLogin.IsToggleButton = false;
            this.btnLogin.IsToggled = false;
            this.btnLogin.Location = new System.Drawing.Point(117, 532);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.btnLogin.NormalForeColor = System.Drawing.Color.White;
            this.btnLogin.NormalOutline = System.Drawing.Color.Empty;
            this.btnLogin.OutlineThickness = 2F;
            this.btnLogin.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.btnLogin.PressedForeColor = System.Drawing.Color.White;
            this.btnLogin.PressedImageTint = System.Drawing.Color.White;
            this.btnLogin.PressedOutline = System.Drawing.Color.Empty;
            this.btnLogin.Rounding = new System.Windows.Forms.Padding(5);
            this.btnLogin.Size = new System.Drawing.Size(471, 49);
            this.btnLogin.TabIndex = 16;
            this.btnLogin.TextAutoCenter = true;
            this.btnLogin.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.Checked = false;
            this.chkShowPassword.CheckedForeground = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.chkShowPassword.CheckedOutlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.chkShowPassword.CheckedSymbolColor = System.Drawing.Color.White;
            this.chkShowPassword.Content = "Show password";
            this.chkShowPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkShowPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.chkShowPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.chkShowPassword.Location = new System.Drawing.Point(115, 490);
            this.chkShowPassword.MinimumSize = new System.Drawing.Size(16, 16);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.OutlineStyle = true;
            this.chkShowPassword.OutlineThickness = 1F;
            this.chkShowPassword.Rounding = 5;
            this.chkShowPassword.ShowSymbols = true;
            this.chkShowPassword.Size = new System.Drawing.Size(121, 16);
            this.chkShowPassword.TabIndex = 15;
            this.chkShowPassword.Text = "Show password";
            this.chkShowPassword.UncheckedForeground = System.Drawing.Color.Empty;
            this.chkShowPassword.UncheckedOutlineColor = System.Drawing.Color.Gray;
            this.chkShowPassword.UncheckedSymbolColor = System.Drawing.Color.Empty;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(437, 487);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "Forgot your password?";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtPassword.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtPassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.txtPassword.BorderRadius = 4;
            this.txtPassword.BorderSize = 1;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtPassword.Icon = ((System.Drawing.Image)(resources.GetObject("txtPassword.Icon")));
            this.txtPassword.IconSize = new System.Drawing.Size(20, 20);
            this.txtPassword.Location = new System.Drawing.Point(117, 428);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(5);
            this.txtPassword.Multiline = false;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = true;
            this.txtPassword.PlaceholderColor = System.Drawing.Color.Silver;
            this.txtPassword.PlaceholderText = "Enter your password";
            this.txtPassword.Size = new System.Drawing.Size(471, 43);
            this.txtPassword.TabIndex = 12;
            this.txtPassword.Texts = "";
            this.txtPassword.UnderlinedStyle = false;
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.White;
            this.txtUsername.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtUsername.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtUsername.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.txtUsername.BorderRadius = 4;
            this.txtUsername.BorderSize = 1;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtUsername.Icon = ((System.Drawing.Image)(resources.GetObject("txtUsername.Icon")));
            this.txtUsername.IconSize = new System.Drawing.Size(20, 20);
            this.txtUsername.Location = new System.Drawing.Point(117, 367);
            this.txtUsername.Multiline = false;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PasswordChar = false;
            this.txtUsername.PlaceholderColor = System.Drawing.Color.Silver;
            this.txtUsername.PlaceholderText = "Enter your username";
            this.txtUsername.Size = new System.Drawing.Size(471, 43);
            this.txtUsername.TabIndex = 11;
            this.txtUsername.Texts = "";
            this.txtUsername.UnderlinedStyle = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 30F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(200, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(299, 54);
            this.label2.TabIndex = 14;
            this.label2.Text = "Welcome Back!";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(138, 304);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(422, 21);
            this.label3.TabIndex = 14;
            this.label3.Text = "Please enter your username or email and password to login";
            // 
            // Login
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1477, 800);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.sataPanel1);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.sataPanel1.ResumeLayout(false);
            this.sataPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SATAUiFramework.Controls.SATAEllipseControl sataEllipseControl1;
        private SATAUiFramework.SATAPanel sataPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox2;
        private FrameworkTest.SATAButton btnLogin;
        private CuoreUI.Controls.cuiCheckbox chkShowPassword;
        private System.Windows.Forms.Label label1;
        private SATATextBox txtPassword;
        private SATATextBox txtUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}