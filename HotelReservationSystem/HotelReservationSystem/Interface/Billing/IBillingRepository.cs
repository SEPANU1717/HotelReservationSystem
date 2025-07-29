using System.Collections.Generic;
using HotelReservationSystem.Model.Billing;

namespace HotelReservationSystem.Interface.Billing
{
    public interface IBillingRepository
    {
        void Add(BillingModel billing);
        void Edit(BillingModel billing);
        void Delete(int id);
        IEnumerable<BillingModel> GetAll();
        IEnumerable<BillingModel> GetByValue(string value);
        BillingModel GetById(int billId);
        BillingModel GetByReservationId(int reservationId);
        int GetNextBillId();
        IEnumerable<BillingModel> GetByPaymentStatus(string paymentStatus);
        IEnumerable<BillingModel> GetByDateRange(System.DateTime startDate, System.DateTime endDate);
        void UpdatePaymentStatus(int billId, string paymentStatus, decimal amountPaid);
    }
}