using System;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories.CheckInOutRepository;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Domain.Interface.CheckInOut;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Presenter;
using HotelReservationSystem.Presenter.Common;
using static HotelReservationSystem.Domain.Enums.ReservationEnum;

namespace HotelReservationSystem.UserControls
{
    public partial class UCCheckINOUT : UserControl, ICheckInOutView
    {
        private CheckInOutRepository checkInRepo;
        private UCINOUTPresenter presenter;

        #region Constructor

        public UCCheckINOUT() : this(null) { }

        public UCCheckINOUT(ReservationModel reservation)
        {
            InitializeComponent();
            checkInRepo = new CheckInOutRepository(DbConfig.GetConnectionString());

            InitializeComboBoxes();
            UserInfoDisplay.UpdateUserInfoDisplay(lblUsername, lblRole, pictureProfile);

            presenter = new UCINOUTPresenter(this, checkInRepo, DbConfig.GetConnectionString());
            AssociateAndRaiseViewEvents();

            if (reservation != null)
            {
                presenter.PopulateFromReservation(reservation);
                ShowTab(2);
            }
        }

        #endregion

        #region Initialization

        private void InitializeComboBoxes()
        {
            cbPaymentStatus.Items.Clear();
            foreach (PaymentState status in Enum.GetValues(typeof(PaymentState)))
            {
                cbPaymentStatus.Items.Add(status);
            }

            cbPaymentMethod.Items.Clear();
            foreach (PaymentMethod method in Enum.GetValues(typeof(PaymentMethod)))
            {
                cbPaymentMethod.Items.Add(method.ToString());
            }

            cbStatus.Items.Clear();
            cbStatus.Items.AddRange(new string[]
            {
                "Pending",
                "Confirmed",
                "CheckedIn",
                "CheckedOut",
                "Cancelled",
                "Reserved"
            });

            cbType.Items.Clear();
            cbType.Items.AddRange(new string[]
            {
                "Standard",
                "Deluxe",
                "Suite",
                "Family",
                "Single"
            });

            dtTimeArrival.Content = DateTime.Now;
            dtCheckIn.Content = DateTime.Now;
            dtCheckOut.Content = DateTime.Now.AddDays(1);
        }

        #endregion

        #region Event Association

        private void AssociateAndRaiseViewEvents()
        {
            btnReservationSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };

            txtReservationSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            btnReservationAddNew.Click += delegate
            {
                if (materialTabControl1.SelectedTab == tabPage3)
                {
                    MessageBox.Show("You are already in the Add Check-In menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AddNewEvent?.Invoke(this, EventArgs.Empty);
                ShowTab(2);
            };

            btnReservationEdit.Click += delegate
            {
                if (materialTabControl1.SelectedTab == tabPage3)
                {
                    MessageBox.Show("You are already in the Edit Check-In menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dataGridCheckInOut.SelectedRows.Count > 0)
                {
                    EditEvent?.Invoke(this, EventArgs.Empty);
                    ShowTab(2);
                }
                else
                {
                    MessageBox.Show("Please select a check-in record to edit.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            btnReservationSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);

                if (isSuccessful)
                {
                    ShowTab(0);
                    MessageBox.Show(Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!string.IsNullOrEmpty(Message))
                {
                    MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnReservationCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                ShowTab(0);
            };

            btnReservationDelete.Click += delegate
            {
                if (dataGridCheckInOut.SelectedRows.Count > 0)
                {
                    var result = MessageBox.Show("Are you sure you want to delete the selected check-in?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        DeleteEvent?.Invoke(this, EventArgs.Empty);

                        if (!string.IsNullOrEmpty(Message))
                        {
                            MessageBox.Show(Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a check-in to delete.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            cbType.SelectedIndexChanged += delegate
            {
                string selectedType = cbType.SelectedItem as string;
                if (!string.IsNullOrEmpty(selectedType))
                {
                    RoomTypeChangedEvent?.Invoke(this, selectedType);
                }
            };

            cbRoomNumber.SelectedIndexChanged += delegate
            {
                string selectedNumber = cbRoomNumber.SelectedItem as string;
                if (!string.IsNullOrEmpty(selectedNumber))
                {
                    RoomNumberChangedEvent?.Invoke(this, selectedNumber);
                }
            };

            btnCheckOut.Click += delegate
            {
                PerformCheckout();
            };
        }

        #endregion

        #region Properties - Interface Implementation

        public string ReservationId
        {
            get => txtReservationId.Texts;
            set => txtReservationId.Texts = value ?? string.Empty;
        }

        public string CustomerName
        {
            get => txtCusName.Texts;
            set => txtCusName.Texts = value ?? string.Empty;
        }

        public string RoomType
        {
            get => cbType.SelectedItem?.ToString();
            set => SetComboBoxItem(cbType, value);
        }

        public string RoomNumber
        {
            get => cbRoomNumber.SelectedItem?.ToString();
            set => SetComboBoxItem(cbRoomNumber, value);
        }

        public string RoomGuests
        {
            get => txtRoomGuests.Texts;
            set => txtRoomGuests.Texts = value ?? string.Empty;
        }

        public string PaymentMethod
        {
            get => cbPaymentMethod.SelectedItem?.ToString();
            set => SetComboBoxItem(cbPaymentMethod, value);
        }

        public string PaymentStatus
        {
            get => cbPaymentStatus.SelectedItem?.ToString();
            set => SetComboBoxItem(cbPaymentStatus, value);
        }

        public string ReservationStatus
        {
            get => cbStatus.SelectedItem?.ToString();
            set => SetComboBoxItem(cbStatus, value);
        }

        public string SearchValue
        {
            get => txtReservationSearch.Texts;
            set => txtReservationSearch.Texts = value ?? string.Empty;
        }

        public string PaymentReference
        {
            get => txtPaymentRef.Texts;
            set => txtPaymentRef.Texts = value ?? string.Empty;
        }

        public DateTime CheckInDate
        {
            get => dtCheckIn.Content;
            set => dtCheckIn.Content = value;
        }

        public DateTime CheckOutDate
        {
            get => dtCheckOut.Content;
            set => dtCheckOut.Content = value;
        }

        public DateTime TimeArrival
        {
            get => dtTimeArrival.Content;
            set => dtTimeArrival.Content = value;
        }

        public decimal TotalPrice
        {
            get => decimal.TryParse(txtPrice.Texts, out var totalPrice) ? totalPrice : 0;
            set => txtPrice.Texts = value.ToString("0.00");
        }

        public decimal DownPayment
        {
            get => decimal.TryParse(txtDownPayment.Texts, out var downPayment) ? downPayment : 0;
            set => txtDownPayment.Texts = value.ToString("0.00");
        }

        public decimal AmountPaid
        {
            get => decimal.TryParse(txtAmountPaid.Texts, out var amountPaid) ? amountPaid : 0;
            set => txtAmountPaid.Texts = value.ToString("0.00");
        }

        public decimal BalanceDue
        {
            get => decimal.TryParse(txtBalanceDue.Texts, out var balanceDue) ? balanceDue : 0;
            set => txtBalanceDue.Texts = value.ToString("0.00");
        }

        public int CompanionCount
        {
            get => int.TryParse(sataTextBox1txtCompanionCount.Texts, out var count) ? count : 0;
            set => sataTextBox1txtCompanionCount.Texts = value.ToString();
        }

        public bool isSuccessful { get; set; }
        public bool isEdit { get; set; }
        public string Message { get; set; }
         #endregion

        #region Events - Interface Implementation

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler AddCompanionEvent;
        public event EventHandler CompanionSaveEvent;
        public event EventHandler CompanionCancelEvent;
        public event EventHandler<string> RoomTypeChangedEvent;
        public event EventHandler<string> RoomNumberChangedEvent;


        #endregion

        #region Public Methods - Interface Implementation

        public void SetReservationListBindingSource(BindingSource checkInList)
        {
            dataGridCheckInOut.DataSource = checkInList;
        }

        public void ShowMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK,
                title.ToLower().Contains("error") ? MessageBoxIcon.Error : MessageBoxIcon.Information);
        }

        public void LoadRoomTypes(string[] roomTypes)
        {
            cbType.Items.Clear();
            if (roomTypes != null && roomTypes.Length > 0)
            {
                cbType.Items.AddRange(roomTypes);
            }
        }

        public void LoadAvailableRooms(string[] roomNumbers)
        {
            cbRoomNumber.Items.Clear();
            if (roomNumbers != null && roomNumbers.Length > 0)
            {
                cbRoomNumber.Items.AddRange(roomNumbers);
            }
        }

        public void SetFieldEnabled(string fieldName, bool enabled)
        {
            switch (fieldName)
            {
                case "CustomerName":
                    txtCusName.Enabled = enabled;
                    break;
                case "ReservationId":
                    txtReservationId.Enabled = enabled;
                    break;
                case "RoomType":
                    cbType.Enabled = enabled;
                    break;
                case "RoomNumber":
                    cbRoomNumber.Enabled = enabled;
                    break;
                case "CheckInDate":
                    dtCheckIn.Enabled = enabled;
                    break;
                case "CheckOutDate":
                    dtCheckOut.Enabled = enabled;
                    break;
            }
        }

        public int GetSelectedReservationId()
        {
            if (dataGridCheckInOut.SelectedRows.Count > 0)
            {
                var cellValue = dataGridCheckInOut.SelectedRows[0].Cells["ReservationId"].Value;
                if (cellValue != null && int.TryParse(cellValue.ToString(), out int id))
                {
                    return id;
                }
            }
            return 0;
        }

        public void ShowTab(int tabIndex)
        {
            materialTabControl1.TabPages.Clear();

            switch (tabIndex)
            {
                case 0:
                    materialTabControl1.TabPages.Add(tabPage1);
                    materialTabControl1.SelectedTab = tabPage1;
                    break;
                case 1:
                    materialTabControl1.TabPages.Add(tabPage2);
                    materialTabControl1.SelectedTab = tabPage2;
                    break;
                case 2:
                    materialTabControl1.TabPages.Add(tabPage3);
                    materialTabControl1.SelectedTab = tabPage3;
                    break;
            }
        }

        public void ClearForm()
        {
            FieldsCleaner.ClearInputs(this);
        }

        #endregion

        #region Singleton Pattern

        public static UCCheckINOUT GetInstance(Form parentContainer) =>
            UserControlFactory<UCCheckINOUT>.GetInstance(parentContainer);

        public static void ResetInstance() =>
            UserControlFactory<UCCheckINOUT>.ResetInstance();

        #endregion

        #region Private Helper Methods

        private void SetComboBoxItem(ComboBox comboBox, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                comboBox.SelectedIndex = -1;
                return;
            }

            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString().Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
        }


        #endregion

        #region Checkout Implementation

        private void PerformCheckout()
        {
            try
            {
                if (dataGridCheckInOut.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        "Please select a guest to check out.",
                        "Selection Required",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                int reservationId = GetSelectedReservationId();
                if (reservationId == 0)
                {
                    MessageBox.Show("Invalid reservation selected.", "Error");
                    return;
                }

                var checkIn = checkInRepo.GetByReservationId(reservationId);
                if (checkIn == null)
                {
                    MessageBox.Show("Check-in record not found.", "Error");
                    return;
                }

                var checkOutService = new HotelReservationSystem.Domain.Services.CheckOutService();
                var validationResult = checkOutService.ValidateCheckout(checkIn);

                // Check if there's an outstanding balance - navigate to billing form
                if (!validationResult.IsValid && validationResult.HasOutstandingBalance)
                {
                    var proceed = MessageBox.Show(
                        string.Format(
                            "Guest has an outstanding balance of ${0:N2}.\nYou must settle the remaining balance before completing checkout.\n\nOpen billing form now?",
                            validationResult.OutstandingAmount),
                        "Outstanding Balance - Partial Payment",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (proceed != DialogResult.Yes)
                        return;

                    // Navigate to billing FORM to settle balance
                    NavigateToBillingForm(checkIn, 0m);
                    return;
                }
                else if (!validationResult.IsValid)
                {
                    // Other validation failures
                    MessageBox.Show(
                        validationResult.ErrorMessage,
                        "Checkout Validation Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // At this point validation passed - FullPayment, no balance
                DateTime actualCheckOut = DateTime.Now;
                decimal lateFee = checkOutService.CalculateLateCheckoutFee(
                    checkIn.CheckOutDate,
                    actualCheckOut);

                // Prompt for damage fee
                decimal damageFee = 0m;
                var damagePrompt = MessageBox.Show(
                    "Are there any damages to report?",
                    "Damage Assessment",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (damagePrompt == DialogResult.Yes)
                {
                    using (var inputForm = new Form())
                    {
                        inputForm.Text = "Damage Fee";
                        inputForm.Size = new System.Drawing.Size(300, 150);
                        inputForm.StartPosition = FormStartPosition.CenterParent;

                        var label = new Label { Text = "Enter damage fee amount:", Left = 10, Top = 20, Width = 260 };
                        var textBox = new TextBox { Text = "0.00", Left = 10, Top = 50, Width = 260 };
                        var okButton = new Button { Text = "OK", Left = 110, Top = 80, DialogResult = DialogResult.OK };
                        var cancelButton = new Button { Text = "Cancel", Left = 190, Top = 80, DialogResult = DialogResult.Cancel };

                        inputForm.Controls.Add(label);
                        inputForm.Controls.Add(textBox);
                        inputForm.Controls.Add(okButton);
                        inputForm.Controls.Add(cancelButton);
                        inputForm.AcceptButton = okButton;
                        inputForm.CancelButton = cancelButton;

                        if (inputForm.ShowDialog() == DialogResult.OK)
                        {
                            decimal.TryParse(textBox.Text, out damageFee);
                        }
                    }
                }

                // Check if there are late fees or damage fees
                if (lateFee > 0 || damageFee > 0)
                {
                    // Navigate to billing form to show additional charges
                    string confirmMessage = string.Format(
                        "Additional charges detected:\n\n" +
                        "Customer: {0}\n" +
                        "Room: {1}\n",
                        checkIn.CustomerName,
                        checkIn.RoomNumber);

                    if (lateFee > 0)
                        confirmMessage += string.Format("Late Checkout Fee: ${0:N2}\n", lateFee);

                    if (damageFee > 0)
                        confirmMessage += string.Format("Damage Fee: ${0:N2}\n", damageFee);

                    confirmMessage += "\nProceed to billing to settle these charges?";

                    var confirm = MessageBox.Show(
                        confirmMessage,
                        "Additional Charges",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirm != DialogResult.Yes)
                        return;

                    NavigateToBillingForm(checkIn, damageFee);
                }
                else
                {
                    // No additional charges - direct checkout (FullPayment)
                    var confirm = MessageBox.Show(
                        string.Format(
                            "Confirm checkout for:\n\n" +
                            "Customer: {0}\n" +
                            "Room: {1}\n" +
                            "Total Paid: ${2:N2}\n" +
                            "Balance: $0.00\n\n" +
                            "Complete checkout now?",
                            checkIn.CustomerName,
                            checkIn.RoomNumber,
                            checkIn.AmountPaid),
                        "Complete Checkout - Full Payment",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirm != DialogResult.Yes)
                        return;

                    // Complete checkout directly
                    CompleteCheckoutDirectly(checkIn);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Checkout Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Error during checkout: {0}", ex.Message),
                    "Checkout Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CompleteCheckoutDirectly(HotelReservationSystem.Domain.Model.CheckInOut.CheckInOutModel checkIn)
        {
            try
            {
                // 1. Update check-in record (mark as checked out)
                checkInRepo.CheckOut(
                    checkIn.ReservationId,
                    DateTime.Now,
                    UserSession.Username);

                // 2. Update reservation status
                var reserveRepo = new HotelReservationSystem.Data.Repositories.ReservationRepository(
                    HotelReservationSystem.DataInitializer.DbInitializer.DbConfig.GetConnectionString());

                var reservation = reserveRepo.GetById(checkIn.ReservationId);
                if (reservation != null)
                {
                    reservation.ReservationStatus = "CheckedOut";
                    reserveRepo.Edit(reservation);
                }

                // 3. Update room status to Available
                var roomRepo = new HotelReservationSystem.Data.Repositories.RoomRepository(
                    HotelReservationSystem.DataInitializer.DbInitializer.DbConfig.GetConnectionString());

                var room = roomRepo.GetByNumber(checkIn.RoomNumber);
                if (room != null)
                {
                    room.RoomStatus = "Available";
                    roomRepo.Edit(room);
                }

                // Refresh the grid
                SearchEvent?.Invoke(this, EventArgs.Empty);

                MessageBox.Show(
                    "Checkout completed successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Error completing checkout: {0}", ex.Message),
                    "Checkout Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void NavigateToBillingForm(HotelReservationSystem.Domain.Model.CheckInOut.CheckInOutModel checkIn, decimal damageFee)
        {
            try
            {
                // Get the parent form (ReservationHome)
                Form parentForm = this.FindForm();
                if (parentForm == null)
                {
                    MessageBox.Show("Cannot find parent form.", "Error");
                    return;
                }

                // Get the billing user control instance
                var billingControl = UCBilling.GetInstance(parentForm);

                // Create billing presenter
                var billingRepo = new HotelReservationSystem.Data.Repositories.BillingRepository(
                    HotelReservationSystem.DataInitializer.DbInitializer.DbConfig.GetConnectionString());
                var billingPresenter = new HotelReservationSystem.Presenter.Billing.BillingPresenter(
                    billingControl,
                    billingRepo);

                // Populate billing from checkout with callback
                billingPresenter.PopulateFromCheckout(checkIn, damageFee, () =>
                {
                    // This callback is invoked when billing is saved or cancelled
                    HandleCheckoutCompleted();
                });

                // Load the billing control in the parent form
                var mainView = parentForm as HotelReservationSystem.Domain.Interface.IMainView;
                if (mainView != null)
                {
                    mainView.LoadUserControl(billingControl);
                }
                else
                {
                    MessageBox.Show("Cannot navigate to billing form.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Error navigating to billing: {0}", ex.Message),
                    "Navigation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void HandleCheckoutCompleted()
        {
            try
            {
                // Navigate back to check-in view
                Form parentForm = this.FindForm();
                if (parentForm != null)
                {
                    var mainView = parentForm as HotelReservationSystem.Domain.Interface.IMainView;
                    if (mainView != null)
                    {
                        // Reload this control to refresh data
                        var checkInControl = UCCheckINOUT.GetInstance(parentForm);
                        mainView.LoadUserControl(checkInControl);

                        // IMPORTANT: Trigger the presenter to reload data
                        // The presenter will refresh when SearchEvent is invoked with empty string
                        if (checkInControl != null)
                        {
                            // Force presenter refresh by triggering search with empty value
                            checkInControl.SearchValue = string.Empty;
                            checkInControl.SearchEvent?.Invoke(checkInControl, EventArgs.Empty);
                        }

                        MessageBox.Show(
                            "Checkout completed successfully! Status updated to CheckedOut.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Error returning from billing: {0}", ex.Message),
                    "Navigation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BillingControl_CheckoutCompleted(object sender, EventArgs e)
        {
            try
            {
                // Unsubscribe from event
                if (sender is UCBilling billingControl)
                {
                    billingControl.CheckoutCompletedEvent -= BillingControl_CheckoutCompleted;
                }

                // Navigate back to check-in view
                Form parentForm = this.FindForm();
                if (parentForm != null)
                {
                    var mainView = parentForm as HotelReservationSystem.Domain.Interface.IMainView;
                    if (mainView != null)
                    {
                        // Reload this control to refresh data
                        var checkInControl = UCCheckINOUT.GetInstance(parentForm);
                        mainView.LoadUserControl(checkInControl);
                        
                        // Refresh the grid
                        SearchEvent?.Invoke(this, EventArgs.Empty);
                        
                        MessageBox.Show(
                            "Checkout completed successfully!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Error returning from billing: {0}", ex.Message),
                    "Navigation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}