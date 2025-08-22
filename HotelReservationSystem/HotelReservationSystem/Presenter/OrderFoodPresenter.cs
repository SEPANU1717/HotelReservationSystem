using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Interface.Service.Food;
using HotelReservationSystem.Domain.Model.Service.Shared;
using HotelReservationSystem.Model.Service;

namespace HotelReservationSystem.Presenter
{
    public class OrderFoodPresenter
    {
        private IOrderFoodView orderView;
        private IOrderFoodRepository repository;
        private BindingSource OrderFoodBindingSource;
        private IEnumerable<SharedAddServiceModel> orderList;

        public OrderFoodPresenter(IOrderFoodView orderView, IOrderFoodRepository repository)
        {
            OrderFoodBindingSource = new BindingSource();
            this.orderView = orderView;
            this.repository = repository;

            this.orderView.OrderAddEvent += AddOrderFood;
            this.orderView.OrderCompleteEvent += CompleteOrderFood;
            this.orderView.OrderCancelEvent += CancelOrderFood;
            this.orderView.OrderClearEvent += ClearOrderFood;

            this.orderView.SetOrderListBindingSource(OrderFoodBindingSource);
            LoadAllOrderFoodList();
        }

        private void LoadAllOrderFoodList()
        {
            try
            {
                orderList = repository.GetAll();
                OrderFoodBindingSource.DataSource = orderList;
                OrderFoodBindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading food order data: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearOrderFood(object sender, EventArgs e)
        {
            try
            {
                repository.RestoreStockForAllOrders();
                repository.ClearAll();
                LoadAllOrderFoodList(); 
                MessageBox.Show("All food orders have been deleted successfully.",
                    "Delete Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting food orders: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CancelOrderFood(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void CompleteOrderFood(object sender, EventArgs e)
        {
            try
            {
                var orders = repository.GetAll();
                foreach (var order in orders)
                {
                    repository.DeductStock(order.ItemName, order.Quantity);
                }
                repository.ClearAll();
                LoadAllOrderFoodList();
                MessageBox.Show("Order completed and stock deducted.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error completing order: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddOrderFood(object sender, EventArgs e)
        {
            try
            {
                string itemName = orderView.ItemName;
                int quantity = int.Parse(orderView.FoodQuantity);
                decimal price = decimal.Parse(orderView.FoodPrice);

                int availableStock = repository.GetStock(itemName);
                if (quantity > availableStock)
                {
                    MessageBox.Show($"Not enough stock. Available: {availableStock}", "Stock Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var foodOrder = new SharedAddServiceModel
                {
                    ItemName = itemName,
                    Quantity = quantity,
                    Price = price
                };

                repository.Add(foodOrder);
                repository.DeductStock(itemName, quantity);
                LoadAllOrderFoodList();
                CleanViewFields(); 

            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for quantity and price.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding food order: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CleanViewFields()
        {   
            orderView.ItemName = "";
            orderView.FoodPrice = "";
            orderView.FoodQuantity = "";
        }
    }
}