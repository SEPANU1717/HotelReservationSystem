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
using HotelReservationSystem.Interface.Reservation;
using HotelReservationSystem.Interface.Rooms;
using HotelReservationSystem.Repositories.Rooms;

namespace HotelReservationSystem.UserControls
{
    public partial class UCReservation : UserControl, IReservationView
    {
        private List<string> comboItems;

        RoomRepository reserveRepo;
        public UCReservation()
        {
            InitializeComponent();
            materialTabControl1.TabPages.Remove(tabPage2);
            AssociateAndraiseViewEvents();
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            reserveRepo = new RoomRepository(connectionString);
        }

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
                //ClearReservationFields();
                //txtRoomId.Texts = roomRepo.GetNextRoomId().ToString();
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Add new room";
            };

            btnReservationEdit.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Edit room";
            };

            btnSave.Click += delegate
            {

                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    //ClearRoomFields();
                    isEdit = false;
                    materialTabControl1.TabPages.Remove(tabPage2);
                    materialTabControl1.TabPages.Add(tabPage1);
                }
                MessageBox.Show(Message);
            };

            btnCancel.Click += delegate
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

        public string ReservationId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CustomerName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string RoomId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string RoomNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string RoomType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Guests { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime CheckInDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime CheckOutDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime ReservedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TotalPrice { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ReservationStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SearchValue { get => txtReservationSearch.Texts; set => txtReservationSearch.Texts = value; }
        public bool isEdit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool isSuccessful { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;


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



        //private void InitializeComboBox()
        //{
        //    comboItems = new List<string>()
        //   {
        //       "Available",
        //       "Occupied",
        //       "Under maintenance"
        //   };

        //    cboRoomStatus.Items = comboItems.ToArray();
        //}
        public void SetReservationListBindingSource(BindingSource reservationList)
        {
            dataGridReservation.DataSource = reservationList;
        }




    }
}
