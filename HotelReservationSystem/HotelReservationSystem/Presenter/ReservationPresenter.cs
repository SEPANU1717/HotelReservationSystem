using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Interface.Reservation;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Presenter.Common;
using HotelReservationSystem.Presenter.Mapper;

namespace HotelReservationSystem.Presenter
{
    public class ReservationPresenter
    {
        private readonly IReservationView reservationView;
        private readonly IReservationRepository reservationRepository;
        private readonly RoomRepository roomRepository;
        private readonly CustomerRepository customerRepository;
        private readonly BindingSource ReservationBindingSource;
        private IEnumerable<ReservationModel> reservationList;
        private static ReservationPresenter _lastPresenterInstance;

        public ReservationPresenter(IReservationView reservationView, IReservationRepository reservationRepository)
        {
            ReservationBindingSource = new BindingSource();
            this.reservationView = reservationView ?? throw new ArgumentNullException(nameof(reservationView));
            this.reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            string connectionString = DbConfig.GetConnectionString();
            this.roomRepository = new RoomRepository(connectionString);
            this.customerRepository = new CustomerRepository(connectionString);

            if (_lastPresenterInstance != null)
                _lastPresenterInstance.UnsubscribeFromViewEvents();

            SubscribeToViewEvents();
            _lastPresenterInstance = this;

            this.reservationView.SetReservationListBindingSource(ReservationBindingSource);
            LoadAllReservationList();
            this.reservationView.Show();
        }

        #region Event Subscription

        private void SubscribeToViewEvents()
        {
            reservationView.SearchEvent += SearchReservation;
            reservationView.AddNewEvent += AddNewReservation;
            reservationView.EditEvent += EditReservation;
            reservationView.DeleteEvent += DeleteReservation;
            reservationView.SaveEvent += SaveReservation;
            reservationView.CancelEvent += CancelAction;
            reservationView.LoadReservationForEditEvent += OnLoadReservationForEdit;
            reservationView.SetCustomerForReservationEvent += OnSetCustomerForReservation;
            reservationView.RoomTypeChangedEvent += OnRoomTypeChanged;
            reservationView.ShowCheckInOutView += OnShowCheckInOutView;
            reservationView.PaymentTypeChangedEvent += OnPaymentTypeChanged; 

        }

        private void UnsubscribeFromViewEvents()
        {
            reservationView.SearchEvent -= SearchReservation;
            reservationView.AddNewEvent -= AddNewReservation;
            reservationView.EditEvent -= EditReservation;
            reservationView.DeleteEvent -= DeleteReservation;
            reservationView.SaveEvent -= SaveReservation;
            reservationView.CancelEvent -= CancelAction;
            reservationView.LoadReservationForEditEvent -= OnLoadReservationForEdit;
            reservationView.SetCustomerForReservationEvent -= OnSetCustomerForReservation;
            reservationView.RoomTypeChangedEvent -= OnRoomTypeChanged;
            reservationView.ShowCheckInOutView -= OnShowCheckInOutView;
            reservationView.PaymentTypeChangedEvent -= OnPaymentTypeChanged;

        }

        #endregion

        #region Event Handlers

        private void OnPaymentTypeChanged(object sender, string paymentType)
        {
            if (string.IsNullOrEmpty(paymentType)) return;

            try
            {
                decimal totalPrice = 0m;
                decimal.TryParse(reservationView.TotalPrice, out totalPrice);

                decimal currentPaid = 0m;
                decimal.TryParse(reservationView.AmountPaid, out currentPaid);

                if (totalPrice <= 0)
                {
                    reservationView.ShowErrorMessage("Please calculate the total price first.");
                    return;
                }

                if (paymentType.Equals("FullPayment", StringComparison.OrdinalIgnoreCase))
                {
                    decimal remaining = totalPrice - currentPaid;
                    if (remaining < 0) remaining = 0m;
                    reservationView.DownPayment = "0";
                    reservationView.AmountPaid = (currentPaid + remaining).ToString(CultureInfo.InvariantCulture);
                    reservationView.BalanceDue = "0";
                }
                else if (paymentType.Equals("Partial", StringComparison.OrdinalIgnoreCase))
                {
                    decimal downPayment = Math.Round(totalPrice * 0.5m, 2);
                    reservationView.DownPayment = downPayment.ToString(CultureInfo.InvariantCulture);

                    if (currentPaid < downPayment)
                        reservationView.AmountPaid = downPayment.ToString(CultureInfo.InvariantCulture);

                    decimal newAmountPaid = 0m;
                    decimal.TryParse(reservationView.AmountPaid, out newAmountPaid);
                    decimal balance = totalPrice - newAmountPaid;
                    reservationView.BalanceDue = balance.ToString(CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                reservationView.ShowErrorMessage($"Error updating payment: {ex.Message}");
            }
        }
        private void LoadAllReservationList()
        {
            reservationList = reservationRepository.GetAll() ?? Enumerable.Empty<ReservationModel>();
            var dtoList = reservationList.Select(ReservationMapper.FromReservationModel).ToList();

            ReservationBindingSource.DataSource = dtoList;
            ReservationBindingSource.ResetBindings(false);
        }

        private void SearchReservation(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(reservationView.SearchValue);
            reservationList = emptyValue
                ? reservationRepository.GetAll()
                : reservationRepository.GetByValue(reservationView.SearchValue);

            var dtoList = (reservationList ?? Enumerable.Empty<ReservationModel>())
                .Select(ReservationMapper.FromReservationModel)
                .ToList();

            ReservationBindingSource.DataSource = dtoList;
            ReservationBindingSource.ResetBindings(false);
        }

        private void AddNewReservation(object sender, EventArgs e)
        {
            reservationView.isEdit = false;
            LoadAllReservationList();
        }

        private void EditReservation(object sender, EventArgs e)
        {
            var dto = (ReservationDto)ReservationBindingSource.Current;
            var model = reservationList.FirstOrDefault(r => r.ReservationId == dto.ReservationId);
            if (model == null) return;

            if (model.ReservationStatus == "CheckedIn")
            {
                reservationView.ShowErrorMessage(
                    "This reservation has already been checked in.\n\n" +
                    "Checked-in reservations cannot be modified from the Reservation module.\n" +
                    "Please use the Check-In/Out module to make changes.");
                return;
            }

            reservationView.ReservationId = model.ReservationId.ToString();
            reservationView.CustomerName = model.CustomerName;
            reservationView.RoomNumber = model.RoomNumber;
            reservationView.RoomType = model.RoomType;
            reservationView.CheckInDate = model.CheckInDate;
            reservationView.CheckOutDate = model.CheckOutDate;
            reservationView.TotalPrice = model.TotalPrice.ToString(CultureInfo.InvariantCulture);
            reservationView.AmountPaid = model.AmountPaid.ToString(CultureInfo.InvariantCulture);
            reservationView.DownPayment = model.DownPayment.ToString(CultureInfo.InvariantCulture);
            reservationView.BalanceDue = model.BalanceDue.ToString(CultureInfo.InvariantCulture);
            reservationView.ReservationStatus = model.ReservationStatus;
            reservationView.PaymentStatus = model.PaymentStatus;
            reservationView.PaymentMethod = model.PaymentMethod;
            reservationView.PaymentReference = model.PaymentReference;
            reservationView.isEdit = true;


        }

        private void DeleteReservation(object sender, EventArgs e)
        {
            try
            {
                int reservationId = reservationView.GetSelectedReservationId();
                if (reservationId == 0)
                {
                    reservationView.ShowErrorMessage("Please select a reservation to delete.");
                    return;
                }

                var reservation = reservationRepository.GetById(reservationId);

                if (reservation != null && reservation.ReservationStatus == "CheckedIn")
                {
                    reservationView.ShowErrorMessage(
                        "This reservation has already been checked in.\n\n" +
                        "Checked-in reservations cannot be deleted from the Reservation module.\n" +
                        "Please use the Check-In/Out module to manage this record.");
                    return;
                }

                string roomNumber = reservation?.RoomNumber;

                reservationRepository.Delete(reservationId);

                if (!string.IsNullOrEmpty(roomNumber))
                {
                    roomRepository.SyncRoomStatusesWithReservations(roomNumber,
                        reservationRepository as ReservationRepository);
                }

                reservationView.isSuccessful = true;
                reservationView.Message = "Reservation deleted successfully.";
                LoadAllReservationList();
                reservationView.ShowSuccessMessage(reservationView.Message);
            }
            catch (Exception ex)
            {
                reservationView.isSuccessful = false;
                reservationView.Message = $"Error: Could not delete reservation. {ex.Message}";
                reservationView.ShowErrorMessage(reservationView.Message);
            }
        }

        private void SaveReservation(object sender, EventArgs e)
        {
            var model = ReservationMapper.FromReservationView(reservationView);
            if (model == null) return; 

            try
            {
                new ModelDataValidation().Validate(model);

                if (model.CheckInDate.Date < DateTime.Today)
                {
                    reservationView.ShowErrorMessage("Check-in date cannot be in the past.");
                    return;
                }

                if (model.CheckOutDate.Date <= model.CheckInDate.Date)
                {
                    reservationView.ShowErrorMessage("Check-out date must be after check-in date.");
                    return;
                }

                var nights = (model.CheckOutDate.Date - model.CheckInDate.Date).Days;
                if (nights < 1)
                {
                    reservationView.ShowErrorMessage("Reservation must be for at least 1 night.");
                    return;
                }

                if (nights > 365)
                {
                    reservationView.ShowErrorMessage("Reservation cannot exceed 365 nights.");
                    return;
                }

                if (string.IsNullOrEmpty(model.RoomNumber))
                {
                    reservationView.ShowErrorMessage("Please select a room.");
                    return;
                }

                if (model.TotalPrice <= 0)
                {
                    reservationView.ShowErrorMessage("Total price must be greater than zero.");
                    return;
                }

                if (model.AmountPaid < 0)
                {
                    reservationView.ShowErrorMessage("Amount paid cannot be negative.");
                    return;
                }

                if (model.AmountPaid > model.TotalPrice)
                {
                    reservationView.ShowErrorMessage(
                        $"Amount paid (${model.AmountPaid:N2}) cannot exceed total price (${model.TotalPrice:N2}).");
                    return;
                }

                if (model.DownPayment < 0)
                {
                    reservationView.ShowErrorMessage("Down payment cannot be negative.");
                    return;
                }

                if (model.DownPayment > model.TotalPrice)
                {
                    reservationView.ShowErrorMessage(
                        $"Down payment (${model.DownPayment:N2}) cannot exceed total price (${model.TotalPrice:N2}).");
                    return;
                }

                if (!string.IsNullOrEmpty(model.RoomNumber))
                {
                    int? excludeReservationId = reservationView.isEdit ? (int?)model.ReservationId : null;
                    
                    if (reservationRepository.HasOverlappingReservation(
                        model.RoomNumber, 
                        model.CheckInDate, 
                        model.CheckOutDate, 
                        excludeReservationId))
                    {
                        reservationView.ShowErrorMessage(
                            $"Room {model.RoomNumber} is already reserved for the selected dates.\n\n" +
                            $"Check-in: {model.CheckInDate:MM/dd/yyyy}\n" +
                            $"Check-out: {model.CheckOutDate:MM/dd/yyyy}\n\n" +
                            "Please select a different room or change the dates.");
                        return;
                    }
                }

                if (string.IsNullOrWhiteSpace(model.CustomerName))
                {
                    reservationView.ShowErrorMessage("Customer name is required.");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(model.PaymentMethod) &&
                    !model.PaymentMethod.Equals("Cash", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(model.PaymentReference))
                    {
                        reservationView.ShowErrorMessage("Payment reference is required for electronic payments.");
                        return;
                    }
                }

                if (reservationView.isEdit)
                {
                    reservationRepository.Edit(model);
                    reservationView.Message = "Reservation updated successfully!";
                }
                else
                {
                    reservationRepository.Add(model);
                    reservationView.Message = "Reservation added successfully!";
                }

                HandleRoomStatusAfterSave(model);

                reservationView.isSuccessful = true;
                reservationView.isEdit = false;
                LoadAllReservationList();
                reservationView.ClearForm();
                reservationView.ShowTab(0); 
                reservationView.ShowSuccessMessage(reservationView.Message);

                var customer = customerRepository.GetByCustomerName(model.CustomerName);

                var confirmResult = MessageBox.Show(
                    "Reservation saved successfully!\n\nWould you like to view the receipt?",
                    "Print Receipt",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        using (var receiptService = new Domain.Services.ReservationReceiptPrintService(model, customer, UserSession.Username))
                        {
                            receiptService.ShowWithOptions();
                        }
                    }
                    catch (Exception ex)
                    {
                        reservationView.ShowErrorMessage($"Receipt preview error: {ex.Message}");
                    }
                }

                var emailResult = MessageBox.Show(
                    "Would you like to email this reservation receipt to the customer?",
                    "Email Receipt",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (emailResult == DialogResult.Yes)
                {
                    EmailReservationReceipt(model, customer);
                }
            }
            catch (Exception ex)
            {
                reservationView.isSuccessful = false;
                reservationView.Message = ex.Message;
                reservationView.ShowErrorMessage(reservationView.Message);
            }
        }

        private void EmailReservationReceipt(ReservationModel model, CustomerModel customer)
        {
            try
            {
                string customerEmail = customer != null ? customer.Email : null;
                string customerFullName = customer != null ? customer.FullName : model.CustomerName;

                if (string.IsNullOrEmpty(customerEmail))
                {
                    reservationView.ShowErrorMessage(
                        $"No email address found for customer '{model.CustomerName}'.\n\n" +
                        "Please ensure the customer has an email address in the Customer module.");
                    return;
                }

                var confirmResult = MessageBox.Show(
                    $"Send reservation receipt to:\n\n" +
                    $"Customer: {customerFullName}\n" +
                    $"Email: {customerEmail}\n" +
                    $"Reservation #: {model.ReservationId}\n\n" +
                    "Proceed?",
                    "Confirm Email Send",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                    return;

                string tempPath = System.IO.Path.GetTempPath();
                string fileName = $"ReservationReceipt_{model.ReservationId}_{DateTime.Now:yyyyMMddHHmmss}.png";
                string filePath = System.IO.Path.Combine(tempPath, fileName);

                using (var receiptService = new Domain.Services.ReservationReceiptPrintService(model, customer, UserSession.Username))
                {
                    receiptService.SaveAsImage(filePath);
                }

                var emailService = new Domain.Services.EmailService();

                if (!emailService.IsConfigured())
                {
                    var configForm = new Presenter.Billing.EmailConfigForm();
                    if (configForm.ShowDialog() == DialogResult.OK)
                    {
                        emailService = new Domain.Services.EmailService(
                            configForm.SmtpServer,
                            configForm.SmtpPort,
                            configForm.SenderEmail,
                            configForm.SenderPassword,
                            configForm.EnableSsl
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

                emailService.SendReservationReceiptEmail(customerEmail, customerFullName, filePath, model.ReservationId.ToString());

                MessageBox.Show($"Reservation receipt successfully sent to {customerEmail}", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (System.IO.File.Exists(filePath))
                {
                    try { System.IO.File.Delete(filePath); } catch { }
                }
            }
            catch (Exception ex)
            {
                reservationView.ShowErrorMessage($"Error emailing receipt: {ex.Message}");
            }
        }

        private void CancelAction(object sender, EventArgs e)
        {
            reservationView.ClearForm();
            reservationView.ShowTab(0);
        }

        private void OnLoadReservationForEdit(object sender, int reservationId)
        {
            var reservation = reservationRepository.GetById(reservationId);
            if (reservation == null)
            {
                reservationView.ShowErrorMessage("Reservation not found.");
                return;
            }

            RoomModel room = null;
            if (!string.IsNullOrEmpty(reservation.RoomNumber))
            {
                room = roomRepository.GetByNumber(reservation.RoomNumber);

                if (room != null)
                {
                    var availableRooms = roomRepository.GetAvailableRoomsByTypeAndDateRange(
                        room.RoomType, 
                        reservation.CheckInDate, 
                        reservation.CheckOutDate, 
                        reservationId);
                    reservationView.LoadAvailableRooms(availableRooms);
                }
            }

            reservationView.PopulateEditForm(reservation, room);
            reservationView.SetOriginalDates(reservation.CheckInDate, reservation.CheckOutDate);
            reservationView.SetOriginalRoomNumber(reservation.RoomNumber);
            reservationView.ShowTab(1); 
            reservationView.EnableField("Status", true);
            reservationView.EnableField("CustomerName", false);
        }

        private void OnSetCustomerForReservation(object sender, string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                reservationView.ShowErrorMessage("Customer name is required.");
                return;
            }

            reservationView.CustomerName = customerName;

            // Find the customer's most recent reservation if any
            var lastReservation = reservationRepository.GetAll()
                .Where(r => r.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(r => r.CreatedAt)
                .FirstOrDefault();

            if (lastReservation != null)
            {
                // If the last reservation is already checked out, ask user whether to create a new one
                if (string.Equals(lastReservation.ReservationStatus, "CheckedOut", StringComparison.OrdinalIgnoreCase))
                {
                    var result = MessageBox.Show(
                        string.Format("Customer '{0}' has a previous reservation (ID: {1}) that is already checked out.\nDo you want to create a new reservation for this customer?",
                            customerName, lastReservation.ReservationId),
                        "Create New Reservation?",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        reservationView.ReservationId = reservationRepository.GetNextReservationId().ToString();
                        reservationView.isEdit = false;
                        reservationView.ReservationStatus = "Reserved";
                        reservationView.EnableField("Status", false);
                        reservationView.EnableField("CustomerName", true);
                        AddNewReservation(sender, EventArgs.Empty);
                        reservationView.ShowTab(1);
                        return;
                    }
                    else
                    {
                        // Load last reservation for review/edit
                        OnLoadReservationForEdit(sender, lastReservation.ReservationId);
                        reservationView.isEdit = true;
                        reservationView.EnableField("Status", true);
                        reservationView.EnableField("CustomerName", false);
                        reservationView.ShowTab(1);
                        return;
                    }
                }

                // If last reservation exists and is not CheckedOut, load it for edit
                OnLoadReservationForEdit(sender, lastReservation.ReservationId);
                reservationView.isEdit = true;
                reservationView.EnableField("Status", true);
                reservationView.EnableField("CustomerName", false);
            }
            else
            {
                // No previous reservation - start new
                reservationView.ReservationId = reservationRepository.GetNextReservationId().ToString();
                reservationView.isEdit = false;
                reservationView.ReservationStatus = "Reserved";
                reservationView.EnableField("Status", false);
                reservationView.EnableField("CustomerName", true);
                AddNewReservation(sender, EventArgs.Empty);
            }

            reservationView.ShowTab(1); 
        }

        private void OnRoomTypeChanged(object sender, string roomType)
        {
            if (string.IsNullOrEmpty(roomType)) return;

            try
            {
                DateTime checkInDate = reservationView.CheckInDate;
                DateTime checkOutDate = reservationView.CheckOutDate;
                
                int? excludeReservationId = null;
                if (reservationView.isEdit && !string.IsNullOrEmpty(reservationView.ReservationId))
                {
                    excludeReservationId = int.Parse(reservationView.ReservationId);
                }

                var availableRooms = roomRepository.GetAvailableRoomsByTypeAndDateRange(
                    roomType, 
                    checkInDate, 
                    checkOutDate, 
                    excludeReservationId);
                
                reservationView.LoadAvailableRooms(availableRooms);
            }
            catch (Exception ex)
            {
                reservationView.ShowErrorMessage($"Error loading rooms: {ex.Message}");
            }
        }

        private void OnShowCheckInOutView(object sender, EventArgs e)
        {
            try
            {
                int reservationId = reservationView.GetSelectedReservationId();
                if (reservationId == 0)
                {
                    reservationView.ShowErrorMessage("Please select a reservation first.");
                    return;
                }

                var reservation = reservationRepository.GetById(reservationId);
                if (reservation == null)
                {
                    reservationView.ShowErrorMessage("Reservation not found.");
                    return;
                }

                var checkInRepo = new Data.Repositories.CheckInOutRepository.CheckInOutRepository(DbConfig.GetConnectionString());
                if (checkInRepo.ExistsForReservation(reservationId))
                {
                    var existingCheckIn = checkInRepo.GetByReservationId(reservationId);
                    MessageBox.Show(
                        $"This reservation has already been checked in.\n\n" +
                        $"Customer: {existingCheckIn.CustomerName}\n" +
                        $"Room: {existingCheckIn.RoomNumber}\n" +
                        $"Check-In Date: {existingCheckIn.CheckInDate:MM/dd/yyyy HH:mm}\n" +
                        $"Status: {existingCheckIn.ReservationStatus}\n\n" +
                        "Use the Edit function in Check-In/Out to modify the existing check-in.",
                        "Already Checked In",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return; // DON'T LOAD THE FORM
                }

                var mainForm = (reservationView as Control)?.FindForm();
                if (mainForm is Forms.ReservationSystem reservationSystem)
                {
                    reservationSystem.LoadUserControl(new UserControls.UCCheckINOUT(reservation));
                }
            }
            catch (Exception ex)
            {
                reservationView.ShowErrorMessage($"Error: {ex.Message}");
            }
        }

        #endregion

        #region Helper Methods

        private void HandleRoomStatusAfterSave(ReservationModel model)
        {
            if (string.IsNullOrEmpty(model.RoomNumber))
                return;

            var room = roomRepository.GetByNumber(model.RoomNumber);
            if (room == null)
                return;

            DateTime now = DateTime.Now.Date;

            switch (model.ReservationStatus)
            {
                case "Reserved":
                case "Confirmed":
                    if (model.CheckInDate.Date <= now && model.CheckOutDate.Date > now)
                    {
                        if (room.RoomStatus != "Occupied")
                        {
                            room.RoomStatus = "Reserved";
                            roomRepository.Edit(room);
                        }
                    }
                    else if (model.CheckInDate.Date > now)
                    {
                        var allReservations = reservationRepository.GetAll()
                            .Where(r => r.RoomNumber == model.RoomNumber 
                                     && r.ReservationStatus != "Cancelled/No Show" 
                                     && r.ReservationStatus != "CheckedOut"
                                     && r.CheckInDate.Date <= now 
                                     && r.CheckOutDate.Date > now)
                            .ToList();
                        
                        if (allReservations.Any())
                        {
                        }
                        else
                        {
                            room.RoomStatus = "Available";
                            roomRepository.Edit(room);
                        }
                    }
                    break;

                case "CheckedIn":
                    room.RoomStatus = "Occupied";
                    roomRepository.Edit(room);
                    break;

                case "Cancelled":
                case "CheckedOut":
                    var activeReservations = reservationRepository.GetAll()
                        .Where(r => r.RoomNumber == model.RoomNumber 
                                 && r.ReservationId != model.ReservationId
                                 && r.ReservationStatus != "Cancelled/No Show" 
                                 && r.ReservationStatus != "CheckedOut"
                                 && r.CheckInDate.Date <= now 
                                 && r.CheckOutDate.Date > now)
                        .ToList();
                    
                    if (activeReservations.Any())
                    {
                        if (activeReservations.Any(r => r.ReservationStatus == "CheckedIn"))
                        {
                            room.RoomStatus = "Occupied";
                        }
                        else
                        {
                            room.RoomStatus = "Reserved";
                        }
                    }
                    else
                    {
                        room.RoomStatus = "Available";
                    }
                    roomRepository.Edit(room);
                    break;
            }
        }

        #endregion

        #region Static Methods

        public static IEnumerable<ReservationDto> GetReservationDtoList()
        {
            var repository = new ReservationRepository(DbConfig.GetConnectionString());
            var reservationList = repository.GetAll() ?? Enumerable.Empty<ReservationModel>();
            return reservationList.Select(ReservationMapper.FromReservationModel).ToList();
        }

        #endregion
    }
}