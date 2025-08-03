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
using HotelReservationSystem.Model.Service.Food;
using HotelReservationSystem.Repositories.Service.Food;

namespace HotelReservationSystem.UserControls
{
    public partial class UCService : UserControl, IFoodStockVIew
    {
        private FoodRepository foodRepo;
        public UCService()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            materialTabControl1.TabPages.Remove(Food);
            foodRepo = new FoodRepository(DbConfig.GetConnectionString());

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
                if (materialTabControl1.SelectedTab == Food)
                {
                    MessageBox.Show("You are already in the Add Food menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearFoodFields();
                txtFoodId.Texts = foodRepo.GetNextFoodId().ToString();
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(Food);
                materialTabControl1.SelectedTab = Food; 
                materialTabControl1.Text = "Add new food";
            };

            btnFoodEdit.Click += delegate
            {
                if (materialTabControl1.SelectedTab == Food)
                {
                    MessageBox.Show("You are already in the Edit Food menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                EditEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(tabPage1);
                materialTabControl1.TabPages.Add(Food);
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
                }
                MessageBox.Show(Message);
            };

            btnFoodCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                materialTabControl1.TabPages.Remove(Food);
                materialTabControl1.TabPages.Add(tabPage1);
            };

            btnFoodDelete.Click += delegate
            {
                if (materialTabControl1.SelectedTab == Food)
                {
                    MessageBox.Show("Return service table to delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        }

        public string FoodId { get => txtFoodId.Texts; set => txtFoodId.Texts = value; }
        public string FoodName { get => txtFoodName.Texts; set => txtFoodName.Texts = value; }
        public string Description { get => txtDescription.Texts; set => txtDescription.Texts = value; }
        public string Price { get => txtFoodPrice.Texts; set => txtFoodPrice.Texts = value; }
        public string Stock { get => txtStock.Texts; set => txtStock.Texts = value; }
        public string SearchValue { get => txtFoodSearch.Texts; set => txtFoodSearch.Texts = value; }
        public bool isSuccessful { get; set; }
        public bool isEdit { get; set; }
        public string Message { get; set; }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;


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
    }
}
