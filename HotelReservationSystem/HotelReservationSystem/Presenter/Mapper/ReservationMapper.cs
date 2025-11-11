using System;
using System.Windows.Forms;
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Interface.Reservation;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Presenter.Mapper
{
    public static class ReservationMapper
    {
        public static ReservationDto FromReservationModel(ReservationModel model)
        {
            return new ReservationDto
            {
                ReservationId = model.ReservationId,
                CustomerName = model.CustomerName,
                RoomNumber = model.RoomNumber,
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                TotalPrice = model.TotalPrice,
                AmountPaid = model.AmountPaid,
                BalanceDue = model.TotalPrice - model.AmountPaid,
                PaymentStatus = model.PaymentStatus.ToString(),
                ReservationStatus = model.ReservationStatus
            };
        }

        public static ReservationModel FromReservationView(IReservationView view)
        {
            decimal totalPrice = string.IsNullOrEmpty(view.TotalPrice) ? 0 : decimal.Parse(view.TotalPrice);

            decimal amountPaid = string.IsNullOrEmpty(view.AmountPaid) ? 0 : decimal.Parse(view.AmountPaid);
            decimal downPayment = string.IsNullOrEmpty(view.DownPayment) ? 0 : decimal.Parse(view.DownPayment);

            if (amountPaid < downPayment)
            {
                MessageBox.Show("Amount paid cannot be less than the required down payment.",
                                "Payment Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return null;
            }

            return new ReservationModel
            {
                ReservationId = string.IsNullOrEmpty(view.ReservationId) ? 0 : int.Parse(view.ReservationId),
                CustomerName = view.CustomerName,
                RoomNumber = view.RoomNumber,
                RoomType = view.RoomType,
                CheckInDate = view.CheckInDate,
                CheckOutDate = view.CheckOutDate,
                TotalPrice = totalPrice,
                DownPayment = downPayment,
                AmountPaid = amountPaid,
                IsDownPaymentPaid = amountPaid >= downPayment,
                ReservationStatus = view.ReservationStatus,
                PaymentStatus = view.PaymentStatus,
                PaymentMethod = view.PaymentMethod,
                CreatedAt = DateTime.Now
            };
        }

    }
}
