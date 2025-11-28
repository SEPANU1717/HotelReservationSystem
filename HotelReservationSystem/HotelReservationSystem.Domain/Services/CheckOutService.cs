using System;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Domain.Model.CheckInOut;

namespace HotelReservationSystem.Domain.Services
{

    public class CheckOutService
    {
        private const decimal LATE_CHECKOUT_FEE_PER_HOUR = 50.00m;
        private const int LATE_CHECKOUT_GRACE_PERIOD_MINUTES = 30;
        

        public decimal CalculateLateCheckoutFee(DateTime scheduledCheckOut, DateTime actualCheckOut)
        {
            if (actualCheckOut <= scheduledCheckOut)
                return 0m;
                
            var lateTime = actualCheckOut - scheduledCheckOut;
            
            if (lateTime.TotalMinutes <= LATE_CHECKOUT_GRACE_PERIOD_MINUTES)
                return 0m;
                
            int lateHours = (int)Math.Ceiling(lateTime.TotalHours);
            return lateHours * LATE_CHECKOUT_FEE_PER_HOUR;
        }

        public CheckOutValidationResult ValidateCheckout(CheckInOutModel checkIn)
        {
            var result = new CheckOutValidationResult { IsValid = true };
            
            if (checkIn.IsCheckedOut)
            {
                result.IsValid = false;
                result.ErrorMessage = "This guest has already been checked out.";
                return result;
            }
            
            if (!checkIn.IsCheckedIn)
            {
                result.IsValid = false;
                result.ErrorMessage = "Guest must be checked in before checkout.";
                return result;
            }
            
            decimal balanceDue = checkIn.BalanceDue;
            if (balanceDue > 0)
            {
                result.IsValid = false;
                result.ErrorMessage = string.Format("Cannot checkout with outstanding balance of ${0:N2}.\n\nPlease settle the payment before proceeding with checkout.", balanceDue);
                result.HasOutstandingBalance = true;
                result.OutstandingAmount = balanceDue;
                return result;
            }
            
            var today = DateTime.Now.Date;
            if (today < checkIn.CheckInDate.Date)
            {
                result.IsValid = false;
                result.ErrorMessage = "Cannot checkout before check-in date.";
                return result;
            }
            
            result.Message = "Validation successful. Ready to checkout.";
            return result;
        }

        public BillingModel PrepareBillingForCheckout(
            CheckInOutModel checkIn, 
            DateTime actualCheckOutDate,
            decimal damageFee = 0m)
        {
            var lateCheckoutFee = CalculateLateCheckoutFee(checkIn.CheckOutDate, actualCheckOutDate);
            
            var billing = new BillingModel
            {
                ReservationId = checkIn.ReservationId,
                CustomerName = checkIn.CustomerName,
                CustomerEmail = checkIn.CustomerEmail,
                RoomType = checkIn.RoomType,
                RoomNumber = checkIn.RoomNumber,
                CheckInDate = checkIn.CheckInDate,
                CheckOutDate = checkIn.CheckOutDate,
                ActualCheckOutDate = actualCheckOutDate,
                RoomCharge = checkIn.TotalPrice,
                LateCheckoutFee = lateCheckoutFee,
                DamageFee = damageFee,
                AmountPaidBefore = checkIn.AmountPaid,
                AmountPaidAtCheckout = 0m,  
                PaymentStatus = checkIn.BalanceDue == 0 ? "Paid" : "Pending",
                PaymentMethod = checkIn.PaymentMethod,
                PaymentReference = checkIn.PaymentReference,
                DateBilled = DateTime.Now
            };
            
            return billing;
        }
    }

    public class CheckOutValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        public string Message { get; set; }
        public bool HasOutstandingBalance { get; set; }
        public decimal OutstandingAmount { get; set; }
    }
}
