using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Model.Service;

namespace HotelReservationSystem.Interface.Service.Laundry
{
    public interface ILaundryServiceRepository
    {
        void Add(SharedAddServiceModel laundry);
        void ClearAll();
        IEnumerable<SharedAddServiceModel> GetAll();
    }
}
