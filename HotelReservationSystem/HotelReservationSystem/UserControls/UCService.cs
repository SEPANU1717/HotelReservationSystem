using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CuoreUI.Controls;
using HotelReservationSystem.Data.Repositories.Service;
using HotelReservationSystem.DataInitializer;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Domain.Interface.Service.Food;
using HotelReservationSystem.Domain.Interface.Service.Laundry;
using HotelReservationSystem.Model.Service;

namespace HotelReservationSystem.UserControls
{
    public partial class UCService : UserControl, IFoodStockVIew, ILaundryView, IOrderFoodView
    {
        #region Fields
        private FoodStockRepository foodRepo;
        private LaundryRepository laundryRepo;
        private FoodOrderRepository foodOrder;
        #endregion
        #region Constructor
        public UCService()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            materialTabControl1.TabPages.Remove(Food);
            materialTabControl1.TabPages.Remove(Laundry);
            materialTabControl1.TabPages.Remove(OrderFood);
            materialTabControl1.TabPages.Remove(tabPage1);
            materialTabControl1.TabPages.Remove(ConfirmOrder);
            materialTabControl1.TabPages.Remove(ConfirmLaundry);
            materialTabControl1.TabPages.Remove(tabPage2);
            foodRepo = new FoodStockRepository(DbConfig.GetConnectionString());
            laundryRepo = new LaundryRepository(DbConfig.GetConnectionString());
            foodOrder = new FoodOrderRepository(DbConfig.GetConnectionString());
            UpdateTotalOrderPriceLabel();

            //temporary
            foodOrder.ClearAll();

        }
        #endregion
        #region Event Association
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
                materialTabControl1.TabPages.Remove(ConfirmLaundry);
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
                materialTabControl1.TabPages.Remove(ConfirmLaundry);
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
                    materialTabControl1.TabPages.Remove(ConfirmLaundry);
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
                materialTabControl1.TabPages.Remove(ConfirmLaundry);
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
                materialTabControl1.TabPages.Remove(ConfirmLaundry);
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
                materialTabControl1.TabPages.Remove(ConfirmLaundry);
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
                materialTabControl1.TabPages.Remove(ConfirmLaundry);

            };

            btnOrderFood.Click += delegate
            {
                materialTabControl1.TabPages.Add(OrderFood);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Remove(Laundry);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(OrderList);
                materialTabControl1.TabPages.Remove(ConfirmLaundry);
            };

            btnBasketOrder.Click += delegate
            {
                materialTabControl1.TabPages.Add(ConfirmOrder);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Remove(Laundry);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Remove(OrderList);
                materialTabControl1.TabPages.Remove(OrderFood);
                materialTabControl1.TabPages.Remove(ConfirmLaundry);
            };

            btnOrderList.Click += delegate
            {
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Remove(Laundry);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(OrderList);
                materialTabControl1.TabPages.Remove(OrderFood);
                materialTabControl1.TabPages.Remove(ConfirmLaundry);
            };

            btnConfirmLaundry.Click += delegate
            {
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Remove(Laundry);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Remove(OrderList);
                materialTabControl1.TabPages.Remove(OrderFood);
                materialTabControl1.TabPages.Add(ConfirmLaundry);
            };

            btnBasketOrder.Click += delegate
            {
                OrderAddEvent?.Invoke(this, EventArgs.Empty);
                txtAddOrderStock.Texts = "";
            };
            clearAllFood.Click += delegate
            {
                OrderClearEvent?.Invoke(this, EventArgs.Empty);
                UpdateTotalOrderPriceLabel();
            };



        }
        #endregion
        #region Properties
        public string FoodId {get => txtFoodId.Texts;set => txtFoodId.Texts = value;}
        public string FoodName{ get => txtFoodName.Texts;set => txtFoodName.Texts = value;}
        public string Description{get => txtDescription.Texts;set => txtDescription.Texts = value;}
        public string LaundryId{get /*=> txtLId.Texts*/;set /*=> txtLId.Texts = value*/;}
        public string LaundryName { get => txtLName.Texts; set => txtLName.Texts = value; }
        public string Quantity{get => txtLQuantity.Texts;set => txtLQuantity.Texts = value;}
        public string LPrice { get => txtLPrice.Texts; set => txtLPrice.Texts = value; }
        public string Price{get => txtFoodPrice.Texts;set => txtFoodPrice.Texts = value;}
        public string Stock{ get => txtStock.Texts;set => txtStock.Texts = value; }
        public string SearchValue{get => txtFoodSearch.Texts; set => txtFoodSearch.Texts = value;  }
        public string ItemName { get => txtAddOrderItemName.Texts; set => txtAddOrderItemName.Texts = value; }
        public string FoodQuantity { get => txtAddOrderQuantity.Texts; set => txtAddOrderQuantity.Texts = value; }
        public string FoodPrice { get => txtAddOrderPrice.Texts; set => txtAddOrderPrice.Texts = value; }
        public bool isSuccessful { get; set; }
        public bool isEdit { get; set; }
        public string Message { get; set; }


        public event EventHandler AddEvent;
        public event EventHandler ClearEvent;
        public event EventHandler CompleteEvent;

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        public event EventHandler OrderAddEvent;
        public event EventHandler OrderClearEvent;
        public event EventHandler OrderCompleteEvent;
        public event EventHandler OrderCancelEvent;
        #endregion
        #region DataGrid Binding
        public void SetLaundryListBindingSource(BindingSource laundryList) => dataGridLaundry.DataSource = laundryList;
        public void SetFoodListBindingSource(BindingSource foodList) => dataGridFoodService.DataSource = foodList;
        public void SetOrderListBindingSource(BindingSource orderList) => dataGridFood.DataSource = orderList;
        #endregion
        #region Singleton
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
        #endregion
        #region Utility Methods
        private void ClearFoodFields()
        {
            txtFoodId.Texts = "";
            txtFoodName.Texts = "";
            txtDescription.Texts = "";
            txtFoodPrice.Texts = "";
            txtStock.Texts = "";
        }

        public void GetStock()
        {
            int stock = foodOrder.GetStock(ItemName);
            txtAddOrderStock.Texts = stock.ToString();
        }
        #endregion
        #region Event Handlers

        private void btnFood1_Click(object sender, EventArgs e) => ServiceInitializer.Food1(this);
        private void btnFood2_Click(object sender, EventArgs e) => ServiceInitializer.Food2(this);
        private void btnFood3_Click(object sender, EventArgs e) => ServiceInitializer.Food3(this);
        private void btnFood4_Click(object sender, EventArgs e) => ServiceInitializer.Food4(this);
        private void btnFood5_Click(object sender, EventArgs e) => ServiceInitializer.Food5(this);
        private void btnFood6_Click(object sender, EventArgs e) => ServiceInitializer.Food6(this);
        private void btnFood7_Click(object sender, EventArgs e) => ServiceInitializer.Drink7(this);
        private void btnFood8_Click(object sender, EventArgs e) => ServiceInitializer.Drink8(this);
        private void btnFood9_Click(object sender, EventArgs e) => ServiceInitializer.Drink9(this);
        private void btnFood10_Click(object sender, EventArgs e) => ServiceInitializer.Drink10(this);
        private void btnFood11_Click(object sender, EventArgs e) => ServiceInitializer.Drink11(this);
        private void btnFood12_Click(object sender, EventArgs e) => ServiceInitializer.Drink12(this);
        private void sataButton1_Click(object sender, EventArgs e) => ServiceInitializer.LaundryBlouse(this);
        private void sataButton2_Click(object sender, EventArgs e) => ServiceInitializer.FormalAttire(this);
        private void sataButton4_Click(object sender, EventArgs e) => ServiceInitializer.Socks(this);
        private void sataButton3_Click_1(object sender, EventArgs e) => ServiceInitializer.PantsTrouser(this);
        private void sataButton5_Click_1(object sender, EventArgs e) => ServiceInitializer.Sensitive(this);
        private void order1_Click(object sender, EventArgs e) {OrderFoodInitializer.OrderSaltedPasta(this); GetStock(); OrderAddEvent?.Invoke(this, EventArgs.Empty); UpdateTotalOrderPriceLabel(); }
        private void order2_Click(object sender, EventArgs e){OrderFoodInitializer.OrderSpicySeafoodNoodles(this);GetStock(); OrderAddEvent?.Invoke(this, EventArgs.Empty); UpdateTotalOrderPriceLabel(); }
        private void order3_Click(object sender, EventArgs e){OrderFoodInitializer.OrderBeefDumpling(this);GetStock(); OrderAddEvent?.Invoke(this, EventArgs.Empty); UpdateTotalOrderPriceLabel(); }
        private void order4_Click(object sender, EventArgs e){OrderFoodInitializer.OrderHealthyNoodles(this);GetStock(); OrderAddEvent?.Invoke(this, EventArgs.Empty); UpdateTotalOrderPriceLabel(); }
        private void order5_Click(object sender, EventArgs e){OrderFoodInitializer.OrderHotFriedRiceWithOmelet(this);GetStock(); OrderAddEvent?.Invoke(this, EventArgs.Empty); UpdateTotalOrderPriceLabel(); }
        private void order6_Click(object sender, EventArgs e){OrderFoodInitializer.OrderSpicyNoodleWithOmelet(this);GetStock(); OrderAddEvent?.Invoke(this, EventArgs.Empty); UpdateTotalOrderPriceLabel(); }
        private void order7_Click(object sender, EventArgs e){OrderFoodInitializer.OrderTropicalBliss(this);GetStock(); OrderAddEvent?.Invoke(this, EventArgs.Empty); UpdateTotalOrderPriceLabel(); }
        private void order8_Click(object sender, EventArgs e){OrderFoodInitializer.OrderSunsetSparkler(this); GetStock(); OrderAddEvent?.Invoke(this, EventArgs.Empty); UpdateTotalOrderPriceLabel(); }
        private void order9_Click(object sender, EventArgs e){OrderFoodInitializer.OrderBerryFizzDelight(this);GetStock(); OrderAddEvent?.Invoke(this, EventArgs.Empty); UpdateTotalOrderPriceLabel(); }
        private void order10_Click(object sender, EventArgs e){OrderFoodInitializer.OrderCherrySplash(this);GetStock(); OrderAddEvent?.Invoke(this, EventArgs.Empty); UpdateTotalOrderPriceLabel(); }
        private void order11_Click(object sender, EventArgs e){OrderFoodInitializer.OrderCitrusCooler(this);GetStock(); OrderAddEvent?.Invoke(this, EventArgs.Empty); UpdateTotalOrderPriceLabel(); }
        private void order12_Click(object sender, EventArgs e){OrderFoodInitializer.OrderMelonMedley(this);GetStock(); OrderAddEvent?.Invoke(this, EventArgs.Empty); UpdateTotalOrderPriceLabel(); }
        #endregion
        #region TotalPriceOrder
        private decimal GetTotalOrderPriceFromDatabase()
        {
            var allOrders = foodOrder.GetAll();
            decimal total = 0;
            foreach (var order in allOrders)
            {
                total += order.Price * order.Quantity;
            }
            return total;
        }

        private void UpdateTotalOrderPriceLabel()
        {
            decimal total = GetTotalOrderPriceFromDatabase();
            var culture = new CultureInfo("en-PH");
            lblTotalPriceOrder.Text = total.ToString("C", culture);
        }
        #endregion TotalPriceOrder
    }
}