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
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Enums;
using HotelReservationSystem.Domain.Interface.Customer;
using HotelReservationSystem.Presenter.Common;
using static HotelReservationSystem.Domain.Enums.CustomerEnum;

namespace HotelReservationSystem.UserControls
{
    public partial class UCCustomers : UserControl, ICustomerView
    {
        private CustomerRepository customerRepo;
        private List<string> comboItems;

        public event EventHandler<CustomerSelectedEventArgs> CustomerSelected;

        public class CustomerSelectedEventArgs : EventArgs
        {
            public int CustomerID { get; }
            public string FullName { get; }

            public CustomerSelectedEventArgs(int customerID, string fullName)
            {
                CustomerID = customerID;
                FullName = fullName;
            }
        }

        #region Constructor

        public UCCustomers()
        {
            InitializeComponent();
            customerRepo = new CustomerRepository(DbConfig.GetConnectionString());

            cbType.Items = Enum.GetNames(typeof(IdentificationType));
            comboItems = cbType.Items.Cast<string>().ToList();
            cbGender.Items = Enum.GetNames(typeof(Gender));

            materialTabControl1.TabPages.Remove(tabPage2);

            UserInfoDisplay.UpdateUserInfoDisplay(lblUsername, lblRole, pictureProfile);

            AssociateAndRaiseViewEvents();
            
            // Apply initial search state
            UpdateSearchControlsState();
        }

        #endregion
        
        #region Role-Based Restrictions
        
        private void UpdateSearchControlsState()
        {
            // Enable search when on grid view (tabPage1)
            // Disable search when on form view (tabPage2)
            bool isOnGridView = materialTabControl1.SelectedTab == tabPage1;
            
            if (txtSearch != null)
            {
                txtSearch.Enabled = isOnGridView;
            }
            
            if (btnSearch != null)
            {
                btnSearch.Enabled = isOnGridView;
            }
        }
        
        #endregion

        #region Event Association

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
                
                // Disable search when entering form view
                UpdateSearchControlsState();
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
                
                // Disable search when entering form view
                UpdateSearchControlsState();
            };

            btnSave.Click += delegate
            {
                if (string.IsNullOrWhiteSpace(CustomerIdType) || !comboItems.Contains(CustomerIdType))
                {
                    MessageBox.Show("ID type is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveEvent?.Invoke(this, EventArgs.Empty);

                if (isSuccessful)
                {
                    materialTabControl1.TabPages.Remove(tabPage2);
                    materialTabControl1.TabPages.Add(tabPage1);
                    CustomerAddedSuccessfully?.Invoke(this, EventArgs.Empty);
                    OnCustomerChanged();
                    
                    // Re-enable search when returning to grid view
                    UpdateSearchControlsState();
                }

                MessageBox.Show(Message);
            };

            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);

                materialTabControl1.TabPages.Remove(tabPage2);
                materialTabControl1.TabPages.Add(tabPage1);
                
                // Re-enable search when returning to grid view
                UpdateSearchControlsState();
            };

            btnDelete.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected customer?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                    OnCustomerChanged();
                }
            };

            btnCusReservation.Click += delegate
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var row = dataGridView1.CurrentRow;
                    int customerId = Convert.ToInt32(row.Cells["CustomerID"].Value);
                    string fullName = row.Cells["FullName"].Value.ToString();

                    CustomerSelected?.Invoke(this, new CustomerSelectedEventArgs(customerId, fullName));
                }
                else
                {
                    MessageBox.Show("Please select a customer first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
        }

        #endregion

        #region Properties

        public string CustomerID { get => txtCusId.Texts; set => txtCusId.Texts = value; }
        public string CustomerFirstName { get => txtFName.Texts; set => txtFName.Texts = value; }
        public string CustomerLastName { get => txtLName.Texts; set => txtLName.Texts = value; }
        public string CustomerMiddleName { get => txtMiddleName.Texts; set => txtMiddleName.Texts = value; }
        public string CustomerIdType { get => GetComboBoxValue(cbType); set => SetComboBoxValue(cbType, value); }
        public string CustomerContact { get => txtContact.Texts; set => txtContact.Texts = value; }
        public string CustomerAddress { get => txtAddress.Texts; set => txtAddress.Texts = value; }
        public string CustomerEmail { get => txtEmail.Texts; set => txtEmail.Texts = value; }
        public string CustomerGender { get => GetComboBoxValue(cbGender); set => SetComboBoxValue(cbGender, value); }
        public string CustomerNationality { get => txtNationaity.Texts; set => txtNationaity.Texts = value; }
        public string CustomerNotes { get => txtNotes.Texts; set => txtNotes.Texts = value; }
        public DateTime CustomerBirthDate { get => dtBirthday.Content; set => dtBirthday.Content = value; }
        public string SearchValue { get => txtSearch.Texts; set => txtSearch.Texts = value; }

        public bool isSuccessful { get; set; }
        public bool isEdit { get; set; }
        public string Message { get; set; }

        #endregion

        #region Events

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler CustomerAddedSuccessfully;
        public event EventHandler CustomerChanged;

        protected void OnCustomerChanged()
        {
            CustomerChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Public Methods

        public static UCCustomers GetInstance(Form parentContainer) =>
            UserControlFactory<UCCustomers>.GetInstance(parentContainer);

        public static void ResetInstance() =>
            UserControlFactory<UCCustomers>.ResetInstance();

        public void SetCustomerListBindingSource(BindingSource customerList) =>
            dataGridView1.DataSource = customerList;

        #endregion

        #region Private Helper Methods

        private string GetComboBoxValue(SATAComboBox comboBox) =>
            comboBox.SelectedItem ?? comboBox.Text;

        private void SetComboBoxValue(SATAComboBox comboBox, string value)
        {
            int index = Array.IndexOf(comboBox.Items, value);
            if (index >= 0)
                comboBox.SelectedIndex = index;
            else
                comboBox.Text = value;
        }

        #endregion
    }
}