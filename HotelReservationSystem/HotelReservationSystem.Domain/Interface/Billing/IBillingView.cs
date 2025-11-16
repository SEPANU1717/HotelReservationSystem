using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Domain.Interface.Billing
{
    /// <summary>
    /// View interface for Billing module following MVP pattern
    /// </summary>
    public interface IBillingView
    {
        // Identity
        string BillId { get; set; }
        string ReservationId { get; set; }
        
        // Guest Information
        string CustomerName { get; set; }
        string RoomType { get; set; }
        string RoomNumber { get; set; }
        
        // Dates
        DateTime CheckInDate { get; set; }
        DateTime CheckOutDate { get; set; }
        DateTime? ActualCheckOutDate { get; set; }
        
        // Charges (Simplified - Only 3 charge types)
        string RoomCharge { get; set; }
        string LateCheckoutFee { get; set; }
        string DamageFee { get; set; }
        
        // Payment Information
        string AmountPaidBefore { get; set; }
        string AmountPaidAtCheckout { get; set; }
        string TotalAmount { get; set; }
        string BalanceDue { get; set; }
        string PaymentStatus { get; set; }
        string PaymentMethod { get; set; }
        string PaymentReference { get; set; }
        
        // Billing Metadata
        DateTime DateBilled { get; set; }
        string BilledBy { get; set; }
        
        // UI State
        string SearchValue { get; set; }
        bool isEdit { get; set; }
        bool isSuccessful { get; set; }
        string Message { get; set; }

        // Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        event EventHandler CheckoutCompletedEvent;

        // Methods
        void SetBillingListBindingSource(BindingSource billingList);
        void ShowMessage(string message, string title);
        void ClearForm();
        void ShowBillingForm();
    }
}
