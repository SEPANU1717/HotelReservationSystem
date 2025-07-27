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
using HotelReservationSystem.Interface;
using HotelReservationSystem.Repositories;
using HotelReservationSystem.Repositories.Rooms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace HotelReservationSystem.UserControls
{
    public partial class UCCustomers : UserControl, ICustomerView
    {

        CustomerRepository customerRepo;
        // Constructor  
        public UCCustomers()
        {
            InitializeComponent();
            AssociateAndraiseViewEvents();
            InitializeComboBox();
            materialTabControl1.TabPages.Remove(tabPage2);
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            customerRepo = new CustomerRepository(connectionString);
        }

        private List<string> comboItems;
        private void InitializeComboBox()
        {
            comboItems = new List<string>()
           {
               "Passport",
               "Driver's License",
               "National ID"
           };

            sataComboBox1.Items = comboItems.ToArray();
        }

        private void AssociateAndraiseViewEvents()
        {
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            btnAddNew.Click += delegate
            {

                txtCusId.Texts = customerRepo.GetNextCustomerId().ToString();
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Add new customer";
            };

            btnEdit.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Edit Customer";
            };

            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
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

            btnDelete.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected customer?", "Warning",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }

        // Properties  
        public string CustomerID { get => txtCusId.Texts; set => txtCusId.Texts = value; }
        public string CustomerName { get => txtName.Texts; set => txtName.Texts = value; }
        public string CustomerIdType { get => txtIdType.Texts; set => txtIdType.Texts = value; }
        public string CustomerContact { get => txtContact.Texts; set => txtContact.Texts = value; }
        public string CustomerAddress { get => txtAddress.Texts; set => txtAddress.Texts = value; }
        public string SearchValue { get => txtSearch.Texts; set => txtSearch.Texts = value; }
        public bool isSuccessful { get; set; }
        public bool isEdit { get; set; }
        public string Message { get; set; }

        // Events  
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        // Methods  
        public static void ResetInstance()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        public void SetCustomerListBindingSource(BindingSource customerList)
        {
            dataGridView1.DataSource = customerList;
        }

        private static UCCustomers _instance;
        public static UCCustomers GetInstance(Form parentContainer)
        {
            if (_instance == null || _instance.IsDisposed || _instance.Parent == null)
            {
                _instance = new UCCustomers();
            }

            _instance.Dock = DockStyle.Fill;
            return _instance;
        }

        private void sataComboBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
