using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.DataInitializer;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Domain.Interface.Rooms;
using HotelReservationSystem.Presenter.Common;
using static HotelReservationSystem.Domain.Enums.RoomEnum;

namespace HotelReservationSystem.UserControls
{
    public partial class UCRooms : UserControl, IRoomView
    {
        #region Fields

        private List<string> comboItems;
        RoomRepository roomRepo;
        ReservationRepository reserveRepo;

        #endregion

        #region Constructor

        public UCRooms()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            materialTabControl1.TabPages.Remove(tabPage2);
            cboRoomStatus.Items = Enum.GetNames(typeof(RoomAvailability));
            cbRoomFilter.DataSource = Enum.GetValues(typeof(RoomStatusFilter));
            roomRepo = new RoomRepository(DbConfig.GetConnectionString());
            reserveRepo = new ReservationRepository(DbConfig.GetConnectionString());
            UserInfoDisplay.UpdateUserInfoDisplay(lblUsername, lblRole, pictureProfile);
            
            dtFromDate.Content = DateTime.Today;
            dtToDate.Content = DateTime.Today.AddDays(1);
            
            ApplyRoleBasedRestrictions();
            UpdateSearchControlsState();
        }

        #endregion
        
        #region Role-Based Restrictions

        private void ApplyRoleBasedRestrictions()
        {
            if (!UserSession.IsAdmin)
            {
                btnRoomAddNew.Enabled = false;
                btnRoomEdit.Enabled = false;
                btnRoomDelete.Enabled = false;
                
                btnStandardRoom.Enabled = false;
                btnDeluxeRoom.Enabled = false;
                btnSuiteRoom.Enabled = false;
                btnfamilyRoom.Enabled = false;
                btnSingleRoom.Enabled = false;
            }
        }
        
        private void UpdateSearchControlsState()
        {
            bool isOnGridView = materialTabControl1.SelectedTab == tabPage1;
            
            if (txtRoomSearch != null)
            {
                txtRoomSearch.Enabled = isOnGridView;
            }
            
            if (btnRoomSearch != null)
            {
                btnRoomSearch.Enabled = isOnGridView;
            }
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
            
            cbRoomFilter.SelectedIndexChanged += delegate 
            { 
                FilterEvent?.Invoke(this, EventArgs.Empty); 
            };
            
            dtFromDate.DateChanged += (s, e) => ApplyDateFilter();
            dtToDate.DateChanged += (s, e) => ApplyDateFilter();

            btnRoomAddNew.Click += delegate
            {
                if (!UserSession.IsAdmin)
                {
                    MessageBox.Show(
                        $"{UserSession.Role} cannot add rooms. Only administrators can manage room inventory.",
                        "Access Denied",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                
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
                
                UpdateSearchControlsState();
            };

            btnRoomEdit.Click += delegate
            {
                if (!UserSession.IsAdmin)
                {
                    MessageBox.Show(
                        $"{UserSession.Role} cannot edit rooms. Only administrators can manage room inventory.",
                        "Access Denied",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                
                if (materialTabControl1.SelectedTab == tabPage2)
                {
                    MessageBox.Show(@"You are already in the Edit Room menu.", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                EditEvent?.Invoke(this, EventArgs.Empty);
                
                if (isEdit)
                {
                    materialTabControl1.TabPages.Remove(tabPage1);
                    materialTabControl1.TabPages.Add(tabPage2);
                    materialTabControl1.Text = @"Edit room";
                    
                    UpdateSearchControlsState();
                }
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
                    
                    UpdateSearchControlsState();
                }
                MessageBox.Show(Message);
            };

            btnRoomCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage2);
                materialTabControl1.TabPages.Add(tabPage1);
                
                UpdateSearchControlsState();
            };

            btnRoomDelete.Click += delegate
            {
                if (!UserSession.IsAdmin)
                {
                    MessageBox.Show(
                        $"{UserSession.Role} cannot delete rooms. Only administrators can manage room inventory.",
                        "Access Denied",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                
                var result = MessageBox.Show(@"Are you sure you want to delete the selected room?", @"Warning",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }

        #endregion
        
        #region Date Filtering

        private void ApplyDateFilter()
        {
            try
            {
                DateTime fromDate = dtFromDate.Content.Date;
                DateTime toDate = dtToDate.Content.Date;

                //if (fromDate > toDate)
                //{
                //    MessageBox.Show("From date cannot be after To date.", "Invalid Date Range", 
                //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                var allRooms = roomRepo.GetAll().ToList();
                var availableRooms = new List<Domain.Model.RoomModel>();

                foreach (var room in allRooms)
                {
                    bool hasReservation = reserveRepo.HasOverlappingReservation(
                        room.RoomNumber, 
                        fromDate, 
                        toDate);

                    if (!hasReservation)
                    {
                        availableRooms.Add(room);
                    }
                }

                dataGridRoom.DataSource = availableRooms;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error filtering rooms: {0}", ex.Message), "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        public string StatusFilter
        {
            get => cbRoomFilter.Text ?? "All";
            set
            {
                cbRoomFilter.Text = cbRoomFilter.Items.Cast<object>().Any(x => x.ToString() == value) ? value : "All";
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
        public event EventHandler FilterEvent;

        #endregion

        #region Singleton

        public static void ResetInstance() =>
            UserControlFactory<UCRooms>.ResetInstance();

        public static UCRooms GetInstance(Form parentContainer) =>
            UserControlFactory<UCRooms>.GetInstance(parentContainer);

        #endregion

        #region Methods

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

        private void btnStandardRoom_Click(object sender, EventArgs e)
        {
            if (!UserSession.IsAdmin)
            {
                MessageBox.Show("Only administrators can use room templates.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RoomInitializer.StandardRoom(this);
        }

        private void btnDeluxeRoom_Click(object sender, EventArgs e)
        {
            if (!UserSession.IsAdmin)
            {
                MessageBox.Show("Only administrators can use room templates.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RoomInitializer.DeluxeRoom(this);
        }

        private void btnSuiteRoom_Click(object sender, EventArgs e)
        {
            if (!UserSession.IsAdmin)
            {
                MessageBox.Show("Only administrators can use room templates.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RoomInitializer.SuiteRoom(this);
        }

        private void btnfamilyRoom_Click(object sender, EventArgs e)
        {
            if (!UserSession.IsAdmin)
            {
                MessageBox.Show("Only administrators can use room templates.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RoomInitializer.FamilyRoom(this);
        }

        private void btnSingleRoom_Click(object sender, EventArgs e)
        {
            if (!UserSession.IsAdmin)
            {
                MessageBox.Show("Only administrators can use room templates.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RoomInitializer.SingleRoom(this);
        }

        #endregion

        private void dtFromDate_Load(object sender, EventArgs e)
        {

        }

        private void dtToDate_Load(object sender, EventArgs e)
        {

        }
    }
}