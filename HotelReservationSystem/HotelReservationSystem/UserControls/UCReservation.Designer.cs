namespace HotelReservationSystem.UserControls
{
    partial class UCReservation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCReservation));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            SATAUiFramework.BorderRadius borderRadius1 = new SATAUiFramework.BorderRadius();
            this.btnReservationSearch = new FrameworkTest.SATAButton();
            this.txtReservationSearch = new SATATextBox();
            this.btnReservationDelete = new FrameworkTest.SATAButton();
            this.btnReservationAddNew = new FrameworkTest.SATAButton();
            this.btnReservationEdit = new FrameworkTest.SATAButton();
            this.dataGridReservation = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.sataPanel1 = new SATAUiFramework.SATAPanel();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cbNumber = new MetroFramework.Controls.MetroComboBox();
            this.cbStatus = new MetroFramework.Controls.MetroComboBox();
            this.cbCusName = new MetroFramework.Controls.MetroComboBox();
            this.cbType = new MetroFramework.Controls.MetroComboBox();
            this.btnReservationSave = new FrameworkTest.SATAButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.txtRoomGuests = new SATATextBox();
            this.txtPrice = new SATATextBox();
            this.txtReservationId = new SATATextBox();
            this.dtCheckOut = new System.Windows.Forms.DateTimePicker();
            this.dtCheckIn = new System.Windows.Forms.DateTimePicker();
            this.btnReservationCancel = new FrameworkTest.SATAButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.sataPictureBox1 = new SATAUiFramework.Controls.SATAPictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReservation)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.sataPanel1.SuspendLayout();
            this.materialTabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sataPictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReservationSearch
            // 
            this.btnReservationSearch.ButtonText = "Search";
            this.btnReservationSearch.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnReservationSearch.CheckedForeColor = System.Drawing.Color.White;
            this.btnReservationSearch.CheckedImageTint = System.Drawing.Color.White;
            this.btnReservationSearch.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnReservationSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReservationSearch.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnReservationSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReservationSearch.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnReservationSearch.HoverForeColor = System.Drawing.Color.White;
            this.btnReservationSearch.HoverImage = null;
            this.btnReservationSearch.HoverImageTint = System.Drawing.Color.White;
            this.btnReservationSearch.HoverOutline = System.Drawing.Color.Empty;
            this.btnReservationSearch.Image = null;
            this.btnReservationSearch.ImageAutoCenter = true;
            this.btnReservationSearch.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnReservationSearch.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnReservationSearch.ImageTint = System.Drawing.Color.White;
            this.btnReservationSearch.IsToggleButton = false;
            this.btnReservationSearch.IsToggled = false;
            this.btnReservationSearch.Location = new System.Drawing.Point(414, 100);
            this.btnReservationSearch.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnReservationSearch.Name = "btnReservationSearch";
            this.btnReservationSearch.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReservationSearch.NormalForeColor = System.Drawing.Color.White;
            this.btnReservationSearch.NormalOutline = System.Drawing.Color.Empty;
            this.btnReservationSearch.OutlineThickness = 2F;
            this.btnReservationSearch.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReservationSearch.PressedForeColor = System.Drawing.Color.White;
            this.btnReservationSearch.PressedImageTint = System.Drawing.Color.White;
            this.btnReservationSearch.PressedOutline = System.Drawing.Color.Empty;
            this.btnReservationSearch.Rounding = new System.Windows.Forms.Padding(5);
            this.btnReservationSearch.Size = new System.Drawing.Size(163, 39);
            this.btnReservationSearch.TabIndex = 9;
            this.btnReservationSearch.TextAutoCenter = true;
            this.btnReservationSearch.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // txtReservationSearch
            // 
            this.txtReservationSearch.BackColor = System.Drawing.Color.White;
            this.txtReservationSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtReservationSearch.BorderColor = System.Drawing.Color.Gainsboro;
            this.txtReservationSearch.BorderFocusColor = System.Drawing.Color.Gainsboro;
            this.txtReservationSearch.BorderRadius = 4;
            this.txtReservationSearch.BorderSize = 1;
            this.txtReservationSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReservationSearch.ForeColor = System.Drawing.Color.Black;
            this.txtReservationSearch.Icon = ((System.Drawing.Image)(resources.GetObject("txtReservationSearch.Icon")));
            this.txtReservationSearch.IconSize = new System.Drawing.Size(20, 20);
            this.txtReservationSearch.Location = new System.Drawing.Point(32, 100);
            this.txtReservationSearch.Multiline = false;
            this.txtReservationSearch.Name = "txtReservationSearch";
            this.txtReservationSearch.PasswordChar = false;
            this.txtReservationSearch.PlaceholderColor = System.Drawing.Color.LightGray;
            this.txtReservationSearch.PlaceholderText = "Search Customer";
            this.txtReservationSearch.Size = new System.Drawing.Size(374, 39);
            this.txtReservationSearch.TabIndex = 8;
            this.txtReservationSearch.Texts = "";
            this.txtReservationSearch.UnderlinedStyle = false;
            // 
            // btnReservationDelete
            // 
            this.btnReservationDelete.ButtonText = "";
            this.btnReservationDelete.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnReservationDelete.CheckedForeColor = System.Drawing.Color.White;
            this.btnReservationDelete.CheckedImageTint = System.Drawing.Color.White;
            this.btnReservationDelete.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnReservationDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReservationDelete.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnReservationDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReservationDelete.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(35)))), ((int)(((byte)(60)))));
            this.btnReservationDelete.HoverForeColor = System.Drawing.Color.White;
            this.btnReservationDelete.HoverImage = null;
            this.btnReservationDelete.HoverImageTint = System.Drawing.Color.White;
            this.btnReservationDelete.HoverOutline = System.Drawing.Color.Empty;
            this.btnReservationDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnReservationDelete.Image")));
            this.btnReservationDelete.ImageAutoCenter = true;
            this.btnReservationDelete.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnReservationDelete.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnReservationDelete.ImageTint = System.Drawing.Color.White;
            this.btnReservationDelete.IsToggleButton = false;
            this.btnReservationDelete.IsToggled = false;
            this.btnReservationDelete.Location = new System.Drawing.Point(1170, 100);
            this.btnReservationDelete.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnReservationDelete.Name = "btnReservationDelete";
            this.btnReservationDelete.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(104)))), ((int)(((byte)(107)))));
            this.btnReservationDelete.NormalForeColor = System.Drawing.Color.White;
            this.btnReservationDelete.NormalOutline = System.Drawing.Color.Empty;
            this.btnReservationDelete.OutlineThickness = 2F;
            this.btnReservationDelete.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(104)))), ((int)(((byte)(107)))));
            this.btnReservationDelete.PressedForeColor = System.Drawing.Color.White;
            this.btnReservationDelete.PressedImageTint = System.Drawing.Color.White;
            this.btnReservationDelete.PressedOutline = System.Drawing.Color.Empty;
            this.btnReservationDelete.Rounding = new System.Windows.Forms.Padding(5);
            this.btnReservationDelete.Size = new System.Drawing.Size(44, 39);
            this.btnReservationDelete.TabIndex = 10;
            this.btnReservationDelete.TextAutoCenter = false;
            this.btnReservationDelete.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // btnReservationAddNew
            // 
            this.btnReservationAddNew.ButtonText = "Add New";
            this.btnReservationAddNew.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnReservationAddNew.CheckedForeColor = System.Drawing.Color.White;
            this.btnReservationAddNew.CheckedImageTint = System.Drawing.Color.White;
            this.btnReservationAddNew.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnReservationAddNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReservationAddNew.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnReservationAddNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReservationAddNew.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnReservationAddNew.HoverForeColor = System.Drawing.Color.White;
            this.btnReservationAddNew.HoverImage = null;
            this.btnReservationAddNew.HoverImageTint = System.Drawing.Color.White;
            this.btnReservationAddNew.HoverOutline = System.Drawing.Color.Empty;
            this.btnReservationAddNew.Image = ((System.Drawing.Image)(resources.GetObject("btnReservationAddNew.Image")));
            this.btnReservationAddNew.ImageAutoCenter = true;
            this.btnReservationAddNew.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnReservationAddNew.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnReservationAddNew.ImageTint = System.Drawing.Color.White;
            this.btnReservationAddNew.IsToggleButton = false;
            this.btnReservationAddNew.IsToggled = false;
            this.btnReservationAddNew.Location = new System.Drawing.Point(984, 100);
            this.btnReservationAddNew.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnReservationAddNew.Name = "btnReservationAddNew";
            this.btnReservationAddNew.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReservationAddNew.NormalForeColor = System.Drawing.Color.White;
            this.btnReservationAddNew.NormalOutline = System.Drawing.Color.Empty;
            this.btnReservationAddNew.OutlineThickness = 2F;
            this.btnReservationAddNew.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReservationAddNew.PressedForeColor = System.Drawing.Color.White;
            this.btnReservationAddNew.PressedImageTint = System.Drawing.Color.White;
            this.btnReservationAddNew.PressedOutline = System.Drawing.Color.Empty;
            this.btnReservationAddNew.Rounding = new System.Windows.Forms.Padding(5);
            this.btnReservationAddNew.Size = new System.Drawing.Size(122, 39);
            this.btnReservationAddNew.TabIndex = 14;
            this.btnReservationAddNew.TextAutoCenter = true;
            this.btnReservationAddNew.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // btnReservationEdit
            // 
            this.btnReservationEdit.ButtonText = "";
            this.btnReservationEdit.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnReservationEdit.CheckedForeColor = System.Drawing.Color.White;
            this.btnReservationEdit.CheckedImageTint = System.Drawing.Color.White;
            this.btnReservationEdit.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnReservationEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReservationEdit.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnReservationEdit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReservationEdit.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnReservationEdit.HoverForeColor = System.Drawing.Color.White;
            this.btnReservationEdit.HoverImage = null;
            this.btnReservationEdit.HoverImageTint = System.Drawing.Color.White;
            this.btnReservationEdit.HoverOutline = System.Drawing.Color.Empty;
            this.btnReservationEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnReservationEdit.Image")));
            this.btnReservationEdit.ImageAutoCenter = true;
            this.btnReservationEdit.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnReservationEdit.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnReservationEdit.ImageTint = System.Drawing.Color.White;
            this.btnReservationEdit.IsToggleButton = false;
            this.btnReservationEdit.IsToggled = false;
            this.btnReservationEdit.Location = new System.Drawing.Point(1116, 100);
            this.btnReservationEdit.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnReservationEdit.Name = "btnReservationEdit";
            this.btnReservationEdit.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReservationEdit.NormalForeColor = System.Drawing.Color.White;
            this.btnReservationEdit.NormalOutline = System.Drawing.Color.Empty;
            this.btnReservationEdit.OutlineThickness = 2F;
            this.btnReservationEdit.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReservationEdit.PressedForeColor = System.Drawing.Color.White;
            this.btnReservationEdit.PressedImageTint = System.Drawing.Color.White;
            this.btnReservationEdit.PressedOutline = System.Drawing.Color.Empty;
            this.btnReservationEdit.Rounding = new System.Windows.Forms.Padding(5);
            this.btnReservationEdit.Size = new System.Drawing.Size(44, 39);
            this.btnReservationEdit.TabIndex = 11;
            this.btnReservationEdit.TextAutoCenter = false;
            this.btnReservationEdit.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // dataGridReservation
            // 
            this.dataGridReservation.AllowUserToAddRows = false;
            this.dataGridReservation.AllowUserToDeleteRows = false;
            this.dataGridReservation.AllowUserToResizeColumns = false;
            this.dataGridReservation.AllowUserToResizeRows = false;
            this.dataGridReservation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridReservation.BackgroundColor = System.Drawing.Color.White;
            this.dataGridReservation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridReservation.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridReservation.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridReservation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridReservation.ColumnHeadersHeight = 33;
            this.dataGridReservation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridReservation.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridReservation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridReservation.EnableHeadersVisualStyles = false;
            this.dataGridReservation.GridColor = System.Drawing.Color.Gainsboro;
            this.dataGridReservation.Location = new System.Drawing.Point(3, 3);
            this.dataGridReservation.MultiSelect = false;
            this.dataGridReservation.Name = "dataGridReservation";
            this.dataGridReservation.ReadOnly = true;
            this.dataGridReservation.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridReservation.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridReservation.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridReservation.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridReservation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridReservation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridReservation.Size = new System.Drawing.Size(1168, 576);
            this.dataGridReservation.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.dataGridReservation);
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
            this.materialTabControl1.Multiline = true;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(1182, 611);
            this.materialTabControl1.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.cbNumber);
            this.tabPage2.Controls.Add(this.cbStatus);
            this.tabPage2.Controls.Add(this.cbCusName);
            this.tabPage2.Controls.Add(this.cbType);
            this.tabPage2.Controls.Add(this.btnReservationSave);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.label36);
            this.tabPage2.Controls.Add(this.txtRoomGuests);
            this.tabPage2.Controls.Add(this.txtPrice);
            this.tabPage2.Controls.Add(this.txtReservationId);
            this.tabPage2.Controls.Add(this.dtCheckOut);
            this.tabPage2.Controls.Add(this.dtCheckIn);
            this.tabPage2.Controls.Add(this.btnReservationCancel);
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1174, 582);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Customer Details";
            // 
            // cbNumber
            // 
            this.cbNumber.FormattingEnabled = true;
            this.cbNumber.ItemHeight = 23;
            this.cbNumber.Location = new System.Drawing.Point(57, 253);
            this.cbNumber.Name = "cbNumber";
            this.cbNumber.Size = new System.Drawing.Size(206, 29);
            this.cbNumber.TabIndex = 27;
            this.cbNumber.SelectedIndexChanged += new System.EventHandler(this.cbNumber_SelectedIndexChanged);
            // 
            // cbStatus
            // 
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.ItemHeight = 23;
            this.cbStatus.Location = new System.Drawing.Point(294, 253);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(206, 29);
            this.cbStatus.TabIndex = 27;
            // 
            // cbCusName
            // 
            this.cbCusName.FormattingEnabled = true;
            this.cbCusName.ItemHeight = 23;
            this.cbCusName.Location = new System.Drawing.Point(57, 188);
            this.cbCusName.Name = "cbCusName";
            this.cbCusName.Size = new System.Drawing.Size(206, 29);
            this.cbCusName.TabIndex = 27;
            // 
            // cbType
            // 
            this.cbType.FormattingEnabled = true;
            this.cbType.ItemHeight = 23;
            this.cbType.Location = new System.Drawing.Point(294, 188);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(206, 29);
            this.cbType.TabIndex = 27;
            // 
            // btnReservationSave
            // 
            this.btnReservationSave.ButtonText = "Save";
            this.btnReservationSave.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnReservationSave.CheckedForeColor = System.Drawing.Color.White;
            this.btnReservationSave.CheckedImageTint = System.Drawing.Color.White;
            this.btnReservationSave.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnReservationSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReservationSave.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnReservationSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReservationSave.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnReservationSave.HoverForeColor = System.Drawing.Color.White;
            this.btnReservationSave.HoverImage = null;
            this.btnReservationSave.HoverImageTint = System.Drawing.Color.White;
            this.btnReservationSave.HoverOutline = System.Drawing.Color.Empty;
            this.btnReservationSave.Image = null;
            this.btnReservationSave.ImageAutoCenter = true;
            this.btnReservationSave.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnReservationSave.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnReservationSave.ImageTint = System.Drawing.Color.White;
            this.btnReservationSave.IsToggleButton = false;
            this.btnReservationSave.IsToggled = false;
            this.btnReservationSave.Location = new System.Drawing.Point(337, 452);
            this.btnReservationSave.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnReservationSave.Name = "btnReservationSave";
            this.btnReservationSave.NormalBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReservationSave.NormalForeColor = System.Drawing.Color.White;
            this.btnReservationSave.NormalOutline = System.Drawing.Color.Empty;
            this.btnReservationSave.OutlineThickness = 2F;
            this.btnReservationSave.PressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReservationSave.PressedForeColor = System.Drawing.Color.White;
            this.btnReservationSave.PressedImageTint = System.Drawing.Color.White;
            this.btnReservationSave.PressedOutline = System.Drawing.Color.Empty;
            this.btnReservationSave.Rounding = new System.Windows.Forms.Padding(5);
            this.btnReservationSave.Size = new System.Drawing.Size(163, 35);
            this.btnReservationSave.TabIndex = 26;
            this.btnReservationSave.TextAutoCenter = true;
            this.btnReservationSave.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(290, 290);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 20);
            this.label8.TabIndex = 25;
            this.label8.Text = "Check-out Date";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(290, 359);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 20);
            this.label9.TabIndex = 25;
            this.label9.Text = "Total Price/Night";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(53, 290);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 20);
            this.label7.TabIndex = 25;
            this.label7.Text = "Check-in Date";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DimGray;
            this.label11.Location = new System.Drawing.Point(53, 225);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 20);
            this.label11.TabIndex = 25;
            this.label11.Text = "Room Number";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DimGray;
            this.label10.Location = new System.Drawing.Point(290, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 20);
            this.label10.TabIndex = 25;
            this.label10.Text = "Room Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(53, 359);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 20);
            this.label6.TabIndex = 25;
            this.label6.Text = "Room Guests";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(290, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 25;
            this.label2.Text = "Room Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(53, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 25;
            this.label1.Text = "Customer Name";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.DimGray;
            this.label36.Location = new System.Drawing.Point(53, 91);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(107, 20);
            this.label36.TabIndex = 25;
            this.label36.Text = "Reservation Id";
            // 
            // txtRoomGuests
            // 
            this.txtRoomGuests.BackColor = System.Drawing.Color.White;
            this.txtRoomGuests.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtRoomGuests.BorderColor = System.Drawing.Color.Gray;
            this.txtRoomGuests.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtRoomGuests.BorderRadius = 3;
            this.txtRoomGuests.BorderSize = 1;
            this.txtRoomGuests.Enabled = false;
            this.txtRoomGuests.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoomGuests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtRoomGuests.Icon = null;
            this.txtRoomGuests.IconSize = new System.Drawing.Size(20, 20);
            this.txtRoomGuests.Location = new System.Drawing.Point(57, 387);
            this.txtRoomGuests.Multiline = false;
            this.txtRoomGuests.Name = "txtRoomGuests";
            this.txtRoomGuests.PasswordChar = false;
            this.txtRoomGuests.PlaceholderColor = System.Drawing.Color.Transparent;
            this.txtRoomGuests.PlaceholderText = "";
            this.txtRoomGuests.Size = new System.Drawing.Size(206, 35);
            this.txtRoomGuests.TabIndex = 18;
            this.txtRoomGuests.Texts = "";
            this.txtRoomGuests.UnderlinedStyle = false;
            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.Color.White;
            this.txtPrice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtPrice.BorderColor = System.Drawing.Color.Gray;
            this.txtPrice.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtPrice.BorderRadius = 3;
            this.txtPrice.BorderSize = 1;
            this.txtPrice.Enabled = false;
            this.txtPrice.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtPrice.Icon = null;
            this.txtPrice.IconSize = new System.Drawing.Size(20, 20);
            this.txtPrice.Location = new System.Drawing.Point(294, 387);
            this.txtPrice.Multiline = false;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.PasswordChar = false;
            this.txtPrice.PlaceholderColor = System.Drawing.Color.Transparent;
            this.txtPrice.PlaceholderText = "";
            this.txtPrice.Size = new System.Drawing.Size(206, 35);
            this.txtPrice.TabIndex = 20;
            this.txtPrice.Text = "0";
            this.txtPrice.Texts = "";
            this.txtPrice.UnderlinedStyle = false;
            // 
            // txtReservationId
            // 
            this.txtReservationId.BackColor = System.Drawing.Color.White;
            this.txtReservationId.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txtReservationId.BorderColor = System.Drawing.Color.Gray;
            this.txtReservationId.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtReservationId.BorderRadius = 3;
            this.txtReservationId.BorderSize = 1;
            this.txtReservationId.Enabled = false;
            this.txtReservationId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReservationId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtReservationId.Icon = null;
            this.txtReservationId.IconSize = new System.Drawing.Size(20, 20);
            this.txtReservationId.Location = new System.Drawing.Point(57, 119);
            this.txtReservationId.Multiline = false;
            this.txtReservationId.Name = "txtReservationId";
            this.txtReservationId.PasswordChar = false;
            this.txtReservationId.PlaceholderColor = System.Drawing.Color.Transparent;
            this.txtReservationId.PlaceholderText = "";
            this.txtReservationId.Size = new System.Drawing.Size(206, 33);
            this.txtReservationId.TabIndex = 22;
            this.txtReservationId.Text = "0";
            this.txtReservationId.Texts = "";
            this.txtReservationId.UnderlinedStyle = false;
            // 
            // dtCheckOut
            // 
            this.dtCheckOut.CalendarTitleForeColor = System.Drawing.Color.Black;
            this.dtCheckOut.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtCheckOut.Location = new System.Drawing.Point(294, 318);
            this.dtCheckOut.Name = "dtCheckOut";
            this.dtCheckOut.Size = new System.Drawing.Size(206, 33);
            this.dtCheckOut.TabIndex = 17;
            // 
            // dtCheckIn
            // 
            this.dtCheckIn.CalendarTitleForeColor = System.Drawing.Color.Black;
            this.dtCheckIn.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtCheckIn.Location = new System.Drawing.Point(57, 318);
            this.dtCheckIn.Name = "dtCheckIn";
            this.dtCheckIn.Size = new System.Drawing.Size(206, 33);
            this.dtCheckIn.TabIndex = 17;
            // 
            // btnReservationCancel
            // 
            this.btnReservationCancel.ButtonText = "Cancel";
            this.btnReservationCancel.CheckedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnReservationCancel.CheckedForeColor = System.Drawing.Color.White;
            this.btnReservationCancel.CheckedImageTint = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReservationCancel.CheckedOutline = System.Drawing.Color.Transparent;
            this.btnReservationCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReservationCancel.CustomDialogResult = System.Windows.Forms.DialogResult.None;
            this.btnReservationCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReservationCancel.HoverBackground = System.Drawing.Color.White;
            this.btnReservationCancel.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReservationCancel.HoverImage = null;
            this.btnReservationCancel.HoverImageTint = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReservationCancel.HoverOutline = System.Drawing.Color.Empty;
            this.btnReservationCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnReservationCancel.Image")));
            this.btnReservationCancel.ImageAutoCenter = true;
            this.btnReservationCancel.ImageExpand = new System.Drawing.Point(0, 0);
            this.btnReservationCancel.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnReservationCancel.ImageTint = System.Drawing.Color.White;
            this.btnReservationCancel.IsToggleButton = false;
            this.btnReservationCancel.IsToggled = false;
            this.btnReservationCancel.Location = new System.Drawing.Point(38, 25);
            this.btnReservationCancel.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnReservationCancel.Name = "btnReservationCancel";
            this.btnReservationCancel.NormalBackground = System.Drawing.Color.Transparent;
            this.btnReservationCancel.NormalForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReservationCancel.NormalOutline = System.Drawing.Color.Empty;
            this.btnReservationCancel.OutlineThickness = 2F;
            this.btnReservationCancel.PressedBackground = System.Drawing.Color.Transparent;
            this.btnReservationCancel.PressedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReservationCancel.PressedImageTint = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReservationCancel.PressedOutline = System.Drawing.Color.Empty;
            this.btnReservationCancel.Rounding = new System.Windows.Forms.Padding(5);
            this.btnReservationCancel.Size = new System.Drawing.Size(115, 35);
            this.btnReservationCancel.TabIndex = 12;
            this.btnReservationCancel.TextAutoCenter = true;
            this.btnReservationCancel.TextOffset = new System.Drawing.Point(0, 0);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(439, -22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1028, 630);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
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
            this.label5.Size = new System.Drawing.Size(164, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "Reservation List";
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
            // UCReservation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.btnReservationSearch);
            this.Controls.Add(this.txtReservationSearch);
            this.Controls.Add(this.btnReservationDelete);
            this.Controls.Add(this.btnReservationAddNew);
            this.Controls.Add(this.btnReservationEdit);
            this.Controls.Add(this.sataPanel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UCReservation";
            this.Size = new System.Drawing.Size(1477, 800);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReservation)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.sataPanel1.ResumeLayout(false);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sataPictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FrameworkTest.SATAButton btnReservationSearch;
        private SATATextBox txtReservationSearch;
        private FrameworkTest.SATAButton btnReservationDelete;
        private FrameworkTest.SATAButton btnReservationAddNew;
        private FrameworkTest.SATAButton btnReservationEdit;
        private System.Windows.Forms.DataGridView dataGridReservation;
        private System.Windows.Forms.TabPage tabPage1;
        private SATAUiFramework.SATAPanel sataPanel1;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label5;
        private SATAUiFramework.Controls.SATAPictureBox sataPictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabPage tabPage2;
        private FrameworkTest.SATAButton btnReservationCancel;
        private System.Windows.Forms.DateTimePicker dtCheckIn;
        private System.Windows.Forms.DateTimePicker dtCheckOut;
        private SATATextBox txtRoomGuests;
        private SATATextBox txtPrice;
        private SATATextBox txtReservationId;
        private System.Windows.Forms.Label label36;
        private FrameworkTest.SATAButton btnReservationSave;
        private MetroFramework.Controls.MetroComboBox cbNumber;
        private MetroFramework.Controls.MetroComboBox cbStatus;
        private MetroFramework.Controls.MetroComboBox cbCusName;
        private MetroFramework.Controls.MetroComboBox cbType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
