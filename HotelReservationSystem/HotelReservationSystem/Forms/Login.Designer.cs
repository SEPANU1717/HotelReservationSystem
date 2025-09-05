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
            this.txtUsername = new SATATextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new SATATextBox();
            this.sataPanel1 = new SATAUiFramework.SATAPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cuiCheckbox1 = new CuoreUI.Controls.cuiCheckbox();
            this.btnLogin = new FrameworkTest.SATAButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
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
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.White;
            this.txtUsername.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtUsername.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtUsername.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtUsername.BorderRadius = 4;
            this.txtUsername.BorderSize = 1;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtUsername.Icon = ((System.Drawing.Image)(resources.GetObject("txtUsername.Icon")));
            this.txtUsername.IconSize = new System.Drawing.Size(20, 20);
            this.txtUsername.Location = new System.Drawing.Point(929, 354);
            this.txtUsername.Multiline = false;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PasswordChar = false;
            this.txtUsername.PlaceholderColor = System.Drawing.Color.Silver;
            this.txtUsername.PlaceholderText = "Enter your username";
            this.txtUsername.Size = new System.Drawing.Size(406, 48);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Texts = "";
            this.txtUsername.UnderlinedStyle = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(1173, 487);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Forgot your password?";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtPassword.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtPassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtPassword.BorderRadius = 4;
            this.txtPassword.BorderSize = 1;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtPassword.Icon = ((System.Drawing.Image)(resources.GetObject("txtPassword.Icon")));
            this.txtPassword.IconSize = new System.Drawing.Size(20, 20);
            this.txtPassword.Location = new System.Drawing.Point(929, 423);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(5);
            this.txtPassword.Multiline = false;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = true;
            this.txtPassword.PlaceholderColor = System.Drawing.Color.Silver;
            this.txtPassword.PlaceholderText = "Enter your password";
            this.txtPassword.Size = new System.Drawing.Size(406, 48);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Texts = "";
            this.txtPassword.UnderlinedStyle = false;
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
            this.sataPanel1.Location = new System.Drawing.Point(12, 12);
            this.sataPanel1.Name = "sataPanel1";
            this.sataPanel1.Size = new System.Drawing.Size(768, 776);
            this.sataPanel1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(768, 776);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // cuiCheckbox1
            // 
            this.cuiCheckbox1.Checked = false;
            this.cuiCheckbox1.CheckedForeground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.cuiCheckbox1.CheckedOutlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.cuiCheckbox1.CheckedSymbolColor = System.Drawing.Color.White;
            this.cuiCheckbox1.Content = "Admin";
            this.cuiCheckbox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cuiCheckbox1.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.cuiCheckbox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.cuiCheckbox1.Location = new System.Drawing.Point(929, 491);
            this.cuiCheckbox1.MinimumSize = new System.Drawing.Size(16, 16);
            this.cuiCheckbox1.Name = "cuiCheckbox1";
            this.cuiCheckbox1.OutlineStyle = true;
            this.cuiCheckbox1.OutlineThickness = 1F;
            this.cuiCheckbox1.Rounding = 5;
            this.cuiCheckbox1.ShowSymbols = true;
            this.cuiCheckbox1.Size = new System.Drawing.Size(90, 16);
            this.cuiCheckbox1.TabIndex = 8;
            this.cuiCheckbox1.Text = "Admin";
            this.cuiCheckbox1.UncheckedForeground = System.Drawing.Color.Empty;
            this.cuiCheckbox1.UncheckedOutlineColor = System.Drawing.Color.Gray;
            this.cuiCheckbox1.UncheckedSymbolColor = System.Drawing.Color.Empty;
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
            this.btnLogin.Location = new System.Drawing.Point(929, 541);
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
            this.btnLogin.Size = new System.Drawing.Size(406, 47);
            this.btnLogin.TabIndex = 9;
            this.btnLogin.TextAutoCenter = true;
            this.btnLogin.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(992, 192);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(149, 129);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(1106, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(141, 50);
            this.label7.TabIndex = 6;
            this.label7.Text = "Lodgix";
            // 
            // Login
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1477, 800);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.cuiCheckbox1);
            this.Controls.Add(this.sataPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
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
        private SATATextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private SATATextBox txtPassword;
        private SATAUiFramework.SATAPanel sataPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private CuoreUI.Controls.cuiCheckbox cuiCheckbox1;
        private FrameworkTest.SATAButton btnLogin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}