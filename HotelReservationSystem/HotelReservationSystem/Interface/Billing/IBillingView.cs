using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Interface.Billing
{
    public interface IBillingView
    {
        // Bill details
        string BillId { get; set; }
        string ReservationId { get; set; }
        string CustomerName { get; set; }
        string RoomNumber { get; set; }
        string RoomType { get; set; }
        DateTime CheckInDate { get; set; }
        DateTime CheckOutDate { get; set; }
        string NumberOfNights { get; set; }
        string RoomRate { get; set; }
        string RoomTotal { get; set; }
        string ServiceCharges { get; set; }
        string TaxAmount { get; set; }
        string DiscountAmount { get; set; }
        string AdditionalCharges { get; set; }
        string AdditionalChargesDescription { get; set; }
        string Subtotal { get; set; }
        string TotalAmount { get; set; }
        string AmountPaid { get; set; }
        string BalanceDue { get; set; }
        string PaymentMethod { get; set; }
        string PaymentStatus { get; set; }
        DateTime BillDate { get; set; }
        DateTime DueDate { get; set; }
        string Notes { get; set; }

        // Search or filter
        string SearchValue { get; set; }

        // Flags
        bool isEdit { get; set; }
        bool isSuccessful { get; set; }
        string Message { get; set; }

        // Events for CRUD operations
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        event EventHandler CalculateEvent;
        event EventHandler PrintBillEvent;
        event EventHandler ProcessPaymentEvent;

        // Load data to DataGridView
        void SetBillingListBindingSource(BindingSource billingList);
        void Show();
    }
}