using System.Collections.Generic;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Domain.Interface.Billing
{
    /// <summary>
    /// Repository interface for Billing data access following Repository pattern
    /// </summary>
    public interface IBillingRepository
    {
        // CRUD Operations
        void Add(BillingModel billingModel);
        void Edit(BillingModel billingModel);
        void Delete(int id);

        // Query Operations
        IEnumerable<BillingModel> GetAll();
        IEnumerable<BillingModel> GetByValue(string value);
        BillingModel GetByReservationId(int reservationId);
        BillingModel GetById(int billId);
        
        // Helper Methods
        bool ExistsForReservation(int reservationId);
        int GetNextBillingId();
    }
}
