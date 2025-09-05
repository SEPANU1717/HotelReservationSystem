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
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Domain.Interface.Customer;
using HotelReservationSystem.Presenter.Common;

namespace HotelReservationSystem.UserControls
{
    public partial class UCCustomers : UserControl, ICustomerView
    {

        CustomerRepository customerRepo;
        // Constructor  
        public UCCustomers()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            InitializeComboBox();
            materialTabControl1.TabPages.Remove(tabPage2);
            customerRepo = new CustomerRepository(DbConfig.GetConnectionString());
            UserInfoDisplay.UpdateUserInfoDisplay(lblUsername, lblRole);
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

            cbType.Items = comboItems.ToArray();
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            btnAddNew.Click += delegate
            {
                if (materialTabControl1.SelectedTab == tabPage2)
                {
                    MessageBox.Show("You are already in the Add Customer menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                txtCusId.Texts = customerRepo.GetNextCustomerId().ToString();
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Add new customer";
            };

            btnEdit.Click += delegate
            {
                if (materialTabControl1.SelectedTab == tabPage2)
                {
                    MessageBox.Show("You are already in the Edit Customer menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                EditEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Edit Customer";
            };

            btnSave.Click += delegate
            {
                if (string.IsNullOrWhiteSpace(CustomerIdType) ||
                    !comboItems.Contains(CustomerIdType))
                {
                    MessageBox.Show(@"Id type is required");
                    return;
                }

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
        public string CustomerFirstName { get => txtFName.Texts; set => txtFName.Texts = value; }
        public string CustomerLastName { get => txtLName.Texts; set => txtLName.Texts = value; }
        public string CustomerIdType
        {
            get => cbType.SelectedItem ?? cbType.Text;
            set
            {
                int index = Array.IndexOf(cbType.Items, value);
                if (index >= 0)
                    cbType.SelectedIndex = index;
                else
                    cbType.Text = value;
            }
        }
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

        public static UCCustomers GetInstance(Form parentContainer) =>
            UserControlFactory<UCCustomers>.GetInstance(parentContainer);
        public static void ResetInstance() =>
            UserControlFactory < UCCustomers>.ResetInstance();

        public void SetCustomerListBindingSource(BindingSource customerList) =>
            dataGridView1.DataSource = customerList;


        private void sataComboBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
