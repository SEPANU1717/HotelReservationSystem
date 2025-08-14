using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HotelReservationSystem.DataInitializer;
using HotelReservationSystem.Helper;
using HotelReservationSystem.Interface.Rooms;
using HotelReservationSystem.Repositories;

namespace HotelReservationSystem.UserControls
{
    public partial class UCRooms : UserControl, IRoomView
    {
        #region Fields

        private List<string> comboItems;
        RoomRepository roomRepo;
        private static UCRooms _instance;

        #endregion
        #region Constructor

        public UCRooms()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            materialTabControl1.TabPages.Remove(tabPage2);
            InitializeComboBox();
            roomRepo = new RoomRepository(DbConfig.GetConnectionString());
        }

        #endregion
        #region Event Association

        private void AssociateAndRaiseViewEvents()
        {
            btnRoomSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtRoomSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            btnRoomAddNew.Click += delegate
            {
                if (materialTabControl1.SelectedTab == tabPage2)
                {
                    MessageBox.Show(@"You are already in the Add Room menu.", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ClearRoomFields();
                txtRoomId.Texts = roomRepo.GetNextRoomId().ToString();
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = @"Add new room";
            };

            btnRoomEdit.Click += delegate
            {
                if (materialTabControl1.SelectedTab == tabPage2)
                {
                    MessageBox.Show(@"You are already in the Edit Room menu.", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                EditEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = @"Edit room";
            };

            btnRoomSave.Click += delegate
            {
                if (txtRoomNumber.Texts is "STD" || txtRoomNumber.Texts is "FML" || txtRoomNumber.Texts is "DLX" ||
                    txtRoomNumber.Texts is "SGL" || txtRoomNumber.Texts is "ST")
                {MessageBox.Show(@"Invalid room number"); return;}

                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    ClearRoomFields();
                    isEdit = false;
                    materialTabControl1.TabPages.Remove(tabPage2);
                    materialTabControl1.TabPages.Add(tabPage1);
                }
                MessageBox.Show(Message);
            };

            btnRoomCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage2);
                materialTabControl1.TabPages.Add(tabPage1);
            };

            btnRoomDelete.Click += delegate
            {
                var result = MessageBox.Show(@"Are you sure you want to delete the selected room?", @"Warning",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };

            btnRefresh.Click += (s, e) => SyncRoomStatuses();
        }

        #endregion
        #region Properties

        public string RoomId { get => txtRoomId.Texts; set => txtRoomId.Texts = value; }
        public string RoomNumber { get => txtRoomNumber.Texts; set => txtRoomNumber.Texts = value; }
        public string RoomType { get => txtRoomType.Texts; set => txtRoomType.Texts = value; }
        public string RoomPrice { get => txtRoomPrice.Texts; set => txtRoomPrice.Texts = value; }
        public string BedCount { get => txtBedCount.Texts; set => txtBedCount.Texts = value; }
        public string RoomGuests { get => txtRoomGuests.Texts; set => txtRoomGuests.Texts = value; }
        public string RoomDescription { get => txtDescription.Texts; set => txtDescription.Texts = value; }
        public string SearchValue { get => txtRoomSearch.Texts; set => txtRoomSearch.Texts = value; }
        public bool isSuccessful { get; set; }
        public bool isEdit { get; set; }
        public string Message { get; set; }

        public string RoomStatus
        {
            get => cboRoomStatus.SelectedItem;
            set
            {
                int index = Array.IndexOf(cboRoomStatus.Items, value);
                if (index >= 0) cboRoomStatus.SelectedIndex = index;
            }
        }

        #endregion
        #region Events

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        #endregion
        #region Singleton

        public static void ResetInstance()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        public static UCRooms GetInstance(Form parentContainer)
        {
            if (_instance == null || _instance.IsDisposed || _instance.Parent == null)
            {
                _instance = new UCRooms();
            }

            _instance.Dock = DockStyle.Fill;
            return _instance;
        }

        #endregion
        #region Methods

        private void InitializeComboBox()
        {
            comboItems = new List<string>()
           {
               "Available",
               "Occupied",
               "Under maintenance"
           };

            cboRoomStatus.Items = comboItems.ToArray();
        }

        public void SetRoomListBindingSource(BindingSource customerList)
        {
            dataGridRoom.DataSource = customerList;
        }

        private void ClearRoomFields()
        {
            txtRoomId.Texts = "";
            txtRoomNumber.Texts = "";
            txtRoomType.Texts = "";
            txtRoomPrice.Texts = "";
            txtBedCount.Texts = "";
            txtRoomGuests.Texts = "";
            txtDescription.Texts = "";
            cboRoomStatus.SelectedIndex = -1;
        }

        private void SyncRoomStatuses()
        {
            var reserveRepo = new ReservationRepository(DbConfig.GetConnectionString());
            foreach (var room in roomRepo.GetAll())
            {
                roomRepo.SyncRoomStatusesWithReservations(room.RoomNumber, reserveRepo);
            }
            MessageBox.Show("Room statuses have been synchronized with reservations.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStandardRoom_Click(object sender, EventArgs e) => RoomInitializer.StandardRoom(this);
        private void btnDeluxeRoom_Click(object sender, EventArgs e) => RoomInitializer.DeluxeRoom(this);
        private void btnSuiteRoom_Click(object sender, EventArgs e) => RoomInitializer.SuiteRoom(this);
        private void btnfamilyRoom_Click(object sender, EventArgs e) => RoomInitializer.FamilyRoom(this);
        private void btnSingleRoom_Click(object sender, EventArgs e) => RoomInitializer.SingleRoom(this);

        #endregion
    }
}