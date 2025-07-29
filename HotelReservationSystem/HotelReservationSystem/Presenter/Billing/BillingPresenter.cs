using System;
using System.Collections.Generic;
using System.Linq;
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
            this.billingView = billingView;
            this.repository = repository;

            // Subscribe to view events
            this.billingView.SearchEvent += SearchBilling;
            this.billingView.DeleteEvent += DeleteBilling;
            this.billingView.AddNewEvent += AddNewBilling;
            this.billingView.EditEvent += EditBilling;
            this.billingView.SaveEvent += SaveBilling;
            this.billingView.CancelEvent += CancelBilling;
            this.billingView.CalculateEvent += CalculateBilling;
            this.billingView.PrintBillEvent += PrintBill;
            this.billingView.ProcessPaymentEvent += ProcessPayment;

            this.billingView.SetBillingListBindingSource(BillingBindingSource);
            LoadAllBillingList();
            this.billingView.Show();
        }

        private void LoadAllBillingList()
        {
            billingList = repository.GetAll();
            BillingBindingSource.DataSource = billingList;
            BillingBindingSource.ResetBindings(false);
        }

        private void CancelBilling(object sender, EventArgs e) => CleanViewFields();

        private void SaveBilling(object sender, EventArgs e)
        {
            var billing = new BillingModel();
            BillingModel model = MapViewToBillingModel();
            
            try
            {
                // Validate required fields
                new ModelDataValidation().Validate(model);

                if (billingView.isEdit)
                {
                    // Update existing billing
                    repository.Edit(model);
                    billingView.Message = "Billing updated successfully";
                }
                else
                {
                    // Add new billing
                    repository.Add(model);
                    billingView.Message = "Billing created successfully";
                }

                billingView.isSuccessful = true;
                LoadAllBillingList();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                billingView.isSuccessful = false;
                billingView.Message = ex.Message;
            }
        }

        private BillingModel MapViewToBillingModel()
        {
            var model = new BillingModel();
            
            // Parse IDs
            int.TryParse(billingView.BillId, out int billId);
            int.TryParse(billingView.ReservationId.Split('-')[0].Trim(), out int reservationId);
            
            // Parse numeric values
            int.TryParse(billingView.NumberOfNights, out int nights);
            decimal.TryParse(billingView.RoomRate, out decimal roomRate);
            decimal.TryParse(billingView.ServiceCharges, out decimal serviceCharges);
            decimal.TryParse(billingView.TaxAmount, out decimal taxAmount);
            decimal.TryParse(billingView.DiscountAmount, out decimal discountAmount);
            decimal.TryParse(billingView.AdditionalCharges, out decimal additionalCharges);
            decimal.TryParse(billingView.AmountPaid, out decimal amountPaid);

            model.BillId = billId;
            model.ReservationId = reservationId;
            model.CustomerName = billingView.CustomerName;
            model.RoomNumber = billingView.RoomNumber;
            model.RoomType = billingView.RoomType;
            model.CheckInDate = billingView.CheckInDate;
            model.CheckOutDate = billingView.CheckOutDate;
            model.NumberOfNights = nights;
            model.RoomRate = roomRate;
            model.ServiceCharges = serviceCharges;
            model.TaxAmount = taxAmount;
            model.DiscountAmount = discountAmount;
            model.AdditionalCharges = additionalCharges;
            model.AdditionalChargesDescription = billingView.AdditionalChargesDescription;
            model.AmountPaid = amountPaid;
            model.PaymentMethod = billingView.PaymentMethod;
            model.PaymentStatus = billingView.PaymentStatus;
            model.BillDate = billingView.BillDate;
            model.DueDate = billingView.DueDate;
            model.Notes = billingView.Notes;

            // Calculate totals
            model.CalculateTotals();

            if (!billingView.isEdit)
            {
                model.CreatedAt = DateTime.Now;
            }
            model.UpdatedAt = DateTime.Now;

            return model;
        }

        private void EditBilling(object sender, EventArgs e)
        {
            var selectedBilling = (BillingModel)BillingBindingSource.Current;
            if (selectedBilling != null)
            {
                billingView.BillId = selectedBilling.BillId.ToString();
                billingView.ReservationId = selectedBilling.ReservationId.ToString();
                billingView.CustomerName = selectedBilling.CustomerName;
                billingView.RoomNumber = selectedBilling.RoomNumber;
                billingView.RoomType = selectedBilling.RoomType;
                billingView.CheckInDate = selectedBilling.CheckInDate;
                billingView.CheckOutDate = selectedBilling.CheckOutDate;
                billingView.NumberOfNights = selectedBilling.NumberOfNights.ToString();
                billingView.RoomRate = selectedBilling.RoomRate.ToString("0.00");
                billingView.RoomTotal = selectedBilling.RoomTotal.ToString("0.00");
                billingView.ServiceCharges = selectedBilling.ServiceCharges.ToString("0.00");
                billingView.TaxAmount = selectedBilling.TaxAmount.ToString("0.00");
                billingView.DiscountAmount = selectedBilling.DiscountAmount.ToString("0.00");
                billingView.AdditionalCharges = selectedBilling.AdditionalCharges.ToString("0.00");
                billingView.AdditionalChargesDescription = selectedBilling.AdditionalChargesDescription;
                billingView.Subtotal = selectedBilling.Subtotal.ToString("0.00");
                billingView.TotalAmount = selectedBilling.TotalAmount.ToString("0.00");
                billingView.AmountPaid = selectedBilling.AmountPaid.ToString("0.00");
                billingView.BalanceDue = selectedBilling.BalanceDue.ToString("0.00");
                billingView.PaymentMethod = selectedBilling.PaymentMethod;
                billingView.PaymentStatus = selectedBilling.PaymentStatus;
                billingView.BillDate = selectedBilling.BillDate;
                billingView.DueDate = selectedBilling.DueDate;
                billingView.Notes = selectedBilling.Notes;
                billingView.isEdit = true;
            }
        }

        private void AddNewBilling(object sender, EventArgs e)
        {
            billingView.isEdit = false;
            CleanViewFields();
        }

        private void DeleteBilling(object sender, EventArgs e)
        {
            try
            {
                var selectedBilling = (BillingModel)BillingBindingSource.Current;
                if (selectedBilling != null)
                {
                    repository.Delete(selectedBilling.BillId);
                    billingView.isSuccessful = true;
                    billingView.Message = "Billing deleted successfully";
                    LoadAllBillingList();
                }
                else
                {
                    billingView.isSuccessful = false;
                    billingView.Message = "Please select a billing record to delete";
                }
            }
            catch (Exception ex)
            {
                billingView.isSuccessful = false;
                billingView.Message = ex.Message;
            }
        }

        private void SearchBilling(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(billingView.SearchValue);
            if (emptyValue == false)
            {
                billingList = repository.GetByValue(billingView.SearchValue);
            }
            else
            {
                billingList = repository.GetAll();
            }
            BillingBindingSource.DataSource = billingList;
            BillingBindingSource.ResetBindings(false);
        }

        private void CalculateBilling(object sender, EventArgs e)
        {
            try
            {
                // Parse values from view
                decimal.TryParse(billingView.RoomRate, out decimal roomRate);
                int.TryParse(billingView.NumberOfNights, out int nights);
                decimal.TryParse(billingView.ServiceCharges, out decimal serviceCharges);
                decimal.TryParse(billingView.TaxAmount, out decimal taxAmount);
                decimal.TryParse(billingView.DiscountAmount, out decimal discountAmount);
                decimal.TryParse(billingView.AdditionalCharges, out decimal additionalCharges);
                decimal.TryParse(billingView.AmountPaid, out decimal amountPaid);

                // Calculate totals
                decimal roomTotal = roomRate * nights;
                decimal subtotal = roomTotal + serviceCharges + additionalCharges - discountAmount;
                decimal totalAmount = subtotal + taxAmount;
                decimal balanceDue = totalAmount - amountPaid;

                // Update view with calculated values
                billingView.RoomTotal = roomTotal.ToString("0.00");
                billingView.Subtotal = subtotal.ToString("0.00");
                billingView.TotalAmount = totalAmount.ToString("0.00");
                billingView.BalanceDue = balanceDue.ToString("0.00");

                billingView.Message = "Totals calculated successfully";
            }
            catch (Exception ex)
            {
                billingView.Message = "Error calculating totals: " + ex.Message;
            }
        }

        private void PrintBill(object sender, EventArgs e)
        {
            try
            {
                var selectedBilling = (BillingModel)BillingBindingSource.Current;
                if (selectedBilling != null || !string.IsNullOrEmpty(billingView.BillId))
                {
                    // Get the billing record to print
                    BillingModel billToPrint = selectedBilling;
                    if (billToPrint == null && !string.IsNullOrEmpty(billingView.BillId))
                    {
                        int.TryParse(billingView.BillId, out int billId);
                        billToPrint = repository.GetById(billId);
                    }

                    if (billToPrint != null)
                    {
                        // Here you would implement your printing logic
                        // For now, we'll show a message
                        string billDetails = GenerateBillText(billToPrint);
                        MessageBox.Show(billDetails, "Bill Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        billingView.Message = "Bill printed successfully";
                    }
                    else
                    {
                        billingView.Message = "No bill selected for printing";
                    }
                }
                else
                {
                    billingView.Message = "Please select a bill to print";
                }
            }
            catch (Exception ex)
            {
                billingView.Message = "Error printing bill: " + ex.Message;
            }
        }

        private string GenerateBillText(BillingModel bill)
        {
            return $@"
HOTEL BILL
==========
Bill ID: {bill.BillId}
Date: {bill.BillDate:yyyy-MM-dd}

Customer: {bill.CustomerName}
Room: {bill.RoomNumber} ({bill.RoomType})
Stay: {bill.CheckInDate:yyyy-MM-dd} to {bill.CheckOutDate:yyyy-MM-dd}
Nights: {bill.NumberOfNights}

CHARGES:
Room Rate: ${bill.RoomRate:0.00} x {bill.NumberOfNights} nights = ${bill.RoomTotal:0.00}
Service Charges: ${bill.ServiceCharges:0.00}
Additional Charges: ${bill.AdditionalCharges:0.00}
Discount: -${bill.DiscountAmount:0.00}
Subtotal: ${bill.Subtotal:0.00}
Tax: ${bill.TaxAmount:0.00}

TOTAL AMOUNT: ${bill.TotalAmount:0.00}
Amount Paid: ${bill.AmountPaid:0.00}
BALANCE DUE: ${bill.BalanceDue:0.00}

Payment Method: {bill.PaymentMethod}
Payment Status: {bill.PaymentStatus}
Due Date: {bill.DueDate:yyyy-MM-dd}

Notes: {bill.Notes}
";
        }

        private void ProcessPayment(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(billingView.BillId))
                {
                    int.TryParse(billingView.BillId, out int billId);
                    decimal.TryParse(billingView.AmountPaid, out decimal amountPaid);
                    string paymentStatus = billingView.PaymentStatus;

                    // Update payment status in repository
                    repository.UpdatePaymentStatus(billId, paymentStatus, amountPaid);
                    
                    billingView.Message = "Payment processed successfully";
                    LoadAllBillingList();
                    
                    // Recalculate balance
                    CalculateBilling(sender, e);
                }
                else
                {
                    billingView.Message = "Please select a bill to process payment";
                }
            }
            catch (Exception ex)
            {
                billingView.Message = "Error processing payment: " + ex.Message;
            }
        }

        private void CleanViewFields()
        {
            billingView.BillId = "0";
            billingView.ReservationId = "";
            billingView.CustomerName = "";
            billingView.RoomNumber = "";
            billingView.RoomType = "";
            billingView.CheckInDate = DateTime.Now;
            billingView.CheckOutDate = DateTime.Now.AddDays(1);
            billingView.NumberOfNights = "1";
            billingView.RoomRate = "0.00";
            billingView.RoomTotal = "0.00";
            billingView.ServiceCharges = "0.00";
            billingView.TaxAmount = "0.00";
            billingView.DiscountAmount = "0.00";
            billingView.AdditionalCharges = "0.00";
            billingView.AdditionalChargesDescription = "";
            billingView.Subtotal = "0.00";
            billingView.TotalAmount = "0.00";
            billingView.AmountPaid = "0.00";
            billingView.BalanceDue = "0.00";
            billingView.PaymentMethod = "";
            billingView.PaymentStatus = "Pending";
            billingView.BillDate = DateTime.Now;
            billingView.DueDate = DateTime.Now.AddDays(30);
            billingView.Notes = "";
        }
    }
}