using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Model.Service;

namespace HotelReservationSystem.Interface.Service.Food
{
    public interface IOrderFoodRepository
    {
        void Add(SharedAddServiceModel food);
        void ClearAll();
        IEnumerable<SharedAddServiceModel> GetAll();
        int GetStock(string itemName);
    }
}
