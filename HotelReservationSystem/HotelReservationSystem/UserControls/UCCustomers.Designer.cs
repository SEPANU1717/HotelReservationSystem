namespace HotelReservationSystem.UserControls
{
    partial class UCCustomers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCustomers));
            this.panel2 = new System.Windows.Forms.Panel();
            this.sataPictureBox1 = new SATAUiFramework.Controls.SATAPictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnDelete = new FrameworkTest.SATAButton();
            this.btnEdit = new FrameworkTest.SATAButton();
            this.btnAddNew = new FrameworkTest.SATAButton();
            this.btnSearch = new FrameworkTest.SATAButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtSearch = new SATATextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCancel = new FrameworkTest.SATAButton();
            this.btnAdd = new FrameworkTest.SATAButton();
            this.txtAddress = new SATATextBox();
            this.txtContact = new SATATextBox();
            this.txtIdType = new SATATextBox();
            this.txtName = new SATATextBox();
            this.txtCusId = new SATATextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sataPictureBox1)).BeginInit();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
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
            this.panel2.TabIndex = 5;
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(27, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "Customers";
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(32, 112);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(1182, 615);
            this.materialTabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.tabPage1.Controls.Add(this.btnDelete);
            this.tabPage1.Controls.Add(this.btnEdit);
            this.tabPage1.Controls.Add(this.btnAddNew);
            this.tabPage1.Controls.Add(this.btnSearch);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.txtSearch);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1174, 586);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Customer List";
            // 
            // btnDelete
            // 
            this.btnDelete.ButtonText = "Delete";
            this.btnDelete.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnDelete.CheckedForeColor = System.Drawing.Color.White;
            this.btnDelete.CheckedImageTint = System.Drawing.Color.White;
            this.btnDelete.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnDelete.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnDelete.HoverForeColor = System.Drawing.Color.White;
            this.btnDelete.HoverImage = null;
            this.btnDelete.HoverImageTint = System.Drawing.Color.White;
            this.btnDelete.HoverOutline = System.Drawing.Color.Empty;
            this.btnDelete.Image = null;
            this.btnDelete.ImageAutoCenter = true;
            this.btnDelete.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnDelete.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnDelete.ImageTint = System.Drawing.Color.White;
            this.btnDelete.IsToggleButton = false;
            this.btnDelete.IsToggled = false;
            this.btnDelete.Location = new System.Drawing.Point(968, 201);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnDelete.NormalForeColor = System.Drawing.Color.White;
            this.btnDelete.NormalOutline = System.Drawing.Color.Empty;
            this.btnDelete.OutlineThickness = 2F;
            this.btnDelete.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnDelete.PressedForeColor = System.Drawing.Color.White;
            this.btnDelete.PressedImageTint = System.Drawing.Color.White;
            this.btnDelete.PressedOutline = System.Drawing.Color.Empty;
            this.btnDelete.Rounding = new System.Windows.Forms.Padding(5);
            this.btnDelete.Size = new System.Drawing.Size(163, 35);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.TextAutoCenter = true;
            this.btnDelete.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // btnEdit
            // 
            this.btnEdit.ButtonText = "Edit";
            this.btnEdit.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnEdit.CheckedForeColor = System.Drawing.Color.White;
            this.btnEdit.CheckedImageTint = System.Drawing.Color.White;
            this.btnEdit.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnEdit.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnEdit.HoverForeColor = System.Drawing.Color.White;
            this.btnEdit.HoverImage = null;
            this.btnEdit.HoverImageTint = System.Drawing.Color.White;
            this.btnEdit.HoverOutline = System.Drawing.Color.Empty;
            this.btnEdit.Image = null;
            this.btnEdit.ImageAutoCenter = true;
            this.btnEdit.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnEdit.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnEdit.ImageTint = System.Drawing.Color.White;
            this.btnEdit.IsToggleButton = false;
            this.btnEdit.IsToggled = false;
            this.btnEdit.Location = new System.Drawing.Point(968, 158);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnEdit.NormalForeColor = System.Drawing.Color.White;
            this.btnEdit.NormalOutline = System.Drawing.Color.Empty;
            this.btnEdit.OutlineThickness = 2F;
            this.btnEdit.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnEdit.PressedForeColor = System.Drawing.Color.White;
            this.btnEdit.PressedImageTint = System.Drawing.Color.White;
            this.btnEdit.PressedOutline = System.Drawing.Color.Empty;
            this.btnEdit.Rounding = new System.Windows.Forms.Padding(5);
            this.btnEdit.Size = new System.Drawing.Size(163, 35);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.TextAutoCenter = true;
            this.btnEdit.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // btnAddNew
            // 
            this.btnAddNew.ButtonText = "Add New";
            this.btnAddNew.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnAddNew.CheckedForeColor = System.Drawing.Color.White;
            this.btnAddNew.CheckedImageTint = System.Drawing.Color.White;
            this.btnAddNew.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnAddNew.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAddNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNew.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnAddNew.HoverForeColor = System.Drawing.Color.White;
            this.btnAddNew.HoverImage = null;
            this.btnAddNew.HoverImageTint = System.Drawing.Color.White;
            this.btnAddNew.HoverOutline = System.Drawing.Color.Empty;
            this.btnAddNew.Image = null;
            this.btnAddNew.ImageAutoCenter = true;
            this.btnAddNew.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnAddNew.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnAddNew.ImageTint = System.Drawing.Color.White;
            this.btnAddNew.IsToggleButton = false;
            this.btnAddNew.IsToggled = false;
            this.btnAddNew.Location = new System.Drawing.Point(968, 115);
            this.btnAddNew.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAddNew.NormalForeColor = System.Drawing.Color.White;
            this.btnAddNew.NormalOutline = System.Drawing.Color.Empty;
            this.btnAddNew.OutlineThickness = 2F;
            this.btnAddNew.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAddNew.PressedForeColor = System.Drawing.Color.White;
            this.btnAddNew.PressedImageTint = System.Drawing.Color.White;
            this.btnAddNew.PressedOutline = System.Drawing.Color.Empty;
            this.btnAddNew.Rounding = new System.Windows.Forms.Padding(5);
            this.btnAddNew.Size = new System.Drawing.Size(163, 35);
            this.btnAddNew.TabIndex = 4;
            this.btnAddNew.TextAutoCenter = true;
            this.btnAddNew.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.ButtonText = "Search";
            this.btnSearch.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnSearch.CheckedForeColor = System.Drawing.Color.White;
            this.btnSearch.CheckedImageTint = System.Drawing.Color.White;
            this.btnSearch.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnSearch.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSearch.HoverForeColor = System.Drawing.Color.White;
            this.btnSearch.HoverImage = null;
            this.btnSearch.HoverImageTint = System.Drawing.Color.White;
            this.btnSearch.HoverOutline = System.Drawing.Color.Empty;
            this.btnSearch.Image = null;
            this.btnSearch.ImageAutoCenter = true;
            this.btnSearch.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnSearch.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnSearch.ImageTint = System.Drawing.Color.White;
            this.btnSearch.IsToggleButton = false;
            this.btnSearch.IsToggled = false;
            this.btnSearch.Location = new System.Drawing.Point(792, 64);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSearch.NormalForeColor = System.Drawing.Color.White;
            this.btnSearch.NormalOutline = System.Drawing.Color.Empty;
            this.btnSearch.OutlineThickness = 2F;
            this.btnSearch.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSearch.PressedForeColor = System.Drawing.Color.White;
            this.btnSearch.PressedImageTint = System.Drawing.Color.White;
            this.btnSearch.PressedOutline = System.Drawing.Color.Empty;
            this.btnSearch.Rounding = new System.Windows.Forms.Padding(5);
            this.btnSearch.Size = new System.Drawing.Size(163, 35);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.TextAutoCenter = true;
            this.btnSearch.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(27, 115);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(928, 444);
            this.dataGridView1.TabIndex = 3;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtSearch.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtSearch.BorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtSearch.BorderRadius = 4;
            this.txtSearch.BorderSize = 1;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Icon = null;
            this.txtSearch.IconSize = new System.Drawing.Size(20, 20);
            this.txtSearch.Location = new System.Drawing.Point(27, 64);
            this.txtSearch.Multiline = false;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = false;
            this.txtSearch.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtSearch.PlaceholderText = "";
            this.txtSearch.Size = new System.Drawing.Size(754, 35);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Texts = "";
            this.txtSearch.UnderlinedStyle = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Customer";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.tabPage2.Controls.Add(this.btnCancel);
            this.tabPage2.Controls.Add(this.btnAdd);
            this.tabPage2.Controls.Add(this.txtAddress);
            this.tabPage2.Controls.Add(this.txtContact);
            this.tabPage2.Controls.Add(this.txtIdType);
            this.tabPage2.Controls.Add(this.txtName);
            this.tabPage2.Controls.Add(this.txtCusId);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1174, 589);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Customer Details";
            // 
            // btnCancel
            // 
            this.btnCancel.ButtonText = "Cancel";
            this.btnCancel.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnCancel.CheckedForeColor = System.Drawing.Color.White;
            this.btnCancel.CheckedImageTint = System.Drawing.Color.White;
            this.btnCancel.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnCancel.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
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
            this.btnCancel.Location = new System.Drawing.Point(343, 389);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnCancel.NormalForeColor = System.Drawing.Color.White;
            this.btnCancel.NormalOutline = System.Drawing.Color.Empty;
            this.btnCancel.OutlineThickness = 2F;
            this.btnCancel.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnCancel.PressedForeColor = System.Drawing.Color.White;
            this.btnCancel.PressedImageTint = System.Drawing.Color.White;
            this.btnCancel.PressedOutline = System.Drawing.Color.Empty;
            this.btnCancel.Rounding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(163, 35);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.TextAutoCenter = true;
            this.btnCancel.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // btnAdd
            // 
            this.btnAdd.ButtonText = "Add New";
            this.btnAdd.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnAdd.CheckedForeColor = System.Drawing.Color.White;
            this.btnAdd.CheckedImageTint = System.Drawing.Color.White;
            this.btnAdd.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnAdd.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnAdd.HoverForeColor = System.Drawing.Color.White;
            this.btnAdd.HoverImage = null;
            this.btnAdd.HoverImageTint = System.Drawing.Color.White;
            this.btnAdd.HoverOutline = System.Drawing.Color.Empty;
            this.btnAdd.Image = null;
            this.btnAdd.ImageAutoCenter = true;
            this.btnAdd.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnAdd.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnAdd.ImageTint = System.Drawing.Color.White;
            this.btnAdd.IsToggleButton = false;
            this.btnAdd.IsToggled = false;
            this.btnAdd.Location = new System.Drawing.Point(154, 389);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAdd.NormalForeColor = System.Drawing.Color.White;
            this.btnAdd.NormalOutline = System.Drawing.Color.Empty;
            this.btnAdd.OutlineThickness = 2F;
            this.btnAdd.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAdd.PressedForeColor = System.Drawing.Color.White;
            this.btnAdd.PressedImageTint = System.Drawing.Color.White;
            this.btnAdd.PressedOutline = System.Drawing.Color.Empty;
            this.btnAdd.Rounding = new System.Windows.Forms.Padding(5);
            this.btnAdd.Size = new System.Drawing.Size(163, 35);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.TextAutoCenter = true;
            this.btnAdd.TextOffset = new System.Drawing.Point(0, 0);
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
            this.txtCusId.Texts = "";
            this.txtCusId.UnderlinedStyle = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // UCCustomers
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.materialTabControl1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UCCustomers";
            this.Size = new System.Drawing.Size(1477, 800);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sataPictureBox1)).EndInit();
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private SATAUiFramework.Controls.SATAPictureBox sataPictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private FrameworkTest.SATAButton btnSearch;
        private FrameworkTest.SATAButton btnDelete;
        private FrameworkTest.SATAButton btnEdit;
        private FrameworkTest.SATAButton btnAddNew;
        private FrameworkTest.SATAButton btnCancel;
        private FrameworkTest.SATAButton btnAdd;
        private SATATextBox txtAddress;
        private SATATextBox txtContact;
        private SATATextBox txtIdType;
        private SATATextBox txtName;
        private SATATextBox txtCusId;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private SATATextBox txtSearch;
    }
}
