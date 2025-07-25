namespace HotelReservationSystem.UserControls
{
    partial class UCRooms
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCRooms));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            SATAUiFramework.BorderRadius borderRadius1 = new SATAUiFramework.BorderRadius();
            this.btnRoomSearch = new FrameworkTest.SATAButton();
            this.txtRoomSearch = new SATATextBox();
            this.btnRoomDelete = new FrameworkTest.SATAButton();
            this.btnRoomAddNew = new FrameworkTest.SATAButton();
            this.btnRoomEdit = new FrameworkTest.SATAButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnRoomCancel = new FrameworkTest.SATAButton();
            this.btnRoomSave = new FrameworkTest.SATAButton();
            this.txtAddress = new SATATextBox();
            this.txtContact = new SATATextBox();
            this.txtIdType = new SATATextBox();
            this.txtName = new SATATextBox();
            this.txtCusId = new SATATextBox();
            this.dataGridRoom = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.sataPanel1 = new SATAUiFramework.SATAPanel();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.sataPictureBox1 = new SATAUiFramework.Controls.SATAPictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRoom)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.sataPanel1.SuspendLayout();
            this.materialTabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sataPictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRoomSearch
            // 
            this.btnRoomSearch.ButtonText = "Search";
            this.btnRoomSearch.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnRoomSearch.CheckedForeColor = System.Drawing.Color.White;
            this.btnRoomSearch.CheckedImageTint = System.Drawing.Color.White;
            this.btnRoomSearch.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnRoomSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRoomSearch.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRoomSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoomSearch.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnRoomSearch.HoverForeColor = System.Drawing.Color.White;
            this.btnRoomSearch.HoverImage = null;
            this.btnRoomSearch.HoverImageTint = System.Drawing.Color.White;
            this.btnRoomSearch.HoverOutline = System.Drawing.Color.Empty;
            this.btnRoomSearch.Image = null;
            this.btnRoomSearch.ImageAutoCenter = true;
            this.btnRoomSearch.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnRoomSearch.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnRoomSearch.ImageTint = System.Drawing.Color.White;
            this.btnRoomSearch.IsToggleButton = false;
            this.btnRoomSearch.IsToggled = false;
            this.btnRoomSearch.Location = new System.Drawing.Point(414, 100);
            this.btnRoomSearch.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnRoomSearch.Name = "btnRoomSearch";
            this.btnRoomSearch.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRoomSearch.NormalForeColor = System.Drawing.Color.White;
            this.btnRoomSearch.NormalOutline = System.Drawing.Color.Empty;
            this.btnRoomSearch.OutlineThickness = 2F;
            this.btnRoomSearch.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRoomSearch.PressedForeColor = System.Drawing.Color.White;
            this.btnRoomSearch.PressedImageTint = System.Drawing.Color.White;
            this.btnRoomSearch.PressedOutline = System.Drawing.Color.Empty;
            this.btnRoomSearch.Rounding = new System.Windows.Forms.Padding(5);
            this.btnRoomSearch.Size = new System.Drawing.Size(163, 39);
            this.btnRoomSearch.TabIndex = 9;
            this.btnRoomSearch.TextAutoCenter = true;
            this.btnRoomSearch.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // txtRoomSearch
            // 
            this.txtRoomSearch.BackColor = System.Drawing.Color.White;
            this.txtRoomSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtRoomSearch.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtRoomSearch.BorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtRoomSearch.BorderRadius = 4;
            this.txtRoomSearch.BorderSize = 1;
            this.txtRoomSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoomSearch.ForeColor = System.Drawing.Color.Black;
            this.txtRoomSearch.Icon = ((System.Drawing.Image)(resources.GetObject("txtRoomSearch.Icon")));
            this.txtRoomSearch.IconSize = new System.Drawing.Size(20, 20);
            this.txtRoomSearch.Location = new System.Drawing.Point(32, 100);
            this.txtRoomSearch.Multiline = false;
            this.txtRoomSearch.Name = "txtRoomSearch";
            this.txtRoomSearch.PasswordChar = false;
            this.txtRoomSearch.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtRoomSearch.PlaceholderText = "Search Customer";
            this.txtRoomSearch.Size = new System.Drawing.Size(374, 39);
            this.txtRoomSearch.TabIndex = 8;
            this.txtRoomSearch.Texts = "";
            this.txtRoomSearch.UnderlinedStyle = false;
            // 
            // btnRoomDelete
            // 
            this.btnRoomDelete.ButtonText = "";
            this.btnRoomDelete.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnRoomDelete.CheckedForeColor = System.Drawing.Color.White;
            this.btnRoomDelete.CheckedImageTint = System.Drawing.Color.White;
            this.btnRoomDelete.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnRoomDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRoomDelete.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRoomDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoomDelete.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(35)))), ((int)(((byte)(60)))));
            this.btnRoomDelete.HoverForeColor = System.Drawing.Color.White;
            this.btnRoomDelete.HoverImage = null;
            this.btnRoomDelete.HoverImageTint = System.Drawing.Color.White;
            this.btnRoomDelete.HoverOutline = System.Drawing.Color.Empty;
            this.btnRoomDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomDelete.Image")));
            this.btnRoomDelete.ImageAutoCenter = true;
            this.btnRoomDelete.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnRoomDelete.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnRoomDelete.ImageTint = System.Drawing.Color.White;
            this.btnRoomDelete.IsToggleButton = false;
            this.btnRoomDelete.IsToggled = false;
            this.btnRoomDelete.Location = new System.Drawing.Point(1170, 100);
            this.btnRoomDelete.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnRoomDelete.Name = "btnRoomDelete";
            this.btnRoomDelete.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(104)))), ((int)(((byte)(107)))));
            this.btnRoomDelete.NormalForeColor = System.Drawing.Color.White;
            this.btnRoomDelete.NormalOutline = System.Drawing.Color.Empty;
            this.btnRoomDelete.OutlineThickness = 2F;
            this.btnRoomDelete.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(104)))), ((int)(((byte)(107)))));
            this.btnRoomDelete.PressedForeColor = System.Drawing.Color.White;
            this.btnRoomDelete.PressedImageTint = System.Drawing.Color.White;
            this.btnRoomDelete.PressedOutline = System.Drawing.Color.Empty;
            this.btnRoomDelete.Rounding = new System.Windows.Forms.Padding(5);
            this.btnRoomDelete.Size = new System.Drawing.Size(44, 39);
            this.btnRoomDelete.TabIndex = 10;
            this.btnRoomDelete.TextAutoCenter = false;
            this.btnRoomDelete.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // btnRoomAddNew
            // 
            this.btnRoomAddNew.ButtonText = "Add New";
            this.btnRoomAddNew.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnRoomAddNew.CheckedForeColor = System.Drawing.Color.White;
            this.btnRoomAddNew.CheckedImageTint = System.Drawing.Color.White;
            this.btnRoomAddNew.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnRoomAddNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRoomAddNew.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRoomAddNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoomAddNew.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnRoomAddNew.HoverForeColor = System.Drawing.Color.White;
            this.btnRoomAddNew.HoverImage = null;
            this.btnRoomAddNew.HoverImageTint = System.Drawing.Color.White;
            this.btnRoomAddNew.HoverOutline = System.Drawing.Color.Empty;
            this.btnRoomAddNew.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomAddNew.Image")));
            this.btnRoomAddNew.ImageAutoCenter = true;
            this.btnRoomAddNew.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnRoomAddNew.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnRoomAddNew.ImageTint = System.Drawing.Color.White;
            this.btnRoomAddNew.IsToggleButton = false;
            this.btnRoomAddNew.IsToggled = false;
            this.btnRoomAddNew.Location = new System.Drawing.Point(984, 100);
            this.btnRoomAddNew.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnRoomAddNew.Name = "btnRoomAddNew";
            this.btnRoomAddNew.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRoomAddNew.NormalForeColor = System.Drawing.Color.White;
            this.btnRoomAddNew.NormalOutline = System.Drawing.Color.Empty;
            this.btnRoomAddNew.OutlineThickness = 2F;
            this.btnRoomAddNew.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRoomAddNew.PressedForeColor = System.Drawing.Color.White;
            this.btnRoomAddNew.PressedImageTint = System.Drawing.Color.White;
            this.btnRoomAddNew.PressedOutline = System.Drawing.Color.Empty;
            this.btnRoomAddNew.Rounding = new System.Windows.Forms.Padding(5);
            this.btnRoomAddNew.Size = new System.Drawing.Size(122, 39);
            this.btnRoomAddNew.TabIndex = 14;
            this.btnRoomAddNew.TextAutoCenter = true;
            this.btnRoomAddNew.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // btnRoomEdit
            // 
            this.btnRoomEdit.ButtonText = "";
            this.btnRoomEdit.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnRoomEdit.CheckedForeColor = System.Drawing.Color.White;
            this.btnRoomEdit.CheckedImageTint = System.Drawing.Color.White;
            this.btnRoomEdit.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnRoomEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRoomEdit.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRoomEdit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoomEdit.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnRoomEdit.HoverForeColor = System.Drawing.Color.White;
            this.btnRoomEdit.HoverImage = null;
            this.btnRoomEdit.HoverImageTint = System.Drawing.Color.White;
            this.btnRoomEdit.HoverOutline = System.Drawing.Color.Empty;
            this.btnRoomEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomEdit.Image")));
            this.btnRoomEdit.ImageAutoCenter = true;
            this.btnRoomEdit.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnRoomEdit.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnRoomEdit.ImageTint = System.Drawing.Color.White;
            this.btnRoomEdit.IsToggleButton = false;
            this.btnRoomEdit.IsToggled = false;
            this.btnRoomEdit.Location = new System.Drawing.Point(1116, 100);
            this.btnRoomEdit.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnRoomEdit.Name = "btnRoomEdit";
            this.btnRoomEdit.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRoomEdit.NormalForeColor = System.Drawing.Color.White;
            this.btnRoomEdit.NormalOutline = System.Drawing.Color.Empty;
            this.btnRoomEdit.OutlineThickness = 2F;
            this.btnRoomEdit.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRoomEdit.PressedForeColor = System.Drawing.Color.White;
            this.btnRoomEdit.PressedImageTint = System.Drawing.Color.White;
            this.btnRoomEdit.PressedOutline = System.Drawing.Color.Empty;
            this.btnRoomEdit.Rounding = new System.Windows.Forms.Padding(5);
            this.btnRoomEdit.Size = new System.Drawing.Size(44, 39);
            this.btnRoomEdit.TabIndex = 11;
            this.btnRoomEdit.TextAutoCenter = false;
            this.btnRoomEdit.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.tabPage2.Controls.Add(this.btnRoomCancel);
            this.tabPage2.Controls.Add(this.btnRoomSave);
            this.tabPage2.Controls.Add(this.txtAddress);
            this.tabPage2.Controls.Add(this.txtContact);
            this.tabPage2.Controls.Add(this.txtIdType);
            this.tabPage2.Controls.Add(this.txtName);
            this.tabPage2.Controls.Add(this.txtCusId);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1174, 582);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Customer Details";
            // 
            // btnRoomCancel
            // 
            this.btnRoomCancel.ButtonText = "Cancel";
            this.btnRoomCancel.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnRoomCancel.CheckedForeColor = System.Drawing.Color.White;
            this.btnRoomCancel.CheckedImageTint = System.Drawing.Color.White;
            this.btnRoomCancel.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnRoomCancel.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRoomCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoomCancel.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnRoomCancel.HoverForeColor = System.Drawing.Color.White;
            this.btnRoomCancel.HoverImage = null;
            this.btnRoomCancel.HoverImageTint = System.Drawing.Color.White;
            this.btnRoomCancel.HoverOutline = System.Drawing.Color.Empty;
            this.btnRoomCancel.Image = null;
            this.btnRoomCancel.ImageAutoCenter = true;
            this.btnRoomCancel.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnRoomCancel.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnRoomCancel.ImageTint = System.Drawing.Color.White;
            this.btnRoomCancel.IsToggleButton = false;
            this.btnRoomCancel.IsToggled = false;
            this.btnRoomCancel.Location = new System.Drawing.Point(343, 389);
            this.btnRoomCancel.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnRoomCancel.Name = "btnRoomCancel";
            this.btnRoomCancel.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRoomCancel.NormalForeColor = System.Drawing.Color.White;
            this.btnRoomCancel.NormalOutline = System.Drawing.Color.Empty;
            this.btnRoomCancel.OutlineThickness = 2F;
            this.btnRoomCancel.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRoomCancel.PressedForeColor = System.Drawing.Color.White;
            this.btnRoomCancel.PressedImageTint = System.Drawing.Color.White;
            this.btnRoomCancel.PressedOutline = System.Drawing.Color.Empty;
            this.btnRoomCancel.Rounding = new System.Windows.Forms.Padding(5);
            this.btnRoomCancel.Size = new System.Drawing.Size(163, 35);
            this.btnRoomCancel.TabIndex = 5;
            this.btnRoomCancel.TextAutoCenter = true;
            this.btnRoomCancel.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // btnRoomSave
            // 
            this.btnRoomSave.ButtonText = "Save";
            this.btnRoomSave.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnRoomSave.CheckedForeColor = System.Drawing.Color.White;
            this.btnRoomSave.CheckedImageTint = System.Drawing.Color.White;
            this.btnRoomSave.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnRoomSave.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRoomSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoomSave.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnRoomSave.HoverForeColor = System.Drawing.Color.White;
            this.btnRoomSave.HoverImage = null;
            this.btnRoomSave.HoverImageTint = System.Drawing.Color.White;
            this.btnRoomSave.HoverOutline = System.Drawing.Color.Empty;
            this.btnRoomSave.Image = null;
            this.btnRoomSave.ImageAutoCenter = true;
            this.btnRoomSave.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnRoomSave.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnRoomSave.ImageTint = System.Drawing.Color.White;
            this.btnRoomSave.IsToggleButton = false;
            this.btnRoomSave.IsToggled = false;
            this.btnRoomSave.Location = new System.Drawing.Point(154, 389);
            this.btnRoomSave.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnRoomSave.Name = "btnRoomSave";
            this.btnRoomSave.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRoomSave.NormalForeColor = System.Drawing.Color.White;
            this.btnRoomSave.NormalOutline = System.Drawing.Color.Empty;
            this.btnRoomSave.OutlineThickness = 2F;
            this.btnRoomSave.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRoomSave.PressedForeColor = System.Drawing.Color.White;
            this.btnRoomSave.PressedImageTint = System.Drawing.Color.White;
            this.btnRoomSave.PressedOutline = System.Drawing.Color.Empty;
            this.btnRoomSave.Rounding = new System.Windows.Forms.Padding(5);
            this.btnRoomSave.Size = new System.Drawing.Size(163, 35);
            this.btnRoomSave.TabIndex = 5;
            this.btnRoomSave.TextAutoCenter = true;
            this.btnRoomSave.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtAddress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtAddress.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtAddress.BorderFocusColor = System.Drawing.Color.White;
            this.txtAddress.BorderRadius = 3;
            this.txtAddress.BorderSize = 1;
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.ForeColor = System.Drawing.Color.White;
            this.txtAddress.Icon = null;
            this.txtAddress.IconSize = new System.Drawing.Size(20, 20);
            this.txtAddress.Location = new System.Drawing.Point(378, 279);
            this.txtAddress.Multiline = false;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.PasswordChar = false;
            this.txtAddress.PlaceholderColor = System.Drawing.Color.Transparent;
            this.txtAddress.PlaceholderText = "";
            this.txtAddress.Size = new System.Drawing.Size(263, 35);
            this.txtAddress.TabIndex = 3;
            this.txtAddress.Texts = "";
            this.txtAddress.UnderlinedStyle = false;
            // 
            // txtContact
            // 
            this.txtContact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtContact.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtContact.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtContact.BorderFocusColor = System.Drawing.Color.White;
            this.txtContact.BorderRadius = 3;
            this.txtContact.BorderSize = 1;
            this.txtContact.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContact.ForeColor = System.Drawing.Color.White;
            this.txtContact.Icon = null;
            this.txtContact.IconSize = new System.Drawing.Size(20, 20);
            this.txtContact.Location = new System.Drawing.Point(80, 279);
            this.txtContact.Multiline = false;
            this.txtContact.Name = "txtContact";
            this.txtContact.PasswordChar = false;
            this.txtContact.PlaceholderColor = System.Drawing.Color.Transparent;
            this.txtContact.PlaceholderText = "";
            this.txtContact.Size = new System.Drawing.Size(263, 35);
            this.txtContact.TabIndex = 3;
            this.txtContact.Texts = "";
            this.txtContact.UnderlinedStyle = false;
            // 
            // txtIdType
            // 
            this.txtIdType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtIdType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtIdType.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtIdType.BorderFocusColor = System.Drawing.Color.White;
            this.txtIdType.BorderRadius = 3;
            this.txtIdType.BorderSize = 1;
            this.txtIdType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdType.ForeColor = System.Drawing.Color.White;
            this.txtIdType.Icon = null;
            this.txtIdType.IconSize = new System.Drawing.Size(20, 20);
            this.txtIdType.Location = new System.Drawing.Point(378, 213);
            this.txtIdType.Multiline = false;
            this.txtIdType.Name = "txtIdType";
            this.txtIdType.PasswordChar = false;
            this.txtIdType.PlaceholderColor = System.Drawing.Color.Transparent;
            this.txtIdType.PlaceholderText = "";
            this.txtIdType.Size = new System.Drawing.Size(263, 35);
            this.txtIdType.TabIndex = 3;
            this.txtIdType.Texts = "";
            this.txtIdType.UnderlinedStyle = false;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtName.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtName.BorderFocusColor = System.Drawing.Color.White;
            this.txtName.BorderRadius = 3;
            this.txtName.BorderSize = 1;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Icon = null;
            this.txtName.IconSize = new System.Drawing.Size(20, 20);
            this.txtName.Location = new System.Drawing.Point(80, 213);
            this.txtName.Multiline = false;
            this.txtName.Name = "txtName";
            this.txtName.PasswordChar = false;
            this.txtName.PlaceholderColor = System.Drawing.Color.Transparent;
            this.txtName.PlaceholderText = "";
            this.txtName.Size = new System.Drawing.Size(263, 35);
            this.txtName.TabIndex = 3;
            this.txtName.Texts = "";
            this.txtName.UnderlinedStyle = false;
            // 
            // txtCusId
            // 
            this.txtCusId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtCusId.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtCusId.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtCusId.BorderFocusColor = System.Drawing.Color.White;
            this.txtCusId.BorderRadius = 3;
            this.txtCusId.BorderSize = 1;
            this.txtCusId.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCusId.ForeColor = System.Drawing.Color.White;
            this.txtCusId.Icon = null;
            this.txtCusId.IconSize = new System.Drawing.Size(20, 20);
            this.txtCusId.Location = new System.Drawing.Point(80, 142);
            this.txtCusId.Multiline = false;
            this.txtCusId.Name = "txtCusId";
            this.txtCusId.PasswordChar = false;
            this.txtCusId.PlaceholderColor = System.Drawing.Color.Transparent;
            this.txtCusId.PlaceholderText = "";
            this.txtCusId.Size = new System.Drawing.Size(263, 35);
            this.txtCusId.TabIndex = 3;
            this.txtCusId.Text = "0";
            this.txtCusId.Texts = "";
            this.txtCusId.UnderlinedStyle = false;
            // 
            // dataGridRoom
            // 
            this.dataGridRoom.AllowUserToAddRows = false;
            this.dataGridRoom.AllowUserToDeleteRows = false;
            this.dataGridRoom.AllowUserToResizeColumns = false;
            this.dataGridRoom.AllowUserToResizeRows = false;
            this.dataGridRoom.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridRoom.BackgroundColor = System.Drawing.Color.White;
            this.dataGridRoom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridRoom.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridRoom.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridRoom.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridRoom.ColumnHeadersHeight = 33;
            this.dataGridRoom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridRoom.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridRoom.EnableHeadersVisualStyles = false;
            this.dataGridRoom.GridColor = System.Drawing.Color.Gainsboro;
            this.dataGridRoom.Location = new System.Drawing.Point(3, 3);
            this.dataGridRoom.MultiSelect = false;
            this.dataGridRoom.Name = "dataGridRoom";
            this.dataGridRoom.ReadOnly = true;
            this.dataGridRoom.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridRoom.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridRoom.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridRoom.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridRoom.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridRoom.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridRoom.Size = new System.Drawing.Size(1168, 576);
            this.dataGridRoom.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.dataGridRoom);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1174, 582);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Customer List";
            // 
            // sataPanel1
            // 
            this.sataPanel1.BackColor = System.Drawing.Color.White;
            this.sataPanel1.BackColor2 = System.Drawing.Color.White;
            this.sataPanel1.BorderColor = System.Drawing.Color.Black;
            borderRadius1.BottomLeft = 10;
            borderRadius1.BottomRight = 10;
            borderRadius1.TopLeft = 10;
            borderRadius1.TopRight = 10;
            this.sataPanel1.BorderRadius = borderRadius1;
            this.sataPanel1.BorderThickness = 0;
            this.sataPanel1.Controls.Add(this.materialTabControl1);
            this.sataPanel1.Location = new System.Drawing.Point(32, 156);
            this.sataPanel1.Name = "sataPanel1";
            this.sataPanel1.Size = new System.Drawing.Size(1182, 611);
            this.sataPanel1.TabIndex = 13;
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialTabControl1.Location = new System.Drawing.Point(0, 0);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(1182, 611);
            this.materialTabControl1.TabIndex = 8;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(27, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "Rooms";
            // 
            // sataPictureBox1
            // 
            this.sataPictureBox1.BorderCapStyle = System.Drawing.Drawing2D.DashCap.Flat;
            this.sataPictureBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(79)))), ((int)(((byte)(165)))));
            this.sataPictureBox1.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(68)))), ((int)(((byte)(142)))));
            this.sataPictureBox1.BorderLineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.sataPictureBox1.BorderSize = 1;
            this.sataPictureBox1.GradientAngle = 50F;
            this.sataPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("sataPictureBox1.Image")));
            this.sataPictureBox1.Location = new System.Drawing.Point(1068, 16);
            this.sataPictureBox1.Name = "sataPictureBox1";
            this.sataPictureBox1.Size = new System.Drawing.Size(43, 43);
            this.sataPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sataPictureBox1.TabIndex = 2;
            this.sataPictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(1119, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Mark Manalo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(1121, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Admin";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.sataPictureBox1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1477, 72);
            this.panel2.TabIndex = 12;
            // 
            // UCRooms
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.btnRoomSearch);
            this.Controls.Add(this.txtRoomSearch);
            this.Controls.Add(this.btnRoomDelete);
            this.Controls.Add(this.btnRoomAddNew);
            this.Controls.Add(this.btnRoomEdit);
            this.Controls.Add(this.sataPanel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UCRooms";
            this.Size = new System.Drawing.Size(1477, 800);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRoom)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.sataPanel1.ResumeLayout(false);
            this.materialTabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sataPictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FrameworkTest.SATAButton btnRoomSearch;
        private SATATextBox txtRoomSearch;
        private FrameworkTest.SATAButton btnRoomDelete;
        private FrameworkTest.SATAButton btnRoomAddNew;
        private FrameworkTest.SATAButton btnRoomEdit;
        private System.Windows.Forms.TabPage tabPage2;
        private FrameworkTest.SATAButton btnRoomCancel;
        private FrameworkTest.SATAButton btnRoomSave;
        private SATATextBox txtAddress;
        private SATATextBox txtContact;
        private SATATextBox txtIdType;
        private SATATextBox txtName;
        private SATATextBox txtCusId;
        private System.Windows.Forms.DataGridView dataGridRoom;
        private System.Windows.Forms.TabPage tabPage1;
        private SATAUiFramework.SATAPanel sataPanel1;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label5;
        private SATAUiFramework.Controls.SATAPictureBox sataPictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
    }
}
