using System;
using System.Windows.Forms;
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Interface.Billing;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Presenter.Mapper
{
    public static class BillingMapper
    {
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
                TotalAmount = model.TotalAmount,
                PaymentStatus = model.PaymentStatus,
                PaymentMethod = model.PaymentMethod,
                DateBilled = model.DateBilled,
                BilledBy = model.BilledBy
            };
        }

        public static BillingModel FromBillingView(IBillingView view)
        {
            decimal roomCharge = string.IsNullOrEmpty(view.RoomCharge) ? 0 : decimal.Parse(view.RoomCharge);
            decimal lateCheckoutFee = string.IsNullOrEmpty(view.LateCheckoutFee) ? 0 : decimal.Parse(view.LateCheckoutFee);
            decimal damageFee = string.IsNullOrEmpty(view.DamageFee) ? 0 : decimal.Parse(view.DamageFee);

            decimal amountPaidBefore = string.IsNullOrEmpty(view.AmountPaidBefore) ? 0 : decimal.Parse(view.AmountPaidBefore);
            decimal amountPaidAtCheckout = string.IsNullOrEmpty(view.AmountPaidAtCheckout) ? 0 : decimal.Parse(view.AmountPaidAtCheckout);

            decimal subtotal = roomCharge + lateCheckoutFee + damageFee;
            decimal totalPaid = amountPaidBefore + amountPaidAtCheckout;

            if (totalPaid > subtotal)
            {
                MessageBox.Show("Total paid cannot exceed the total bill amount.",
                                "Payment Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return null;
            }

            return new BillingModel
            {
                BillId = string.IsNullOrEmpty(view.BillId) ? 0 : int.Parse(view.BillId),
                ReservationId = string.IsNullOrEmpty(view.ReservationId) ? 0 : int.Parse(view.ReservationId),

                CustomerName = view.CustomerName,
                RoomType = view.RoomType,
                RoomNumber = view.RoomNumber,

                CheckInDate = view.CheckInDate,
                CheckOutDate = view.CheckOutDate,
                ActualCheckOutDate = view.ActualCheckOutDate,

                RoomCharge = roomCharge,
                LateCheckoutFee = lateCheckoutFee,
                DamageFee = damageFee,

                AmountPaidBefore = amountPaidBefore,
                AmountPaidAtCheckout = amountPaidAtCheckout,

                PaymentStatus = view.PaymentStatus,
                PaymentMethod = view.PaymentMethod,
                PaymentReference = view.PaymentReference,

                DateBilled = DateTime.Now,
                BilledBy = view.BilledBy
            };
        }
    }
}
