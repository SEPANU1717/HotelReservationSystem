using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Interface.Service.Food;
using HotelReservationSystem.Domain.Model.Service.Shared;

namespace HotelReservationSystem.Presenter
{
    public class OrderFoodPresenter
    {
        private IOrderFoodView orderView;
        private IOrderFoodRepository repository;
        private BindingSource OrderFoodBindingSource;
        private IEnumerable<SharedAddServiceModel> orderList;
        private static OrderFoodPresenter _lastPresenterInstance;

        public OrderFoodPresenter(IOrderFoodView orderView, IOrderFoodRepository repository)
        {
            OrderFoodBindingSource = new BindingSource();
            this.orderView = orderView;
            this.repository = repository;

            if (_lastPresenterInstance != null) _lastPresenterInstance.UnsubscribeFromViewEvents();

            SubscribeToViewEvents();
            _lastPresenterInstance = this;

            this.orderView.SetOrderListBindingSource(OrderFoodBindingSource);
            LoadAllOrderFoodList();
        }

        private void SubscribeToViewEvents()
        {
            this.orderView.OrderAddEvent += OnOrderAddEvent;
            this.orderView.OrderCompleteEvent += CompleteOrderFood;
            this.orderView.OrderCancelEvent += CancelOrderFood;
            this.orderView.OrderClearEvent += ClearOrderFood;
        }

        private void UnsubscribeFromViewEvents()
        {
            this.orderView.OrderAddEvent -= OnOrderAddEvent;
            this.orderView.OrderCompleteEvent -= CompleteOrderFood;
            this.orderView.OrderCancelEvent -= CancelOrderFood;
            this.orderView.OrderClearEvent -= ClearOrderFood;
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

        private void OnOrderAddEvent(object sender, EventArgs e)
        {
            if (orderView.SelectedOrder == null)
            {
                MessageBox.Show("No order selected.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AddOrder(orderView.SelectedOrder);
        }

        public void AddOrder(SharedAddServiceModel order)
        {
            try
            {
                int availableStock = repository.GetStock(order.ItemName);
                if (order.Quantity > availableStock)
                {
                    MessageBox.Show($"Not enough stock. Available: {availableStock}", "Stock Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                repository.Add(order);
                repository.DeductStock(order.ItemName, order.Quantity);
                LoadAllOrderFoodList();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding food order: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CleanViewFields() => FieldsCleaner.ClearInputs(orderView as Control);
    }
}
