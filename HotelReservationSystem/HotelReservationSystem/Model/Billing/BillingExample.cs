using System;
using System.Collections.Generic;
using HotelReservationSystem.Model.Customer;
using HotelReservationSystem.Model.Reservation;
using HotelReservationSystem.Model.Rooms;

namespace HotelReservationSystem.Model.Billing
{
    /// <summary>
    /// Example class demonstrating how to use the comprehensive billing model system
    /// </summary>
    public class BillingExample
    {
        private readonly BillingService billingService;

        public BillingExample()
        {
            billingService = new BillingService();
        }

        /// <summary>
        /// Demonstrates a complete billing workflow
        /// </summary>
        public void DemonstrateCompleteBillingWorkflow()
        {
            Console.WriteLine("=== Hotel Billing System Demonstration ===\n");

            // 1. Create sample customer
            var customer = new CustomerModel
            {
                CustomerID = 1,
                FirstName = "John",
                LastName = "Doe",
                Contact = "555-0123",
                Address = "123 Main St, City, State 12345",
                IDType = "Driver's License"
            };

            // 2. Create sample room
            var room = new RoomModel
            {
                RoomId = 101,
                RoomNumber = "A-101",
                RoomType = "Deluxe King",
                RoomPrice = "199.99", // Note: This should ideally be decimal
                RoomStatus = "Occupied",
                BedCount = "1",
                RoomGuests = "2",
                RoomDescription = "Deluxe king room with city view"
            };

            // 3. Create sample reservation
            var reservation = new ReservationModel
            {
                ReservationId = 12345,
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                CheckInDate = DateTime.Now.Date,
                CheckOutDate = DateTime.Now.Date.AddDays(3),
                TotalPrice = 659.97m, // 3 nights × $199.99 + tax
                ReservationStatus = "Confirmed",
                CreatedAt = DateTime.Now.AddDays(-5)
            };

            Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"Room: {room.RoomNumber} ({room.RoomType})");
            Console.WriteLine($"Stay: {reservation.CheckInDate:yyyy-MM-dd} to {reservation.CheckOutDate:yyyy-MM-dd}");
            Console.WriteLine($"Nights: {(reservation.CheckOutDate - reservation.CheckInDate).Days}");
            Console.WriteLine();

            // 4. Create bill from reservation
            var bill = billingService.CreateBillFromReservation(reservation, customer, room);
            Console.WriteLine($"Bill Created - Invoice: {bill.InvoiceNumber}");
            Console.WriteLine($"Room Subtotal: {bill.RoomSubtotal:C}");
            Console.WriteLine($"Tax ({bill.TaxRate:P}): {bill.TaxAmount:C}");
            Console.WriteLine($"Total: {bill.TotalAmount:C}");
            Console.WriteLine();

            // 5. Add additional services
            Console.WriteLine("Adding additional services...");
            
            // Room service
            billingService.AddServiceCharge(bill, BillingItemType.RoomService, 
                "Breakfast - Continental", 2, 25.00m);
            
            // Spa services
            billingService.AddServiceCharge(bill, BillingItemType.SpaServices, 
                "60-minute Swedish Massage", 1, 120.00m);
            
            // Minibar
            billingService.AddServiceCharge(bill, BillingItemType.Minibar, 
                "Beverages and Snacks", 1, 45.50m);
            
            // Parking
            billingService.AddServiceCharge(bill, BillingItemType.Parking, 
                "Valet Parking - 3 nights", 3, 25.00m);

            Console.WriteLine($"Updated Total with Services: {bill.TotalAmount:C}");
            Console.WriteLine($"Additional Services: {bill.AdditionalServicesTotal:C}");
            Console.WriteLine();

            // 6. Apply discount
            Console.WriteLine("Applying 10% loyalty member discount...");
            billingService.ApplyDiscount(bill, DiscountType.LoyaltyMember, 10, "Gold member discount");
            Console.WriteLine($"Discount Applied: {bill.DiscountAmount:C}");
            Console.WriteLine($"Final Total: {bill.TotalAmount:C}");
            Console.WriteLine();

            // 7. Process payments
            Console.WriteLine("Processing payments...");
            
            // Partial payment by credit card
            var payment1 = billingService.ProcessPayment(bill, PaymentMethod.CreditCard, 
                400.00m, "Front Desk Clerk", "AUTH123456", "1234");
            Console.WriteLine($"Payment 1: {payment1.Amount:C} via {payment1.PaymentMethod}");
            Console.WriteLine($"Reference: {payment1.ReferenceNumber}");
            Console.WriteLine($"Processing Fee: {payment1.ProcessingFee:C}");
            Console.WriteLine($"Balance Due: {bill.BalanceDue:C}");
            Console.WriteLine();

            // Final payment by cash
            var payment2 = billingService.ProcessPayment(bill, PaymentMethod.Cash, 
                bill.BalanceDue, "Front Desk Manager");
            Console.WriteLine($"Payment 2: {payment2.Amount:C} via {payment2.PaymentMethod}");
            Console.WriteLine($"Final Balance: {bill.BalanceDue:C}");
            Console.WriteLine($"Payment Status: {bill.PaymentStatus}");
            Console.WriteLine($"Fully Paid: {bill.IsFullyPaid}");
            Console.WriteLine();

            // 8. Display detailed bill breakdown
            Console.WriteLine("=== DETAILED BILL BREAKDOWN ===");
            Console.WriteLine($"Invoice Number: {bill.InvoiceNumber}");
            Console.WriteLine($"Bill Date: {bill.DateBilled:yyyy-MM-dd}");
            Console.WriteLine($"Due Date: {bill.DueDate:yyyy-MM-dd}");
            Console.WriteLine();

            Console.WriteLine("LINE ITEMS:");
            Console.WriteLine($"Room ({bill.Nights} nights × {bill.RoomRate:C}): {bill.RoomSubtotal:C}");
            
            foreach (var lineItem in bill.LineItems)
            {
                Console.WriteLine($"{lineItem.ItemDescription} " +
                    $"({lineItem.Quantity} × {lineItem.UnitPrice:C}): {lineItem.TotalAmount:C}");
            }
            
            Console.WriteLine($"Subtotal: {bill.Subtotal:C}");
            Console.WriteLine($"Tax ({bill.TaxRate:P}): {bill.TaxAmount:C}");
            Console.WriteLine($"Discount ({bill.DiscountType}): -{bill.DiscountAmount:C}");
            Console.WriteLine($"TOTAL: {bill.TotalAmount:C}");
            Console.WriteLine();

            Console.WriteLine("PAYMENT HISTORY:");
            foreach (var payment in bill.PaymentTransactions)
            {
                Console.WriteLine($"{payment.TransactionDate:yyyy-MM-dd HH:mm} - " +
                    $"{payment.PaymentMethod}: {payment.Amount:C} " +
                    $"({payment.TransactionStatus}) - Ref: {payment.ReferenceNumber}");
            }
            Console.WriteLine();

            // 9. Validate bill
            var validationErrors = billingService.ValidateBill(bill);
            Console.WriteLine($"Bill Validation: {(validationErrors.Count == 0 ? "PASSED" : "FAILED")}");
            if (validationErrors.Count > 0)
            {
                Console.WriteLine("Validation Errors:");
                foreach (var error in validationErrors)
                {
                    Console.WriteLine($"- {error}");
                }
            }
            Console.WriteLine();

            // 10. Generate billing summary
            var bills = new List<BillingModel> { bill };
            var summary = billingService.GetBillingSummary(bills, DateTime.Now.Date, DateTime.Now.Date);
            
            Console.WriteLine("=== BILLING SUMMARY ===");
            Console.WriteLine($"Total Bills: {summary.TotalBills}");
            Console.WriteLine($"Total Revenue: {summary.TotalRevenue:C}");
            Console.WriteLine($"Total Paid: {summary.TotalPaid:C}");
            Console.WriteLine($"Average Ticket: {summary.AverageTicket:C}");
            Console.WriteLine($"Payment Rate: {summary.PaymentRate:F1}%");
            Console.WriteLine($"Collection Rate: {summary.CollectionRate:F1}%");
        }

        /// <summary>
        /// Demonstrates refund processing
        /// </summary>
        public void DemonstrateRefundProcess()
        {
            Console.WriteLine("\n=== REFUND DEMONSTRATION ===");
            
            // Create a simple paid bill
            var bill = new BillingModel
            {
                BillId = 999,
                TotalAmount = 300.00m,
                AmountPaid = 300.00m,
                PaymentStatus = "Paid"
            };

            Console.WriteLine($"Original Bill: {bill.TotalAmount:C} (Paid)");

            // Process partial refund
            var refund = billingService.ProcessRefund(bill, 50.00m, 
                "Minibar items not consumed", "Manager");
            
            Console.WriteLine($"Refund Processed: {refund.Amount:C}");
            Console.WriteLine($"Refund Reference: {refund.ReferenceNumber}");
            Console.WriteLine($"New Payment Status: {bill.PaymentStatus}");
            Console.WriteLine($"Remaining Paid Amount: {bill.AmountPaid:C}");
        }

        /// <summary>
        /// Demonstrates various discount scenarios
        /// </summary>
        public void DemonstrateDiscountScenarios()
        {
            Console.WriteLine("\n=== DISCOUNT SCENARIOS ===");

            var bill = new BillingModel
            {
                Subtotal = 500.00m,
                TaxRate = 0.08m
            };

            Console.WriteLine($"Base Subtotal: {bill.Subtotal:C}");

            // Percentage discount
            billingService.ApplyDiscount(bill, DiscountType.Percentage, 15, "15% off promotion");
            Console.WriteLine($"After 15% discount: {bill.TotalAmount:C} (Saved: {bill.DiscountAmount:C})");

            // Reset for fixed amount discount
            bill.DiscountAmount = 0;
            billingService.ApplyDiscount(bill, DiscountType.FixedAmount, 75, "$75 off coupon");
            Console.WriteLine($"After $75 fixed discount: {bill.TotalAmount:C} (Saved: {bill.DiscountAmount:C})");

            // Reset for senior discount
            bill.DiscountAmount = 0;
            billingService.ApplyDiscount(bill, DiscountType.SeniorDiscount, 20, "Senior citizen discount");
            Console.WriteLine($"After 20% senior discount: {bill.TotalAmount:C} (Saved: {bill.DiscountAmount:C})");
        }
    }
}