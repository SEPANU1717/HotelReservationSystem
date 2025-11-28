using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Domain.Interface.Billing
{

    public interface IBillingView
    {
        string BillId { get; set; }
        string ReservationId { get; set; }
        
        string CustomerName { get; set; }
        string RoomType { get; set; }
        string RoomNumber { get; set; }
        
        // Dates
        DateTime CheckInDate { get; set; }
        DateTime CheckOutDate { get; set; }
        DateTime? ActualCheckOutDate { get; set; }
        
        string RoomCharge { get; set; }
        string LateCheckoutFee { get; set; }
        string DamageFee { get; set; }
        
        string AmountPaidBefore { get; set; }
        string AmountPaidAtCheckout { get; set; }
        string TotalAmount { get; set; }
        string BalanceDue { get; set; }
        string PaymentStatus { get; set; }
        string PaymentMethod { get; set; }
        string PaymentReference { get; set; }
        
        DateTime DateBilled { get; set; }
        string BilledBy { get; set; }
        
        string SearchValue { get; set; }
        bool isEdit { get; set; }
        bool isSuccessful { get; set; }
        string Message { get; set; }

        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        event EventHandler CheckoutCompletedEvent;
        event EventHandler PrintInvoiceEvent;
        event EventHandler EmailInvoiceEvent;

        void SetBillingListBindingSource(BindingSource billingList);
        void ShowMessage(string message, string title);
        void ClearForm();
        void ShowBillingForm();
        void ShowGridView();
        int GetSelectedBillId();
    }
}
