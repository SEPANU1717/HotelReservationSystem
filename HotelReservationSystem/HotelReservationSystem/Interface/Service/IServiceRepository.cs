using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Model;
using HotelReservationSystem.Model.Service.Food;

namespace HotelReservationSystem.Interface.Service
{
    public interface IServiceRepository
    {
        void Add(FoodStockModel room);
        void Edit(RoomModel room);
        void Delete(int id);


        IEnumerable<RoomModel> GetAll();
        IEnumerable<RoomModel> GetByValue(string value);
    }
}
