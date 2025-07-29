using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Interface.Billing;
using HotelReservationSystem.Model.Billing;
using HotelReservationSystem.Repositories;
using HotelReservationSystem.Repositories.Billing;
using MaterialSkin.Controls;

namespace HotelReservationSystem.UserControls
{
    public partial class UCBilling : UserControl, IBillingView
    {
        private BillingRepository billingRepo;
        private ReservationRepository reservationRepo;

        public UCBilling()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            InitializeRepositories();
            InitializeComboBoxes();
        }

        private void InitializeRepositories()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            billingRepo = new BillingRepository(connectionString);
            reservationRepo = new ReservationRepository(connectionString);
        }

        private void InitializeComboBoxes()
        {
            // Payment Method ComboBox
            cbPaymentMethod.Items.Clear();
            cbPaymentMethod.Items.AddRange(new string[]
            {
                "Cash",
                "Credit Card",
                "Debit Card",
                "Bank Transfer",
                "Check"
            });

            // Payment Status ComboBox
            cbPaymentStatus.Items.Clear();
            cbPaymentStatus.Items.AddRange(new string[]
            {
                "Pending",
                "Paid",
                "Partially Paid",
                "Overdue",
                "Refunded"
            });

            // Reservation ComboBox (populate from reservations)
            LoadReservationComboBox();
        }

        private void LoadReservationComboBox()
        {
            cbReservationId.Items.Clear();
            var reservations = reservationRepo.GetAll();
            foreach (var reservation in reservations)
            {
                cbReservationId.Items.Add($"{reservation.ReservationId} - {reservation.CustomerName}");
            }
        }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler CalculateEvent;
        public event EventHandler PrintBillEvent;
        public event EventHandler ProcessPaymentEvent;

        private void AssociateAndRaiseViewEvents()
        {
            // Search Event
            btnBillingSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtBillingSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            // Add New Event
            btnBillingAddNew.Click += delegate
            {
                ClearBillingFields();
                txtBillId.Text = billingRepo.GetNextBillId().ToString();
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Add new bill";
            };

            // Edit Event
            btnBillingEdit.Click += delegate
            {
                if (dataGridBilling.SelectedRows.Count > 0)
                {
                    int billId = Convert.ToInt32(dataGridBilling.SelectedRows[0].Cells["BillId"].Value);
                    LoadBillingForEdit(billId);
                    EditEvent?.Invoke(this, EventArgs.Empty);
                    materialTabControl1.TabPages.Remove(tabPage1);
                    materialTabControl1.TabPages.Add(tabPage2);
                    materialTabControl1.Text = "Edit bill";
                }
                else
                {
                    MessageBox.Show("Please select a bill to edit.");
                }
            };

            // Save Event
            btnBillingSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    ClearBillingFields();
                    isEdit = false;
                    materialTabControl1.TabPages.Remove(tabPage2);
                    materialTabControl1.TabPages.Add(tabPage1);
                }
                MessageBox.Show(Message);
            };

            // Cancel Event
            btnBillingCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage2);
                materialTabControl1.TabPages.Add(tabPage1);
            };

            // Delete Event
            btnBillingDelete.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected bill?", "Warning",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };

            // Calculate Event
            btnCalculate.Click += delegate { CalculateEvent?.Invoke(this, EventArgs.Empty); };

            // Print Bill Event
            btnPrintBill.Click += delegate { PrintBillEvent?.Invoke(this, EventArgs.Empty); };

            // Process Payment Event
            btnProcessPayment.Click += delegate { ProcessPaymentEvent?.Invoke(this, EventArgs.Empty); };

            // Auto-calculate when values change
            txtRoomRate.TextChanged += (s, e) => CalculateTotals();
            txtNumberOfNights.TextChanged += (s, e) => CalculateTotals();
            txtServiceCharges.TextChanged += (s, e) => CalculateTotals();
            txtTaxAmount.TextChanged += (s, e) => CalculateTotals();
            txtDiscountAmount.TextChanged += (s, e) => CalculateTotals();
            txtAdditionalCharges.TextChanged += (s, e) => CalculateTotals();
            txtAmountPaid.TextChanged += (s, e) => CalculateTotals();

            // Load bill data when reservation is selected
            cbReservationId.SelectedIndexChanged += LoadReservationData;
        }

        private void LoadReservationData(object sender, EventArgs e)
        {
            if (cbReservationId.SelectedItem != null)
            {
                string selectedItem = cbReservationId.SelectedItem.ToString();
                if (int.TryParse(selectedItem.Split('-')[0].Trim(), out int reservationId))
                {
                    var reservation = reservationRepo.GetById(reservationId);
                    if (reservation != null)
                    {
                        CustomerName = reservation.CustomerName;
                        CheckInDate = reservation.CheckInDate;
                        CheckOutDate = reservation.CheckOutDate;
                        
                        // Calculate number of nights
                        int nights = (int)(reservation.CheckOutDate - reservation.CheckInDate).TotalDays;
                        NumberOfNights = nights.ToString();
                        
                        // Set default tax rate (adjust as needed)
                        TaxAmount = (reservation.TotalPrice * 0.1m).ToString("0.00"); // 10% tax
                        
                        CalculateTotals();
                    }
                }
            }
        }

        private void CalculateTotals()
        {
            try
            {
                decimal roomRate = decimal.TryParse(txtRoomRate.Text, out var rate) ? rate : 0;
                int nights = int.TryParse(txtNumberOfNights.Text, out var nightCount) ? nightCount : 0;
                decimal serviceCharges = decimal.TryParse(txtServiceCharges.Text, out var service) ? service : 0;
                decimal taxAmount = decimal.TryParse(txtTaxAmount.Text, out var tax) ? tax : 0;
                decimal discountAmount = decimal.TryParse(txtDiscountAmount.Text, out var discount) ? discount : 0;
                decimal additionalCharges = decimal.TryParse(txtAdditionalCharges.Text, out var additional) ? additional : 0;
                decimal amountPaid = decimal.TryParse(txtAmountPaid.Text, out var paid) ? paid : 0;

                decimal roomTotal = roomRate * nights;
                decimal subtotal = roomTotal + serviceCharges + additionalCharges - discountAmount;
                decimal totalAmount = subtotal + taxAmount;
                decimal balanceDue = totalAmount - amountPaid;

                txtRoomTotal.Text = roomTotal.ToString("0.00");
                txtSubtotal.Text = subtotal.ToString("0.00");
                txtTotalAmount.Text = totalAmount.ToString("0.00");
                txtBalanceDue.Text = balanceDue.ToString("0.00");
            }
            catch (Exception ex)
            {
                // Handle calculation errors silently or log them
            }
        }

        private void ClearBillingFields()
        {
            txtBillId.Text = "";
            cbReservationId.SelectedIndex = -1;
            txtCustomerName.Text = "";
            txtRoomNumber.Text = "";
            txtRoomType.Text = "";
            dtCheckInDate.Value = DateTime.Now;
            dtCheckOutDate.Value = DateTime.Now.AddDays(1);
            txtNumberOfNights.Text = "1";
            txtRoomRate.Text = "0.00";
            txtRoomTotal.Text = "0.00";
            txtServiceCharges.Text = "0.00";
            txtTaxAmount.Text = "0.00";
            txtDiscountAmount.Text = "0.00";
            txtAdditionalCharges.Text = "0.00";
            txtAdditionalChargesDescription.Text = "";
            txtSubtotal.Text = "0.00";
            txtTotalAmount.Text = "0.00";
            txtAmountPaid.Text = "0.00";
            txtBalanceDue.Text = "0.00";
            cbPaymentMethod.SelectedIndex = -1;
            cbPaymentStatus.SelectedIndex = 0; // Default to "Pending"
            dtBillDate.Value = DateTime.Now;
            dtDueDate.Value = DateTime.Now.AddDays(30);
            txtNotes.Text = "";
        }

        private void LoadBillingForEdit(int billId)
        {
            var billing = billingRepo.GetById(billId);
            if (billing != null)
            {
                BillId = billing.BillId.ToString();
                ReservationId = billing.ReservationId.ToString();
                CustomerName = billing.CustomerName;
                RoomNumber = billing.RoomNumber;
                RoomType = billing.RoomType;
                CheckInDate = billing.CheckInDate;
                CheckOutDate = billing.CheckOutDate;
                NumberOfNights = billing.NumberOfNights.ToString();
                RoomRate = billing.RoomRate.ToString("0.00");
                RoomTotal = billing.RoomTotal.ToString("0.00");
                ServiceCharges = billing.ServiceCharges.ToString("0.00");
                TaxAmount = billing.TaxAmount.ToString("0.00");
                DiscountAmount = billing.DiscountAmount.ToString("0.00");
                AdditionalCharges = billing.AdditionalCharges.ToString("0.00");
                AdditionalChargesDescription = billing.AdditionalChargesDescription;
                Subtotal = billing.Subtotal.ToString("0.00");
                TotalAmount = billing.TotalAmount.ToString("0.00");
                AmountPaid = billing.AmountPaid.ToString("0.00");
                BalanceDue = billing.BalanceDue.ToString("0.00");
                PaymentMethod = billing.PaymentMethod;
                PaymentStatus = billing.PaymentStatus;
                BillDate = billing.BillDate;
                DueDate = billing.DueDate;
                Notes = billing.Notes;
            }
        }

        // IBillingView Properties Implementation
        public string BillId { get => txtBillId.Text; set => txtBillId.Text = value; }
        public string ReservationId { get => cbReservationId.Text; set => cbReservationId.Text = value; }
        public string CustomerName { get => txtCustomerName.Text; set => txtCustomerName.Text = value; }
        public string RoomNumber { get => txtRoomNumber.Text; set => txtRoomNumber.Text = value; }
        public string RoomType { get => txtRoomType.Text; set => txtRoomType.Text = value; }
        public DateTime CheckInDate { get => dtCheckInDate.Value; set => dtCheckInDate.Value = value; }
        public DateTime CheckOutDate { get => dtCheckOutDate.Value; set => dtCheckOutDate.Value = value; }
        public string NumberOfNights { get => txtNumberOfNights.Text; set => txtNumberOfNights.Text = value; }
        public string RoomRate { get => txtRoomRate.Text; set => txtRoomRate.Text = value; }
        public string RoomTotal { get => txtRoomTotal.Text; set => txtRoomTotal.Text = value; }
        public string ServiceCharges { get => txtServiceCharges.Text; set => txtServiceCharges.Text = value; }
        public string TaxAmount { get => txtTaxAmount.Text; set => txtTaxAmount.Text = value; }
        public string DiscountAmount { get => txtDiscountAmount.Text; set => txtDiscountAmount.Text = value; }
        public string AdditionalCharges { get => txtAdditionalCharges.Text; set => txtAdditionalCharges.Text = value; }
        public string AdditionalChargesDescription { get => txtAdditionalChargesDescription.Text; set => txtAdditionalChargesDescription.Text = value; }
        public string Subtotal { get => txtSubtotal.Text; set => txtSubtotal.Text = value; }
        public string TotalAmount { get => txtTotalAmount.Text; set => txtTotalAmount.Text = value; }
        public string AmountPaid { get => txtAmountPaid.Text; set => txtAmountPaid.Text = value; }
        public string BalanceDue { get => txtBalanceDue.Text; set => txtBalanceDue.Text = value; }
        public string PaymentMethod { get => cbPaymentMethod.SelectedItem as string; set => cbPaymentMethod.SelectedItem = value; }
        public string PaymentStatus { get => cbPaymentStatus.SelectedItem as string; set => cbPaymentStatus.SelectedItem = value; }
        public DateTime BillDate { get => dtBillDate.Value; set => dtBillDate.Value = value; }
        public DateTime DueDate { get => dtDueDate.Value; set => dtDueDate.Value = value; }
        public string Notes { get => txtNotes.Text; set => txtNotes.Text = value; }
        public string SearchValue { get => txtBillingSearch.Text; set => txtBillingSearch.Text = value; }
        public bool isEdit { get; set; }
        public bool isSuccessful { get; set; }
        public string Message { get; set; }

        // Singleton pattern
        private static UCBilling _instance;
        public static void ResetInstance()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        public static UCBilling GetInstance(Form parentContainer)
        {
            if (_instance == null || _instance.IsDisposed || _instance.Parent == null)
            {
                _instance = new UCBilling();
            }

            _instance.Dock = DockStyle.Fill;
            return _instance;
        }

        public void SetBillingListBindingSource(BindingSource billingList)
        {
            dataGridBilling.DataSource = billingList;
        }
    }
}
