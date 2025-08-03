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
using HotelReservationSystem.Helper;
using HotelReservationSystem.Interface.Rooms;
using HotelReservationSystem.Repositories.Rooms;

namespace HotelReservationSystem.UserControls
{
    public partial class UCRooms : UserControl, IRoomView
    {
        private List<string> comboItems;

        RoomRepository roomRepo;
        public UCRooms()
        {
            InitializeComponent();
            AssociateAndraiseViewEvents();
            materialTabControl1.TabPages.Remove(tabPage2);
            InitializeComboBox();
            roomRepo = new RoomRepository(DbConfig.GetConnectionString());

        }

        private void AssociateAndraiseViewEvents()
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
                    MessageBox.Show("You are already in the Add Room menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ClearRoomFields();
                txtRoomId.Texts = roomRepo.GetNextRoomId().ToString();
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Add new room";
            };

            btnRoomEdit.Click += delegate
            {
                if (materialTabControl1.SelectedTab == tabPage2)
                {
                    MessageBox.Show("You are already in the Edit Room menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                EditEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Edit room";
            };

            btnRoomSave.Click += delegate
            {
                
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
                var result = MessageBox.Show("Are you sure you want to delete the selected room?", "Warning",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }



        public string RoomId { get => txtRoomId.Texts; set => txtRoomId.Texts = value; }
        public string RoomNumber { get => txtRoomNumber.Texts; set => txtRoomNumber.Texts = value; }
        public string RoomType { get => txtRoomType.Texts; set => txtRoomType.Texts = value; }
        public string RoomPrice { get => txtRoomPrice.Texts; set => txtRoomPrice.Texts = value ; }
        public string BedCount { get => txtBedCount.Texts ; set => txtBedCount.Texts = value ; }
        public string RoomGuests { get => txtRoomGuests.Texts; set => txtRoomGuests.Texts = value; }
        public string RoomDescription { get => txtDescription.Texts ; set => txtDescription.Texts =  value ; }
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


        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;



        //Methods
        public static void ResetInstance()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        
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

        private static UCRooms _instance;
        public static UCRooms GetInstance(Form parentContainer)
        {
            if (_instance == null || _instance.IsDisposed || _instance.Parent == null)
            {
                _instance = new UCRooms();
            }

            _instance.Dock = DockStyle.Fill;
            return _instance;
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

        public void btnStandardRoom_Click(object sender, EventArgs e) => RoomHelper.StandardRoom(this);

        private void btnDeluxeRoom_Click(object sender, EventArgs e) => RoomHelper.DeluxeRoom(this);

        private void btnSuiteRoom_Click(object sender, EventArgs e) => RoomHelper.SuiteRoom(this);

        private void btnfamilyRoom_Click(object sender, EventArgs e) => RoomHelper.FamilyRoom(this);

        private void btnSingleRoom_Click(object sender, EventArgs e) => RoomHelper.SingleRoom(this);
    }
}

