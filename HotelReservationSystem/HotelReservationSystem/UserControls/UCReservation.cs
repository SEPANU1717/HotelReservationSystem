using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Domain.Helper;
using HotelReservationSystem.Domain.Interface.Reservation;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Forms;
using HotelReservationSystem.Presenter.Common;
using static HotelReservationSystem.Domain.Enums.ReservationEnum;

namespace HotelReservationSystem.UserControls
{
    public partial class UCReservation : UserControl, IReservationView
    {
        #region Fields

        private ReservationRepository reserveRepo;
        private RoomRepository roomRepo;
        private CustomerRepository customerRepo;
        private DateTime originalCheckInDate;
        private DateTime originalCheckOutDate;
        private List<RoomModel> availableRooms = new List<RoomModel>();
        private string originalRoomNumber;

        public TabPage ReservationTabPage => tabPage2;
        public MaterialSkin.Controls.MaterialTabControl ReservationTabControl => materialTabControl1;


        #endregion

        #region Constructor

        public UCReservation()
        {
            InitializeComponent();
            materialTabControl1.TabPages.Remove(tabPage2);
            cbPaymentStatus.DataSource = Enum.GetValues(typeof(PaymentState));
            cbPaymentType.DataSource = Enum.GetValues(typeof(PaymentMethod));
            AssociateAndraiseViewEvents();
            customerRepo = new CustomerRepository(DbConfig.GetConnectionString());
            reserveRepo = new ReservationRepository(DbConfig.GetConnectionString());
            roomRepo = new RoomRepository(DbConfig.GetConnectionString());
            //InitializeCustomerComboBox();
            InitializeRoomTypeComboBox();
            InitializeRoomStatusComboBox();
            cbNumber.SelectedIndexChanged += cbNumber_SelectedIndexChanged;
            cbType.SelectedIndexChanged += cbType_SelectedIndexChanged;
            dtCheckIn.DateChanged += DateOrRoomChanged;
            dtCheckOut.DateChanged += DateOrRoomChanged;
            cbNumber.SelectedIndexChanged += DateOrRoomChanged;
            dtCheckIn.Content = DateTime.Now;
            dtCheckOut.Content = DateTime.Now.AddDays(1);
            //Reload();
            UserInfoDisplay.UpdateUserInfoDisplay(lblUsername, lblRole, pictureProfile);
            
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

        #endregion
        #region Event Association

        private void AssociateAndraiseViewEvents()
        {
            btnReservationSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtReservationSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            //btnReservationAddNew.Click += delegate
            //{
            //    if (materialTabControl1.SelectedTab == tabPage2)
            //    {
            //        MessageBox.Show("You are already in the Add Reservation menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //    txtReservationId.Texts = reserveRepo.GetNextReservationId().ToString();
            //    AddNewEvent?.Invoke(this, EventArgs.Empty);
            //    materialTabControl1.TabPages.Remove(tabPage1);
            //    materialTabControl1.TabPages.Add(tabPage2);
            //    materialTabControl1.Text = "Add new room";

            //    cbStatus.SelectedItem = "Pending";
            //    cbStatus.Enabled = false;
            //    txtCusName.Enabled = true;
            //};

            btnReservationEdit.Click += delegate
            {
                if (materialTabControl1.SelectedTab == tabPage2)
                {
                    MessageBox.Show("You are already in the Edit Reservation menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dataGridReservation.SelectedRows.Count > 0)
                {
                    int reservationId =
                        Convert.ToInt32(dataGridReservation.SelectedRows[0].Cells["ReservationId"].Value);
                    LoadReservationForEdit(reservationId);
                    EditEvent?.Invoke(this, EventArgs.Empty);
                    materialTabControl1.TabPages.Remove(tabPage1);
                    materialTabControl1.TabPages.Add(tabPage2);
                    materialTabControl1.Text = "Edit room";
                    cbStatus.Enabled = true;
                    txtCusName.Enabled = false;
                }
                else
                {
                    MessageBox.Show(@"Please select a reservation to edit.");
                }
            };

            btnReservationSave.Click += delegate
            {
                string selectedRoomNumber = cbNumber.SelectedItem as string;
                RoomNumber = selectedRoomNumber;

                if (isEdit && originalRoomNumber != RoomNumber)
                {
                    ReservationStatus = "Pending";
                    cbStatus.SelectedItem = "Pending";
                }

                SaveEvent?.Invoke(this, EventArgs.Empty);

                if (isSuccessful && ReservationStatus == "Canceled" && !string.IsNullOrEmpty(RoomNumber))
                {
                    var room = roomRepo.GetByNumber(RoomNumber);
                    if (room != null)
                    {
                        room.RoomStatus = "Available";
                        roomRepo.Edit(room);
                    }
                }

                if (isSuccessful)
                {
                    isEdit = false;
                    materialTabControl1.TabPages.Remove(tabPage2);
                    materialTabControl1.TabPages.Add(tabPage1);
                    cbType_SelectedIndexChanged(null, null);
                }

                MessageBox.Show(Message);
            };

            btnReservationCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage2);
                materialTabControl1.TabPages.Add(tabPage1);
            };

            btnReservationDelete.Click += delegate
            {
                if (dataGridReservation.SelectedRows.Count > 0)
                {
                    int reservationId = Convert.ToInt32(dataGridReservation.SelectedRows[0].Cells["ReservationId"].Value);

                    var reservation = reserveRepo.GetById(reservationId);
                    string roomNumber = reservation?.RoomNumber;

                    var result = MessageBox.Show("Are you sure you want to delete the selected reservation?", "Warning",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        DeleteEvent?.Invoke(this, EventArgs.Empty);

                        if (!string.IsNullOrEmpty(roomNumber))
                            roomRepo.SyncRoomStatusesWithReservations(roomNumber, reserveRepo);

                        MessageBox.Show(Message);
                    }
                }
                else
                {
                    MessageBox.Show(@"Please select a reservation to delete.");
                }
            };

            btnCheckIn.Click += delegate
            {
                var mainForm = FindForm() as ReservationSystem;
                if (mainForm != null) { mainForm.LoadUserControl(new UCINOUT()); }
                else { MessageBox.Show("Main form not found.", "Error"); }
            };
        }


        #endregion
            #region Properties

        public string ReservationId { get => txtReservationId.Texts; set => txtReservationId.Texts = value; }
        public string Guests { get => txtRoomGuests.Texts; set => txtRoomGuests.Texts = value; }
        public DateTime CheckInDate { get => dtCheckIn.Content; set => dtCheckIn.Content = value; }
        public DateTime CheckOutDate { get => dtCheckOut.Content; set => dtCheckOut.Content = value; }
        public string CustomerName { get => txtCusName.Texts; set => txtCusName.Texts = value; }
        public string RoomNumber { get => cbNumber.SelectedItem as string; set => cbNumber.SelectedItem = value; }
        public string RoomType { get => cbType.SelectedItem as string; set => cbType.SelectedItem = value; }
        public string TotalPrice { get => txtPrice.Texts; set => txtPrice.Texts = value; }
        public string ReservationStatus { get => cbStatus.SelectedItem as string; set => cbStatus.SelectedItem = value; }
        public string SearchValue { get => txtReservationSearch.Texts; set => txtReservationSearch.Texts = value; }
        public bool isSuccessful { get; set; }
        public bool isEdit { get; set; }
        public string Message { get; set; }
        public string DownPayment { get => txtDownPayment.Texts; set => txtDownPayment.Texts = value; }
        public string AmountPaid { get => txtAmountPaid.Texts; set => txtAmountPaid.Texts = value; }
        public string PaymentMethod { get => cbPaymentType.SelectedItem as string; set => cbPaymentType.SelectedItem = value; }
        public PaymentState PaymentStatus { get => (PaymentState)cbPaymentStatus.SelectedItem ; set => cbPaymentStatus.SelectedItem = value; }
        public string BalanceDue { get => txtBalanceDue.Texts; set => txtBalanceDue.Texts = value; }

        #endregion
        #region Singleton

        public static void ResetInstance() =>
            UserControlFactory<UCReservation>.ResetInstance();

        public static UCReservation GetInstance(Form parentContainer) =>
            UserControlFactory<UCReservation>.GetInstance(parentContainer);

        #endregion
        #region Public Methods

        public void SetReservationListBindingSource(BindingSource reservationList) =>
            dataGridReservation.DataSource = reservationList;

        public void LoadReservationForEdit(int reservationId)
        {
            var reservation = reserveRepo.GetById(reservationId);
            if (reservation != null)
            {
                txtReservationId.Texts = reservation.ReservationId.ToString();
                originalRoomNumber = reservation.RoomNumber;

                txtCusName.Texts = reservation.CustomerName;

                CheckInDate = reservation.CheckInDate;
                CheckOutDate = reservation.CheckOutDate;
                originalCheckInDate = reservation.CheckInDate;
                originalCheckOutDate = reservation.CheckOutDate;
                TotalPrice = reservation.TotalPrice.ToString("0.00");
                ReservationStatus = reservation.ReservationStatus;

                if (!string.IsNullOrEmpty(reservation.RoomNumber))
                {
                    var room = roomRepo.GetByNumber(reservation.RoomNumber);
                    if (room != null)
                    {
                        RoomType = room.RoomType;
                        RoomNumber = reservation.RoomNumber;
                        if (cbNumber.SelectedItem == null && !string.IsNullOrEmpty(reservation.RoomNumber))
                        {
                            cbNumber.Items.Add(reservation.RoomNumber);
                            cbNumber.SelectedItem = reservation.RoomNumber;
                        }
                    }
                }
            }
        }

        #endregion
        #region Private Methods

        private void InitializeRoomTypeComboBox()
        {
            cbType.Items.Clear();
            cbType.Items.AddRange(new string[]
            {
                "Standard",
                "Deluxe",
                "Suite",
                "Family",
                "Single"
            });
        }

        //private void InitializeCustomerComboBox()
        //{
        //    cbCusNames.Items.Clear();
        //    var customerNames = customerRepo.GetCustomerNamesWithoutReservation();
        //    if (customerNames != null)
        //        cbCusNames.Items.AddRange(customerNames.ToArray());
        //}

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = cbType.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedType))
            {
                availableRooms = roomRepo.GetAvailableRoomsByType(selectedType).ToList();
                cbNumber.Items.Clear();
                cbNumber.Items.AddRange(availableRooms.Select(r => r.RoomNumber).ToArray());
                cbNumber.SelectedIndex = -1;
                txtRoomGuests.Texts = string.Empty;
            }
            else
            {
                cbNumber.Items.Clear();
                availableRooms.Clear();
                txtRoomGuests.Texts = string.Empty;
            }
        }

        private void cbNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedNumber = cbNumber.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedNumber))
            {
                var selectedRoom = availableRooms.FirstOrDefault(r => r.RoomNumber == selectedNumber);
                if (selectedRoom != null)
                {
                    txtRoomGuests.Texts = selectedRoom.RoomGuests;
                }
                else
                {
                    txtRoomGuests.Texts = string.Empty;
                }
            }
            else
            {
                txtRoomGuests.Texts = string.Empty;
            }
        }

        private void InitializeRoomStatusComboBox()
        {
            cbStatus.Items.Clear();
            cbStatus.Items.AddRange(new string[]
            {
                "Reserved",
                "Pending",
                "Canceled"
            });
        }

        private void DateOrRoomChanged(object sender, EventArgs e)
        {
            string selectedNumber = cbNumber.SelectedItem as string;
            var selectedRoom = availableRooms.FirstOrDefault(r => r.RoomNumber == selectedNumber);
            if (isEdit && dtCheckIn.Content.Date == originalCheckInDate.Date && dtCheckOut.Content.Date == originalCheckOutDate.Date)
            {
                return;
            }

            if (selectedRoom != null)
            {
                decimal pricePerNight = 0;
                decimal.TryParse(selectedRoom.RoomPrice, out pricePerNight);

                int nights = (int)(dtCheckOut.Content.Date - dtCheckIn.Content.Date).TotalDays;
                if (nights < 1) nights = 1;

                decimal total = pricePerNight * nights;
                txtPrice.Texts = total.ToString("0.00");

                txtDownPayment.Texts = RoomRateHelper.GetAutoDownPayment(total).ToString("0.00");

                txtBalanceDue.Texts = (total - RoomRateHelper.GetAutoDownPayment(total)).ToString("0.00");
            }
            else
            {
                txtPrice.Texts = "0.00";
            }
        }
        public void SetCustomerForReservation(string customerName)
        {
            CustomerName = customerName;
            var reservation = reserveRepo.GetByCustomerName(customerName);

            if (reservation != null)
            {
                LoadReservationForEdit(reservation.ReservationId);
                isEdit = true;
                cbStatus.Enabled = true;
                txtCusName.Enabled = false;
            }
            else
            {
                // Prepare new reservation
                txtReservationId.Texts = reserveRepo.GetNextReservationId().ToString();
                isEdit = false;
                cbStatus.SelectedItem = "Pending";
                cbStatus.Enabled = false;
                txtCusName.Enabled = true;

                // Notify presenter we are adding a new reservation
                AddNewEvent?.Invoke(this, EventArgs.Empty);
            }

            // Switch to tabPage2
            if (!materialTabControl1.TabPages.Contains(tabPage2))
                materialTabControl1.TabPages.Add(tabPage2);

            if (materialTabControl1.TabPages.Contains(tabPage1))
                materialTabControl1.TabPages.Remove(tabPage1);

            materialTabControl1.SelectedTab = tabPage2;
            materialTabControl1.Text = "Add new room";
        }




        #endregion

        //private void Reload()
        //{
        //    var customersControl = UCCustomers.GetInstance(this.ParentForm);
        //    customersControl.CustomerChanged += (s, e) =>
        //    {
        //        InitializeCustomerComboBox();
        //    };
        //}
        private void dtCheckIn_Load(object sender, EventArgs e)
        {

        }
    }
}