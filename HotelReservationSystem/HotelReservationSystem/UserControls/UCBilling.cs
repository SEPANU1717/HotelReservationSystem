using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelReservationSystem.Interface.Billing;
using HotelReservationSystem.Model.Reservation;
using HotelReservationSystem.Repositories;
using HotelReservationSystem.Repositories.Billing;
using HotelReservationSystem.Repositories.Rooms;

namespace HotelReservationSystem.UserControls
{
    public partial class UCBilling : UserControl, IBillingView
    {
        BillingRepository billRepo;
        ReservationRepository reserveRepo;
        RoomRepository roomRepo;
        private bool isInEditMode = false;

        public UCBilling()
        {
            InitializeComponent();
            AssociateAndraiseViewEvents();
            materialTabControl1.TabPages.Remove(tabPage2);
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            billRepo = new BillingRepository(connectionString);
            reserveRepo = new ReservationRepository(connectionString);
            roomRepo = new RoomRepository(connectionString);
            cbReservationId.SelectedIndexChanged += cbReservationId_SelectedIndexChanged;
            InitializeRoomTypeComboBox();
        }

        private void LoadReservationIds()
        {
            var reservations = reserveRepo.GetAll().ToList();
            cbReservationId.DataSource = reservations;
            cbReservationId.DisplayMember = "ReservationId";
            cbReservationId.ValueMember = "ReservationId";
            cbReservationId.SelectedIndex = -1;
        }

        private void AssociateAndraiseViewEvents()
        {
            btnBillingSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtBillingSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            btnBillingAddNew.Click += delegate
            {
                isInEditMode = false;
                cbReservationId.Enabled = true;
                txtBillingId.Texts = billRepo.GetNextBillingId().ToString();
                LoadReservationIds();
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Add new billing";
            };

            btnBillingEdit.Click += delegate
            {
                isInEditMode = true;
                EditEvent?.Invoke(this, EventArgs.Empty);
                SetEditModeReservationId();
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Edit billing";
            };

            btnReservationPay.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    string billingStatus = cbStatus.SelectedItem as string;
                    string selectedReservationId = cbReservationId.SelectedValue?.ToString();
                    int reservationId;
                    if (int.TryParse(selectedReservationId, out reservationId))
                    {
                        var reservation = reserveRepo.GetById(reservationId);
                        if (reservation != null)
                        {
                            if (billingStatus == "Completed")
                                reservation.ReservationStatus = "Reserved";
                            else if (billingStatus == "Pending")
                                reservation.ReservationStatus = "Pending";

                            reserveRepo.Edit(reservation);

                            if (!string.IsNullOrEmpty(reservation.RoomNumber))
                            {
                                var room = roomRepo.GetByNumber(reservation.RoomNumber);
                                if (room != null)
                                {
                                    if (billingStatus == "Completed")
                                        room.RoomStatus = "Occupied";
                                    else if (billingStatus == "Pending")
                                        room.RoomStatus = "Available";
                                    roomRepo.Edit(room);
                                }
                            }
                        }
                    }

                    isEdit = false;
                    isInEditMode = false;
                    cbReservationId.Enabled = true;
                    materialTabControl1.TabPages.Remove(tabPage2);
                    materialTabControl1.TabPages.Add(tabPage1);
                }
                MessageBox.Show(Message);
            };

            btnBillingCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                isInEditMode = false;
                cbReservationId.Enabled = true;
                materialTabControl1.TabPages.Remove(tabPage2);
                materialTabControl1.TabPages.Add(tabPage1);
            };

            btnDeleteDelete.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected billing?", "Warning",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }

        private void SetEditModeReservationId()
        {
            string currentReservationId = ReservationId;

            if (string.IsNullOrEmpty(currentReservationId))
            {
                if (dataGridBilling.CurrentRow != null)
                {
                    var selectedRow = dataGridBilling.CurrentRow;
                    currentReservationId = selectedRow.Cells["ReservationId"]?.Value?.ToString();
                }
            }

            if (!string.IsNullOrEmpty(currentReservationId))
            {
                int reservationId;
                if (int.TryParse(currentReservationId, out reservationId))
                {
                    var currentReservation = reserveRepo.GetById(reservationId);
                    if (currentReservation != null)
                    {
                        var reservationList = new List<ReservationModel> { currentReservation };
                        cbReservationId.DataSource = reservationList;
                        cbReservationId.DisplayMember = "ReservationId";
                        cbReservationId.ValueMember = "ReservationId";
                        cbReservationId.SelectedValue = reservationId;

                        cbReservationId.Enabled = false;
                    }
                    else
                    {
                        cbReservationId.DataSource = null;
                        cbReservationId.Items.Clear();
                        cbReservationId.Text = currentReservationId;
                        cbReservationId.Enabled = false;
                        MessageBox.Show($"Reservation with ID {currentReservationId} not found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                cbReservationId.DataSource = null;
                cbReservationId.Items.Clear();
                cbReservationId.Text = "";
                cbReservationId.Enabled = false;
                MessageBox.Show("No reservation ID found for this billing record.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadBillingForEdit(int billId, int reservationId, string customerName, string roomType, string roomNumber, string totalAmount, string paymentStatus)
        {
            BillId = billId.ToString();

            cbReservationId.DataSource = null;
            cbReservationId.Items.Clear();
            cbReservationId.Items.Add(reservationId.ToString());
            cbReservationId.Text = reservationId.ToString();

            CustomerName = customerName ?? "";
            RoomType = roomType ?? "";
            RoomNumber = roomNumber ?? "";
            TotalAmount = totalAmount ?? "";
            PaymentStatus = paymentStatus ?? "";
        }

        public string BillId { get => txtBillingId.Texts; set => txtBillingId.Texts = value; }
        public string ReservationId
        {
            get => cbReservationId.SelectedValue?.ToString();
            set => cbReservationId.SelectedValue = value;
        }
        public string CustomerName { get => txtCusName.Texts; set => txtCusName.Texts = value; }
        public string RoomType { get => txtRoomType.Texts; set => txtRoomType.Texts = value; }
        public string RoomNumber { get => txtRoomNumber.Texts; set => txtRoomNumber.Texts = value; }
        public string TotalAmount { get => txtTotalAmount.Texts; set => txtTotalAmount.Texts = value; }
        public string PaymentStatus { get => cbStatus.SelectedItem as string; set => cbStatus.SelectedItem = value; }
        public string SearchValue { get => txtBillingSearch.Texts; set => txtBillingSearch.Texts = value; }
        public bool isSuccessful { get; set; }
        public bool isEdit { get; set; }
        public string Message { get; set; }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        public void SetBillingListBindingSource(BindingSource billingList)
        {
            dataGridBilling.DataSource = billingList;
        }

        private static UCBilling _instance;
        public static UCBilling GetInstance(Form parentContainer)
        {
            if (_instance == null || _instance.IsDisposed || _instance.Parent == null)
            {
                _instance = new UCBilling();
            }

            _instance.Dock = DockStyle.Fill;
            return _instance;
        }

        public static void ResetInstance()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        private void InitializeRoomTypeComboBox()
        {
            cbStatus.Items.Clear();
            cbStatus.Items.AddRange(new string[]
            {
                    "Completed",
                    "Pending"
            });
        }

        private void cbReservationId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInEditMode && cbReservationId.SelectedItem is ReservationModel reservation)
            {
                CustomerName = reservation.CustomerName ?? "";
                RoomNumber = reservation.RoomNumber?.Trim() ?? "";
                RoomType = "";
                TotalAmount = reservation.TotalPrice.ToString("F2");

                if (!string.IsNullOrEmpty(RoomNumber))
                {
                    var room = roomRepo.GetByNumber(RoomNumber);

                    if (room == null)
                        MessageBox.Show($"Room not found for number: {RoomNumber}", "Room Lookup Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        RoomType = room.RoomType ?? "";
                }
                else
                {
                    MessageBox.Show("RoomNumber is empty in reservation data.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}