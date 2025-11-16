using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Interface.Billing;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Presenter.Common;

namespace HotelReservationSystem.Presenter.Billing
{
    /// <summary>
    /// Presenter for Billing module following MVP pattern
    /// Handles all billing business logic, validation, and authorization
    /// </summary>
    public class BillingPresenter
    {
        #region Fields
        private readonly IBillingView billingView;
        private readonly IBillingRepository repository;
        private readonly BindingSource BillingBindingSource;
        private IEnumerable<BillingModel> billingList;
        private static BillingPresenter _lastPresenterInstance;
        #endregion

        #region Constructor
        public BillingPresenter(IBillingView billingView, IBillingRepository repository)
        {
            BillingBindingSource = new BindingSource();
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.billingView = billingView ?? throw new ArgumentNullException(nameof(billingView));

            // Unsubscribe previous instance to prevent memory leaks
            if (_lastPresenterInstance != null)
                _lastPresenterInstance.UnsubscribeFromViewEvents();

            SubscribeToViewEvents();
            _lastPresenterInstance = this;

            this.billingView.SetBillingListBindingSource(BillingBindingSource);
            LoadAllBillingList();
        }
        #endregion

        #region Event Subscription
        private void SubscribeToViewEvents()
        {
            this.billingView.SearchEvent += SearchBill;
            this.billingView.AddNewEvent += AddNewBill;
            this.billingView.EditEvent += EditBill;
            this.billingView.DeleteEvent += DeleteBill;
            this.billingView.SaveEvent += SaveBill;
            this.billingView.CancelEvent += CancelBill;
        }

        private void UnsubscribeFromViewEvents()
        {
            this.billingView.SearchEvent -= SearchBill;
            this.billingView.AddNewEvent -= AddNewBill;
            this.billingView.EditEvent -= EditBill;
            this.billingView.DeleteEvent -= DeleteBill;
            this.billingView.SaveEvent -= SaveBill;
            this.billingView.CancelEvent -= CancelBill;
        }
        #endregion

        #region Data Loading
        private void LoadAllBillingList()
        {
            try
            {
                billingList = repository.GetAll() ?? Enumerable.Empty<BillingModel>();
                BillingBindingSource.DataSource = billingList;
                BillingBindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                billingView.ShowMessage(string.Format("Error loading billing records: {0}", ex.Message), "Error");
            }
        }
        #endregion

        #region Event Handlers

        private void SearchBill(object sender, EventArgs e)
        {
            try
            {
                bool emptyValue = string.IsNullOrWhiteSpace(billingView.SearchValue);
                billingList = emptyValue
                    ? repository.GetAll()
                    : repository.GetByValue(billingView.SearchValue);

                BillingBindingSource.DataSource = billingList ?? Enumerable.Empty<BillingModel>();
                BillingBindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                billingView.ShowMessage(string.Format("Error searching billing records: {0}", ex.Message), "Error");
            }
        }

        private void AddNewBill(object sender, EventArgs e)
        {
            // Authorization - Both Admin and Staff can add billing
            if (!UserSession.IsLoggedIn)
            {
                billingView.ShowMessage("You must be logged in to add billing records.", "Access Denied");
                return;
            }

            billingView.isEdit = false;
            billingView.BillId = repository.GetNextBillingId().ToString();
            billingView.DateBilled = DateTime.Now;
            billingView.BilledBy = UserSession.Username;
            billingView.ClearForm();
        }

        private void EditBill(object sender, EventArgs e)
        {
            try
            {
                // Authorization - Only Admin can edit billing
                if (!UserSession.IsAdmin)
                {
                    billingView.ShowMessage(
                        string.Format("{0} cannot edit billing records. Only administrators can modify billing.", 
                            UserSession.Role),
                        "Access Denied");
                    return;
                }

                var billing = BillingBindingSource.Current as BillingModel;
                if (billing == null)
                {
                    billingView.ShowMessage("Please select a billing record to edit.", "No Selection");
                    return;
                }

                // Populate view with selected billing data
                billingView.BillId = billing.BillId.ToString();
                billingView.ReservationId = billing.ReservationId.ToString();
                billingView.CustomerName = billing.CustomerName;
                billingView.RoomType = billing.RoomType;
                billingView.RoomNumber = billing.RoomNumber;
                
                billingView.CheckInDate = billing.CheckInDate;
                billingView.CheckOutDate = billing.CheckOutDate;
                billingView.ActualCheckOutDate = billing.ActualCheckOutDate;
                
                billingView.RoomCharge = billing.RoomCharge.ToString("F2");
                billingView.LateCheckoutFee = billing.LateCheckoutFee.ToString("F2");
                billingView.DamageFee = billing.DamageFee.ToString("F2");
                
                billingView.AmountPaidBefore = billing.AmountPaidBefore.ToString("F2");
                billingView.AmountPaidAtCheckout = billing.AmountPaidAtCheckout.ToString("F2");
                billingView.TotalAmount = billing.TotalAmount.ToString("F2");
                billingView.BalanceDue = billing.BalanceDue.ToString("F2");
                
                billingView.PaymentStatus = billing.PaymentStatus;
                billingView.PaymentMethod = billing.PaymentMethod;
                billingView.PaymentReference = billing.PaymentReference;
                
                billingView.DateBilled = billing.DateBilled;
                billingView.BilledBy = billing.BilledBy;

                billingView.isEdit = true;
            }
            catch (Exception ex)
            {
                billingView.ShowMessage(string.Format("Error loading billing for editing: {0}", ex.Message), "Error");
            }
        }

        private void SaveBill(object sender, EventArgs e)
        {
            try
            {
                // Parse all fields from view
                var model = new BillingModel
                {
                    BillId = int.TryParse(billingView.BillId, out int billId) ? billId : 0,
                    ReservationId = int.TryParse(billingView.ReservationId, out int resId) ? resId : 0,
                    CustomerName = billingView.CustomerName,
                    RoomType = billingView.RoomType,
                    RoomNumber = billingView.RoomNumber,
                    
                    CheckInDate = billingView.CheckInDate,
                    CheckOutDate = billingView.CheckOutDate,
                    ActualCheckOutDate = billingView.ActualCheckOutDate,
                    
                    RoomCharge = decimal.TryParse(billingView.RoomCharge, out decimal roomCharge) ? roomCharge : 0m,
                    LateCheckoutFee = decimal.TryParse(billingView.LateCheckoutFee, out decimal lateFee) ? lateFee : 0m,
                    DamageFee = decimal.TryParse(billingView.DamageFee, out decimal damageFee) ? damageFee : 0m,
                    
                    AmountPaidBefore = decimal.TryParse(billingView.AmountPaidBefore, out decimal paidBefore) ? paidBefore : 0m,
                    AmountPaidAtCheckout = decimal.TryParse(billingView.AmountPaidAtCheckout, out decimal paidCheckout) ? paidCheckout : 0m,
                    
                    PaymentStatus = billingView.PaymentStatus,
                    PaymentMethod = billingView.PaymentMethod,
                    PaymentReference = billingView.PaymentReference,
                    
                    DateBilled = billingView.DateBilled,
                    BilledBy = billingView.BilledBy
                };

                // Authorization
                if (billingView.isEdit)
                {
                    if (!UserSession.IsAdmin)
                    {
                        billingView.isSuccessful = false;
                        billingView.Message = string.Format("{0} cannot edit billing. Only administrators can modify billing records.", UserSession.Role);
                        billingView.ShowMessage(billingView.Message, "Access Denied");
                        return;
                    }
                }
                else
                {
                    // Both Admin and Staff can add billing
                    if (!UserSession.IsLoggedIn)
                    {
                        billingView.isSuccessful = false;
                        billingView.Message = "You must be logged in to create billing records.";
                        billingView.ShowMessage(billingView.Message, "Access Denied");
                        return;
                    }
                }

                // Validation
                new ModelDataValidation().Validate(model);

                // Business Rules Validation
                if (model.TotalAmount <= 0)
                {
                    billingView.ShowMessage("Total amount must be greater than zero.", "Validation Error");
                    return;
                }

                if (model.AmountPaidBefore < 0 || model.AmountPaidAtCheckout < 0)
                {
                    billingView.ShowMessage("Payment amounts cannot be negative.", "Validation Error");
                    return;
                }

                if (model.CheckOutDate <= model.CheckInDate)
                {
                    billingView.ShowMessage("Check-out date must be after check-in date.", "Validation Error");
                    return;
                }

                // Save operation
                if (billingView.isEdit)
                {
                    repository.Edit(model);
                    billingView.Message = "Billing record updated successfully!";
                }
                else
                {
                    repository.Add(model);
                    billingView.Message = "Billing record created successfully!";
                }

                billingView.isSuccessful = true;
                LoadAllBillingList();
                billingView.ShowMessage(billingView.Message, "Success");
            }
            catch (Exception ex)
            {
                billingView.isSuccessful = false;
                billingView.Message = ex.Message;
                billingView.ShowMessage(string.Format("Error saving billing: {0}", ex.Message), "Error");
            }
        }

        private void DeleteBill(object sender, EventArgs e)
        {
            try
            {
                // Authorization - Only Admin can delete billing
                if (!UserSession.IsAdmin)
                {
                    billingView.ShowMessage(
                        string.Format("{0} cannot delete billing records. Only administrators can delete billing.", 
                            UserSession.Role),
                        "Access Denied");
                    return;
                }

                var billing = BillingBindingSource.Current as BillingModel;
                if (billing == null)
                {
                    billingView.ShowMessage("Please select a billing record to delete.", "No Selection");
                    return;
                }

                var result = MessageBox.Show(
                    string.Format("Are you sure you want to delete billing for '{0}'?\n\nBill ID: {1}\nReservation ID: {2}\nTotal Amount: ${3:N2}",
                        billing.CustomerName, billing.BillId, billing.ReservationId, billing.TotalAmount),
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    repository.Delete(billing.BillId);
                    billingView.isSuccessful = true;
                    billingView.Message = "Billing record deleted successfully!";
                    LoadAllBillingList();
                    billingView.ShowMessage(billingView.Message, "Success");
                }
            }
            catch (Exception ex)
            {
                billingView.isSuccessful = false;
                billingView.Message = string.Format("Error deleting billing: {0}", ex.Message);
                billingView.ShowMessage(billingView.Message, "Error");
            }
        }

        private void CancelBill(object sender, EventArgs e)
        {
            billingView.ClearForm();
        }
        #endregion

        #region Public Helper Methods
        /// <summary>
        /// Load billing for a specific reservation (used from Check-Out flow)
        /// </summary>
        public void LoadBillingForReservation(int reservationId)
        {
            try
            {
                var billing = repository.GetByReservationId(reservationId);
                if (billing != null)
                {
                    // Populate view for editing existing billing
                    var dummyEventArgs = new EventArgs();
                    BillingBindingSource.DataSource = new List<BillingModel> { billing };
                    BillingBindingSource.Position = 0;
                    EditBill(this, dummyEventArgs);
                }
                else
                {
                    billingView.ShowMessage(
                        string.Format("No billing record found for Reservation ID: {0}", reservationId),
                        "Not Found");
                }
            }
            catch (Exception ex)
            {
                billingView.ShowMessage(
                    string.Format("Error loading billing for reservation: {0}", ex.Message),
                    "Error");
            }
        }
        #endregion
    }
}
