using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Helper;
using HotelReservationSystem.Domain.Interface.Reservation;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Presenter.Common;
using static HotelReservationSystem.Domain.Enums.ReservationEnum;

namespace HotelReservationSystem.UserControls
{
    public partial class UCReservation : UserControl, IReservationView
    {
        #region Fields

        private DateTime originalCheckInDate;
        private DateTime originalCheckOutDate;
        private List<RoomModel> availableRooms = new List<RoomModel>();
        private string originalRoomNumber;
        private bool isInitializing;

        public TabPage ReservationTabPage => tabPage2;
        public MaterialSkin.Controls.MaterialTabControl ReservationTabControl => materialTabControl1;

        #endregion

        #region Constructor

        public UCReservation()
        {
            isInitializing = true;
            InitializeComponent();
            InitializePaymentDropdowns();
            InitializeRoomTypeComboBox();
            InitializeRoomStatusComboBox();
            materialTabControl1.TabPages.Remove(tabPage2);

            cbNumber.SelectedIndexChanged += cbNumber_SelectedIndexChanged;
            cbType.SelectedIndexChanged += OnRoomTypeChanged;
            dtCheckIn.DateChanged += DateOrRoomChanged;
            dtCheckOut.DateChanged += DateOrRoomChanged;
            cbNumber.SelectedIndexChanged += DateOrRoomChanged;

            dtCheckIn.Content = DateTime.Now;
            dtCheckOut.Content = DateTime.Now.AddDays(1);

            UserInfoDisplay.UpdateUserInfoDisplay(lblUsername, lblRole, pictureProfile);
            AssociateAndRaiseViewEvents();
            isInitializing = false;
        }

        #endregion

        #region Events

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler ShowCheckInOutView;
        public event EventHandler<int> LoadReservationForEditEvent;
        public event EventHandler<string> SetCustomerForReservationEvent;
        public event EventHandler<string> RoomTypeChangedEvent;
        public event EventHandler<string> PaymentTypeChangedEvent;

        #endregion

        #region Event Association

        private void AssociateAndRaiseViewEvents()
        {

            cbPaymentStatus.SelectedIndexChanged += delegate
            {
                string selectedPaymentType = cbPaymentStatus.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedPaymentType))
                {
                    PaymentTypeChangedEvent?.Invoke(this, selectedPaymentType);
                }
            };

            btnReservationSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtReservationSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            btnReservationEdit.Click += delegate
            {
                if (materialTabControl1.SelectedTab == tabPage2)
                {
                    MessageBox.Show("You are already in the Edit Reservation menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dataGridReservation.SelectedRows.Count > 0)
                {
                    int reservationId = Convert.ToInt32(dataGridReservation.SelectedRows[0].Cells["ReservationId"].Value);
                    LoadReservationForEditEvent?.Invoke(this, reservationId);
                    EditEvent?.Invoke(this, EventArgs.Empty);
                    ShowTab(1);
                    EnableField("CustomerName", false);
                }
                else
                {
                    MessageBox.Show("Please select a reservation to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            btnReservationSave.Click += delegate
            {
                RoomNumber = cbNumber.SelectedItem as string;

                if (isEdit && originalRoomNumber != RoomNumber)
                {
                    ReservationStatus = "Pending";
                }

                SaveEvent?.Invoke(this, EventArgs.Empty);
            };

            btnReservationCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
            };

            btnReservationDelete.Click += delegate
            {
                if (dataGridReservation.SelectedRows.Count > 0)
                {
                    var result = MessageBox.Show("Are you sure you want to delete the selected reservation?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        DeleteEvent?.Invoke(this, EventArgs.Empty);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a reservation to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            btnCheckIn.Click += delegate
            {
                if (dataGridReservation.SelectedRows.Count > 0)
                {
                    ShowCheckInOutView?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Please select a reservation first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
        }

        #endregion

        #region Properties

        public string ReservationId { get => txtReservationId.Texts; set => txtReservationId.Texts = value ?? string.Empty; }
        public string Guests { get => txtRoomGuests.Texts; set => txtRoomGuests.Texts = value ?? string.Empty; }
        public DateTime CheckInDate { get => dtCheckIn.Content; set => dtCheckIn.Content = value; }
        public DateTime CheckOutDate { get => dtCheckOut.Content; set => dtCheckOut.Content = value; }
        public string CustomerName { get => txtCusName.Texts; set => txtCusName.Texts = value ?? string.Empty; }
        public string RoomNumber { get => cbNumber.SelectedItem?.ToString(); set => SetComboBoxItem(cbNumber, value); }
        public string RoomType { get => cbType.SelectedItem?.ToString(); set => SetComboBoxItem(cbType, value); }
        public string TotalPrice { get => txtPrice.Texts; set => txtPrice.Texts = value ?? "0.00"; }
        public string ReservationStatus { get => cbStatus.SelectedItem?.ToString(); set => SetComboBoxItem(cbStatus, value); }
        public string SearchValue { get => txtReservationSearch.Texts; set => txtReservationSearch.Texts = value ?? string.Empty; }
        public string DownPayment { get => txtDownPayment.Texts; set => txtDownPayment.Texts = value ?? "0.00"; }
        public string AmountPaid { get => txtAmountPaid.Texts; set => txtAmountPaid.Texts = value ?? "0.00"; }
        public string PaymentMethod { get => cbPaymentType.SelectedItem?.ToString(); set => SetComboBoxItem(cbPaymentType, value, true); }
        public string BalanceDue { get => txtBalanceDue.Texts; set => txtBalanceDue.Texts = value ?? "0.00"; }
        public bool isSuccessful { get; set; }
        public bool isEdit { get; set; }
        public string Message { get; set; }

        public PaymentState PaymentStatus
        {
            get => Enum.TryParse(cbPaymentStatus.SelectedItem?.ToString(), out PaymentState status) ? status : PaymentState.Pending;
            set => SetComboBoxItem(cbPaymentStatus, value.ToString());
        }

        #endregion

        #region Singleton

        public static void ResetInstance() => UserControlFactory<UCReservation>.ResetInstance();
        public static UCReservation GetInstance(Form parentContainer) => UserControlFactory<UCReservation>.GetInstance(parentContainer);

        #endregion

        #region Public Methods

        public void SetReservationListBindingSource(BindingSource reservationList) => dataGridReservation.DataSource = reservationList;

        public void LoadAvailableRooms(IEnumerable<RoomModel> rooms)
        {
            var selectedRoom = cbNumber.SelectedItem;
            availableRooms = rooms?.ToList() ?? new List<RoomModel>();
            cbNumber.Items.Clear();

            if (availableRooms.Any())
                cbNumber.Items.AddRange(availableRooms.Select(r => r.RoomNumber).ToArray());

            if (selectedRoom != null && cbNumber.Items.Contains(selectedRoom))
                cbNumber.SelectedItem = selectedRoom;
            else
            {
                cbNumber.SelectedIndex = -1;
                txtRoomGuests.Texts = string.Empty;
            }
        }

        public void PopulateEditForm(ReservationModel reservation, RoomModel room)
        {
            if (reservation == null) return;

            ReservationId = reservation.ReservationId.ToString();
            CustomerName = reservation.CustomerName;
            CheckInDate = reservation.CheckInDate;
            CheckOutDate = reservation.CheckOutDate;
            TotalPrice = reservation.TotalPrice.ToString("0.00");
            DownPayment = reservation.DownPayment.ToString("0.00");
            AmountPaid = reservation.AmountPaid.ToString("0.00");
            BalanceDue = (reservation.TotalPrice - reservation.AmountPaid).ToString("0.00");
            ReservationStatus = reservation.ReservationStatus;
            PaymentStatus = reservation.PaymentStatus;
            PaymentMethod = reservation.PaymentMethod;

            if (room != null && !string.IsNullOrEmpty(reservation.RoomNumber))
            {
                RoomType = room.RoomType;
                if (!cbNumber.Items.Contains(reservation.RoomNumber))
                    cbNumber.Items.Add(reservation.RoomNumber);
                RoomNumber = reservation.RoomNumber;
                Guests = room.RoomGuests;
            }
        }

        public void ShowTab(int tabIndex)
        {
            if (tabIndex == 0)
            {
                if (materialTabControl1.TabPages.Contains(tabPage2))
                    materialTabControl1.TabPages.Remove(tabPage2);
                if (!materialTabControl1.TabPages.Contains(tabPage1))
                    materialTabControl1.TabPages.Add(tabPage1);
                materialTabControl1.SelectedTab = tabPage1;
            }
            else
            {
                if (materialTabControl1.TabPages.Contains(tabPage1))
                    materialTabControl1.TabPages.Remove(tabPage1);
                if (!materialTabControl1.TabPages.Contains(tabPage2))
                    materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.SelectedTab = tabPage2;
            }
        }

        public void EnableField(string fieldName, bool enabled)
        {
            switch (fieldName)
            {
                case "CustomerName": txtCusName.Enabled = enabled; break;
                case "RoomType": cbType.Enabled = enabled; break;
                case "RoomNumber": cbNumber.Enabled = enabled; break;
            }
        }

        public void SetOriginalDates(DateTime checkIn, DateTime checkOut)
        {
            originalCheckInDate = checkIn;
            originalCheckOutDate = checkOut;
        }

        public void SetOriginalRoomNumber(string roomNumber) => originalRoomNumber = roomNumber;

        public int GetSelectedReservationId()
        {
            if (dataGridReservation.SelectedRows.Count > 0)
                return Convert.ToInt32(dataGridReservation.SelectedRows[0].Cells["ReservationId"].Value);
            return 0;
        }

        public void ClearForm()
        {
            FieldsCleaner.ClearInputs(this);
            availableRooms.Clear();
            cbNumber.Items.Clear();
            cbType.SelectedIndex = -1;
        }

        public void ShowSuccessMessage(string message) =>
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public void ShowErrorMessage(string message) =>
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void TriggerSetCustomerForReservation(string customerName) =>
            SetCustomerForReservationEvent?.Invoke(this, customerName);

        #endregion

        #region Private Methods

        private void InitializePaymentDropdowns()
        {
            cbPaymentStatus.Items.Clear();
            foreach (PaymentState status in Enum.GetValues(typeof(PaymentState)))
                cbPaymentStatus.Items.Add(status.ToString());

            cbPaymentType.Items.Clear();
            foreach (PaymentMethod method in Enum.GetValues(typeof(PaymentMethod)))
                cbPaymentType.Items.Add(method.ToString());
        }

        private void InitializeRoomTypeComboBox()
        {
            cbType.Items.Clear();
            cbType.Items.AddRange(new[] { "Standard", "Deluxe", "Suite", "Family", "Single" });
        }

        private void InitializeRoomStatusComboBox()
        {
            cbStatus.Items.Clear();
            cbStatus.Items.AddRange(new[] { "Pending", "CheckedIn", "CheckedOut", "Cancelled/No Show", "Reserved" });
        }

        private void OnRoomTypeChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;

            string selectedType = cbType.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedType))
                RoomTypeChangedEvent?.Invoke(this, selectedType);
            else
            {
                cbNumber.Items.Clear();
                availableRooms.Clear();
                txtRoomGuests.Texts = string.Empty;
            }
        }

        private void cbNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;

            string selectedNumber = cbNumber.SelectedItem as string;
            var selectedRoom = availableRooms.FirstOrDefault(r => r.RoomNumber == selectedNumber);
            txtRoomGuests.Texts = selectedRoom?.RoomGuests ?? string.Empty;
        }

        private void DateOrRoomChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;
            
             if (sender == dtCheckIn || sender == dtCheckOut)
            {
                if (isEdit && dtCheckIn.Content.Date == originalCheckInDate.Date && dtCheckOut.Content.Date == originalCheckOutDate.Date)
                {
                }
                else
                {
                    string selectedType = cbType.SelectedItem as string;
                    if (!string.IsNullOrEmpty(selectedType))
                    {
                        RoomTypeChangedEvent?.Invoke(this, selectedType);
                    }
                }
            }

            var selectedRoom = availableRooms.FirstOrDefault(r => r.RoomNumber == (cbNumber.SelectedItem as string));
            if (selectedRoom == null)
            {
                txtPrice.Texts = "0.00";
                txtDownPayment.Texts = "0.00";
                txtBalanceDue.Texts = "0.00";
                return;
            }

            decimal.TryParse(selectedRoom.RoomPrice, out decimal pricePerNight);
            int nights = Math.Max(1, (int)(dtCheckOut.Content.Date - dtCheckIn.Content.Date).TotalDays);
            decimal total = pricePerNight * nights;
            decimal downPayment = RoomRateHelper.GetAutoDownPayment(total);
            decimal.TryParse(txtAmountPaid.Texts, out decimal currentAmountPaid);

            if (currentAmountPaid == 0)
                currentAmountPaid = downPayment;

            txtPrice.Texts = total.ToString("0.00");
            txtDownPayment.Texts = downPayment.ToString("0.00");
            txtAmountPaid.Texts = currentAmountPaid.ToString("0.00");
            txtBalanceDue.Texts = (total - currentAmountPaid).ToString("0.00");
        }

        private void SetComboBoxItem(ComboBox comboBox, string value, bool addIfMissing = false)
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

            if (addIfMissing)
            {
                comboBox.Items.Add(value);
                comboBox.SelectedItem = value;
            }
        }

        #endregion
    }
}