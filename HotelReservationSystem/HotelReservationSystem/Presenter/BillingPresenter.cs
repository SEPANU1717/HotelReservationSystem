using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelReservationSystem.Interface.Billing;
using HotelReservationSystem.Model.Billing;
using HotelReservationSystem.Presenter.Common;

namespace HotelReservationSystem.Presenter.Billing
{
    public class BillingPresenter
    {
        private IBillingView billingView;
        private IBillingRepository repository;
        private BindingSource BillingBindingSource;
        private IEnumerable<BillingModel> billingList;

        public BillingPresenter(IBillingView billingView, IBillingRepository repository)
        {
            BillingBindingSource = new BindingSource();
            this.repository = repository;
            this.billingView = billingView;

            this.billingView.SearchEvent += SearchBill;
            this.billingView.AddNewEvent += AddNewBill;
            this.billingView.EditEvent += EditBill;
            this.billingView.DeleteEvent += DeleteBill;
            this.billingView.SaveEvent += SaveBill;
            this.billingView.CancelEvent += CancelBill;

            this.billingView.SetBillingListBindingSource(BillingBindingSource);
            LoadAllReservationList();
        }

        private void LoadAllReservationList()
        {
            billingList = repository.GetAll();
            BillingBindingSource.DataSource = billingList;
            BillingBindingSource.ResetBindings(false);
        }

        private void CancelBill(object sender, EventArgs e) => CleanviewFields();

        private void SaveBill(object sender, EventArgs e)
        {
            var model = new BillingModel();
            int billId, reservationId;
            decimal totalAmount;

            int.TryParse(billingView.BillId, out billId);
            int.TryParse(billingView.ReservationId, out reservationId);
            decimal.TryParse(billingView.TotalAmount, out totalAmount);

            model.BillId = billId;
            model.ReservationId = reservationId;
            model.CustomerName = billingView.CustomerName;
            model.RoomType = billingView.RoomType;
            model.RoomNumber = billingView.RoomNumber;
            model.TotalAmount = totalAmount;
            model.PaymentStatus = billingView.PaymentStatus;

            try
            {
                new ModelDataValidation().Validate(model);
                if (billingView.isEdit)
                {
                    repository.Edit(model);
                    billingView.Message = "Billing edited successfully";
                }
                else
                {
                    repository.Add(model);
                    billingView.Message = "Billing added successfully";
                }
                billingView.isSuccessful = true;
                LoadAllReservationList();
            }
            catch (Exception ex)
            {
                billingView.isSuccessful = false;
                billingView.Message = ex.Message;
            }
        }

        private void DeleteBill(object sender, EventArgs e)
        {
            try
            {
                var billing = BillingBindingSource.Current as BillingModel;
                if (billing == null)
                {
                    billingView.isSuccessful = false;
                    billingView.Message = "No billing selected for deletion";
                    return;
                }
                repository.Delete(billing.BillId);
                billingView.isSuccessful = true;
                billingView.Message = "Billing deleted successfully";
                LoadAllReservationList();
            }
            catch (Exception ex)
            {
                billingView.isSuccessful = false;
                billingView.Message = "An error occurred, could not delete billing: " + ex.Message;
            }
        }

        private void EditBill(object sender, EventArgs e)
        {
            var billing = BillingBindingSource.Current as BillingModel;
            if (billing == null) return;

            billingView.BillId = billing.BillId.ToString();
            billingView.ReservationId = billing.ReservationId.ToString();
            billingView.CustomerName = billing.CustomerName;
            billingView.RoomType = billing.RoomType;
            billingView.RoomNumber = billing.RoomNumber;
            billingView.TotalAmount = billing.TotalAmount.ToString("0.00");
            billingView.PaymentStatus = billing.PaymentStatus;
            billingView.isEdit = true;
        }

        private void AddNewBill(object sender, EventArgs e) => billingView.isEdit = false;

        private void SearchBill(object sender, EventArgs e)
        {
            bool emptyvalue = string.IsNullOrWhiteSpace(billingView.SearchValue);
            billingList = emptyvalue
                ? repository.GetAll()
                : repository.GetByValue(billingView.SearchValue);

            BillingBindingSource.DataSource = billingList;
            BillingBindingSource.ResetBindings(false);
        }

        private void CleanviewFields()
        {
            billingView.BillId = "";
            billingView.ReservationId = "";
            billingView.CustomerName = "";
            billingView.RoomType = "";
            billingView.RoomNumber = "";
            billingView.TotalAmount = "";
            billingView.PaymentStatus = "";
        }
    }
}
