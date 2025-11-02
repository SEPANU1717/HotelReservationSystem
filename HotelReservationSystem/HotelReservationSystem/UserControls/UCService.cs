using System;
using System.Globalization;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories.Service;
using HotelReservationSystem.DataInitializer;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Domain.Interface.Service.Food;
using HotelReservationSystem.Domain.Model.Service.Shared;
using HotelReservationSystem.Presenter.Common;

namespace HotelReservationSystem.UserControls
{
    public partial class UCService : UserControl, IFoodStockVIew, IOrderFoodView
    {


        #region Fields
        private FoodStockRepository foodRepo;
        private FoodOrderRepository foodOrder;

        #endregion
        #region Constructor
        public UCService()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            materialTabControl1.TabPages.Remove(Food);
            materialTabControl1.TabPages.Remove(OrderFood);
            materialTabControl1.TabPages.Remove(tabPage1);
            materialTabControl1.TabPages.Remove(ConfirmOrder);
            foodRepo = new FoodStockRepository(DbConfig.GetConnectionString());
            foodOrder = new FoodOrderRepository(DbConfig.GetConnectionString());
            UpdateTotalOrderPriceLabel();
            UserInfoDisplay.UpdateUserInfoDisplay(lblUsername, lblRole, pictureProfile);

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
                if (materialTabControl1.SelectedTab == Food)
                {
                    MessageBox.Show("You are already in the Add Food menu.", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                ClearFoodFields();
                txtFoodId.Texts = foodRepo.GetNextFoodId().ToString();
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(Food);
                materialTabControl1.TabPages.Remove(OrderFood);
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(OrderList);
                materialTabControl1.SelectedTab = Food;
                materialTabControl1.Text = "Add new food";
            };

            btnFoodEdit.Click += delegate
            {
                if (materialTabControl1.SelectedTab == Food)
                {
                    MessageBox.Show("You are already in the Edit Food menu.", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                EditEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
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
                materialTabControl1.TabPages.Add(tabPage1);
                materialTabControl1.TabPages.Remove(OrderFood);
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(OrderList);
            };

            btnFoodDelete.Click += delegate
            {
                if (materialTabControl1.SelectedTab == Food)
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
                materialTabControl1.TabPages.Remove(OrderFood);
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(OrderList);



            };

            btnOrderFood.Click += delegate
            {
                materialTabControl1.TabPages.Add(OrderFood);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(OrderList);

            };


            btnOrderList.Click += delegate
            {
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(OrderList);
                materialTabControl1.TabPages.Remove(OrderFood);
            };


            clearAllFood.Click += delegate
            {
                OrderClearEvent?.Invoke(this, EventArgs.Empty);
                UpdateTotalOrderPriceLabel();
            };

            btnOrderFoodCancel.Click += delegate
            {
                OrderCancelEvent?.Invoke(this, EventArgs.Empty);
                UpdateTotalOrderPriceLabel();
                materialTabControl1.TabPages.Remove(ConfirmOrder);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Remove(OrderList);
                materialTabControl1.TabPages.Remove(OrderFood);
            };



        }
        #endregion
        #region Properties
        public string FoodId {get => txtFoodId.Texts;set => txtFoodId.Texts = value;}
        public string FoodName{ get => txtFoodName.Texts;set => txtFoodName.Texts = value;}
        public string Description{get => txtDescription.Texts;set => txtDescription.Texts = value;}
        public string LaundryId{get /*=> txtLId.Texts*/;set /*=> txtLId.Texts = value*/;}
        public string Price{get => txtFoodPrice.Texts;set => txtFoodPrice.Texts = value;}
        public string Stock{ get => txtStock.Texts;set => txtStock.Texts = value; }
        public string SearchValue{get => txtFoodSearch.Texts; set => txtFoodSearch.Texts = value;  }
        public bool isSuccessful { get; set; }
        public bool isEdit { get; set; }
        public string Message { get; set; }
        private SharedAddServiceModel _selectedOrder = new SharedAddServiceModel();

        public SharedAddServiceModel SelectedOrder => _selectedOrder;


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
        public void SetFoodListBindingSource(BindingSource foodList) => dataGridFoodService.DataSource = foodList;
        public void SetOrderListBindingSource(BindingSource orderList) => dataGridFood.DataSource = orderList;
        #endregion
        #region Singleton

        public static UCService GetInstance(Form parentContainer) =>
            UserControlFactory<UCService>.GetInstance(parentContainer);

        public static void ResetInstance() =>
            UserControlFactory<UCService>.ResetInstance();

        #endregion
        #region Utility Methods
        private void ClearFoodFields()
        {
            txtFoodId.Texts = string.Empty;
            txtFoodName.Texts = string.Empty;
            txtDescription.Texts = string.Empty;
            txtFoodPrice.Texts = string.Empty;
            txtStock.Texts = string.Empty;
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
        private void order1_Click(object sender, EventArgs e) => PlaceOrder(OrderFoodInitializer.OrderSaltedPasta);
        private void order2_Click(object sender, EventArgs e) => PlaceOrder(OrderFoodInitializer.OrderSpicySeafoodNoodles);
        private void order3_Click(object sender, EventArgs e) => PlaceOrder(OrderFoodInitializer.OrderBeefDumpling);
        private void order4_Click(object sender, EventArgs e) => PlaceOrder(OrderFoodInitializer.OrderHealthyNoodles);
        private void order5_Click(object sender, EventArgs e) => PlaceOrder(OrderFoodInitializer.OrderHotFriedRiceWithOmelet);
        private void order6_Click(object sender, EventArgs e) => PlaceOrder(OrderFoodInitializer.OrderSpicyNoodleWithOmelet);
        private void order7_Click(object sender, EventArgs e) => PlaceOrder(OrderFoodInitializer.OrderTropicalBliss);
        private void order8_Click(object sender, EventArgs e) => PlaceOrder(OrderFoodInitializer.OrderSunsetSparkler);
        private void order9_Click(object sender, EventArgs e) => PlaceOrder(OrderFoodInitializer.OrderBerryFizzDelight);
        private void order10_Click(object sender, EventArgs e) => PlaceOrder(OrderFoodInitializer.OrderCherrySplash);
        private void order11_Click(object sender, EventArgs e) => PlaceOrder(OrderFoodInitializer.OrderCitrusCooler);
        private void order12_Click(object sender, EventArgs e) => PlaceOrder(OrderFoodInitializer.OrderMelonMedley);

        private void PlaceOrder(Action<SharedAddServiceModel> orderInitializer)
        {
            orderInitializer(SelectedOrder);   
            OrderAddEvent?.Invoke(this, EventArgs.Empty);
            UpdateTotalOrderPriceLabel();
        }


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