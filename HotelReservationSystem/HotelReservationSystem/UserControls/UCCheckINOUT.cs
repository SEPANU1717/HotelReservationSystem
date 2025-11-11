using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.Data.Repositories.CheckInOut;
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
        private ReservationModel initialReservation;

        #region Constructor

        public UCCheckINOUT() : this(null) { }

        public UCCheckINOUT(ReservationModel reservation)
        {
            InitializeComponent();
            checkInRepo = new CheckInOutRepository(DbConfig.GetConnectionString());
            initialReservation = reservation;

            InitializeComboBoxes();
            UserInfoDisplay.UpdateUserInfoDisplay(lblUsername, lblRole, pictureProfile);
            AssociateAndRaiseViewEvents();

            // Initialize presenter AFTER event associations
            new UCINOUTPresenter(this, checkInRepo, DbConfig.GetConnectionString());

            // Auto-populate if reservation is provided
            if (reservation != null)
            {
                PopulateFromReservation(reservation);

                // Switch to detail tab automatically
                if (materialTabControl1.TabPages.Count > 1)
                {
                    materialTabControl1.SelectedIndex = 1;
                }
            }
        }

        #endregion

        #region Initialization

        private void InitializeComboBoxes()
        {
            // Payment Status
            cbPaymentStatus.Items.Clear();
            foreach (PaymentState status in Enum.GetValues(typeof(PaymentState)))
            {
                cbPaymentStatus.Items.Add(status);
            }

            // Payment Method
            cbPaymentMethod.Items.Clear();
            foreach (PaymentMethod method in Enum.GetValues(typeof(PaymentMethod)))
            {
                cbPaymentMethod.Items.Add(method.ToString());
            }

            // Reservation Status
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

            // Room Type
            cbType.Items.Clear();
            cbType.Items.AddRange(new string[]
            {
                "Standard",
                "Deluxe",
                "Suite",
                "Family",
                "Single"
            });

            // Set default dates
            dtTimeArrival.Content = DateTime.Now;
            dtCheckIn.Content = DateTime.Now;
            dtCheckOut.Content = DateTime.Now.AddDays(1);
        }

        private void PopulateFromReservation(ReservationModel reservation)
        {
            try
            {
                // Populate basic information
                txtReservationId.Texts = reservation.ReservationId.ToString();
                txtCusName.Texts = reservation.CustomerName;

                // Set dates
                dtCheckIn.Content = reservation.CheckInDate;
                dtCheckOut.Content = reservation.CheckOutDate;
                dtTimeArrival.Content = reservation.TimeArrival != default
                    ? reservation.TimeArrival
                    : DateTime.Now;

                // Set room type first
                SetComboBoxItem(cbType, reservation.RoomType);

                // Load available rooms for this type
                if (!string.IsNullOrEmpty(reservation.RoomType))
                {
                    var roomRepo = new RoomRepository(DbConfig.GetConnectionString());
                    var availableRooms = roomRepo.GetAvailableRoomsByType(reservation.RoomType).ToList();

                    // Include the current room even if not available
                    if (!string.IsNullOrEmpty(reservation.RoomNumber))
                    {
                        var currentRoom = roomRepo.GetByNumber(reservation.RoomNumber);
                        if (currentRoom != null && !availableRooms.Any(r => r.RoomNumber == reservation.RoomNumber))
                        {
                            availableRooms.Add(currentRoom);
                        }
                    }

                    // Load rooms into combo box
                    cbRoomNumber.Items.Clear();
                    foreach (var room in availableRooms)
                    {
                        cbRoomNumber.Items.Add(room.RoomNumber);
                    }
                }

                // Set room number
                SetComboBoxItem(cbRoomNumber, reservation.RoomNumber);

                // Set financial information
                txtPrice.Texts = reservation.TotalPrice.ToString("0.00");
                txtDownPayment.Texts = reservation.DownPayment.ToString("0.00");
                txtAmountPaid.Texts = reservation.AmountPaid.ToString("0.00");
                txtBalanceDue.Texts = reservation.BalanceDue.ToString("0.00");

                // Set payment information
                SetComboBoxItem(cbPaymentMethod, reservation.PaymentMethod);
                SetComboBoxItem(cbPaymentStatus, reservation.PaymentStatus.ToString());
                txtPaymentRef.Texts = reservation.PaymentReference ?? string.Empty;

                // Set reservation status - default to CheckedIn for check-in process
                SetComboBoxItem(cbStatus, "CheckedIn");

                // Set companion count to 0 initially
                sataTextBox1txtCompanionCount.Texts = "0";

                // Mark as edit mode
                isEdit = true;

                // Disable reservation ID and customer name fields
                txtReservationId.Enabled = false;
                txtCusName.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating reservation data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Event Association

        private void AssociateAndRaiseViewEvents()
        {
            // Search Event
            btnReservationSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };

            txtReservationSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            // Add New Event
            btnReservationAddNew.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
            };

            // Edit Event
            btnReservationEdit.Click += delegate
            {
                if (dataGridCheckInOut.SelectedRows.Count > 0)
                {
                    EditEvent?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Please select a check-in record to edit.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            // Save Event
            btnReservationSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                MessageBox.Show(Message);
            };

            // Cancel Event
            btnReservationCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
            };

            // Delete Event
            btnReservationDelete.Click += delegate
            {
                if (dataGridCheckInOut.SelectedRows.Count > 0)
                {
                    var result = MessageBox.Show("Are you sure you want to delete the selected check-in?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        DeleteEvent?.Invoke(this, EventArgs.Empty);
                        MessageBox.Show(Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a check-in to delete.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            // Room Type Changed Event
            cbType.SelectedIndexChanged += delegate
            {
                string selectedType = cbType.SelectedItem as string;
                if (!string.IsNullOrEmpty(selectedType))
                {
                    RoomTypeChangedEvent?.Invoke(this, selectedType);
                }
            };

            // Room Number Changed Event
            cbRoomNumber.SelectedIndexChanged += delegate
            {
                string selectedNumber = cbRoomNumber.SelectedItem as string;
                if (!string.IsNullOrEmpty(selectedNumber))
                {
                    RoomNumberChangedEvent?.Invoke(this, selectedNumber);
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