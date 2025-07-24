using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelReservationSystem.Interface;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace HotelReservationSystem.UserControls
{
    public partial class UCCustomers : UserControl, ICustomerView
    {
        public UCCustomers()
        {
            InitializeComponent();
            AssociateAndraiseViewEvents();
            materialTabControl1.TabPages.Remove(tabPage2);

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
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.Text = "Add new customer";
            };

        }



        //Properties
        public string CustomerID { get => txtCusId.Text; set => txtCusId.Text = value; }
        public string CustomerName { get => txtName.Text; set => txtName.Text = value; }
        public string CustomerIdType { get => txtIdType.Text; set => txtIdType.Text = value; }
        public string CustomerContact { get => txtContact.Text; set => txtContact.Text = value; }
        public string CustomerAddress { get => txtAddress.Text; set => txtAddress.Text = value; }
        public string SearchValue { get => txtSearch.Text; set => txtSearch.Text = value; }
        public bool isSuccessful { get; set; }
        public bool isEdit { get; set; }
        public string Message { get; set; }


        //Events
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler UpdateEvent;
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

    }
}
