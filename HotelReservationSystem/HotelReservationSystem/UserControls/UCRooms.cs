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
            btnStandardRoom.Click += btnStandardRoom_Click;
            InitializeComboBox();
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            roomRepo = new RoomRepository(connectionString);

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
                ClearRoomFields();
                txtRoomId.Texts = roomRepo.GetNextRoomId().ToString();
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Add new room";
            };

            btnRoomEdit.Click += delegate
            {
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
                if (index >= 0)
                {
                    cboRoomStatus.SelectedIndex = index;
                }
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


        private void btnStandardRoom_Click(object sender, EventArgs e)
        {
            txtRoomType.Texts = "Standard";
            txtDescription.Texts = "Comfortable room with essential amenities.";
            txtRoomGuests.Texts = "2";
            txtBedCount.Texts = "1";
            txtRoomPrice.Texts = "2499";



        }

        private void btnDeluxeRoom_Click(object sender, EventArgs e)
        {
            txtRoomType.Texts = "Deluxe";
            txtDescription.Texts = "Room B";
            txtRoomGuests.Texts = "2";
            txtBedCount.Texts = "1";
            txtRoomPrice.Texts = "5999"; 

        }

        private void btnSuiteRoom_Click(object sender, EventArgs e)
        {
            txtRoomType.Texts = "Suite";
            txtDescription.Texts = "Room C";
            txtRoomGuests.Texts = "4";
            txtBedCount.Texts = "2";
            txtRoomPrice.Texts = "7999";
        }

        private void btnfamilyRoom_Click(object sender, EventArgs e)
        {
            txtRoomType.Texts = "Family";
            txtDescription.Texts = "Room D";
            txtRoomGuests.Texts = "6";
            txtBedCount.Texts = "3";
            txtRoomPrice.Texts = "4999";
        }

        private void btnSingleRoom_Click(object sender, EventArgs e)
        {
            txtRoomType.Texts = "Single";
            txtDescription.Texts = "Room E";
            txtRoomGuests.Texts = "1";
            txtBedCount.Texts = "1";
            txtRoomPrice.Texts = "1999";
        }

        private void dataGridRoom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
