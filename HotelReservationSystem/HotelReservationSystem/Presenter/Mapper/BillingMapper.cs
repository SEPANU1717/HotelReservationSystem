using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Presenter.Mapper
{
    /// <summary>
    /// Mapper class to convert between BillingModel and BillingDto
    /// Follows DTO pattern for clean data transfer
    /// </summary>
    public static class BillingMapper
    {
        /// <summary>
        /// Convert BillingModel to BillingDto for display purposes
        /// </summary>
        public static BillingDto ToDto(BillingModel model)
        {
            if (model == null) return null;

            return new BillingDto
            {
                BillId = model.BillId,
                ReservationId = model.ReservationId,
                CustomerName = model.CustomerName,
                RoomNumber = model.RoomNumber,
                RoomType = model.RoomType,
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                ActualCheckOutDate = model.ActualCheckOutDate,
                RoomCharge = model.RoomCharge.ToString("C2"),
                LateCheckoutFee = model.LateCheckoutFee.ToString("C2"),
                DamageFee = model.DamageFee.ToString("C2"),
                TotalAmount = model.TotalAmount.ToString("C2"),
                AmountPaidBefore = model.AmountPaidBefore.ToString("C2"),
                AmountPaidAtCheckout = model.AmountPaidAtCheckout.ToString("C2"),
                BalanceDue = model.BalanceDue.ToString("C2"),
                PaymentStatus = model.PaymentStatus,
                PaymentMethod = model.PaymentMethod,
                DateBilled = model.DateBilled,
                BilledBy = model.BilledBy,
                NumberOfNights = model.NumberOfNights,
                IsLateCheckout = model.IsLateCheckout
            };
        }

        /// <summary>
        /// Convert BillingDto back to BillingModel (if needed for editing)
        /// </summary>
        public static BillingModel ToModel(BillingDto dto)
        {
            if (dto == null) return null;

            return new BillingModel
            {
                BillId = dto.BillId,
                ReservationId = dto.ReservationId,
                CustomerName = dto.CustomerName,
                RoomNumber = dto.RoomNumber,
                RoomType = dto.RoomType,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                ActualCheckOutDate = dto.ActualCheckOutDate,
                RoomCharge = decimal.Parse(dto.RoomCharge.Replace("$", "").Replace(",", "")),
                LateCheckoutFee = decimal.Parse(dto.LateCheckoutFee.Replace("$", "").Replace(",", "")),
                DamageFee = decimal.Parse(dto.DamageFee.Replace("$", "").Replace(",", "")),
                AmountPaidBefore = decimal.Parse(dto.AmountPaidBefore.Replace("$", "").Replace(",", "")),
                AmountPaidAtCheckout = decimal.Parse(dto.AmountPaidAtCheckout.Replace("$", "").Replace(",", "")),
                PaymentStatus = dto.PaymentStatus,
                PaymentMethod = dto.PaymentMethod,
                DateBilled = dto.DateBilled,
                BilledBy = dto.BilledBy
            };
        }
    }
}
