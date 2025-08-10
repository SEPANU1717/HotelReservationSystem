using System.Collections.Generic;
using HotelReservationSystem.Model;

namespace HotelReservationSystem.Interface.Reservation
{
    public interface IReservationRepository
    {
        void Add(ReservationModel reservation);
        void Edit(ReservationModel reservation);
        void Delete(int id);
        IEnumerable<ReservationModel> GetAll();
        IEnumerable<ReservationModel> GetByValue(string value);

    }
}
