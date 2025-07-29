namespace HotelReservationSystem.UserControls
{
    partial class UCBilling
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
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridBilling = new System.Windows.Forms.DataGridView();
            this.txtBillingSearch = new System.Windows.Forms.TextBox();
            this.btnBillingSearch = new System.Windows.Forms.Button();
            this.btnBillingAddNew = new System.Windows.Forms.Button();
            this.btnBillingEdit = new System.Windows.Forms.Button();
            this.btnBillingDelete = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRoomType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRoomNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbReservationId = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBillId = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRoomTotal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRoomRate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNumberOfNights = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtCheckOutDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtCheckInDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtAdditionalChargesDescription = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtAdditionalCharges = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDiscountAmount = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTaxAmount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtServiceCharges = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtBalanceDue = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtAmountPaid = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.dtDueDate = new System.Windows.Forms.DateTimePicker();
            this.label22 = new System.Windows.Forms.Label();
            this.dtBillDate = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.cbPaymentStatus = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBilling)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
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
            this.materialTabControl1.Size = new System.Drawing.Size(1200, 800);
            this.materialTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnBillingDelete);
            this.tabPage1.Controls.Add(this.btnBillingEdit);
            this.tabPage1.Controls.Add(this.btnBillingAddNew);
            this.tabPage1.Controls.Add(this.btnBillingSearch);
            this.tabPage1.Controls.Add(this.txtBillingSearch);
            this.tabPage1.Controls.Add(this.dataGridBilling);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1192, 774);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Billing List";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridBilling
            // 
            this.dataGridBilling.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridBilling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridBilling.Location = new System.Drawing.Point(6, 80);
            this.dataGridBilling.Name = "dataGridBilling";
            this.dataGridBilling.ReadOnly = true;
            this.dataGridBilling.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridBilling.Size = new System.Drawing.Size(1180, 688);
            this.dataGridBilling.TabIndex = 0;
            // 
            // txtBillingSearch
            // 
            this.txtBillingSearch.Location = new System.Drawing.Point(6, 20);
            this.txtBillingSearch.Name = "txtBillingSearch";
            this.txtBillingSearch.Size = new System.Drawing.Size(300, 20);
            this.txtBillingSearch.TabIndex = 1;
            // 
            // btnBillingSearch
            // 
            this.btnBillingSearch.Location = new System.Drawing.Point(320, 18);
            this.btnBillingSearch.Name = "btnBillingSearch";
            this.btnBillingSearch.Size = new System.Drawing.Size(75, 23);
            this.btnBillingSearch.TabIndex = 2;
            this.btnBillingSearch.Text = "Search";
            this.btnBillingSearch.UseVisualStyleBackColor = true;
            // 
            // btnBillingAddNew
            // 
            this.btnBillingAddNew.Location = new System.Drawing.Point(6, 50);
            this.btnBillingAddNew.Name = "btnBillingAddNew";
            this.btnBillingAddNew.Size = new System.Drawing.Size(75, 23);
            this.btnBillingAddNew.TabIndex = 3;
            this.btnBillingAddNew.Text = "Add New";
            this.btnBillingAddNew.UseVisualStyleBackColor = true;
            // 
            // btnBillingEdit
            // 
            this.btnBillingEdit.Location = new System.Drawing.Point(87, 50);
            this.btnBillingEdit.Name = "btnBillingEdit";
            this.btnBillingEdit.Size = new System.Drawing.Size(75, 23);
            this.btnBillingEdit.TabIndex = 4;
            this.btnBillingEdit.Text = "Edit";
            this.btnBillingEdit.UseVisualStyleBackColor = true;
            // 
            // btnBillingDelete
            // 
            this.btnBillingDelete.Location = new System.Drawing.Point(168, 50);
            this.btnBillingDelete.Name = "btnBillingDelete";
            this.btnBillingDelete.Size = new System.Drawing.Size(75, 23);
            this.btnBillingDelete.TabIndex = 5;
            this.btnBillingDelete.Text = "Delete";
            this.btnBillingDelete.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.btnBillingCancel);
            this.tabPage2.Controls.Add(this.btnBillingSave);
            this.tabPage2.Controls.Add(this.btnProcessPayment);
            this.tabPage2.Controls.Add(this.btnPrintBill);
            this.tabPage2.Controls.Add(this.btnCalculate);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1192, 774);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Bill Details";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtRoomType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtRoomNumber);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCustomerName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbReservationId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBillId);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(580, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic Information";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Room Type";
            // 
            // txtRoomType
            // 
            this.txtRoomType.Location = new System.Drawing.Point(100, 85);
            this.txtRoomType.Name = "txtRoomType";
            this.txtRoomType.ReadOnly = true;
            this.txtRoomType.Size = new System.Drawing.Size(150, 20);
            this.txtRoomType.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(350, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Room Number";
            // 
            // txtRoomNumber
            // 
            this.txtRoomNumber.Location = new System.Drawing.Point(450, 55);
            this.txtRoomNumber.Name = "txtRoomNumber";
            this.txtRoomNumber.ReadOnly = true;
            this.txtRoomNumber.Size = new System.Drawing.Size(100, 20);
            this.txtRoomNumber.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Customer";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(100, 55);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(200, 20);
            this.txtCustomerName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Reservation";
            // 
            // cbReservationId
            // 
            this.cbReservationId.FormattingEnabled = true;
            this.cbReservationId.Location = new System.Drawing.Point(350, 25);
            this.cbReservationId.Name = "cbReservationId";
            this.cbReservationId.Size = new System.Drawing.Size(200, 21);
            this.cbReservationId.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bill ID";
            // 
            // txtBillId
            // 
            this.txtBillId.Location = new System.Drawing.Point(100, 25);
            this.txtBillId.Name = "txtBillId";
            this.txtBillId.ReadOnly = true;
            this.txtBillId.Size = new System.Drawing.Size(120, 20);
            this.txtBillId.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtRoomTotal);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtRoomRate);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtNumberOfNights);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.dtCheckOutDate);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.dtCheckInDate);
            this.groupBox2.Location = new System.Drawing.Point(6, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(580, 90);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stay Information";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(220, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Room Total";
            // 
            // txtRoomTotal
            // 
            this.txtRoomTotal.Location = new System.Drawing.Point(300, 55);
            this.txtRoomTotal.Name = "txtRoomTotal";
            this.txtRoomTotal.ReadOnly = true;
            this.txtRoomTotal.Size = new System.Drawing.Size(100, 20);
            this.txtRoomTotal.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Room Rate";
            // 
            // txtRoomRate
            // 
            this.txtRoomRate.Location = new System.Drawing.Point(100, 55);
            this.txtRoomRate.Name = "txtRoomRate";
            this.txtRoomRate.Size = new System.Drawing.Size(100, 20);
            this.txtRoomRate.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(420, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Nights";
            // 
            // txtNumberOfNights
            // 
            this.txtNumberOfNights.Location = new System.Drawing.Point(500, 25);
            this.txtNumberOfNights.Name = "txtNumberOfNights";
            this.txtNumberOfNights.Size = new System.Drawing.Size(60, 20);
            this.txtNumberOfNights.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(220, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Check-Out";
            // 
            // dtCheckOutDate
            // 
            this.dtCheckOutDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtCheckOutDate.Location = new System.Drawing.Point(300, 25);
            this.dtCheckOutDate.Name = "dtCheckOutDate";
            this.dtCheckOutDate.Size = new System.Drawing.Size(100, 20);
            this.dtCheckOutDate.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Check-In";
            // 
            // dtCheckInDate
            // 
            this.dtCheckInDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtCheckInDate.Location = new System.Drawing.Point(100, 25);
            this.dtCheckInDate.Name = "dtCheckInDate";
            this.dtCheckInDate.Size = new System.Drawing.Size(100, 20);
            this.dtCheckInDate.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txtAdditionalChargesDescription);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.txtAdditionalCharges);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtDiscountAmount);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtTaxAmount);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtServiceCharges);
            this.groupBox3.Location = new System.Drawing.Point(6, 235);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(580, 120);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Additional Charges";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(20, 88);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 13);
            this.label15.TabIndex = 9;
            this.label15.Text = "Description";
            // 
            // txtAdditionalChargesDescription
            // 
            this.txtAdditionalChargesDescription.Location = new System.Drawing.Point(100, 85);
            this.txtAdditionalChargesDescription.Name = "txtAdditionalChargesDescription";
            this.txtAdditionalChargesDescription.Size = new System.Drawing.Size(450, 20);
            this.txtAdditionalChargesDescription.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(220, 58);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 13);
            this.label14.TabIndex = 7;
            this.label14.Text = "Additional";
            // 
            // txtAdditionalCharges
            // 
            this.txtAdditionalCharges.Location = new System.Drawing.Point(300, 55);
            this.txtAdditionalCharges.Name = "txtAdditionalCharges";
            this.txtAdditionalCharges.Size = new System.Drawing.Size(100, 20);
            this.txtAdditionalCharges.TabIndex = 6;
            this.txtAdditionalCharges.Text = "0.00";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 58);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "Discount";
            // 
            // txtDiscountAmount
            // 
            this.txtDiscountAmount.Location = new System.Drawing.Point(100, 55);
            this.txtDiscountAmount.Name = "txtDiscountAmount";
            this.txtDiscountAmount.Size = new System.Drawing.Size(100, 20);
            this.txtDiscountAmount.TabIndex = 4;
            this.txtDiscountAmount.Text = "0.00";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(220, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Tax";
            // 
            // txtTaxAmount
            // 
            this.txtTaxAmount.Location = new System.Drawing.Point(300, 25);
            this.txtTaxAmount.Name = "txtTaxAmount";
            this.txtTaxAmount.Size = new System.Drawing.Size(100, 20);
            this.txtTaxAmount.TabIndex = 2;
            this.txtTaxAmount.Text = "0.00";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Service";
            // 
            // txtServiceCharges
            // 
            this.txtServiceCharges.Location = new System.Drawing.Point(100, 25);
            this.txtServiceCharges.Name = "txtServiceCharges";
            this.txtServiceCharges.Size = new System.Drawing.Size(100, 20);
            this.txtServiceCharges.TabIndex = 0;
            this.txtServiceCharges.Text = "0.00";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.txtBalanceDue);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.txtAmountPaid);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.txtTotalAmount);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.txtSubtotal);
            this.groupBox4.Location = new System.Drawing.Point(6, 365);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(580, 90);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Totals";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(220, 58);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(75, 13);
            this.label19.TabIndex = 7;
            this.label19.Text = "Balance Due";
            // 
            // txtBalanceDue
            // 
            this.txtBalanceDue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalanceDue.ForeColor = System.Drawing.Color.Red;
            this.txtBalanceDue.Location = new System.Drawing.Point(300, 55);
            this.txtBalanceDue.Name = "txtBalanceDue";
            this.txtBalanceDue.ReadOnly = true;
            this.txtBalanceDue.Size = new System.Drawing.Size(100, 20);
            this.txtBalanceDue.TabIndex = 6;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(20, 58);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 13);
            this.label18.TabIndex = 5;
            this.label18.Text = "Amount Paid";
            // 
            // txtAmountPaid
            // 
            this.txtAmountPaid.Location = new System.Drawing.Point(100, 55);
            this.txtAmountPaid.Name = "txtAmountPaid";
            this.txtAmountPaid.Size = new System.Drawing.Size(100, 20);
            this.txtAmountPaid.TabIndex = 4;
            this.txtAmountPaid.Text = "0.00";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(220, 28);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Total";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Location = new System.Drawing.Point(300, 25);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(100, 20);
            this.txtTotalAmount.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(20, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(46, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Subtotal";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Location = new System.Drawing.Point(100, 25);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(100, 20);
            this.txtSubtotal.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Controls.Add(this.txtNotes);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.dtDueDate);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.dtBillDate);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.cbPaymentStatus);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.cbPaymentMethod);
            this.groupBox5.Location = new System.Drawing.Point(6, 465);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(580, 120);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Payment Information";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(20, 88);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(35, 13);
            this.label24.TabIndex = 9;
            this.label24.Text = "Notes";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(100, 85);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(450, 20);
            this.txtNotes.TabIndex = 8;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(220, 58);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 13);
            this.label23.TabIndex = 7;
            this.label23.Text = "Due Date";
            // 
            // dtDueDate
            // 
            this.dtDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDueDate.Location = new System.Drawing.Point(300, 55);
            this.dtDueDate.Name = "dtDueDate";
            this.dtDueDate.Size = new System.Drawing.Size(100, 20);
            this.dtDueDate.TabIndex = 6;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(20, 58);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(48, 13);
            this.label22.TabIndex = 5;
            this.label22.Text = "Bill Date";
            // 
            // dtBillDate
            // 
            this.dtBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtBillDate.Location = new System.Drawing.Point(100, 55);
            this.dtBillDate.Name = "dtBillDate";
            this.dtBillDate.Size = new System.Drawing.Size(100, 20);
            this.dtBillDate.TabIndex = 4;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(270, 28);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(37, 13);
            this.label21.TabIndex = 3;
            this.label21.Text = "Status";
            // 
            // cbPaymentStatus
            // 
            this.cbPaymentStatus.FormattingEnabled = true;
            this.cbPaymentStatus.Location = new System.Drawing.Point(350, 25);
            this.cbPaymentStatus.Name = "cbPaymentStatus";
            this.cbPaymentStatus.Size = new System.Drawing.Size(150, 21);
            this.cbPaymentStatus.TabIndex = 2;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(20, 28);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(46, 13);
            this.label20.TabIndex = 1;
            this.label20.Text = "Method";
            // 
            // cbPaymentMethod
            // 
            this.cbPaymentMethod.FormattingEnabled = true;
            this.cbPaymentMethod.Location = new System.Drawing.Point(100, 25);
            this.cbPaymentMethod.Name = "cbPaymentMethod";
            this.cbPaymentMethod.Size = new System.Drawing.Size(150, 21);
            this.cbPaymentMethod.TabIndex = 0;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(620, 350);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(100, 30);
            this.btnCalculate.TabIndex = 5;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            // 
            // btnPrintBill
            // 
            this.btnPrintBill.Location = new System.Drawing.Point(620, 390);
            this.btnPrintBill.Name = "btnPrintBill";
            this.btnPrintBill.Size = new System.Drawing.Size(100, 30);
            this.btnPrintBill.TabIndex = 6;
            this.btnPrintBill.Text = "Print Bill";
            this.btnPrintBill.UseVisualStyleBackColor = true;
            // 
            // btnProcessPayment
            // 
            this.btnProcessPayment.Location = new System.Drawing.Point(620, 430);
            this.btnProcessPayment.Name = "btnProcessPayment";
            this.btnProcessPayment.Size = new System.Drawing.Size(100, 30);
            this.btnProcessPayment.TabIndex = 7;
            this.btnProcessPayment.Text = "Process Payment";
            this.btnProcessPayment.UseVisualStyleBackColor = true;
            // 
            // btnBillingSave
            // 
            this.btnBillingSave.Location = new System.Drawing.Point(620, 500);
            this.btnBillingSave.Name = "btnBillingSave";
            this.btnBillingSave.Size = new System.Drawing.Size(100, 30);
            this.btnBillingSave.TabIndex = 8;
            this.btnBillingSave.Text = "Save";
            this.btnBillingSave.UseVisualStyleBackColor = true;
            // 
            // btnBillingCancel
            // 
            this.btnBillingCancel.Location = new System.Drawing.Point(620, 540);
            this.btnBillingCancel.Name = "btnBillingCancel";
            this.btnBillingCancel.Size = new System.Drawing.Size(100, 30);
            this.btnBillingCancel.TabIndex = 9;
            this.btnBillingCancel.Text = "Cancel";
            this.btnBillingCancel.UseVisualStyleBackColor = true;
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.materialTabControl1);
            this.Name = "UCBilling";
            this.Size = new System.Drawing.Size(1200, 800);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBilling)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridBilling;
        private System.Windows.Forms.TextBox txtBillingSearch;
        private System.Windows.Forms.Button btnBillingSearch;
        private System.Windows.Forms.Button btnBillingAddNew;
        private System.Windows.Forms.Button btnBillingEdit;
        private System.Windows.Forms.Button btnBillingDelete;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBillId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbReservationId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRoomNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRoomType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtCheckInDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtCheckOutDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNumberOfNights;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRoomRate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRoomTotal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtServiceCharges;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTaxAmount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDiscountAmount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtAdditionalCharges;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtAdditionalChargesDescription;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtAmountPaid;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtBalanceDue;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cbPaymentMethod;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbPaymentStatus;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.DateTimePicker dtBillDate;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.DateTimePicker dtDueDate;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnPrintBill;
        private System.Windows.Forms.Button btnProcessPayment;
        private System.Windows.Forms.Button btnBillingSave;
        private System.Windows.Forms.Button btnBillingCancel;
    }
}
