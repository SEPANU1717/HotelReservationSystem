using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.Data.Repositories.CheckInOutRepository;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Helper;
using HotelReservationSystem.Domain.Interface.CheckInOut;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Domain.Model.CheckInOut;
using HotelReservationSystem.Presenter.Common;
using HotelReservationSystem.Presenter.Mapper;
using HotelReservationSystem.Presenter.Billing;
using static HotelReservationSystem.Domain.Enums.ReservationEnum;

namespace HotelReservationSystem.Presenter
{
    public class UCINOUTPresenter
    {
        private readonly ICheckInOutView checkInView;
        private readonly CheckInOutRepository checkInRepository;
        private readonly RoomRepository roomRepository;
        private readonly ReservationRepository reservationRepository;
        private readonly BindingSource CheckInBindingSource;
        private IEnumerable<CheckInOutModel> checkInList;
        private static UCINOUTPresenter _lastPresenterInstance;
        private readonly CompanionPresenter companionPresenter;
        private decimal _existingAmountPaid = 0m;
        private bool _amountPaidIsDelta = false;

        public UCINOUTPresenter(ICheckInOutView checkInView, CheckInOutRepository repository, string connectionString)
        {
            CheckInBindingSource = new BindingSource();
            this.checkInView = checkInView ?? throw new ArgumentNullException(nameof(checkInView));
            this.checkInRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.roomRepository = new RoomRepository(connectionString);
            this.reservationRepository = new ReservationRepository(connectionString);

            if (_lastPresenterInstance != null)
                _lastPresenterInstance.UnsubscribeFromViewEvents();

            SubscribeToViewEvents();
            _lastPresenterInstance = this;

            this.checkInView.SetReservationListBindingSource(CheckInBindingSource);
            LoadAllCheckInList();
        }

        #region Event Subscription

        private void SubscribeToViewEvents()
        {
            checkInView.SearchEvent += SearchCheckIn;
            checkInView.AddNewEvent += AddNewCheckIn;
            checkInView.EditEvent += EditCheckIn;
            checkInView.DeleteEvent += DeleteCheckIn;
            checkInView.SaveEvent += SaveCheckIn;
            checkInView.CancelEvent += CancelAction;
            checkInView.RoomTypeChangedEvent += OnRoomTypeChanged;
            checkInView.RoomNumberChangedEvent += OnRoomNumberChanged;
            checkInView.PaymentStatusChangedEvent += OnPaymentStatusChanged;
            checkInView.AmountPaidChangedEvent += OnAmountPaidChanged;
            checkInView.CheckoutEvent += OnCheckout;
        }

        private void UnsubscribeFromViewEvents()
        {
            checkInView.SearchEvent -= SearchCheckIn;
            checkInView.AddNewEvent -= AddNewCheckIn;
            checkInView.EditEvent -= EditCheckIn;
            checkInView.DeleteEvent -= DeleteCheckIn;
            checkInView.SaveEvent -= SaveCheckIn;
            checkInView.CancelEvent -= CancelAction;
            checkInView.RoomTypeChangedEvent -= OnRoomTypeChanged;
            checkInView.RoomNumberChangedEvent -= OnRoomNumberChanged;
            checkInView.PaymentStatusChangedEvent -= OnPaymentStatusChanged;
            checkInView.AmountPaidChangedEvent -= OnAmountPaidChanged;
            checkInView.CheckoutEvent -= OnCheckout;
        }

        #endregion

        #region Data Loading

        private void LoadAllCheckInList()
        {
            checkInList = checkInRepository.GetAll() ?? Enumerable.Empty<CheckInOutModel>();
            var dtoList = checkInList.Select(CheckInMapper.ToCheckInDto).ToList();
            CheckInBindingSource.DataSource = dtoList;
            CheckInBindingSource.ResetBindings(false);

            try
            {
                if (dtoList != null && dtoList.Count > 0)
                {
                    CheckInBindingSource.Position = 0;
                }
            }
            catch { }
        }

        private void LoadRoomTypes()
        {
            string[] roomTypes = new string[] { "Standard", "Deluxe", "Suite", "Family", "Single" };
            checkInView.LoadRoomTypes(roomTypes);
        }

        private void LoadRoomInformation(CheckInOutModel model)
        {
            if (string.IsNullOrEmpty(model.RoomType))
                return;

            var availableRooms = roomRepository.GetAvailableRoomsByTypeAndDateRange(
                model.RoomType,
                model.CheckInDate,
                model.CheckOutDate,
                model.ReservationId).ToList();

            if (!string.IsNullOrEmpty(model.RoomNumber))
            {
                var currentRoom = roomRepository.GetByNumber(model.RoomNumber);
                if (currentRoom != null && !availableRooms.Any(r => r.RoomNumber == model.RoomNumber))
                {
                    availableRooms.Add(currentRoom);
                }
            }

            var roomNumbers = availableRooms.Select(r => r.RoomNumber).ToArray();
            checkInView.LoadAvailableRooms(roomNumbers);
        }

        #endregion

        #region CRUD Event Handlers

        private void SearchCheckIn(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(checkInView.SearchValue);
            checkInList = emptyValue
                ? checkInRepository.GetAll()
                : checkInRepository.GetByValue(checkInView.SearchValue);

            var dtoList = (checkInList ?? Enumerable.Empty<CheckInOutModel>())
                .Select(CheckInMapper.ToCheckInDto)
                .ToList();

            CheckInBindingSource.DataSource = dtoList;
            CheckInBindingSource.ResetBindings(false);
        }

        private void AddNewCheckIn(object sender, EventArgs e)
        {
            checkInView.isEdit = false;
            checkInView.ReservationStatus = "CheckedIn";
            LoadRoomTypes();
            checkInView.SetFieldEnabled("ReservationId", true);
            checkInView.SetFieldEnabled("CustomerName", true);
            CleanViewFields();

            int nextReservationId = reservationRepository.GetNextReservationId();
            checkInView.ReservationId = nextReservationId.ToString();

            checkInView.CheckInDate = DateTime.Now;
            checkInView.CheckOutDate = DateTime.Now.AddDays(1);
            checkInView.TimeArrival = DateTime.Now;
            checkInView.CustomerEmail = string.Empty;

            // sensible defaults
            checkInView.TotalPrice = 0m;
            checkInView.DownPayment = 0m;
            checkInView.AmountPaid = 0m;
            checkInView.BalanceDue = 0m;
            checkInView.PaymentStatus = "Pending";
            checkInView.PaymentMethod = "Cash";
        }

        private void EditCheckIn(object sender, EventArgs e)
        {
            var dto = (CheckInDto)CheckInBindingSource.Current;
            if (dto == null)
            {
                checkInView.ShowMessage("Please select a check-in record to edit.", "Warning");
                return;
            }

            var model = checkInList.FirstOrDefault(c => c.ReservationId == dto.ReservationId);
            if (model == null)
                return;

            LoadRoomTypes();

            checkInView.ReservationId = model.ReservationId.ToString();
            checkInView.CustomerName = model.CustomerName;
            checkInView.RoomType = model.RoomType;
            checkInView.CheckInDate = model.CheckInDate;
            checkInView.CheckOutDate = model.CheckOutDate;
            checkInView.TimeArrival = model.TimeArrival;
            checkInView.TotalPrice = model.TotalPrice;
            checkInView.DownPayment = model.DownPayment;
            checkInView.AmountPaid = model.AmountPaid;
            checkInView.BalanceDue = model.BalanceDue;
            checkInView.PaymentMethod = model.PaymentMethod;
            checkInView.PaymentReference = model.PaymentReference;
            checkInView.PaymentStatus = model.PaymentStatus.ToString();
            checkInView.ReservationStatus = model.ReservationStatus;
            checkInView.CustomerEmail = model.CustomerEmail ?? string.Empty;

            LoadRoomInformation(model);

            checkInView.RoomNumber = model.RoomNumber;

            checkInView.SetFieldEnabled("ReservationId", false);
            checkInView.SetFieldEnabled("CustomerName", false);
            checkInView.isEdit = true;

            checkInView.ShowTab(1);
        }

        private void SaveCheckIn(object sender, EventArgs e)
        {
            var model = CheckInMapper.FromCheckInView(checkInView);
            if (model == null)
            {
                checkInView.isSuccessful = false;
                checkInView.Message = "Invalid check-in data.";
                return;
            }

            try
            {
                new ModelDataValidation().Validate(model);


                string oldRoomNumber = null;

                if (checkInView.isEdit)
                {
                    var existingCheckIn = checkInRepository.GetByReservationId(model.ReservationId);
                    if (existingCheckIn != null)
                    {
                        oldRoomNumber = existingCheckIn.RoomNumber;
                    }

                    if (_amountPaidIsDelta)
                    {
                        model.AmountPaid = _existingAmountPaid + checkInView.AmountPaid;
                    }

                    checkInRepository.Edit(model);
                    checkInView.Message = "Check-in updated successfully!";

                    _existingAmountPaid = model.AmountPaid;
                    _amountPaidIsDelta = false;
                }
                else
                {
                    if (checkInRepository.ExistsForReservation(model.ReservationId))
                    {
                        var existingCheckIn = checkInRepository.GetByReservationId(model.ReservationId);
                        checkInView.ShowMessage(
                            $"A check-in already exists for this reservation.\n\n" +
                            $"Customer: {existingCheckIn.CustomerName}\n" +
                            $"Room: {existingCheckIn.RoomNumber}\n" +
                            $"Check-In Date: {existingCheckIn.CheckInDate:MM/dd/yyyy}\n" +
                            $"Status: {existingCheckIn.ReservationStatus}\n\n" +
                            "Please use the Edit function to modify the existing check-in.",
                            "Duplicate Check-In");
                        checkInView.isSuccessful = false;
                        return;
                    }

                    var reservation = reservationRepository.GetById(model.ReservationId);

                    if (reservation == null)
                    {
                        CreateWalkInReservation(model);
                    }
                    else if (!string.IsNullOrEmpty(reservation.RoomNumber))
                    {
                        oldRoomNumber = reservation.RoomNumber;
                    }

                    if (_amountPaidIsDelta)
                    {
                        model.AmountPaid = _existingAmountPaid + checkInView.AmountPaid;
                    }

                    checkInRepository.Add(model);
                    checkInView.Message = "Check-in saved successfully!";

                    _existingAmountPaid = model.AmountPaid;
                    _amountPaidIsDelta = false;
                }

                if (!string.IsNullOrEmpty(oldRoomNumber) && oldRoomNumber != model.RoomNumber)
                {
                    System.Diagnostics.Debug.WriteLine($"SaveCheckIn: Room changed from {oldRoomNumber} to {model.RoomNumber}");
                    var oldRoom = roomRepository.GetByNumber(oldRoomNumber);
                    if (oldRoom != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"SaveCheckIn: Setting old room {oldRoomNumber} status from {oldRoom.RoomStatus} to Available");
                        oldRoom.RoomStatus = "Available";
                        roomRepository.Edit(oldRoom);
                        System.Diagnostics.Debug.WriteLine($"SaveCheckIn: Old room {oldRoomNumber} updated to Available");
                    }
                }

                HandleRoomStatusAfterSave(model);
                SyncReservationWithCheckIn(model);

                checkInView.isSuccessful = true;
                checkInView.isEdit = false;
                LoadAllCheckInList();
                CleanViewFields();

                checkInView.ShowMessage(checkInView.Message, "Success");

                var customerRepo = new CustomerRepository(DbConfig.GetConnectionString());
                var customer = customerRepo.GetByCustomerName(model.CustomerName);

                var confirmResult = MessageBox.Show(
                    "Check-in saved successfully!\n\nWould you like to view the receipt?",
                    "Print Receipt",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        using (var receiptService = new Domain.Services.CheckInReceiptPrintService(model, customer))
                        {
                            receiptService.ShowWithOptions();
                        }
                    }
                    catch (Exception ex)
                    {
                        checkInView.ShowMessage($"Receipt preview error: {ex.Message}", "Error");
                    }
                }

                var emailResult = MessageBox.Show(
                    "Would you like to email this check-in receipt to the customer?",
                    "Email Receipt",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (emailResult == DialogResult.Yes)
                {
                    EmailCheckInReceipt(model);
                }
            }
            catch (Exception ex)
            {
                checkInView.isSuccessful = false;
                checkInView.Message = ex.Message;
            }
        }

        private void CreateWalkInReservation(CheckInOutModel checkInModel)
        {
            try
            {
                var walkInReservation = new ReservationModel
                {
                    ReservationId = checkInModel.ReservationId,
                    CustomerName = checkInModel.CustomerName,
                    RoomType = checkInModel.RoomType,
                    RoomNumber = checkInModel.RoomNumber,
                    CheckInDate = checkInModel.CheckInDate,
                    CheckOutDate = checkInModel.CheckOutDate,
                    TimeArrival = checkInModel.TimeArrival,
                    TotalPrice = checkInModel.TotalPrice,
                    DownPayment = checkInModel.DownPayment,
                    AmountPaid = checkInModel.AmountPaid,
                    IsDownPaymentPaid = checkInModel.AmountPaid >= checkInModel.DownPayment,
                    PaymentStatus = checkInModel.PaymentStatus,
                    PaymentMethod = checkInModel.PaymentMethod ?? "Cash",
                    PaymentReference = checkInModel.PaymentReference,
                    ReservationStatus = "WalkIn",
                    CreatedAt = DateTime.Now
                };

                reservationRepository.Add(walkInReservation);
                System.Diagnostics.Debug.WriteLine($"CreateWalkInReservation: Created walk-in reservation {walkInReservation.ReservationId} for {walkInReservation.CustomerName}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CreateWalkInReservation ERROR: {ex.Message}");
                throw new Exception($"Failed to create walk-in reservation: {ex.Message}");
            }
        }

        private void DeleteCheckIn(object sender, EventArgs e)
        {
            try
            {
                int checkInId = checkInView.GetSelectedReservationId();
                if (checkInId == 0)
                {
                    checkInView.Message = "Please select a check-in to delete.";
                    checkInView.isSuccessful = false;
                    return;
                }

                var checkIn = checkInRepository.GetByReservationId(checkInId);
                string roomNumber = checkIn?.RoomNumber;
                int reservationId = checkIn?.ReservationId ?? 0;

                checkInRepository.Delete(checkInId);

                if (!string.IsNullOrEmpty(roomNumber))
                {
                    roomRepository.SyncRoomStatusesWithReservations(roomNumber, reservationRepository);
                }

                if (reservationId > 0)
                {
                    RevertReservationStatusAfterDelete(reservationId);
                }

                checkInView.isSuccessful = true;
                checkInView.Message = "Check-in deleted successfully.";
                LoadAllCheckInList();
            }
            catch (Exception ex)
            {
                checkInView.isSuccessful = false;
                checkInView.Message = $"Error: Could not delete check-in. {ex.Message}";
            }
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        #endregion

        #region Room Event Handlers

        private void OnRoomTypeChanged(object sender, string roomType)
        {
            if (string.IsNullOrEmpty(roomType))
                return;

            try
            {
                DateTime checkInDate = checkInView.CheckInDate;
                DateTime checkOutDate = checkInView.CheckOutDate;

                int? excludeReservationId = null;
                if (checkInView.isEdit)
                {
                    excludeReservationId = checkInView.GetSelectedReservationId();
                    if (excludeReservationId == 0) excludeReservationId = null;
                }

                var availableRooms = roomRepository.GetAvailableRoomsByTypeAndDateRange(
                    roomType,
                    checkInDate,
                    checkOutDate,
                    excludeReservationId);
                var roomNumbers = availableRooms.Select(r => r.RoomNumber).ToArray();
                checkInView.LoadAvailableRooms(roomNumbers);

                if (Enum.TryParse<RoomType>(roomType, out var roomTypeEnum))
                {
                    decimal roomRate = RoomRateHelper.GetRoomRate(roomTypeEnum);
                    UpdatePriceForRoomType(roomRate);
                }
            }
            catch (Exception ex)
            {
                checkInView.ShowMessage($"Error loading rooms: {ex.Message}", "Error");
            }
        }

        private void OnRoomNumberChanged(object sender, string roomNumber)
        {
            if (string.IsNullOrEmpty(roomNumber))
                return;

            try
            {
                var room = roomRepository.GetByNumber(roomNumber);
                if (room != null)
                {
                    checkInView.RoomGuests = room.RoomGuests;

                    if (decimal.TryParse(room.RoomPrice, out decimal roomPrice))
                    {
                        UpdatePriceForRoomType(roomPrice);
                    }
                }
            }
            catch (Exception ex)
            {
                checkInView.ShowMessage($"Error loading room details: {ex.Message}", "Error");
            }
        }

        private void OnPaymentStatusChanged(object sender, EventArgs e)
        {
            ApplyPaymentStatusAdjustments();
        }

        private void OnAmountPaidChanged(object sender, EventArgs e)
        {
            RecalculateBalance();
        }

        private void OnCheckout(object sender, EventArgs e)
        {
            try
            {
                int reservationId = checkInView.GetSelectedReservationId();
                if (reservationId == 0)
                {
                    checkInView.ShowMessage("Please select a guest to check out.", "Selection Required");
                    return;
                }

                var checkIn = checkInRepository.GetByReservationId(reservationId);
                if (checkIn == null)
                {
                    checkInView.ShowMessage("Check-in record not found.", "Error");
                    return;
                }

                var checkOutService = new HotelReservationSystem.Domain.Services.CheckOutService();
                var validationResult = checkOutService.ValidateCheckout(checkIn);

                decimal damageFee = 0m; 

                bool hasDamagesAlways = checkInView.ShowConfirmation("Are there any damages to report?", "Damage Assessment");
                if (hasDamagesAlways)
                {
                    string damageInputAlways = checkInView.PromptForInput("Damage Fee", "Enter damage fee amount:", "0.00");
                    if (!string.IsNullOrEmpty(damageInputAlways))
                    {
                        decimal.TryParse(damageInputAlways, out damageFee);
                    }
                }

                if (!validationResult.IsValid && validationResult.HasOutstandingBalance)
                {
                    bool proceed = checkInView.ShowConfirmation(
                        string.Format("Guest has an outstanding balance of ₱{0:N2}.\nYou must settle the remaining balance before completing checkout.\n\nOpen billing form now?",
                            validationResult.OutstandingAmount),
                        "Outstanding Balance - Partial Payment");

                    if (!proceed)
                        return;

                    NavigateToBilling(checkIn, damageFee);
                    return;
                }
                else if (!validationResult.IsValid)
                {
                    checkInView.ShowMessage(validationResult.ErrorMessage, "Checkout Validation Failed");
                    return;
                }

                DateTime actualCheckOut = DateTime.Now;
                decimal lateFee = checkOutService.CalculateLateCheckoutFee(checkIn.CheckOutDate, actualCheckOut);

                if (lateFee > 0 || damageFee > 0)
                {
                    string confirmMessage = string.Format(
                        "Additional charges detected:\n\n" +
                        "Customer: {0}\n" +
                        "Room: {1}\n",
                        checkIn.CustomerName,
                        checkIn.RoomNumber);

                    if (lateFee > 0)
                        confirmMessage += string.Format("Late Checkout Fee: ₱{0:N2}\n", lateFee);

                    if (damageFee > 0)
                        confirmMessage += string.Format("Damage Fee: ₱{0:N2}\n", damageFee);

                    confirmMessage += "\nProceed to billing to settle these charges?";

                    bool confirm = checkInView.ShowConfirmation(confirmMessage, "Additional Charges");
                    if (!confirm)
                        return;

                    NavigateToBilling(checkIn, damageFee);
                }
                else
                {
                    if (damageFee > 0)
                    {
                        bool proceedToBillingForDamage = checkInView.ShowConfirmation(
                            string.Format("Damage fee has been reported: ₱{0:N2}.\nProceed to billing to settle this charge?", damageFee),
                            "Damage Fee Detected");

                        if (!proceedToBillingForDamage)
                            return;

                        NavigateToBilling(checkIn, damageFee);
                        return;
                    }

                    bool confirm = checkInView.ShowConfirmation(
                        string.Format("Confirm checkout for:\n\nCustomer: {0}\nRoom: {1}\nTotal Paid: ₱{2:N2}\nBalance: ₱0.00\n\nComplete checkout now?",
                            checkIn.CustomerName,
                            checkIn.RoomNumber,
                            checkIn.AmountPaid),
                        "Complete Checkout - Full Payment");

                    if (!confirm)
                        return;

                    CompleteCheckoutDirectly(checkIn);
                }
            }
            catch (InvalidOperationException ex)
            {
                checkInView.ShowMessage(ex.Message, "Checkout Error");
            }
            catch (Exception ex)
            {
                checkInView.ShowMessage(string.Format("Error during checkout: {0}", ex.Message), "Checkout Error");
            }
        }

        private void CompleteCheckoutDirectly(CheckInOutModel checkIn)
        {
            try
            {
                var checkOutService = new HotelReservationSystem.Domain.Services.CheckOutService();
                DateTime actualCheckOut = DateTime.Now;
                var billing = checkOutService.PrepareBillingForCheckout(checkIn, actualCheckOut, 0m);

                billing.BilledBy = UserSession.Username;
                billing.DateBilled = DateTime.Now;

                var billingRepo = new HotelReservationSystem.Data.Repositories.BillingRepository(DbConfig.GetConnectionString());
                billingRepo.Add(billing);

                System.Diagnostics.Debug.WriteLine($"Auto-created billing record {billing.BillId} for full payment checkout");

                checkInRepository.CheckOut(checkIn.ReservationId, DateTime.Now, UserSession.Username);

                var reservation = reservationRepository.GetById(checkIn.ReservationId);
                if (reservation != null)
                {
                    reservation.ReservationStatus = "CheckedOut";
                    reservationRepository.Edit(reservation);
                }

                var room = roomRepository.GetByNumber(checkIn.RoomNumber);
                if (room != null)
                {
                    room.RoomStatus = "Available";
                    roomRepository.Edit(room);
                }

                LoadAllCheckInList();
                checkInView.ShowTab(0);
                checkInView.ShowMessage("Checkout completed successfully! Billing record created automatically.", "Success");

                PromptCustomerDeletion(checkIn.CustomerName);
            }
            catch (Exception ex)
            {
                checkInView.ShowMessage(string.Format("Error completing checkout: {0}", ex.Message), "Checkout Error");
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

                var futureReservations = reservationRepository.GetAll()
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

                var pastVisits = checkInRepository.GetAll()
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

        private void NavigateToBilling(CheckInOutModel checkIn, decimal damageFee)
        {
            try
            {
                var form = (checkInView as Control)?.FindForm();
                if (form == null)
                {
                    checkInView.ShowMessage("Cannot find parent form.", "Error");
                    return;
                }

                var billingControl = UserControls.UCBilling.GetInstance(form);
                var billingRepo = new HotelReservationSystem.Data.Repositories.BillingRepository(
                    DbConfig.GetConnectionString());
                var billingPresenter = new HotelReservationSystem.Presenter.Billing.BillingPresenter(
                    billingControl,
                    billingRepo);

                billingPresenter.PopulateFromCheckout(checkIn, damageFee, () =>
                {
                    LoadAllCheckInList();
                    checkInView.ShowTab(0);
                    checkInView.ShowMessage("Checkout completed successfully! Status updated to CheckedOut.", "Success");
                });

                var mainView = form as HotelReservationSystem.Domain.Interface.IMainView;
                if (mainView != null)
                {
                    mainView.LoadUserControl(billingControl);
                }
                else
                {
                    checkInView.ShowMessage("Cannot navigate to billing form.", "Error");
                }
            }
            catch (Exception ex)
            {
                checkInView.ShowMessage(string.Format("Error navigating to billing: {0}", ex.Message), "Navigation Error");
            }
        }

        #endregion

        #region Synchronization Methods

        private void SyncReservationWithCheckIn(CheckInOutModel checkInModel)
        {
            try
            {
                var reservation = reservationRepository.GetById(checkInModel.ReservationId);
                if (reservation == null)
                    return;

                bool needsUpdate = false;

                if (reservation.RoomNumber != checkInModel.RoomNumber)
                {
                    reservation.RoomNumber = checkInModel.RoomNumber;
                    needsUpdate = true;
                }

                if (reservation.RoomType != checkInModel.RoomType)
                {
                    reservation.RoomType = checkInModel.RoomType;
                    needsUpdate = true;
                }

                string newReservationStatus = MapCheckInStatusToReservationStatus(checkInModel.ReservationStatus);
                if (reservation.ReservationStatus != "WalkIn" && reservation.ReservationStatus != newReservationStatus)
                {
                    reservation.ReservationStatus = newReservationStatus;
                    needsUpdate = true;
                }

                if (reservation.TotalPrice != checkInModel.TotalPrice)
                {
                    reservation.TotalPrice = checkInModel.TotalPrice;
                    needsUpdate = true;
                }

                if (reservation.AmountPaid != checkInModel.AmountPaid)
                {
                    reservation.AmountPaid = checkInModel.AmountPaid;
                    needsUpdate = true;
                }

                if (reservation.PaymentStatus != checkInModel.PaymentStatus)
                {
                    reservation.PaymentStatus = checkInModel.PaymentStatus;
                    needsUpdate = true;
                }

                if ((reservation.PaymentReference ?? string.Empty) != (checkInModel.PaymentReference ?? string.Empty))
                {
                    reservation.PaymentReference = checkInModel.PaymentReference;
                    needsUpdate = true;
                }

                if (needsUpdate)
                {
                    reservationRepository.Edit(reservation);
                }
            }
            catch (Exception ex)
            {
                checkInView.ShowMessage($"Warning: Could not sync reservation: {ex.Message}", "Warning");
            }
        }

        private void RevertReservationStatusAfterDelete(int reservationId)
        {
            try
            {
                var reservation = reservationRepository.GetById(reservationId);
                if (reservation == null)
                    return;

                if (reservation.ReservationStatus == "CheckedIn" || reservation.ReservationStatus == "CheckedOut")
                {
                    reservation.ReservationStatus = "Confirmed";
                    reservationRepository.Edit(reservation);
                }
            }
            catch (Exception ex)
            {
                checkInView.ShowMessage($"Warning: Could not revert reservation status: {ex.Message}", "Warning");
            }
        }

        private void HandleRoomStatusAfterSave(CheckInOutModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.RoomNumber))
                {
                    System.Diagnostics.Debug.WriteLine("HandleRoomStatusAfterSave: No room number provided");
                    return;
                }

                var room = roomRepository.GetByNumber(model.RoomNumber);
                if (room == null)
                {
                    System.Diagnostics.Debug.WriteLine($"HandleRoomStatusAfterSave: Room {model.RoomNumber} not found");
                    checkInView.ShowMessage($"Warning: Room {model.RoomNumber} not found in database.", "Warning");
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"HandleRoomStatusAfterSave: Room {model.RoomNumber} current status: {room.RoomStatus}, Check-in status: {model.ReservationStatus}");

                string targetStatus = null;
                switch (model.ReservationStatus)
                {
                    case "CheckedIn":
                        targetStatus = "Occupied";
                        break;

                    case "CheckedOut":
                        targetStatus = "Available";
                        break;

                    case "Confirmed":
                    case "Reserved":
                        targetStatus = "Reserved";
                        break;
                }

                if (targetStatus != null && room.RoomStatus != targetStatus)
                {
                    System.Diagnostics.Debug.WriteLine($"HandleRoomStatusAfterSave: Updating room {model.RoomNumber} from {room.RoomStatus} to {targetStatus}");
                    room.RoomStatus = targetStatus;
                    roomRepository.Edit(room);
                    System.Diagnostics.Debug.WriteLine($"HandleRoomStatusAfterSave: Room {model.RoomNumber} updated successfully");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"HandleRoomStatusAfterSave: Room {model.RoomNumber} already has correct status: {room.RoomStatus}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"HandleRoomStatusAfterSave ERROR: {ex.Message}");
                checkInView.ShowMessage($"Warning: Could not update room status: {ex.Message}", "Warning");
            }
        }

        #endregion

        #region Helper Methods

        public void PopulateFromReservation(ReservationModel reservation)
        {
            if (reservation == null)
                return;

            try
            {
                LoadRoomTypes();
                CheckInMapper.ToCheckInView(reservation, checkInView);
                _existingAmountPaid = reservation.AmountPaid;
                _amountPaidIsDelta = false;

                var customerRepo = new CustomerRepository(DbConfig.GetConnectionString());
                var customer = customerRepo.GetByCustomerName(reservation.CustomerName);
                if (customer != null)
                {
                    checkInView.CustomerEmail = customer.Email ?? string.Empty;
                }

                if (!string.IsNullOrEmpty(reservation.RoomType))
                {
                    var availableRooms = roomRepository.GetAvailableRoomsByTypeAndDateRange(
                        reservation.RoomType,
                        reservation.CheckInDate,
                        reservation.CheckOutDate,
                        reservation.ReservationId).ToList();

                    if (!string.IsNullOrEmpty(reservation.RoomNumber))
                    {
                        var currentRoom = roomRepository.GetByNumber(reservation.RoomNumber);
                        if (currentRoom != null && !availableRooms.Any(r => r.RoomNumber == reservation.RoomNumber))
                        {
                            availableRooms.Add(currentRoom);
                        }
                    }

                    var roomNumbers = availableRooms.Select(r => r.RoomNumber).ToArray();
                    checkInView.LoadAvailableRooms(roomNumbers);
                    checkInView.RoomNumber = reservation.RoomNumber;
                }

                checkInView.ReservationStatus = "CheckedIn";
                checkInView.isEdit = false;
                checkInView.SetFieldEnabled("ReservationId", false);
                checkInView.SetFieldEnabled("CustomerName", false);
            }
            catch (Exception ex)
            {
                checkInView.ShowMessage($"Error populating reservation data: {ex.Message}", "Error");
            }
        }

        private void UpdatePriceForRoomType(decimal roomPricePerNight)
        {
            int nights = (checkInView.CheckOutDate - checkInView.CheckInDate).Days;
            if (nights <= 0)
                nights = 1;

            decimal newTotalPrice = roomPricePerNight * nights;
            CheckInMapper.UpdateFinancialFields(checkInView, newTotalPrice);

            ApplyPaymentStatusAdjustments();
        }

        private void RecalculateBalance()
        {
            decimal totalPrice = checkInView.TotalPrice;
            decimal amountPaid = checkInView.AmountPaid;
            decimal effectivePaid = _amountPaidIsDelta ? (_existingAmountPaid + amountPaid) : amountPaid;
            decimal balance = totalPrice - effectivePaid;
            if (balance < 0)
            {
                checkInView.BalanceDue = 0m;
                decimal change = Math.Abs(balance);
                checkInView.ShowMessage($"Change to return: ?{change:N2}", "Overpayment");
            }
            else
            {
                checkInView.BalanceDue = balance;
                if (balance == 0m && effectivePaid > 0m)
                {
                    checkInView.PaymentStatus = "FullPayment";
                }
                else if (effectivePaid > 0m)
                {
                    checkInView.PaymentStatus = "Partial";
                }
                else
                {
                    checkInView.PaymentStatus = "Pending";
                }
            }
        }

        private void ApplyPaymentStatusAdjustments()
        {
            try
            {
                string status = checkInView.PaymentStatus ?? string.Empty;
                decimal total = checkInView.TotalPrice;
                decimal amountPaid = checkInView.AmountPaid;
                if (status.Equals("FullPayment", StringComparison.OrdinalIgnoreCase))
                {
                    checkInView.DownPayment = 0m;
                    decimal remaining = total - _existingAmountPaid;
                    if (remaining < 0) remaining = 0m;
                    checkInView.AmountPaid = remaining; 
                    _amountPaidIsDelta = true;
                    checkInView.BalanceDue = 0m;
                }
                else if (status.Equals("Partial", StringComparison.OrdinalIgnoreCase))
                {
                    decimal down = Math.Round(total * 0.5m, 2);
                    checkInView.DownPayment = down;
                    if (!_amountPaidIsDelta && amountPaid < down)
                        checkInView.AmountPaid = down;
                    _amountPaidIsDelta = false;
                    decimal balance = total - checkInView.AmountPaid;
                    if (balance < 0)
                    {
                        checkInView.BalanceDue = 0m;
                        decimal change = Math.Abs(balance);
                        checkInView.ShowMessage($"Change to return: ₱{change:N2}", "Overpayment");
                    }
                    else
                    {
                        checkInView.BalanceDue = balance;
                    }
                }
                else
                {
                    RecalculateBalance();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ApplyPaymentStatusAdjustments ERROR: {ex.Message}");
            }
        }

        private string MapCheckInStatusToReservationStatus(string checkInStatus)
        {
            if (Enum.TryParse(checkInStatus, true, out RoomStatus parsed))
                return parsed.ToString();

            switch (checkInStatus)
            {
                case "Pending":
                    return RoomStatus.Pending.ToString();
                default:
                    return RoomStatus.CheckedIn.ToString();
            }
        }

        private void CleanViewFields()
        {
            FieldsCleaner.ClearInputs(checkInView as Control);
        }

        #endregion

        #region Static Methods

        public static IEnumerable<CheckInDto> GetCheckInDtoList()
        {
            var repository = new CheckInOutRepository(DbConfig.GetConnectionString());
            var checkInList = repository.GetAll() ?? Enumerable.Empty<CheckInOutModel>();
            return checkInList.Select(CheckInMapper.ToCheckInDto).ToList();
        }

        public void RefreshCheckInList()
        {
            LoadAllCheckInList();
        }

        #endregion

        #region Email Helpers

        private void EmailCheckInReceipt(CheckInOutModel model)
        {
            try
            {
                var customerRepo = new CustomerRepository(DbConfig.GetConnectionString());
                var customer = customerRepo.GetByCustomerName(model.CustomerName);
                string customerEmail = customer?.Email ?? checkInView.CustomerEmail;
                string customerName = customer?.FullName ?? model.CustomerName;

                if (string.IsNullOrEmpty(customerEmail))
                {
                    checkInView.ShowMessage($"No email address found for customer '{model.CustomerName}'.\n\nPlease ensure the customer has an email address in the Customer module.", "No Email");
                    return;
                }

                var confirm = MessageBox.Show(
                    $"Send check-in receipt to:\n\nCustomer: {customerName}\nEmail: {customerEmail}\nReservation #: {model.ReservationId}\n\nProceed?",
                    "Confirm Email Send",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                string tempPath = System.IO.Path.GetTempPath();
                string fileName = $"CheckInReceipt_{model.ReservationId}_{DateTime.Now:yyyyMMddHHmmss}.png";
                string filePath = System.IO.Path.Combine(tempPath, fileName);

                using (var receiptService = new Domain.Services.CheckInReceiptPrintService(model, customer))
                {
                    receiptService.SaveAsImage(filePath);
                }

                var saveCopy = MessageBox.Show("Do you want to save a local copy of the receipt before emailing?", "Save Copy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveCopy == DialogResult.Yes)
                {
                    using (SaveFileDialog saveDialog = new SaveFileDialog())
                    {
                        saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
                        saveDialog.FileName = fileName;
                        saveDialog.DefaultExt = "png";
                        if (saveDialog.ShowDialog() == DialogResult.OK)
                        {
                            File.Copy(filePath, saveDialog.FileName, true);
                        }
                    }
                }

                var emailService = new Domain.Services.EmailService();
                if (!emailService.IsConfigured())
                {
                    var form = new Presenter.Billing.EmailConfigForm();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        emailService = new Domain.Services.EmailService(form.SmtpServer, form.SmtpPort, form.SenderEmail, form.SenderPassword, form.EnableSsl);
                    }
                    else
                    {
                        if (File.Exists(filePath)) try { File.Delete(filePath); } catch { }
                        return;
                    }
                }

                emailService.SendCheckInReceiptEmail(customerEmail, customerName, filePath, model.ReservationId.ToString());

                MessageBox.Show($"Check-in receipt successfully sent to {customerEmail}", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (File.Exists(filePath)) try { File.Delete(filePath); } catch { }
            }
            catch (Exception ex)
            {
                checkInView.ShowMessage($"Error emailing check-in receipt: {ex.Message}", "Email Error");
            }
        }

        #endregion
    }
}