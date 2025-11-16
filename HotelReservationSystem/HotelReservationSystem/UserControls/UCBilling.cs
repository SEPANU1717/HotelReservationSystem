using System;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Domain.Interface.Billing;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Presenter.Common;

namespace HotelReservationSystem.UserControls
{
    public partial class UCBilling : UserControl, IBillingView
    {
        #region Fields
        private BillingRepository billRepo;
        private ReservationRepository reserveRepo;
        private RoomRepository roomRepo;
        
        private DateTimePicker dtCheckInDate;
        private DateTimePicker dtCheckOutDate;
        private DateTimePicker dtActualCheckOut;
        #endregion

        #region Constructor
        public UCBilling()
        {
            InitializeComponent();
            billRepo = new BillingRepository(DbConfig.GetConnectionString());
            reserveRepo = new ReservationRepository(DbConfig.GetConnectionString());
            roomRepo = new RoomRepository(DbConfig.GetConnectionString());
            UserInfoDisplay.UpdateUserInfoDisplay(lblUsername, lblRole, pictureProfile);

           
            InitializeControls();
            AssociateAndRaiseViewEvents();
        }
        #endregion

        #region Initialization
        private void InitializeControls()
        {
          
            DateBilled = DateTime.Now;
            ActualCheckOutDate = null;
            
            RoomCharge = "0.00";
            LateCheckoutFee = "0.00";
            DamageFee = "0.00";
            AmountPaidBefore = "0.00";
            AmountPaidAtCheckout = "0.00";
            
            if (cbPaymentStatus != null)
            {
                cbPaymentStatus.Items.Clear();
                cbPaymentStatus.Items.AddRange(new object[] { "Paid", "Pending", "Partial", "Refunded" });
            }
            
            if (cbPaymentMethod != null)
            {
                cbPaymentMethod.Items.Clear();
                cbPaymentMethod.Items.AddRange(new object[] { "Cash", "Credit Card", "Debit Card", "Bank Transfer", "Online Payment", "Gcash", "PayMaya" });
            }
        }
        
        private void AssociateAndRaiseViewEvents()
        {
            // Search
            if (btnBillingSearch != null)
                btnBillingSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            
            if (txtBillingSearch != null)
                txtBillingSearch.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                        SearchEvent?.Invoke(this, EventArgs.Empty);
                };
            
            // Add New
            if (btnBillingAddNew != null)
                btnBillingAddNew.Click += delegate { AddNewEvent?.Invoke(this, EventArgs.Empty); };
            
            // Edit
            if (btnBillingEdit != null)
                btnBillingEdit.Click += delegate { EditEvent?.Invoke(this, EventArgs.Empty); };
            
            // Delete
            if (btnDeleteDelete != null)
                btnDeleteDelete.Click += delegate { DeleteEvent?.Invoke(this, EventArgs.Empty); };
            
            // Save
            if (btnBillingSave != null)
                btnBillingSave.Click += delegate { SaveEvent?.Invoke(this, EventArgs.Empty); };
            
            // Cancel
            if (btnBillingCancel != null)
                btnBillingCancel.Click += delegate { CancelEvent?.Invoke(this, EventArgs.Empty); };

            if (btnBillPrint != null)
            {
                btnBillPrint.Click += delegate { PrintInvoiceEvent?.Invoke(this, EventArgs.Empty); };
            }

            if (dataGridBilling != null)
            {
                dataGridBilling.CellDoubleClick += (s, e) =>
                {
                    if (e.RowIndex >= 0)
                    {
                        EmailInvoiceEvent?.Invoke(this, EventArgs.Empty);
                    }
                };
            }
        }
        #endregion

        #region IBillingView Properties - Identity
        public string BillId
        {
            get => txtBillId?.Texts ?? string.Empty;
            set
            {
                if (txtBillId != null)
                    txtBillId.Texts = value ?? string.Empty;
            }
        }

        public string ReservationId
        {
            get => txtReservationId?.Texts ?? string.Empty;
            set
            {
                if (txtReservationId != null)
                    txtReservationId.Texts = value ?? string.Empty;
            }
        }
        #endregion

        #region IBillingView Properties - Guest Information
        public string CustomerName
        {
            get => txtCustomerName?.Texts ?? string.Empty;
            set
            {
                if (txtCustomerName != null)
                    txtCustomerName.Texts = value ?? string.Empty;
            }
        }

        public string RoomType
        {
            get => txtRoomType?.Texts ?? string.Empty;
            set
            {
                if (txtRoomType != null)
                    txtRoomType.Texts = value ?? string.Empty;
            }
        }

        public string RoomNumber
        {
            get => txtRoomNumber?.Texts ?? string.Empty;
            set
            {
                if (txtRoomNumber != null)
                    txtRoomNumber.Texts = value ?? string.Empty;
            }
        }
        #endregion

        #region IBillingView Properties - Dates
        public DateTime CheckInDate
        {
            get
            {
                if (dtCheckInDate != null)
                    return dtCheckInDate.Value;
                    
                if (txtCheckInDate != null && DateTime.TryParse(txtCheckInDate.Texts, out DateTime date))
                    return date;
                    
                return DateTime.Now;
            }
            set
            {
                if (dtCheckInDate != null)
                    dtCheckInDate.Value = value;
                    
                if (txtCheckInDate != null)
                    txtCheckInDate.Texts = value.ToString("MM/dd/yyyy");
            }
        }

        public DateTime CheckOutDate
        {
            get
            {
                if (dtCheckOutDate != null)
                    return dtCheckOutDate.Value;
                    
                if (txtScheduledCheckOut != null && DateTime.TryParse(txtScheduledCheckOut.Texts, out DateTime date))
                    return date;
                    
                return DateTime.Now.AddDays(1);
            }
            set
            {
                if (dtCheckOutDate != null)
                    dtCheckOutDate.Value = value;
                    
                if (txtScheduledCheckOut != null)
                    txtScheduledCheckOut.Texts = value.ToString("MM/dd/yyyy");
            }
        }

        public DateTime? ActualCheckOutDate
        {
            get
            {
                if (dtActualCheckOut != null)
                    return dtActualCheckOut.Value;
                    
                if (txtActualCheckOut != null && DateTime.TryParse(txtActualCheckOut.Texts, out DateTime date))
                    return date;
                    
                return null;
            }
            set
            {
                if (dtActualCheckOut != null && value.HasValue)
                    dtActualCheckOut.Value = value.Value;
                    
                if (txtActualCheckOut != null && value.HasValue)
                    txtActualCheckOut.Texts = value.Value.ToString("MM/dd/yyyy");
            }
        }

        public DateTime DateBilled { get; set; } = DateTime.Now;
        #endregion

        #region IBillingView Properties - Charges
        public string RoomCharge
        {
            get => txtRoomCharge?.Texts ?? "0.00";
            set
            {
                if (txtRoomCharge != null)
                    txtRoomCharge.Texts = value ?? "0.00";
                UpdateCalculatedFields();
            }
        }

        public string LateCheckoutFee
        {
            get => txtLateCheckoutFee?.Texts ?? "0.00";
            set
            {
                if (txtLateCheckoutFee != null)
                    txtLateCheckoutFee.Texts = value ?? "0.00";
                UpdateCalculatedFields();
            }
        }

        public string DamageFee
        {
            get => txtDamageFee?.Texts ?? "0.00";
            set
            {
                if (txtDamageFee != null)
                    txtDamageFee.Texts = value ?? "0.00";
                UpdateCalculatedFields();
            }
        }
        #endregion

        #region IBillingView Properties - Payment
        public string AmountPaidBefore
        {
            get => txtAmountPaidBefore?.Texts ?? "0.00";
            set
            {
                if (txtAmountPaidBefore != null)
                    txtAmountPaidBefore.Texts = value ?? "0.00";
                UpdateCalculatedFields();
            }
        }

        public string AmountPaidAtCheckout
        {
            get => txtAmountPaidAtCheckout?.Texts ?? "0.00";
            set
            {
                if (txtAmountPaidAtCheckout != null)
                    txtAmountPaidAtCheckout.Texts = value ?? "0.00";
                UpdateCalculatedFields();
            }
        }

        public string TotalAmount
        {
            get => txtTotalAmount?.Texts ?? "0.00";
            set
            {
                if (txtTotalAmount != null)
                    txtTotalAmount.Texts = value ?? "0.00";
            }
        }

        public string BalanceDue
        {
            get => txtBalanceDue?.Texts ?? "0.00";
            set
            {
                if (txtBalanceDue != null)
                    txtBalanceDue.Texts = value ?? "0.00";
            }
        }

        public string PaymentStatus
        {
            get => cbPaymentStatus?.SelectedItem?.ToString() ?? string.Empty;
            set
            {
                if (cbPaymentStatus != null && !string.IsNullOrEmpty(value))
                {
                    for (int i = 0; i < cbPaymentStatus.Items.Count; i++)
                    {
                        if (cbPaymentStatus.Items[i].ToString().Equals(value, StringComparison.OrdinalIgnoreCase))
                        {
                            cbPaymentStatus.SelectedIndex = i;
                            return;
                        }
                    }
                }
            }
        }

        public string PaymentMethod
        {
            get => cbPaymentMethod?.SelectedItem?.ToString() ?? string.Empty;
            set
            {
                if (cbPaymentMethod != null && !string.IsNullOrEmpty(value))
                {
                    for (int i = 0; i < cbPaymentMethod.Items.Count; i++)
                    {
                        if (cbPaymentMethod.Items[i].ToString().Equals(value, StringComparison.OrdinalIgnoreCase))
                        {
                            cbPaymentMethod.SelectedIndex = i;
                            return;
                        }
                    }
                }
            }
        }

        public string PaymentReference
        {
            get => txtPaymentReference?.Texts ?? string.Empty;
            set
            {
                if (txtPaymentReference != null)
                    txtPaymentReference.Texts = value ?? string.Empty;
            }
        }
        #endregion

        #region IBillingView Properties - Metadata
        public string BilledBy { get; set; } = string.Empty;
        #endregion

        #region IBillingView Properties - UI State
        public string SearchValue
        {
            get => txtBillingSearch?.Texts ?? string.Empty;
            set
            {
                if (txtBillingSearch != null)
                    txtBillingSearch.Texts = value ?? string.Empty;
            }
        }

        public bool isEdit { get; set; }
        public bool isSuccessful { get; set; }
        public string Message { get; set; }
        #endregion

        #region IBillingView Events
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler CheckoutCompletedEvent;
        public event EventHandler PrintInvoiceEvent;
        public event EventHandler EmailInvoiceEvent;

        #endregion

        #region IBillingView Methods
        public void SetBillingListBindingSource(BindingSource billingList)
        {
            if (dataGridBilling != null)
            {
                dataGridBilling.DataSource = billingList;
            }
        }

        public void ShowMessage(string message, string title)
        {
            MessageBoxIcon icon = title.Contains("Error") || title.Contains("Denied") 
                ? MessageBoxIcon.Error 
                : title.Contains("Warning") 
                    ? MessageBoxIcon.Warning 
                    : MessageBoxIcon.Information;

            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        public void ClearForm()
        {
            FieldsCleaner.ClearInputs(this);

            DateBilled = DateTime.Now;
            BilledBy = UserSession.Username;
            
            isEdit = false;
        }

        public void ShowBillingForm()
        {
             this.Visible = true;
            this.BringToFront();
            
            if (materialTabControl1 != null && ReservationBillingForm != null)
            {
                materialTabControl1.TabPages.Clear();
                materialTabControl1.TabPages.Add(ReservationBillingForm);
                materialTabControl1.SelectedTab = ReservationBillingForm;
            }
        }

        public void ShowGridView()
        {
            this.Visible = true;
            this.BringToFront();
            
            if (materialTabControl1 != null && tabPage1 != null)
            {
                materialTabControl1.TabPages.Clear();
                materialTabControl1.TabPages.Add(tabPage1);
                materialTabControl1.SelectedTab = tabPage1;
            }
        }

        public int GetSelectedBillId()
        {
            if (dataGridBilling == null || dataGridBilling.CurrentRow == null)
                return 0;

            try
            {
                var cellValue = dataGridBilling.CurrentRow.Cells["BillId"].Value;
                if (cellValue != null && int.TryParse(cellValue.ToString(), out int billId))
                {
                    return billId;
                }
            }
            catch
            {
                return 0;
            }

            return 0;
        }

        #endregion

        #region Helper Methods
        private void UpdateCalculatedFields()
        {
            try
            {
                decimal roomCharge = decimal.TryParse(RoomCharge, out decimal rc) ? rc : 0m;
                decimal lateFee = decimal.TryParse(LateCheckoutFee, out decimal lf) ? lf : 0m;
                decimal damageFee = decimal.TryParse(DamageFee, out decimal df) ? df : 0m;
                
                decimal subtotal = roomCharge + lateFee + damageFee;
                
                if (txtSubtotal != null)
                    txtSubtotal.Texts = subtotal.ToString("F2");
                
                TotalAmount = subtotal.ToString("F2");
                
                decimal paidBefore = decimal.TryParse(AmountPaidBefore, out decimal pb) ? pb : 0m;
                decimal paidNow = decimal.TryParse(AmountPaidAtCheckout, out decimal pn) ? pn : 0m;
                
                decimal balance = subtotal - (paidBefore + paidNow);
                BalanceDue = balance.ToString("F2");
            }
            catch
            {
                //wala pa
            }
        }
        #endregion

        #region Singleton Pattern
        public static UCBilling GetInstance(Form parentContainer) =>
            UserControlFactory<UCBilling>.GetInstance(parentContainer);

        public static void ResetInstance() => 
            UserControlFactory<UCBilling>.ResetInstance();
        #endregion
    }
}