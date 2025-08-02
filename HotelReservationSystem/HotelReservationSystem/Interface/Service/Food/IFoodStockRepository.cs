using System.Collections.Generic;
using HotelReservationSystem.Model;
using HotelReservationSystem.Model.Service.Food;

namespace HotelReservationSystem.Interface.Service.Food
{
    public interface IFoodStockRepository
    {
        void Add(FoodStockModel foodStock);
        void Edit(FoodStockModel foodStock);
        void Delete(int id);


        IEnumerable<FoodStockModel> GetAll();
        IEnumerable<FoodStockModel> GetByValue(string value);
    }
}