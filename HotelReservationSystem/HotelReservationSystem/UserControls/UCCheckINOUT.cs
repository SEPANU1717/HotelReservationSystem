using System;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories.CheckInOutRepository;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Domain.Interface.CheckInOut;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Presenter;
using HotelReservationSystem.Presenter.Common;
using static HotelReservationSystem.Domain.Enums.ReservationEnum;
using HotelReservationSystem.Data.Repositories;

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

            ApplyRoleBasedRestrictions();

            if (reservation != null)
            {
                presenter.PopulateFromReservation(reservation);
                ShowTab(2);
            }
        }

        #endregion

        #region Role-Based Restrictions

        private void ApplyRoleBasedRestrictions()
        {

            UpdateSearchControlsState();
        }

        private void UpdateSearchControlsState()
        {

            bool isOnGridView = materialTabControl1.SelectedTab == tabPage1;

            if (txtReservationSearch != null)
            {
                txtReservationSearch.Enabled = isOnGridView;
            }

            if (btnReservationSearch != null)
            {
                btnReservationSearch.Enabled = isOnGridView;
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

            txtReservationId.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;

                if (int.TryParse(txtReservationId.Texts, out int resId) && resId > 0)
                {
                    try
                    {
                        var reservationRepo = new ReservationRepository(DbConfig.GetConnectionString());
                        var reservation = reservationRepo.GetById(resId);
                        if (reservation != null)
                        {
                            presenter.PopulateFromReservation(reservation);
                            ShowTab(2);
                        }
                        else
                        {
                            MessageBox.Show($"Reservation {resId} not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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

            cbPaymentStatus.SelectedIndexChanged += delegate
            {
                PaymentStatusChangedEvent?.Invoke(this, EventArgs.Empty);
            };

            txtAmountPaid.TextChanged += delegate
            {
                AmountPaidChangedEvent?.Invoke(this, EventArgs.Empty);
            };

            btnCheckOut.Click += delegate
            {
                CheckoutEvent?.Invoke(this, EventArgs.Empty);
            };

            btnCheckInPrint.Click += delegate
            {
                if (dataGridCheckInOut.SelectedRows.Count > 0)
                {
                    try
                    {
                        int reservationId = GetSelectedReservationId();

                        var checkIn = checkInRepo.GetByReservationId(reservationId);
                        if (checkIn == null)
                        {
                            MessageBox.Show("Check-in record not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var customerRepo = new HotelReservationSystem.Data.Repositories.CustomerRepository(DbConfig.GetConnectionString());
                        var customer = customerRepo.GetByCustomerName(checkIn.CustomerName);

                        using (var receiptService = new HotelReservationSystem.Domain.Services.CheckInReceiptPrintService(checkIn, customer))
                        {
                            receiptService.ShowWithOptions();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error showing receipt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a check-in record to print.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
            get => decimal.TryParse(txtBalanceDue.Texts, out var balance) ? balance : 0;
            set
            {
                if (txtBalanceDue != null)
                {
                    txtBalanceDue.Texts = value == 0m ? "0" : value.ToString("F2");
                }
            }
        }

        public string CustomerEmail
        {
            get => txtEmail?.Texts ?? string.Empty;
            set => txtEmail.Texts = value ?? string.Empty;
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
        public event EventHandler PaymentStatusChangedEvent;
        public event EventHandler AmountPaidChangedEvent;
        public event EventHandler CheckInDateChangedEvent;
        public event EventHandler CheckOutDateChangedEvent;
        public event EventHandler CheckoutEvent;


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

            UpdateSearchControlsState();
        }

        public void ClearForm()
        {
            FieldsCleaner.ClearInputs(this);
        }

        public bool ShowConfirmation(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }


        //auto form na lang to guys
        public string PromptForInput(string title, string label, string defaultValue = "0.00")
        {
            using (var inputForm = new Form())
            {
                inputForm.Text = title;
                inputForm.Size = new System.Drawing.Size(300, 150);
                inputForm.StartPosition = FormStartPosition.CenterParent;

                var labelControl = new Label { Text = label, Left = 10, Top = 20, Width = 260 };
                var textBox = new TextBox { Text = defaultValue, Left = 10, Top = 50, Width = 260 };
                var okButton = new Button { Text = "OK", Left = 110, Top = 80, DialogResult = DialogResult.OK };
                var cancelButton = new Button { Text = "Cancel", Left = 190, Top = 80, DialogResult = DialogResult.Cancel };

                inputForm.Controls.Add(labelControl);
                inputForm.Controls.Add(textBox);
                inputForm.Controls.Add(okButton);
                inputForm.Controls.Add(cancelButton);
                inputForm.AcceptButton = okButton;
                inputForm.CancelButton = cancelButton;

                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    return textBox.Text;
                }
                return null;
            }
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
    }
}