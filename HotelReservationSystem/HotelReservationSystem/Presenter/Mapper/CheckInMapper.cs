using System;
using System.Windows.Forms;
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Interface.CheckInOut;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Domain.Model.CheckInOut;
using static HotelReservationSystem.Domain.Enums.ReservationEnum;

namespace HotelReservationSystem.Presenter.Mapper
{
    public static class CheckInMapper
    {
        public static CheckInOutModel FromCheckInView(ICheckInOutView view)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(view.ReservationId))
            {
                MessageBox.Show("Reservation ID is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (string.IsNullOrWhiteSpace(view.CustomerName))
            {
                MessageBox.Show("Customer name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (string.IsNullOrWhiteSpace(view.RoomType))
            {
                MessageBox.Show("Room type is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (string.IsNullOrWhiteSpace(view.RoomNumber))
            {
                MessageBox.Show("Room number is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Validate dates
            if (view.CheckOutDate <= view.CheckInDate)
            {
                MessageBox.Show("Check-out date must be after check-in date.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Validate payment amounts
            if (view.AmountPaid < 0)
            {
                MessageBox.Show("Amount paid cannot be negative.", "Payment Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Determine check-in/check-out status
            bool isCheckedIn = view.ReservationStatus == "CheckedIn";
            bool isCheckedOut = view.ReservationStatus == "CheckedOut";

            return new CheckInOutModel
            {
                CheckInId = 0,
                ReservationId = int.Parse(view.ReservationId),
                CustomerName = view.CustomerName.Trim(),
                RoomType = view.RoomType,
                RoomNumber = view.RoomNumber,
                CheckInDate = view.CheckInDate,
                CheckOutDate = view.CheckOutDate,
                TimeArrival = view.TimeArrival,
                TotalPrice = view.TotalPrice,
                TotalCompanionCost = 0,
                DownPayment = view.DownPayment,
                AmountPaid = view.AmountPaid,
                CompanionCount = view.CompanionCount,
                PaymentMethod = view.PaymentMethod,
                PaymentReference = view.PaymentReference,
                PaymentStatus = Enum.TryParse<PaymentState>(view.PaymentStatus, out var status)
                    ? status
                    : PaymentState.Pending,
                ReservationStatus = view.ReservationStatus,
                IsCheckedIn = isCheckedIn,
                IsCheckedOut = isCheckedOut,
                ActualCheckIn = isCheckedIn ? (DateTime?)DateTime.Now : null,
                ActualCheckOut = null,
                CheckedInBy = isCheckedIn ? Environment.UserName : null,
                CheckedOutBy = null,
                CreatedAt = DateTime.Now
            };
        }

        public static void ToCheckInView(ReservationModel model, ICheckInOutView view)
        {
            view.ReservationId = model.ReservationId > 0 ? model.ReservationId.ToString() : string.Empty;
            view.CustomerName = model.CustomerName ?? string.Empty;
            view.RoomType = model.RoomType ?? string.Empty;
            view.CheckInDate = model.CheckInDate != default ? model.CheckInDate : DateTime.Now;
            view.CheckOutDate = model.CheckOutDate != default ? model.CheckOutDate : DateTime.Now.AddDays(1);
            view.TimeArrival = model.TimeArrival != default ? model.TimeArrival : DateTime.Now;
            view.TotalPrice = model.TotalPrice;
            view.DownPayment = model.DownPayment;
            view.AmountPaid = model.AmountPaid;
            view.BalanceDue = model.BalanceDue;
            view.PaymentMethod = model.PaymentMethod ?? string.Empty;
            view.PaymentStatus = model.PaymentStatus.ToString();
            view.PaymentReference = model.PaymentReference ?? string.Empty;
            view.ReservationStatus = !string.IsNullOrEmpty(model.ReservationStatus) ? model.ReservationStatus : "CheckedIn";
            view.CompanionCount = 0;
        }

        public static void UpdateFinancialFields(ICheckInOutView view, decimal newTotalPrice)
        {
            decimal existingDownPayment = view.DownPayment;
            decimal existingAmountPaid = view.AmountPaid;

            view.TotalPrice = newTotalPrice;
            view.DownPayment = existingDownPayment;
            view.AmountPaid = existingAmountPaid;
            view.BalanceDue = newTotalPrice - existingAmountPaid;
        }

        public static CheckInDto ToCheckInDto(CheckInOutModel model)
        {
            if (model == null) return null;

            return new CheckInDto
            {
                CheckInId = model.CheckInId,
                ReservationId = model.ReservationId,
                CustomerName = model.CustomerName ?? string.Empty,
                RoomNumber = model.RoomNumber ?? string.Empty,
                RoomType = model.RoomType ?? string.Empty,
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                GrandTotal = model.GrandTotal,
                BalanceDue = model.BalanceDue,
                PaymentStatus = model.PaymentStatus.ToString(),
                ReservationStatus = model.ReservationStatus ?? string.Empty,
                IsCheckedIn = model.IsCheckedIn,
                IsCheckedOut = model.IsCheckedOut
            };
        }
    }
}