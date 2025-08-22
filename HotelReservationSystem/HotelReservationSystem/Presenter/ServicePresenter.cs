using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Interface.Service.Food;
using HotelReservationSystem.Domain.Model.Service;
using HotelReservationSystem.Presenter.Common;

namespace HotelReservationSystem.Presenter
{
    public class ServicePresenter
    {
        private IFoodStockVIew foodView;
        private IFoodStockRepository repository;
        private BindingSource FoodStockBindingSource;
        private IEnumerable<FoodStockModel> foodList;

        public ServicePresenter(IFoodStockVIew foodView, IFoodStockRepository repository)
        {
            FoodStockBindingSource = new BindingSource();
            this.foodView = foodView;
            this.repository = repository;

            // Subscribe
            this.foodView.SearchEvent += SearchFood;
            this.foodView.DeleteEvent += DeleteFood;
            this.foodView.AddNewEvent += AddNewFood;
            this.foodView.EditEvent += EditFood;
            this.foodView.SaveEvent += SaveFood;
            this.foodView.CancelEvent += CancelFood;

            this.foodView.SetFoodListBindingSource(FoodStockBindingSource);
            LoadAllFoodList();
        }

        private void LoadAllFoodList()
        {
            foodList = repository.GetAll();
            FoodStockBindingSource.DataSource = foodList;
            FoodStockBindingSource.ResetBindings(false);
        }

        private void CancelFood(object sender, EventArgs e) => CleanViewFields();

        private void SaveFood(object sender, EventArgs e)
        {
            var model = new FoodStockModel();
            int.TryParse(foodView.FoodId, out int foodId);
            model.FoodId = foodId;
            model.FoodName = foodView.FoodName;
            model.Description = foodView.Description;
            model.Price = decimal.TryParse(foodView.Price, out decimal price) ? price : 0;
            model.Stock = int.TryParse(foodView.Stock, out int stock) ? stock : 0;

            try
            {
                new ModelDataValidation().Validate(model);

                if (foodView.isEdit)
                {
                    repository.Edit(model);
                    foodView.Message = "Food item edited successfully";
                }
                else
                {
                    repository.Add(model);
                    foodView.Message = "Food item added successfully";
                }
                foodView.isSuccessful = true;
                LoadAllFoodList();
            }
            catch (Exception ex)
            {
                foodView.isSuccessful = false;
                foodView.Message = ex.Message;
            }
        }

        private void EditFood(object sender, EventArgs e)
        {
            var food = (FoodStockModel)FoodStockBindingSource.Current;
            foodView.FoodId = food.FoodId.ToString();
            foodView.FoodName = food.FoodName;
            foodView.Description = food.Description;
            foodView.Price = food.Price.ToString();
            foodView.Stock = food.Stock.ToString();

            foodView.isEdit = true;
        }

        private void AddNewFood(object sender, EventArgs e)
        {
            foodView.isEdit = false;
        }

        private void DeleteFood(object sender, EventArgs e)
        {
            try
            {
                var food = (FoodStockModel)FoodStockBindingSource.Current;
                repository.Delete(food.FoodId);
                foodView.isSuccessful = true;
                foodView.Message = "Food item deleted successfully";
                LoadAllFoodList();
            }
            catch
            {
                foodView.isSuccessful = false;
                foodView.Message = "An error occurred, could not delete food item";
            }
        }

        private void SearchFood(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(foodView.SearchValue);
            foodList = emptyValue
                ? repository.GetAll()
                : repository.GetByValue(foodView.SearchValue);

            FoodStockBindingSource.DataSource = foodList;
            FoodStockBindingSource.ResetBindings(false);
        }

        private void CleanViewFields()
        {
            foodView.FoodId = "0";
            foodView.FoodName = "";
            foodView.Description = "";
            foodView.Price = "0";
            foodView.Stock = "0";
        }
    }
}