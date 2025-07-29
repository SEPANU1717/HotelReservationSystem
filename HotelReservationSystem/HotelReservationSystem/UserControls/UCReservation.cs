using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Interface.Reservation;
using HotelReservationSystem.Interface.Rooms;
using HotelReservationSystem.Model.Rooms;
using HotelReservationSystem.Repositories;
using HotelReservationSystem.Repositories.Rooms;

namespace HotelReservationSystem.UserControls
{
    public partial class UCReservation : UserControl, IReservationView
    {
        private List<string> comboItems;
        private ReservationRepository reserveRepo;
        private RoomRepository roomRepo;
        private CustomerRepository customerRepo;

        // Store only the available rooms for the selected type
        private List<RoomModel> availableRooms = new List<RoomModel>();

        public UCReservation()
        {
            InitializeComponent();
            materialTabControl1.TabPages.Remove(tabPage2);
            AssociateAndraiseViewEvents();
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            customerRepo = new CustomerRepository(connectionString);
            reserveRepo = new ReservationRepository(connectionString);
            roomRepo = new RoomRepository(connectionString);
            InitializeCustomerComboBox();
            InitializeRoomTypeComboBox();
            InitializeRoomStatusComboBox();
            cbNumber.SelectedIndexChanged += cbNumber_SelectedIndexChanged;
            cbType.SelectedIndexChanged += cbType_SelectedIndexChanged;
            dtCheckIn.ValueChanged += DateOrRoomChanged;
            dtCheckOut.ValueChanged += DateOrRoomChanged;
            cbNumber.SelectedIndexChanged += DateOrRoomChanged; 

        }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        private void AssociateAndraiseViewEvents()
        {
            btnReservationSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtReservationSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            btnReservationAddNew.Click += delegate
            {
                txtReservationId.Texts = reserveRepo.GetNextReservationId().ToString();
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Add new room";
            };

            btnReservationEdit.Click += delegate
            {
                if (dataGridReservation.SelectedRows.Count > 0)
                {
                    int reservationId = Convert.ToInt32(dataGridReservation.SelectedRows[0].Cells["ReservationId"].Value);
                    // Duplicate block
                    if (dataGridReservation.SelectedRows.Count > 0)
                    {
                        int ReservationId = Convert.ToInt32(dataGridReservation.SelectedRows[0].Cells["ReservationId"].Value);
                        LoadReservationForEdit(reservationId);
                    }
                    LoadReservationForEdit(reservationId);
                    EditEvent?.Invoke(this, EventArgs.Empty);
                    materialTabControl1.TabPages.Remove(tabPage1);
                    materialTabControl1.TabPages.Add(tabPage2);
                    materialTabControl1.Text = "Edit room";
                }
                else
                {
                    MessageBox.Show("Please select a reservation to edit.");
                }
            };



            btnReservationSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    // Update room status to "Occupied"
                    string selectedRoomNumber = cbNumber.SelectedItem as string;
                    if (!string.IsNullOrEmpty(selectedRoomNumber))
                    {
                        var room = roomRepo.GetAll().FirstOrDefault(r => r.RoomNumber == selectedRoomNumber);
                        if (room != null)
                        {
                            room.RoomStatus = "Occupied";
                            roomRepo.Edit(room);
                        }
                    }

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
                var result = MessageBox.Show("Are you sure you want to delete the selected room?", "Warning",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }

        public string ReservationId { get => txtReservationId.Texts; set => txtReservationId.Texts = value; }
        public string Guests { get => txtRoomGuests.Texts; set => txtRoomGuests.Texts = value; }
        public DateTime CheckInDate { get => dtCheckIn.Value; set => dtCheckIn.Value = value; }
        public DateTime CheckOutDate { get => dtCheckOut.Value; set => dtCheckOut.Value = value; }
        public string SearchValue { get => txtReservationSearch.Texts; set => txtReservationSearch.Texts = value; }
        public bool isSuccessful { get; set; }
        public bool isEdit { get; set; }
        public string Message { get; set; }
        public string CustomerName
        {
            get => cbCusName.SelectedItem as string;
            set
            {
                int index = cbCusName.Items.Cast<object>().ToList().IndexOf(value);
                if (index >= 0) cbCusName.SelectedIndex = index;
            }
        }
        public string RoomNumber
        {
            get => cbNumber.SelectedItem as string;
            set
            {
                if (cbNumber.Items != null)
                {
                    int index = cbNumber.Items.Cast<object>().ToList().IndexOf(value);
                    if (index >= 0) cbNumber.SelectedIndex = index;
                }
            }
        }
        public string RoomType
        {
            get => cbType.SelectedItem as string;
            set
            {
                int index = cbType.Items.Cast<object>().ToList().IndexOf(value);
                if (index >= 0) cbType.SelectedIndex = index;
            }
        }
        public string TotalPrice { get => txtPrice.Texts; set => txtPrice.Texts = value; }
        public string ReservationStatus
        {
            get => cbStatus.SelectedItem as string;
            set
            {
                if (cbStatus.Items != null)
                {
                    int index = cbStatus.Items.Cast<object>().ToList().IndexOf(value);
                    if (index >= 0) cbStatus.SelectedIndex = index;
                }
            }
        }



        private static UCReservation _instance;
        public static void ResetInstance()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        public static UCReservation GetInstance(Form parentContainer)
        {
            if (_instance == null || _instance.IsDisposed || _instance.Parent == null)
            {
                _instance = new UCReservation();
            }

            _instance.Dock = DockStyle.Fill;
            return _instance;
        }

        public void SetReservationListBindingSource(BindingSource reservationList)
        {
            dataGridReservation.DataSource = reservationList;
        }

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

        private void InitializeCustomerComboBox()
        {
            cbCusName.Items.Clear();
            cbCusName.Items.AddRange(customerRepo.GetAllCustomerNames().ToArray());
        }

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
            "Pending"
            });
        }
        private void btnReservationSave_Click(object sender, EventArgs e)
        {
            if (isSuccessful)
            {
                string selectedRoomNumber = cbNumber.SelectedItem as string;
                if (!string.IsNullOrEmpty(selectedRoomNumber))
                {
                    var room = roomRepo.GetAll().FirstOrDefault(r => r.RoomNumber == selectedRoomNumber);
                    if (room != null)
                    {
                        room.RoomStatus = "Occupied";
                        roomRepo.Edit(room);
                    }
                }
            }
        }

        private void DateOrRoomChanged(object sender, EventArgs e)
        {
            string selectedNumber = cbNumber.SelectedItem as string;
            var selectedRoom = availableRooms.FirstOrDefault(r => r.RoomNumber == selectedNumber);

            if (selectedRoom != null)
            {
                decimal pricePerNight = 0;
                decimal.TryParse(selectedRoom.RoomPrice, out pricePerNight);

                int nights = (int)(dtCheckOut.Value.Date - dtCheckIn.Value.Date).TotalDays;
                if (nights < 1) nights = 1; 

                decimal total = pricePerNight * nights;
                txtPrice.Texts = total.ToString("0.00");
            }
            else
            {
                txtPrice.Texts = "0.00";
            }
        }
        public void LoadReservationForEdit(int reservationId)
        {
            var reservation = reserveRepo.GetById(reservationId);
            if (reservation != null)
            {
                txtReservationId.Texts = reservation.ReservationId.ToString();
                CustomerName = reservation.CustomerName;
                CheckInDate = reservation.CheckInDate;
                CheckOutDate = reservation.CheckOutDate;
                TotalPrice = reservation.TotalPrice.ToString("0.00");
                ReservationStatus = reservation.ReservationStatus;



                }
            }
        }
    }

