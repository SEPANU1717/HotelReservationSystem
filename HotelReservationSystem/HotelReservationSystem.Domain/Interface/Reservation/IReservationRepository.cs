using System.Collections.Generic;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Domain.Interface.Reservation
{
    public interface IReservationRepository
    {
        void Add(ReservationModel reservation);
        void Edit(ReservationModel reservation);
        void Delete(int id);
        IEnumerable<ReservationModel> GetAll();
        IEnumerable<ReservationModel> GetByValue(string value);

        // Add these missing methods
        ReservationModel GetById(int reservationId);
        ReservationModel GetByCustomerName(string customerName);
        int GetNextReservationId();
    }
}