using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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
            checkInView.CompanionCount = model.CompanionCount;

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

                    checkInRepository.Edit(model);
                    checkInView.Message = "Check-in updated successfully!";
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
                    if (reservation != null && !string.IsNullOrEmpty(reservation.RoomNumber))
                    {
                        oldRoomNumber = reservation.RoomNumber;
                    }

                    checkInRepository.Add(model);
                    checkInView.Message = "Check-in saved successfully!";
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
            }
            catch (Exception ex)
            {
                checkInView.isSuccessful = false;
                checkInView.Message = ex.Message;
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
            string status = checkInView.PaymentStatus;
            if (status == "FullPayment")
            {
                decimal totalPrice = checkInView.TotalPrice;
                checkInView.AmountPaid = totalPrice;
                checkInView.BalanceDue = 0;
            }
            else
            {
                RecalculateBalance();
            }
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

                if (!validationResult.IsValid && validationResult.HasOutstandingBalance)
                {
                    bool proceed = checkInView.ShowConfirmation(
                        string.Format("Guest has an outstanding balance of ${0:N2}.\nYou must settle the remaining balance before completing checkout.\n\nOpen billing form now?",
                            validationResult.OutstandingAmount),
                        "Outstanding Balance - Partial Payment");

                    if (!proceed)
                        return;

                    NavigateToBilling(checkIn, 0m);
                    return;
                }
                else if (!validationResult.IsValid)
                {
                    checkInView.ShowMessage(validationResult.ErrorMessage, "Checkout Validation Failed");
                    return;
                }

                DateTime actualCheckOut = DateTime.Now;
                decimal lateFee = checkOutService.CalculateLateCheckoutFee(checkIn.CheckOutDate, actualCheckOut);

                decimal damageFee = 0m;
                bool hasDamages = checkInView.ShowConfirmation("Are there any damages to report?", "Damage Assessment");
                
                if (hasDamages)
                {
                    string damageInput = checkInView.PromptForInput("Damage Fee", "Enter damage fee amount:", "0.00");
                    if (!string.IsNullOrEmpty(damageInput))
                    {
                        decimal.TryParse(damageInput, out damageFee);
                    }
                }

                if (lateFee > 0 || damageFee > 0)
                {
                    string confirmMessage = string.Format(
                        "Additional charges detected:\n\n" +
                        "Customer: {0}\n" +
                        "Room: {1}\n",
                        checkIn.CustomerName,
                        checkIn.RoomNumber);

                    if (lateFee > 0)
                        confirmMessage += string.Format("Late Checkout Fee: ${0:N2}\n", lateFee);

                    if (damageFee > 0)
                        confirmMessage += string.Format("Damage Fee: ${0:N2}\n", damageFee);

                    confirmMessage += "\nProceed to billing to settle these charges?";

                    bool confirm = checkInView.ShowConfirmation(confirmMessage, "Additional Charges");
                    if (!confirm)
                        return;

                    NavigateToBilling(checkIn, damageFee);
                }
                else
                {
                    bool confirm = checkInView.ShowConfirmation(
                        string.Format("Confirm checkout for:\n\nCustomer: {0}\nRoom: {1}\nTotal Paid: ${2:N2}\nBalance: $0.00\n\nComplete checkout now?",
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
                checkInView.ShowMessage("Checkout completed successfully!", "Success");
            }
            catch (Exception ex)
            {
                checkInView.ShowMessage(string.Format("Error completing checkout: {0}", ex.Message), "Checkout Error");
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
                if (reservation.ReservationStatus != newReservationStatus)
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

                if (!string.IsNullOrEmpty(checkInModel.PaymentMethod) && reservation.PaymentMethod != checkInModel.PaymentMethod)
                {
                    reservation.PaymentMethod = checkInModel.PaymentMethod;
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
        }

        private void RecalculateBalance()
        {
            decimal totalPrice = checkInView.TotalPrice;
            decimal amountPaid = checkInView.AmountPaid;
            decimal balance = totalPrice - amountPaid;
            checkInView.BalanceDue = balance < 0 ? 0 : balance;
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
    }
}