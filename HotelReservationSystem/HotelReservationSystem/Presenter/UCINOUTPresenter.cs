using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.Data.Repositories.CheckInOut;
using HotelReservationSystem.DataInitializer.DbInitializer;
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Helper;
using HotelReservationSystem.Domain.Interface.CheckInOut;
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
        private System.Collections.Generic.IEnumerable<CheckInOutModel> checkInList;
        private static UCINOUTPresenter _lastPresenterInstance;

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
        }

        #endregion

        #region Load Data

        private void LoadAllCheckInList()
        {
            checkInList = checkInRepository.GetAll() ?? Enumerable.Empty<CheckInOutModel>();
            var dtoList = checkInList.Select(CheckInMapper.ToCheckInDto).ToList();

            CheckInBindingSource.DataSource = dtoList;
            CheckInBindingSource.ResetBindings(false);
        }

        #endregion

        #region Event Handlers

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
            checkInView.ReservationStatus = "Pending";
            LoadRoomTypes();
            checkInView.SetFieldEnabled("ReservationId", true);
            checkInView.SetFieldEnabled("CustomerName", true);
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
            if (model == null) return;

            checkInView.ReservationId = model.ReservationId.ToString();
            checkInView.CustomerName = model.CustomerName;
            checkInView.RoomType = model.RoomType;
            checkInView.RoomNumber = model.RoomNumber;
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
            checkInView.SetFieldEnabled("ReservationId", false);
            checkInView.SetFieldEnabled("CustomerName", false);
            checkInView.isEdit = true;
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

                checkInRepository.Delete(checkInId);

                if (!string.IsNullOrEmpty(roomNumber))
                {
                    roomRepository.SyncRoomStatusesWithReservations(roomNumber, reservationRepository);
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

                if (checkInView.isEdit)
                {
                    checkInRepository.Edit(model);
                    checkInView.Message = "Check-in updated successfully!";
                }
                else
                {
                    checkInRepository.Add(model);
                    checkInView.Message = "Check-in saved successfully!";
                }

                HandleRoomStatusAfterSave(model);

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

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        #endregion

        #region Room Event Handlers

        private void OnRoomTypeChanged(object sender, string roomType)
        {
            if (string.IsNullOrEmpty(roomType)) return;

            try
            {
                var availableRooms = roomRepository.GetAvailableRoomsByType(roomType);
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
            if (string.IsNullOrEmpty(roomNumber)) return;

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

        #endregion

        #region Helper Methods

        private void UpdatePriceForRoomType(decimal roomPricePerNight)
        {
            int nights = (checkInView.CheckOutDate - checkInView.CheckInDate).Days;
            if (nights <= 0) nights = 1;

            decimal newTotalPrice = roomPricePerNight * nights;
            CheckInMapper.UpdateFinancialFields(checkInView, newTotalPrice);
        }

        private void LoadRoomTypes()
        {
            string[] roomTypes = new string[] { "Standard", "Deluxe", "Suite", "Family", "Single" };
            checkInView.LoadRoomTypes(roomTypes);
        }

        private void LoadRoomInformation(CheckInOutModel model)
        {
            if (!string.IsNullOrEmpty(model.RoomType))
            {
                var availableRooms = roomRepository.GetAvailableRoomsByType(model.RoomType).ToList();

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
        }

        private void HandleRoomStatusAfterSave(CheckInOutModel model)
        {
            if (string.IsNullOrEmpty(model.RoomNumber))
                return;

            var room = roomRepository.GetByNumber(model.RoomNumber);
            if (room == null)
                return;

            switch (model.ReservationStatus)
            {
                case "CheckedIn":
                    room.RoomStatus = "Occupied";
                    roomRepository.Edit(room);
                    break;

                case "CheckedOut":
                    room.RoomStatus = "Available";
                    roomRepository.Edit(room);
                    break;
            }
        }

        private void CleanViewFields()
        {
            FieldsCleaner.ClearInputs(checkInView as Control);
        }

        #endregion

        #region Static Methods

        public static System.Collections.Generic.IEnumerable<CheckInDto> GetCheckInDtoList()
        {
            var repository = new CheckInOutRepository(DbConfig.GetConnectionString());
            var checkInList = repository.GetAll() ?? Enumerable.Empty<CheckInOutModel>();
            return checkInList.Select(CheckInMapper.ToCheckInDto).ToList();
        }

        #endregion
    }
}