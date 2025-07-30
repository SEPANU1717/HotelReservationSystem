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
using HotelReservationSystem.Repositories.Billing;

namespace HotelReservationSystem.UserControls
{
    public partial class UCBilling : UserControl, IBillingView
    {
        BillingRepository billRepo;
        public UCBilling()
        {
            InitializeComponent();
            AssociateAndraiseViewEvents();
            materialTabControl1.TabPages.Remove(tabPage2);
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            billRepo = new BillingRepository(connectionString);
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
                ClearFields();
                txtBillingId.Texts = billRepo.GetNextBillingId().ToString();
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Add new room";
            };

            btnBillingEdit.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Edit room";
            };

            btnReservationPay.Click += delegate
            {

                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    ClearFields();
                    isEdit = false;
                    materialTabControl1.TabPages.Remove(tabPage2);
                    materialTabControl1.TabPages.Add(tabPage1);
                }
                MessageBox.Show(Message);
            };

            btnBillingCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage2);
                materialTabControl1.TabPages.Add(tabPage1);
            };

            btnDeleteDelete.Click += delegate
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

        public string BillId { get => txtBillingId.Texts; set => txtBillingId.Texts = value; }
        public string ReservationId { get => cbReservationId.SelectedItem as string; set => cbReservationId.SelectedItem = value; }
        public string CustomerName { get => txtCusName.Texts; set => txtCusName.Texts = value; }
        public string RoomType { get => txtRoomType.Texts; set => txtRoomType.Texts = value; }
        public string RoomNumber { get => txtRoomNumber.Texts; set => txtRoomNumber.Texts = value; }
        public string TotalAmount { get => txtTotalAmount.Texts; set => txtTotalAmount.Texts = value; }
        public string PaymentStatus { get => cbStatus.SelectedItem as string; set => cbStatus.SelectedItem = value; }
        public string SearchValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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
        private void ClearFields()
        {
            BillId = "";
            ReservationId = "";
            CustomerName = "";
            RoomType = "";
            RoomNumber = "";
            TotalAmount = "";
            PaymentStatus = "";
        }
    }
}
