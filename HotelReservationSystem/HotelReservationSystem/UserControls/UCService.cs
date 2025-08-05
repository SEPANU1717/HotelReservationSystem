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
using CuoreUI.Controls;
using HotelReservationSystem.Helper;
using HotelReservationSystem.Interface.Service.Food;
using HotelReservationSystem.Interface.Service.Laundry;
using HotelReservationSystem.Model.Service;
using HotelReservationSystem.Model.Service.Food;
using HotelReservationSystem.Repositories.Service.Food;

namespace HotelReservationSystem.UserControls
{
    public partial class UCService : UserControl, IFoodStockVIew, ILaundryView
    {
        private FoodRepository foodRepo;
        private LaundryRepository laundryRepo;

        public UCService()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            materialTabControl1.TabPages.Remove(Food);
            materialTabControl1.TabPages.Remove(Laundry);
            materialTabControl1.TabPages.Remove(OrderFood);
            materialTabControl1.TabPages.Remove(tabPage1);
            materialTabControl1.TabPages.Remove(ConfirmOrder);
            foodRepo = new FoodRepository(DbConfig.GetConnectionString());
            laundryRepo = new  LaundryRepository(DbConfig.GetConnectionString());
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnFoodSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtFoodSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            btnAddFood.Click += delegate
            {
                if (materialTabControl1.SelectedTab == Food || materialTabControl1.SelectedTab == Laundry)
                {
                    MessageBox.Show("You are already in the Add Food menu.", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                ClearFoodFields();
                txtFoodId.Texts = foodRepo.GetNextFoodId().ToString();
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Remove(Laundry);
                materialTabControl1.TabPages.Add(Food);
                materialTabControl1.TabPages.Remove(OrderFood);
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(OrderList);
                materialTabControl1.SelectedTab = Food;
                materialTabControl1.Text = "Add new food";
            };

            btnFoodEdit.Click += delegate
            {
                if (materialTabControl1.SelectedTab == Food || materialTabControl1.SelectedTab == Laundry)
                {
                    MessageBox.Show("You are already in the Edit Food menu.", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                EditEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Remove(Laundry);
                materialTabControl1.TabPages.Add(Food);
                materialTabControl1.TabPages.Remove(OrderFood);
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(OrderList);
                materialTabControl1.Text = "Edit food";
            };

            btnFoodAdd.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    ClearFoodFields();
                    isEdit = false;
                    materialTabControl1.TabPages.Remove(Food);
                    materialTabControl1.TabPages.Remove(Laundry);
                    materialTabControl1.TabPages.Add(tabPage1);
                    materialTabControl1.TabPages.Remove(OrderFood);
                    materialTabControl1.TabPages.Remove(ConfirmOrder);
                    materialTabControl1.TabPages.Remove(OrderList);
                }

                MessageBox.Show(Message);
            };

            btnFoodCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Remove(Laundry);
                materialTabControl1.TabPages.Add(tabPage1);
                materialTabControl1.TabPages.Remove(OrderFood);
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(OrderList);
            };

            btnFoodDelete.Click += delegate
            {
                if (materialTabControl1.SelectedTab == Food || materialTabControl1.SelectedTab == Laundry)
                {
                    MessageBox.Show("Return service table to delete", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show("Are you sure you want to delete the selected food item?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
            btnLaundry.Click += delegate
            {
                //txtLId.Texts = laundryRepo.GetNextReservationId().ToString();
                if (materialTabControl1.SelectedTab == Laundry || materialTabControl1.SelectedTab == Food)
                {
                    MessageBox.Show("You are already in the Laundry menu.", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Add(Laundry);
                materialTabControl1.TabPages.Remove(OrderFood);
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(OrderList);
            };

            btnBillingCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Add(tabPage1);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Remove(Laundry);
                materialTabControl1.TabPages.Remove(OrderFood);
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(OrderList);
            };
            btnLAdd.Click += delegate { AddEvent?.Invoke(this, EventArgs.Empty); };


                btnLComplete.Click += delegate { CompleteEvent?.Invoke(this, EventArgs.Empty); };

            btnLClear.Click += delegate
            {
                ClearEvent?.Invoke(this, EventArgs.Empty);
            };
            btnFoodStock.Click += delegate
            {
                if (materialTabControl1.SelectedTab == tabPage1)
                {
                    MessageBox.Show("You are already in the Food Stock menu.", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                materialTabControl1.TabPages.Add(tabPage1);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Remove(Laundry);
                materialTabControl1.TabPages.Remove(OrderFood);
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(OrderList);

            };

            btnOrderFood.Click += delegate
            {
                materialTabControl1.TabPages.Add(OrderFood);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Remove(Laundry);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(OrderList);
            };

            btnConfirmOrder.Click += delegate
            {
                materialTabControl1.TabPages.Add(ConfirmOrder);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Remove(Laundry);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Remove(OrderList);
                materialTabControl1.TabPages.Remove(OrderFood);
            };

            btnOrderList.Click += delegate
            {
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Remove(Laundry);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(OrderList);
                materialTabControl1.TabPages.Remove(OrderFood);
            };
        }

        public string FoodId
        {
            get => txtFoodId.Texts;
            set => txtFoodId.Texts = value;
        }

        public string FoodName
        {
            get => txtFoodName.Texts;
            set => txtFoodName.Texts = value;
        }

        public string Description
        {
            get => txtDescription.Texts;
            set => txtDescription.Texts = value;
        }

        public string LaundryId
        {
            get /*=> txtLId.Texts*/;
            set /*=> txtLId.Texts = value*/;
        }
        public string LaundryName { get => txtLName.Texts; set => txtLName.Texts = value; }
        public string Quantity
        {
            get => txtLQuantity.Texts;
            set => txtLQuantity.Texts = value;
        }
        public string LPrice { get => txtLPrice.Texts; set => txtLPrice.Texts = value; }

        public string Price
        {
            get => txtFoodPrice.Texts;
            set => txtFoodPrice.Texts = value;
        }

        public event EventHandler AddEvent;
        public event EventHandler ClearEvent;
        public event EventHandler CompleteEvent;

        public string Stock
        {
            get => txtStock.Texts;
            set => txtStock.Texts = value;
        }

        public string SearchValue
        {
            get => txtFoodSearch.Texts;
            set => txtFoodSearch.Texts = value;
        }

        public bool isSuccessful { get; set; }
        public bool isEdit { get; set; }
        public string Message { get; set; }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public void SetLaundryListBindingSource(BindingSource laundryList)
        {
            dataGridLaundry.DataSource = laundryList;
        }


        public void SetFoodListBindingSource(BindingSource foodList)
        {
            dataGridFoodService.DataSource = foodList;
        }

        private static UCService _instance;

        public static UCService GetInstance(Form parentContainer)
        {
            if (_instance == null || _instance.IsDisposed || _instance.Parent == null)
            {
                _instance = new UCService();
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

        private void ClearFoodFields()
        {
            txtFoodId.Texts = "";
            txtFoodName.Texts = "";
            txtDescription.Texts = "";
            txtFoodPrice.Texts = "";
            txtStock.Texts = "";
        }

        private void sataPictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnFood1_Click(object sender, EventArgs e) => ServiceHelper.Food1(this);
        private void btnFood2_Click(object sender, EventArgs e) => ServiceHelper.Food2(this);
        private void btnFood3_Click(object sender, EventArgs e) => ServiceHelper.Food3(this);
        private void btnFood4_Click(object sender, EventArgs e) => ServiceHelper.Food4(this);
        private void btnFood5_Click(object sender, EventArgs e) => ServiceHelper.Food5(this);
        private void btnFood6_Click(object sender, EventArgs e) => ServiceHelper.Food6(this);
        private void btnFood7_Click(object sender, EventArgs e) => ServiceHelper.Drink7(this);
        private void btnFood8_Click(object sender, EventArgs e) => ServiceHelper.Drink8(this);
        private void btnFood9_Click(object sender, EventArgs e) => ServiceHelper.Drink9(this);
        private void btnFood10_Click(object sender, EventArgs e) => ServiceHelper.Drink10(this);
        private void btnFood11_Click(object sender, EventArgs e) => ServiceHelper.Drink11(this);
        private void btnFood12_Click(object sender, EventArgs e) => ServiceHelper.Drink12(this);


        private void sataButton1_Click(object sender, EventArgs e) => ServiceHelper.LaundryBlouse(this);
        private void sataButton2_Click(object sender, EventArgs e) => ServiceHelper.FormalAttire(this);
        private void sataButton4_Click(object sender, EventArgs e) => ServiceHelper.Socks(this);
        private void sataButton3_Click_1(object sender, EventArgs e)=> ServiceHelper.PantsTrouser(this);
        private void sataButton5_Click_1(object sender, EventArgs e)=> ServiceHelper.Sensitive(this);

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
