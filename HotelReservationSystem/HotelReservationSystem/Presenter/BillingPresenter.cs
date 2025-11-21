using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.Data.Repositories.CheckInOutRepository;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Enums;
using HotelReservationSystem.Domain.Interface.Billing;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Domain.Model.CheckInOut;
using HotelReservationSystem.Domain.Services;
using HotelReservationSystem.Presenter.Common;
using HotelReservationSystem.Presenter.Mapper;

namespace HotelReservationSystem.Presenter.Billing
{
    public class BillingPresenter
    {
        #region Fields
        private readonly IBillingView billingView;
        private readonly IBillingRepository repository;
        private readonly BindingSource BillingBindingSource;
        private IEnumerable<BillingModel> billingList;
        private static BillingPresenter _lastPresenterInstance;
        private bool isFromCheckout = false;
        private Action onCheckoutCompleted;
        #endregion

        #region Constructor
        public BillingPresenter(IBillingView billingView, IBillingRepository repository)
        {
            BillingBindingSource = new BindingSource();
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.billingView = billingView ?? throw new ArgumentNullException(nameof(billingView));

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
            this.billingView.PrintInvoiceEvent += PrintInvoice;
            this.billingView.EmailInvoiceEvent += EmailInvoice;
        }

        private void UnsubscribeFromViewEvents()
        {
            this.billingView.SearchEvent -= SearchBill;
            this.billingView.AddNewEvent -= AddNewBill;
            this.billingView.EditEvent -= EditBill;
            this.billingView.DeleteEvent -= DeleteBill;
            this.billingView.SaveEvent -= SaveBill;
            this.billingView.CancelEvent -= CancelBill;
            this.billingView.PrintInvoiceEvent -= PrintInvoice;
            this.billingView.EmailInvoiceEvent -= EmailInvoice;
        }
        #endregion

        #region Data Loading
        private void LoadAllBillingList()
        {
            try
            {
                billingList = repository.GetAll() ?? Enumerable.Empty<BillingModel>();

                var dtoList = billingList.Select(BillingMapper.ToDto).ToList();

                BillingBindingSource.DataSource = dtoList;
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

                var dtoList = (billingList ?? Enumerable.Empty<BillingModel>())
                    .Select(BillingMapper.ToDto)
                    .ToList();

                BillingBindingSource.DataSource = dtoList;
                BillingBindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                billingView.ShowMessage(string.Format("Error searching billing records: {0}", ex.Message), "Error");
            }
        }

        private void AddNewBill(object sender, EventArgs e)
        {
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
            
            billingView.ShowBillingForm();
        }

        private void EditBill(object sender, EventArgs e)
        {
            try
            {
                if (!UserSession.IsAdmin)
                {
                    billingView.ShowMessage(
                        string.Format("{0} cannot edit billing records. Only administrators can modify billing.",
                            UserSession.Role),
                        "Access Denied");
                    return;
                }

                var dto = BillingBindingSource.Current as BillingDto;
                if (dto == null)
                {
                    billingView.ShowMessage("Please select a billing record to edit.", "No Selection");
                    return;
                }

                var billing = billingList.FirstOrDefault(b => b.BillId == dto.BillId);
                if (billing == null)
                {
                    billingView.ShowMessage("Billing record not found.", "Error");
                    return;
                }

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
                
                billingView.ShowBillingForm();
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
                // Get customer details for preservation - but don't rely only on Customers table
                var customerRepo = new CustomerRepository(DbConfig.GetConnectionString());
                var customer = customerRepo.GetByCustomerName(billingView.CustomerName);
                
                // ✅ NEW: Get email from check-in if customer doesn't exist (walk-in scenario)
                string customerEmail = customer?.Email;
                string customerContact = customer?.Contact;
                string customerAddress = customer?.Address;
                
                // If customer doesn't exist, try to get info from check-in record
                if (customer == null)
                {
                    var checkInRepo = new CheckInOutRepository(DbConfig.GetConnectionString());
                    if (int.TryParse(billingView.ReservationId, out int resId))
                    {
                        var checkIn = checkInRepo.GetByReservationId(resId);
                        if (checkIn != null)
                        {
                            customerEmail = checkIn.CustomerEmail;
                            // For walk-ins, we don't have contact/address, leave them null
                        }
                    }
                }
                
                var model = new BillingModel
                {
                    BillId = int.TryParse(billingView.BillId, out int billId) ? billId : 0,
                    ReservationId = int.TryParse(billingView.ReservationId, out int resId2) ? resId2 : 0,
                    CustomerName = billingView.CustomerName,
                    CustomerEmail = customerEmail, // ✅ NOW INCLUDES WALK-IN EMAILS
                    CustomerContact = customerContact,
                    CustomerAddress = customerAddress,
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
                else if (!UserSession.IsLoggedIn)
                {
                    billingView.isSuccessful = false;
                    billingView.Message = "You must be logged in to create billing records.";
                    billingView.ShowMessage(billingView.Message, "Access Denied");
                    return;
                }

                new ModelDataValidation().Validate(model);

                if (model.RoomCharge + model.LateCheckoutFee + model.DamageFee <= 0)
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

                decimal total = model.RoomCharge + model.LateCheckoutFee + model.DamageFee;
                decimal totalPaid = model.AmountPaidBefore + model.AmountPaidAtCheckout;
                decimal balance = total - totalPaid;
                decimal change = 0m;

                // Handle overpayment - NO CONFIRMATION, just calculate
                if (totalPaid > total)
                {
                    change = totalPaid - total;
                    balance = 0; // Set balance to 0, not negative
                    model.PaymentStatus = "Paid";
                    billingView.PaymentStatus = "Paid";
                }
                else if (balance <= 0)
                {
                    model.PaymentStatus = "Paid";
                    billingView.PaymentStatus = "Paid";
                }
                else if (totalPaid > 0 && totalPaid < total)
                {
                    model.PaymentStatus = "Partial";
                    billingView.PaymentStatus = "Partial";
                }
                else
                {
                    model.PaymentStatus = string.IsNullOrEmpty(model.PaymentStatus) ? "Pending" : model.PaymentStatus;
                    billingView.PaymentStatus = model.PaymentStatus;
                }
                
                if (billingView.isEdit)
                {
                    repository.Edit(model);
                    billingView.Message = "Billing record updated successfully!";
                    
                    if (change > 0)
                    {
                        billingView.Message += string.Format("\n\nChange Due: ₱{0:N2}\nPlease return to customer.", change);
                    }
                }
                else
                {
                    repository.Add(model);
                    billingView.Message = "Billing record created successfully!";
                    
                    if (change > 0)
                    {
                        billingView.Message += string.Format("\n\nChange Due: ₱{0:N2}\nPlease return to customer.", change);
                    }
                }
                
                if (isFromCheckout)
                {
                    if (balance > 0)
                    {
                        billingView.ShowMessage("Billing saved as Partial. Please settle remaining balance before final checkout.", "Partial Payment");
                        billingView.isSuccessful = true;
                        LoadAllBillingList();
                        return; 
                    }
                    CompleteCheckoutProcess(model);
                }

                billingView.isSuccessful = true;
                LoadAllBillingList();
                billingView.ShowMessage(billingView.Message, "Success");

                if (isFromCheckout)
                {
                    isFromCheckout = false;
                    onCheckoutCompleted?.Invoke();
                    onCheckoutCompleted = null;
                }
            }
            catch (Exception ex)
            {
                billingView.isSuccessful = false;
                billingView.Message = ex.Message;
                billingView.ShowMessage(string.Format("Error saving billing: {0}", ex.Message), "Error");
            }
        }

        private void CompleteCheckoutProcess(BillingModel billing)
        {
            try
            {
                var checkInRepo = new CheckInOutRepository(
                    HotelReservationSystem.DataInitializer.DbInitializer.DbConfig.GetConnectionString());

                var checkIn = checkInRepo.GetByReservationId(billing.ReservationId);
                if (checkIn != null)
                {
                    // Update check-in AmountPaid to billing totals (covers amount paid before + at checkout)
                    checkIn.AmountPaid = billing.TotalPaid;

                    checkIn.PaymentMethod = billing.PaymentMethod;
                    checkIn.PaymentReference = billing.PaymentReference ?? string.Empty;

                    // Update payment state based on totals
                    if (checkIn.AmountPaid >= checkIn.GrandTotal)
                        checkIn.PaymentStatus = HotelReservationSystem.Domain.Enums.ReservationEnum.PaymentState.FullPayment;
                    else if (checkIn.AmountPaid > 0)
                        checkIn.PaymentStatus = HotelReservationSystem.Domain.Enums.ReservationEnum.PaymentState.Partial;
                    else
                        checkIn.PaymentStatus = HotelReservationSystem.Domain.Enums.ReservationEnum.PaymentState.Pending;

                    checkIn.ReservationStatus = "CheckedOut";
                    checkIn.IsCheckedOut = true;
                    checkIn.ActualCheckOut = billing.ActualCheckOutDate ?? DateTime.Now;
                    checkIn.CheckedOutBy = UserSession.Username;
                    
                    checkInRepo.Edit(checkIn);
                }
                else
                {
                    checkInRepo.CheckOut(
                        billing.ReservationId,
                        billing.ActualCheckOutDate ?? DateTime.Now,
                        UserSession.Username);
                }

                var reserveRepo = new ReservationRepository(
                    DbConfig.GetConnectionString());

                var reservation = reserveRepo.GetById(billing.ReservationId);
                if (reservation != null)
                {
                    reservation.ReservationStatus = "CheckedOut";
                    
                    // Always update reservation AmountPaid from billing totals
                    reservation.AmountPaid = billing.TotalPaid;
                    
                    if (reservation.AmountPaid >= reservation.TotalPrice)
                        reservation.PaymentStatus = ReservationEnum.PaymentState.FullPayment;
                    else if (reservation.AmountPaid > 0)
                        reservation.PaymentStatus = ReservationEnum.PaymentState.Partial;
                    else
                        reservation.PaymentStatus = ReservationEnum.PaymentState.Pending;

                    reserveRepo.Edit(reservation);
                }

                var roomRepo = new  RoomRepository(
                   DbConfig.GetConnectionString());

                var room = roomRepo.GetByNumber(billing.RoomNumber);
                if (room != null)
                {
                    room.RoomStatus = "Available";
                    roomRepo.Edit(room);
                }
                
                PromptCustomerDeletion(billing.CustomerName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error completing checkout process: {0}", ex.Message));
                throw;
            }
        }
        
        private void PromptCustomerDeletion(string customerName)
        {
            try
            {
                var customerRepo = new CustomerRepository(DbConfig.GetConnectionString());
                var customer = customerRepo.GetByCustomerName(customerName);
                
                if (customer == null)
                {
                    return;
                }
                
                var reserveRepo = new ReservationRepository(DbConfig.GetConnectionString());
                var futureReservations = reserveRepo.GetAll()
                    .Where(r => r.CustomerName == customerName && 
                                r.CheckInDate > DateTime.Now && 
                                r.ReservationStatus != "CheckedOut" && 
                                r.ReservationStatus != "Cancelled")
                    .ToList();
                
                if (futureReservations.Any())
                {
                    MessageBox.Show(
                        string.Format("Customer '{0}' has {1} upcoming reservation(s).\n\n" +
                            "Customer information cannot be removed while active reservations exist.\n\n" +
                            "Future reservations:\n{2}",
                            customer.FullName,
                            futureReservations.Count,
                            string.Join("\n", futureReservations.Select(r => 
                                $"- {r.CheckInDate:MMM dd, yyyy} to {r.CheckOutDate:MMM dd, yyyy} (Room {r.RoomNumber})"))),
                        "Cannot Remove Customer",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                
                var checkInRepo = new CheckInOutRepository(DbConfig.GetConnectionString());
                var pastVisits = checkInRepo.GetAll()
                    .Where(c => c.CustomerName == customerName)
                    .Count();
                
                string visitInfo = pastVisits == 1 
                    ? "This is a one-time guest."
                    : string.Format("⚠️ REPEAT GUEST: This customer has visited {0} times.", pastVisits);
                
                string recommendation = pastVisits > 1
                    ? "\n\nRECOMMENDATION: Keep this customer for future bookings."
                    : "";
                
                var result = MessageBox.Show(
                    string.Format("Checkout completed successfully!\n\n" +
                        "Would you like to remove the customer information from the Customer module?\n\n" +
                        "Customer: {0}\n" +
                        "Email: {1}\n" +
                        "Contact: {2}\n" +
                        "Visit History: {3}{4}\n\n" +
                        "Note: Customer information will be removed from the Customer list, " +
                        "but all billing, reservation, and check-in records will be preserved " +
                        "for historical purposes and printing.\n\n" +
                        "Delete customer from Customer module?",
                        customer.FullName,
                        customer.Email ?? "N/A",
                        customer.Contact ?? "N/A",
                        visitInfo,
                        recommendation),
                    "Remove Customer Information",
                    MessageBoxButtons.YesNo,
                    pastVisits > 1 ? MessageBoxIcon.Question : MessageBoxIcon.Information);
                
                if (result == DialogResult.Yes)
                {
                    customerRepo.Delete(customer.CustomerID);
                    
                    MessageBox.Show(
                        string.Format("Customer '{0}' has been removed from the Customer module.\n\n" +
                            "All billing and historical records have been preserved.",
                            customer.FullName),
                        "Customer Removed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Note: Customer information could not be removed.\n\nReason: {0}\n\n" +
                        "The customer record may still be in use. " +
                        "All billing records have been saved successfully.",
                        ex.Message),
                    "Customer Deletion Note",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void DeleteBill(object sender, EventArgs e)
        {
            try
            {
                if (!UserSession.IsAdmin)
                {
                    billingView.ShowMessage(
                        string.Format("{0} cannot delete billing records. Only administrators can delete billing.",
                            UserSession.Role),
                        "Access Denied");
                    return;
                }

                var dto = BillingBindingSource.Current as BillingDto;
                if (dto == null)
                {
                    billingView.ShowMessage("Please select a billing record to delete.", "No Selection");
                    return;
                }

                var billing = billingList.FirstOrDefault(b => b.BillId == dto.BillId);
                if (billing == null)
                {
                    billingView.ShowMessage("Billing record not found.", "Error");
                    return;
                }

                var result = MessageBox.Show(
                    string.Format("Are you sure you want to delete billing for '{0}'?\n\nBill ID: {1}\nReservation ID: {2}\nTotal Amount: ₱{3:N2}",
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

            if (isFromCheckout)
            {
                isFromCheckout = false;
                onCheckoutCompleted?.Invoke();
                onCheckoutCompleted = null;
            }
            else
            {
                billingView.ShowGridView();
            }
        }

        private void PrintInvoice(object sender, EventArgs e)
        {
            try
            {
                int billId = billingView.GetSelectedBillId();
                if (billId == 0)
                {
                    billingView.ShowMessage("Please select a billing record to print.", "No Selection");
                    return;
                }

                var billing = repository.GetById(billId);
                if (billing == null)
                {
                    billingView.ShowMessage("Billing record not found.", "Error");
                    return;
                }

                var customerRepo = new CustomerRepository(DbConfig.GetConnectionString());
                var customer = customerRepo.GetByCustomerName(billing.CustomerName);

                using (var printService = new InvoicePrintServiceWithPDF(billing, customer))
                {
                    printService.ShowWithOptions();
                }
            }
            catch (Exception ex)
            {
                billingView.ShowMessage($"Error printing invoice: {ex.Message}", "Print Error");
            }
        }

        private void EmailInvoice(object sender, EventArgs e)
        {
            try
            {
                int billId = billingView.GetSelectedBillId();
                if (billId == 0)
                {
                    billingView.ShowMessage("Please select a billing record to email.", "No Selection");
                    return;
                }

                var billing = repository.GetById(billId);
                if (billing == null)
                {
                    billingView.ShowMessage("Billing record not found.", "Error");
                    return;
                }

                var customerRepo = new CustomerRepository(DbConfig.GetConnectionString());
                var customer = customerRepo.GetByCustomerName(billing.CustomerName);

                // ✅ IMPROVED EMAIL PRIORITY LOGIC
                // Priority 1: Billing record (preserved from checkout)
                // Priority 2: Customer master record (if exists)
                // Priority 3: Check-in record (for walk-ins)
                string customerEmail = billing.CustomerEmail;
                
                if (string.IsNullOrEmpty(customerEmail) && customer != null)
                {
                    customerEmail = customer.Email;
                }
                
                // ✅ NEW: Fallback to check-in record for walk-in customers
                if (string.IsNullOrEmpty(customerEmail))
                {
                    try
                    {
                        var checkInRepo = new CheckInOutRepository(DbConfig.GetConnectionString());
                        var checkIn = checkInRepo.GetByReservationId(billing.ReservationId);
                        if (checkIn != null && !string.IsNullOrEmpty(checkIn.CustomerEmail))
                        {
                            customerEmail = checkIn.CustomerEmail;
                            System.Diagnostics.Debug.WriteLine($"EmailInvoice: Using email from CheckIns table for walk-in customer: {customerEmail}");
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"EmailInvoice: Error getting email from CheckIns: {ex.Message}");
                    }
                }
                
                string customerFullName = customer != null ? customer.FullName : billing.CustomerName;

                if (string.IsNullOrEmpty(customerEmail))
                {
                    billingView.ShowMessage(
                        $"No email address found for customer '{billing.CustomerName}'.\n\n" +
                        "Please ensure the customer has an email address in:\n" +
                        "- Customer module, or\n" +
                        "- Check-in record", 
                        "No Email");
                    return;
                }

                var confirmResult = MessageBox.Show(
                    string.Format(
                        "Send invoice email to customer?\n\n" +
                        "Customer: {0}\n" +
                        "Email: {1}\n" +
                        "Invoice #: {2}\n" +
                        "Amount: ₱{3:N2}\n\n" +
                        "Do you want to proceed?",
                        customerFullName,
                        customerEmail,
                        billing.BillId,
                        billing.TotalAmount),
                    "Confirm Send Invoice",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                {
                    return;
                }

                string tempPath = System.IO.Path.GetTempPath();
                string fileName = $"Invoice_{billing.BillId}_{DateTime.Now:yyyyMMddHHmmss}.png";
                string filePath = System.IO.Path.Combine(tempPath, fileName);

                // Use InvoicePrintServiceWithPDF (same as print) for consistent formatting and customer info preservation
                using (var printService = new InvoicePrintServiceWithPDF(billing, customer))
                {
                    printService.SaveAsPDF(filePath);
                }

                var emailService = new EmailService();

                if (!emailService.IsConfigured())
                {
                    var form = new EmailConfigForm();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        emailService = new EmailService(
                            form.SmtpServer,
                            form.SmtpPort,
                            form.SenderEmail,
                            form.SenderPassword,
                            form.EnableSsl
                        );
                    }
                    else
                    {
                        if (System.IO.File.Exists(filePath))
                        {
                            try { System.IO.File.Delete(filePath); } catch { }
                        }
                        return;
                    }
                }

                emailService.SendInvoiceEmail(customerEmail, customerFullName, filePath, billing.BillId.ToString());

                billingView.ShowMessage($"Invoice successfully sent to {customerEmail}", "Email Sent");

                if (System.IO.File.Exists(filePath))
                {
                    try { System.IO.File.Delete(filePath); } catch { }
                }
            }
            catch (Exception ex)
            {
                billingView.ShowMessage($"Error emailing invoice: {ex.Message}", "Email Error");
            }
        }

        #endregion

        #region Public Helper Methods
        public void LoadBillingForReservation(int reservationId)
        {
            try
            {
                var billing = repository.GetByReservationId(reservationId);
                if (billing != null)
                {
                    var dto = BillingMapper.ToDto(billing);

                    BillingBindingSource.DataSource = new List<BillingDto> { dto };
                    BillingBindingSource.Position = 0;
                    EditBill(this, EventArgs.Empty);
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

        public void PopulateFromCheckout(CheckInOutModel checkIn, decimal damageFee, Action onCompleted)
        {
            try
            {
                isFromCheckout = true;
                onCheckoutCompleted = onCompleted;

                var checkOutService = new CheckOutService();
                DateTime actualCheckOut = DateTime.Now;
                var billing = checkOutService.PrepareBillingForCheckout(checkIn, actualCheckOut, damageFee);

                billing.BilledBy = UserSession.Username;

                billingView.isEdit = false;
                billingView.BillId = repository.GetNextBillingId().ToString();
                billingView.ReservationId = billing.ReservationId.ToString();
                billingView.CustomerName = billing.CustomerName;
                billingView.RoomType = billing.RoomType;
                billingView.RoomNumber = billing.RoomNumber;

                billingView.CheckInDate = billing.CheckInDate;
                billingView.CheckOutDate = billing.CheckOutDate;
                billingView.ActualCheckOutDate = actualCheckOut;

                billingView.RoomCharge = billing.RoomCharge.ToString("F2");
                billingView.LateCheckoutFee = billing.LateCheckoutFee.ToString("F2");
                billingView.DamageFee = billing.DamageFee.ToString("F2");

                billingView.AmountPaidBefore = billing.AmountPaidBefore.ToString("F2");
                billingView.AmountPaidAtCheckout = "0.00";

                billingView.PaymentStatus = billing.PaymentStatus;
                billingView.PaymentMethod = billing.PaymentMethod ?? string.Empty;
                billingView.PaymentReference = billing.PaymentReference ?? string.Empty;

                billingView.DateBilled = DateTime.Now;
                billingView.BilledBy = UserSession.Username;

                billingView.ShowBillingForm();
            }
            catch (Exception ex)
            {
                isFromCheckout = false;
                onCheckoutCompleted = null;
                billingView.ShowMessage(
                    string.Format("Error populating billing from checkout: {0}", ex.Message),
                    "Error");
            }
        }

        #endregion
    }
}