using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Model.Reservation;

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
