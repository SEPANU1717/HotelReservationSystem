using System.Collections.Generic;
using HotelReservationSystem.Domain.Model.Service.Shared;
namespace HotelReservationSystem.Domain.Interface.Service.Laundry
{
    public interface ILaundryServiceRepository
    {
        void Add(SharedAddServiceModel laundry);
        void ClearAll();
        IEnumerable<SharedAddServiceModel> GetAll();
    }
}
