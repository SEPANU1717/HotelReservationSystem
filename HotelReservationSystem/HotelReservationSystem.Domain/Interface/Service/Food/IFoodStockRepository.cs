using System.Collections.Generic;
using HotelReservationSystem.Domain.Model.Service;

namespace HotelReservationSystem.Domain.Interface.Service.Food
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