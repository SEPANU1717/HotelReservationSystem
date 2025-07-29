using System;
using System.Collections.Generic;
using System.Linq;
using HotelReservationSystem.Model.Customer;
using HotelReservationSystem.Model.Reservation;
using HotelReservationSystem.Model.Rooms;

namespace HotelReservationSystem.Model.Billing
{
    public class BillingService
    {
        private readonly decimal DefaultTaxRate = 0.08m; // 8% default tax rate
        
        /// <summary>
        /// Creates a new bill for a reservation
        /// </summary>
        public BillingModel CreateBillFromReservation(ReservationModel reservation, CustomerModel customer, RoomModel room)
        {
            if (reservation == null) throw new ArgumentNullException(nameof(reservation));
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            if (room == null) throw new ArgumentNullException(nameof(room));

            var bill = new BillingModel
            {
                ReservationId = reservation.ReservationId,
                CustomerId = customer.CustomerID,
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                RoomNumber = room.RoomNumber,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                DateBilled = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                PaymentStatus = "Pending",
                TaxRate = DefaultTaxRate
            };

            // Parse room rate (assuming it's stored as string in RoomModel)
            if (decimal.TryParse(room.RoomPrice, out decimal roomRate))
            {
                bill.RoomRate = roomRate;
            }

            // Calculate nights
            bill.Nights = Math.Max(1, (reservation.CheckOutDate - reservation.CheckInDate).Days);

            // Set due date (typically checkout date + 7 days for corporate bookings)
            bill.DueDate = reservation.CheckOutDate.AddDays(7);

            // Calculate totals
            bill.CalculateTotals();

            // Generate invoice number
            bill.GenerateInvoiceNumber();

            return bill;
        }

        /// <summary>
        /// Adds a service charge to the bill
        /// </summary>
        public void AddServiceCharge(BillingModel bill, BillingItemType itemType, string description, 
            decimal quantity, decimal unitPrice, bool isTaxable = true)
        {
            var lineItem = new BillingLineItem
            {
                BillId = bill.BillId,
                ItemType = itemType.ToString(),
                ItemDescription = description,
                Quantity = quantity,
                UnitPrice = unitPrice,
                IsTaxable = isTaxable,
                Category = GetCategoryFromItemType(itemType)
            };

            lineItem.CalculateTotal();
            bill.AddLineItem(lineItem);
        }

        /// <summary>
        /// Processes a payment for a bill
        /// </summary>
        public PaymentTransaction ProcessPayment(BillingModel bill, PaymentMethod paymentMethod, 
            decimal amount, string processedBy, string authorizationCode = null, string cardLastFour = null)
        {
            var transaction = new PaymentTransaction
            {
                BillId = bill.BillId,
                PaymentMethod = paymentMethod.ToString(),
                Amount = amount,
                TransactionType = "Payment",
                TransactionStatus = "Completed",
                AuthorizationCode = authorizationCode,
                CardLastFour = cardLastFour,
                ProcessedBy = processedBy
            };

            // Set card type based on first digit of last four (simplified logic)
            if (!string.IsNullOrEmpty(cardLastFour) && cardLastFour.Length == 4)
            {
                transaction.CardType = DetermineCardType(cardLastFour);
            }

            // Calculate processing fee (example: 2.9% + $0.30 for credit cards)
            if (paymentMethod == PaymentMethod.CreditCard || paymentMethod == PaymentMethod.DebitCard)
            {
                transaction.ProcessingFee = (amount * 0.029m) + 0.30m;
            }

            transaction.CalculateNetAmount();
            transaction.GenerateReferenceNumber();

            bill.AddPayment(transaction);

            return transaction;
        }

        /// <summary>
        /// Applies a discount to the bill
        /// </summary>
        public void ApplyDiscount(BillingModel bill, DiscountType discountType, decimal discountValue, string reason = null)
        {
            bill.DiscountType = discountType.ToString();

            switch (discountType)
            {
                case DiscountType.Percentage:
                    bill.DiscountAmount = bill.Subtotal * (discountValue / 100);
                    break;
                case DiscountType.FixedAmount:
                    bill.DiscountAmount = discountValue;
                    break;
                default:
                    // For other discount types, apply as percentage
                    bill.DiscountAmount = bill.Subtotal * (discountValue / 100);
                    break;
            }

            // Ensure discount doesn't exceed subtotal
            bill.DiscountAmount = Math.Min(bill.DiscountAmount, bill.Subtotal);

            if (!string.IsNullOrEmpty(reason))
            {
                bill.Notes = string.IsNullOrEmpty(bill.Notes) ? reason : $"{bill.Notes}; {reason}";
            }

            bill.CalculateTotals();
        }

        /// <summary>
        /// Processes a refund for a bill
        /// </summary>
        public PaymentTransaction ProcessRefund(BillingModel bill, decimal refundAmount, string reason, string processedBy)
        {
            var refundTransaction = new PaymentTransaction
            {
                BillId = bill.BillId,
                PaymentMethod = "Refund",
                Amount = refundAmount,
                TransactionType = "Refund",
                TransactionStatus = "Completed",
                Notes = reason,
                ProcessedBy = processedBy
            };

            refundTransaction.GenerateReferenceNumber();

            // Update bill status
            bill.AmountPaid -= refundAmount;
            if (bill.AmountPaid <= 0)
            {
                bill.PaymentStatus = "Refunded";
            }
            else
            {
                bill.PaymentStatus = "Partial";
            }

            bill.CalculateTotals();
            bill.PaymentTransactions.Add(refundTransaction);

            return refundTransaction;
        }

        /// <summary>
        /// Gets billing summary for reporting
        /// </summary>
        public BillingSummary GetBillingSummary(List<BillingModel> bills, DateTime startDate, DateTime endDate)
        {
            var filteredBills = bills.Where(b => b.DateBilled >= startDate && b.DateBilled <= endDate).ToList();

            return new BillingSummary
            {
                TotalBills = filteredBills.Count,
                TotalRevenue = filteredBills.Sum(b => b.TotalAmount),
                TotalPaid = filteredBills.Sum(b => b.AmountPaid),
                TotalOutstanding = filteredBills.Sum(b => b.BalanceDue),
                AverageTicket = filteredBills.Count > 0 ? filteredBills.Average(b => b.TotalAmount) : 0,
                PaidBills = filteredBills.Count(b => b.IsFullyPaid),
                OverdueBills = filteredBills.Count(b => b.IsOverdue),
                PendingBills = filteredBills.Count(b => b.PaymentStatus == "Pending"),
                StartDate = startDate,
                EndDate = endDate
            };
        }

        /// <summary>
        /// Validates if a bill can be processed
        /// </summary>
        public List<string> ValidateBill(BillingModel bill)
        {
            var errors = new List<string>();

            if (bill.CheckOutDate <= bill.CheckInDate)
                errors.Add("Check-out date must be after check-in date");

            if (bill.RoomRate < 0)
                errors.Add("Room rate cannot be negative");

            if (bill.Nights < 1)
                errors.Add("Number of nights must be at least 1");

            if (bill.TaxRate < 0 || bill.TaxRate > 1)
                errors.Add("Tax rate must be between 0 and 100%");

            if (bill.DiscountAmount > bill.Subtotal)
                errors.Add("Discount amount cannot exceed subtotal");

            if (string.IsNullOrWhiteSpace(bill.CustomerName))
                errors.Add("Customer name is required");

            if (string.IsNullOrWhiteSpace(bill.RoomNumber))
                errors.Add("Room number is required");

            return errors;
        }

        private string GetCategoryFromItemType(BillingItemType itemType)
        {
            return itemType switch
            {
                BillingItemType.Room => "Accommodation",
                BillingItemType.FoodBeverage => "F&B",
                BillingItemType.RoomService => "F&B",
                BillingItemType.Minibar => "F&B",
                BillingItemType.SpaServices => "Wellness",
                BillingItemType.FitnessCenter => "Wellness",
                BillingItemType.Laundry => "Services",
                BillingItemType.Telephone => "Communications",
                BillingItemType.Internet => "Communications",
                BillingItemType.Parking => "Transportation",
                BillingItemType.Transportation => "Transportation",
                BillingItemType.ConferenceRoom => "Business",
                BillingItemType.BusinessCenter => "Business",
                _ => "Other"
            };
        }

        private string DetermineCardType(string lastFour)
        {
            if (string.IsNullOrEmpty(lastFour) || lastFour.Length != 4)
                return "Unknown";

            var firstDigit = lastFour[0];
            return firstDigit switch
            {
                '4' => "Visa",
                '5' => "MasterCard",
                '3' => "American Express",
                '6' => "Discover",
                _ => "Other"
            };
        }
    }

    /// <summary>
    /// Summary class for billing reports
    /// </summary>
    public class BillingSummary
    {
        public int TotalBills { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalOutstanding { get; set; }
        public decimal AverageTicket { get; set; }
        public int PaidBills { get; set; }
        public int OverdueBills { get; set; }
        public int PendingBills { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal PaymentRate => TotalBills > 0 ? (decimal)PaidBills / TotalBills * 100 : 0;
        public decimal CollectionRate => TotalRevenue > 0 ? TotalPaid / TotalRevenue * 100 : 0;
    }
}