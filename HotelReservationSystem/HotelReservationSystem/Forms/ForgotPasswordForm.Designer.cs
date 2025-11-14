namespace HotelReservationSystem.Forms
{
    partial class ForgotPasswordForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCodeSent = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsernameRequest = new SATATextBox();
            this.btnRequestReset = new FrameworkTest.SATAButton();
            this.btnCancel = new FrameworkTest.SATAButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBackToRequest = new FrameworkTest.SATAButton();
            this.chkShowConfirmPassword = new CuoreUI.Controls.cuiCheckbox();
            this.txtResetToken = new SATATextBox();
            this.txtUsernameVerify = new SATATextBox();
            this.chkShowNewPassword = new CuoreUI.Controls.cuiCheckbox();
            this.btnVerifyReset = new FrameworkTest.SATAButton();
            this.txtConfirmPassword = new SATATextBox();
            this.txtNewPassword = new SATATextBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(36, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(228, 23);
            this.lblTitle.TabIndex = 122;
            this.lblTitle.Text = "Request Reset Password";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(90)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(558, 56);
            this.panel1.TabIndex = 123;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Depth = 0;
            this.tabControl1.Location = new System.Drawing.Point(40, 62);
            this.tabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(477, 520);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.lblCodeSent);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtUsernameRequest);
            this.tabPage1.Controls.Add(this.btnRequestReset);
            this.tabPage1.Controls.Add(this.btnCancel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(469, 494);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(70, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 23);
            this.label4.TabIndex = 134;
            this.label4.Text = "Forgot Password";
            // 
            // lblCodeSent
            // 
            this.lblCodeSent.AutoSize = true;
            this.lblCodeSent.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodeSent.Location = new System.Drawing.Point(71, 188);
            this.lblCodeSent.Name = "lblCodeSent";
            this.lblCodeSent.Size = new System.Drawing.Size(0, 17);
            this.lblCodeSent.TabIndex = 132;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(71, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 17);
            this.label1.TabIndex = 133;
            this.label1.Text = "Enter your username or email";
            // 
            // txtUsernameRequest
            // 
            this.txtUsernameRequest.BackColor = System.Drawing.Color.White;
            this.txtUsernameRequest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtUsernameRequest.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtUsernameRequest.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtUsernameRequest.BorderRadius = 3;
            this.txtUsernameRequest.BorderSize = 1;
            this.txtUsernameRequest.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsernameRequest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtUsernameRequest.Icon = null;
            this.txtUsernameRequest.IconSize = new System.Drawing.Size(20, 20);
            this.txtUsernameRequest.Location = new System.Drawing.Point(74, 133);
            this.txtUsernameRequest.Multiline = false;
            this.txtUsernameRequest.Name = "txtUsernameRequest";
            this.txtUsernameRequest.PasswordChar = false;
            this.txtUsernameRequest.PlaceholderColor = System.Drawing.Color.Transparent;
            this.txtUsernameRequest.PlaceholderText = "";
            this.txtUsernameRequest.Size = new System.Drawing.Size(317, 35);
            this.txtUsernameRequest.TabIndex = 129;
            this.txtUsernameRequest.Texts = "";
            this.txtUsernameRequest.UnderlinedStyle = false;
            // 
            // btnRequestReset
            // 
            this.btnRequestReset.ButtonText = "Send Code";
            this.btnRequestReset.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnRequestReset.CheckedForeColor = System.Drawing.Color.White;
            this.btnRequestReset.CheckedImageTint = System.Drawing.Color.White;
            this.btnRequestReset.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnRequestReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRequestReset.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRequestReset.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRequestReset.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(90)))), ((int)(((byte)(240)))));
            this.btnRequestReset.HoverForeColor = System.Drawing.Color.White;
            this.btnRequestReset.HoverImage = null;
            this.btnRequestReset.HoverImageTint = System.Drawing.Color.White;
            this.btnRequestReset.HoverOutline = System.Drawing.Color.Empty;
            this.btnRequestReset.Image = null;
            this.btnRequestReset.ImageAutoCenter = true;
            this.btnRequestReset.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnRequestReset.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnRequestReset.ImageTint = System.Drawing.Color.White;
            this.btnRequestReset.IsToggleButton = false;
            this.btnRequestReset.IsToggled = false;
            this.btnRequestReset.Location = new System.Drawing.Point(74, 231);
            this.btnRequestReset.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnRequestReset.Name = "btnRequestReset";
            this.btnRequestReset.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.btnRequestReset.NormalForeColor = System.Drawing.Color.White;
            this.btnRequestReset.NormalOutline = System.Drawing.Color.Empty;
            this.btnRequestReset.OutlineThickness = 2F;
            this.btnRequestReset.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.btnRequestReset.PressedForeColor = System.Drawing.Color.White;
            this.btnRequestReset.PressedImageTint = System.Drawing.Color.White;
            this.btnRequestReset.PressedOutline = System.Drawing.Color.Empty;
            this.btnRequestReset.Rounding = new System.Windows.Forms.Padding(5);
            this.btnRequestReset.Size = new System.Drawing.Size(152, 39);
            this.btnRequestReset.TabIndex = 130;
            this.btnRequestReset.TextAutoCenter = true;
            this.btnRequestReset.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // btnCancel
            // 
            this.btnCancel.ButtonText = "Cancel";
            this.btnCancel.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnCancel.CheckedForeColor = System.Drawing.Color.White;
            this.btnCancel.CheckedImageTint = System.Drawing.Color.White;
            this.btnCancel.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(90)))), ((int)(((byte)(240)))));
            this.btnCancel.HoverForeColor = System.Drawing.Color.White;
            this.btnCancel.HoverImage = null;
            this.btnCancel.HoverImageTint = System.Drawing.Color.White;
            this.btnCancel.HoverOutline = System.Drawing.Color.Empty;
            this.btnCancel.Image = null;
            this.btnCancel.ImageAutoCenter = true;
            this.btnCancel.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnCancel.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnCancel.ImageTint = System.Drawing.Color.White;
            this.btnCancel.IsToggleButton = false;
            this.btnCancel.IsToggled = false;
            this.btnCancel.Location = new System.Drawing.Point(239, 231);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.btnCancel.NormalForeColor = System.Drawing.Color.White;
            this.btnCancel.NormalOutline = System.Drawing.Color.Empty;
            this.btnCancel.OutlineThickness = 2F;
            this.btnCancel.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.btnCancel.PressedForeColor = System.Drawing.Color.White;
            this.btnCancel.PressedImageTint = System.Drawing.Color.White;
            this.btnCancel.PressedOutline = System.Drawing.Color.Empty;
            this.btnCancel.Rounding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(152, 39);
            this.btnCancel.TabIndex = 131;
            this.btnCancel.TextAutoCenter = true;
            this.btnCancel.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.btnBackToRequest);
            this.tabPage2.Controls.Add(this.chkShowConfirmPassword);
            this.tabPage2.Controls.Add(this.txtResetToken);
            this.tabPage2.Controls.Add(this.txtUsernameVerify);
            this.tabPage2.Controls.Add(this.chkShowNewPassword);
            this.tabPage2.Controls.Add(this.btnVerifyReset);
            this.tabPage2.Controls.Add(this.txtConfirmPassword);
            this.tabPage2.Controls.Add(this.txtNewPassword);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(469, 494);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(74, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 23);
            this.label7.TabIndex = 148;
            this.label7.Text = "Reset Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(75, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 17);
            this.label3.TabIndex = 142;
            this.label3.Text = "Enter 6-Digit Code";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(75, 321);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 17);
            this.label6.TabIndex = 143;
            this.label6.Text = "Confirm Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(75, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 17);
            this.label5.TabIndex = 144;
            this.label5.Text = "New Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(75, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 17);
            this.label2.TabIndex = 145;
            this.label2.Text = "Username/Email";
            // 
            // btnBackToRequest
            // 
            this.btnBackToRequest.ButtonText = "Back";
            this.btnBackToRequest.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnBackToRequest.CheckedForeColor = System.Drawing.Color.White;
            this.btnBackToRequest.CheckedImageTint = System.Drawing.Color.White;
            this.btnBackToRequest.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnBackToRequest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackToRequest.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnBackToRequest.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackToRequest.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(90)))), ((int)(((byte)(240)))));
            this.btnBackToRequest.HoverForeColor = System.Drawing.Color.White;
            this.btnBackToRequest.HoverImage = null;
            this.btnBackToRequest.HoverImageTint = System.Drawing.Color.White;
            this.btnBackToRequest.HoverOutline = System.Drawing.Color.Empty;
            this.btnBackToRequest.Image = null;
            this.btnBackToRequest.ImageAutoCenter = true;
            this.btnBackToRequest.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnBackToRequest.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnBackToRequest.ImageTint = System.Drawing.Color.White;
            this.btnBackToRequest.IsToggleButton = false;
            this.btnBackToRequest.IsToggled = false;
            this.btnBackToRequest.Location = new System.Drawing.Point(243, 429);
            this.btnBackToRequest.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnBackToRequest.Name = "btnBackToRequest";
            this.btnBackToRequest.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.btnBackToRequest.NormalForeColor = System.Drawing.Color.White;
            this.btnBackToRequest.NormalOutline = System.Drawing.Color.Empty;
            this.btnBackToRequest.OutlineThickness = 2F;
            this.btnBackToRequest.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.btnBackToRequest.PressedForeColor = System.Drawing.Color.White;
            this.btnBackToRequest.PressedImageTint = System.Drawing.Color.White;
            this.btnBackToRequest.PressedOutline = System.Drawing.Color.Empty;
            this.btnBackToRequest.Rounding = new System.Windows.Forms.Padding(5);
            this.btnBackToRequest.Size = new System.Drawing.Size(152, 39);
            this.btnBackToRequest.TabIndex = 140;
            this.btnBackToRequest.TextAutoCenter = true;
            this.btnBackToRequest.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // chkShowConfirmPassword
            // 
            this.chkShowConfirmPassword.Checked = false;
            this.chkShowConfirmPassword.CheckedForeground = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.chkShowConfirmPassword.CheckedOutlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.chkShowConfirmPassword.CheckedSymbolColor = System.Drawing.Color.White;
            this.chkShowConfirmPassword.Content = "Show password";
            this.chkShowConfirmPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkShowConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.chkShowConfirmPassword.Location = new System.Drawing.Point(78, 387);
            this.chkShowConfirmPassword.MinimumSize = new System.Drawing.Size(16, 16);
            this.chkShowConfirmPassword.Name = "chkShowConfirmPassword";
            this.chkShowConfirmPassword.OutlineStyle = true;
            this.chkShowConfirmPassword.OutlineThickness = 1F;
            this.chkShowConfirmPassword.Rounding = 5;
            this.chkShowConfirmPassword.ShowSymbols = true;
            this.chkShowConfirmPassword.Size = new System.Drawing.Size(121, 16);
            this.chkShowConfirmPassword.TabIndex = 146;
            this.chkShowConfirmPassword.Text = "Show password";
            this.chkShowConfirmPassword.UncheckedForeground = System.Drawing.Color.Empty;
            this.chkShowConfirmPassword.UncheckedOutlineColor = System.Drawing.Color.Gray;
            this.chkShowConfirmPassword.UncheckedSymbolColor = System.Drawing.Color.Empty;
            // 
            // txtResetToken
            // 
            this.txtResetToken.BackColor = System.Drawing.Color.White;
            this.txtResetToken.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtResetToken.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtResetToken.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtResetToken.BorderRadius = 3;
            this.txtResetToken.BorderSize = 1;
            this.txtResetToken.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResetToken.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtResetToken.Icon = null;
            this.txtResetToken.IconSize = new System.Drawing.Size(20, 20);
            this.txtResetToken.Location = new System.Drawing.Point(78, 169);
            this.txtResetToken.Multiline = false;
            this.txtResetToken.Name = "txtResetToken";
            this.txtResetToken.PasswordChar = false;
            this.txtResetToken.PlaceholderColor = System.Drawing.Color.Transparent;
            this.txtResetToken.PlaceholderText = "";
            this.txtResetToken.Size = new System.Drawing.Size(317, 35);
            this.txtResetToken.TabIndex = 136;
            this.txtResetToken.Texts = "";
            this.txtResetToken.UnderlinedStyle = false;
            // 
            // txtUsernameVerify
            // 
            this.txtUsernameVerify.BackColor = System.Drawing.Color.White;
            this.txtUsernameVerify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtUsernameVerify.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtUsernameVerify.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtUsernameVerify.BorderRadius = 3;
            this.txtUsernameVerify.BorderSize = 1;
            this.txtUsernameVerify.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsernameVerify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtUsernameVerify.Icon = null;
            this.txtUsernameVerify.IconSize = new System.Drawing.Size(20, 20);
            this.txtUsernameVerify.Location = new System.Drawing.Point(78, 99);
            this.txtUsernameVerify.Multiline = false;
            this.txtUsernameVerify.Name = "txtUsernameVerify";
            this.txtUsernameVerify.PasswordChar = false;
            this.txtUsernameVerify.PlaceholderColor = System.Drawing.Color.Transparent;
            this.txtUsernameVerify.PlaceholderText = "";
            this.txtUsernameVerify.Size = new System.Drawing.Size(317, 35);
            this.txtUsernameVerify.TabIndex = 137;
            this.txtUsernameVerify.Texts = "";
            this.txtUsernameVerify.UnderlinedStyle = false;
            // 
            // chkShowNewPassword
            // 
            this.chkShowNewPassword.Checked = false;
            this.chkShowNewPassword.CheckedForeground = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.chkShowNewPassword.CheckedOutlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.chkShowNewPassword.CheckedSymbolColor = System.Drawing.Color.White;
            this.chkShowNewPassword.Content = "Show password";
            this.chkShowNewPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkShowNewPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.chkShowNewPassword.Location = new System.Drawing.Point(78, 285);
            this.chkShowNewPassword.MinimumSize = new System.Drawing.Size(16, 16);
            this.chkShowNewPassword.Name = "chkShowNewPassword";
            this.chkShowNewPassword.OutlineStyle = true;
            this.chkShowNewPassword.OutlineThickness = 1F;
            this.chkShowNewPassword.Rounding = 5;
            this.chkShowNewPassword.ShowSymbols = true;
            this.chkShowNewPassword.Size = new System.Drawing.Size(121, 16);
            this.chkShowNewPassword.TabIndex = 147;
            this.chkShowNewPassword.Text = "Show password";
            this.chkShowNewPassword.UncheckedForeground = System.Drawing.Color.Empty;
            this.chkShowNewPassword.UncheckedOutlineColor = System.Drawing.Color.Gray;
            this.chkShowNewPassword.UncheckedSymbolColor = System.Drawing.Color.Empty;
            // 
            // btnVerifyReset
            // 
            this.btnVerifyReset.ButtonText = "Reset Password";
            this.btnVerifyReset.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnVerifyReset.CheckedForeColor = System.Drawing.Color.White;
            this.btnVerifyReset.CheckedImageTint = System.Drawing.Color.White;
            this.btnVerifyReset.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnVerifyReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerifyReset.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnVerifyReset.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerifyReset.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(90)))), ((int)(((byte)(240)))));
            this.btnVerifyReset.HoverForeColor = System.Drawing.Color.White;
            this.btnVerifyReset.HoverImage = null;
            this.btnVerifyReset.HoverImageTint = System.Drawing.Color.White;
            this.btnVerifyReset.HoverOutline = System.Drawing.Color.Empty;
            this.btnVerifyReset.Image = null;
            this.btnVerifyReset.ImageAutoCenter = true;
            this.btnVerifyReset.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnVerifyReset.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnVerifyReset.ImageTint = System.Drawing.Color.White;
            this.btnVerifyReset.IsToggleButton = false;
            this.btnVerifyReset.IsToggled = false;
            this.btnVerifyReset.Location = new System.Drawing.Point(78, 429);
            this.btnVerifyReset.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnVerifyReset.Name = "btnVerifyReset";
            this.btnVerifyReset.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.btnVerifyReset.NormalForeColor = System.Drawing.Color.White;
            this.btnVerifyReset.NormalOutline = System.Drawing.Color.Empty;
            this.btnVerifyReset.OutlineThickness = 2F;
            this.btnVerifyReset.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))));
            this.btnVerifyReset.PressedForeColor = System.Drawing.Color.White;
            this.btnVerifyReset.PressedImageTint = System.Drawing.Color.White;
            this.btnVerifyReset.PressedOutline = System.Drawing.Color.Empty;
            this.btnVerifyReset.Rounding = new System.Windows.Forms.Padding(5);
            this.btnVerifyReset.Size = new System.Drawing.Size(152, 39);
            this.btnVerifyReset.TabIndex = 141;
            this.btnVerifyReset.TextAutoCenter = true;
            this.btnVerifyReset.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.BackColor = System.Drawing.Color.White;
            this.txtConfirmPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtConfirmPassword.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtConfirmPassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtConfirmPassword.BorderRadius = 3;
            this.txtConfirmPassword.BorderSize = 1;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtConfirmPassword.Icon = null;
            this.txtConfirmPassword.IconSize = new System.Drawing.Size(20, 20);
            this.txtConfirmPassword.Location = new System.Drawing.Point(78, 346);
            this.txtConfirmPassword.Multiline = false;
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = false;
            this.txtConfirmPassword.PlaceholderColor = System.Drawing.Color.Transparent;
            this.txtConfirmPassword.PlaceholderText = "";
            this.txtConfirmPassword.Size = new System.Drawing.Size(317, 35);
            this.txtConfirmPassword.TabIndex = 138;
            this.txtConfirmPassword.Texts = "";
            this.txtConfirmPassword.UnderlinedStyle = false;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.BackColor = System.Drawing.Color.White;
            this.txtNewPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtNewPassword.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtNewPassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtNewPassword.BorderRadius = 3;
            this.txtNewPassword.BorderSize = 1;
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtNewPassword.Icon = null;
            this.txtNewPassword.IconSize = new System.Drawing.Size(20, 20);
            this.txtNewPassword.Location = new System.Drawing.Point(78, 244);
            this.txtNewPassword.Multiline = false;
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = false;
            this.txtNewPassword.PlaceholderColor = System.Drawing.Color.Transparent;
            this.txtNewPassword.PlaceholderText = "";
            this.txtNewPassword.Size = new System.Drawing.Size(317, 35);
            this.txtNewPassword.TabIndex = 139;
            this.txtNewPassword.Texts = "";
            this.txtNewPassword.UnderlinedStyle = false;
            // 
            // ForgotPasswordForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(558, 620);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ForgotPasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ForgotPasswordForm";
            this.Load += new System.EventHandler(this.ForgotPasswordForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialTabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCodeSent;
        private System.Windows.Forms.Label label1;
        private SATATextBox txtUsernameRequest;
        private FrameworkTest.SATAButton btnRequestReset;
        private FrameworkTest.SATAButton btnCancel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private FrameworkTest.SATAButton btnBackToRequest;
        private CuoreUI.Controls.cuiCheckbox chkShowConfirmPassword;
        private SATATextBox txtResetToken;
        private SATATextBox txtUsernameVerify;
        private CuoreUI.Controls.cuiCheckbox chkShowNewPassword;
        private FrameworkTest.SATAButton btnVerifyReset;
        private SATATextBox txtConfirmPassword;
        private SATATextBox txtNewPassword;
    }
}