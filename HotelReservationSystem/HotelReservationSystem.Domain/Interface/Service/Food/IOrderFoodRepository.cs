using System.Collections.Generic;
using HotelReservationSystem.Domain.Model.Service.Shared;
using HotelReservationSystem.Model.Service;

namespace HotelReservationSystem.Domain.Interface.Service.Food
{
    public interface IOrderFoodRepository
    {
        void Add(SharedAddServiceModel food);
        void ClearAll();
        IEnumerable<SharedAddServiceModel> GetAll();
        int GetStock(string itemName);
        void DeductStock(string itemName, int quantity);
        void RestoreStockForAllOrders();
    }
}
