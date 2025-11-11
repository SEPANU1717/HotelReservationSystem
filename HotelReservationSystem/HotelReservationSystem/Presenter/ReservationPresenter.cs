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
        }

        #endregion

        #region Event Handlers

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
            }
            catch (Exception ex)
            {
                reservationView.isSuccessful = false;
                reservationView.Message = ex.Message;
                reservationView.ShowErrorMessage(reservationView.Message);
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
                    var availableRooms = roomRepository.GetAvailableRoomsByType(room.RoomType);
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
            var reservation = reservationRepository.GetByCustomerName(customerName);

            if (reservation != null)
            {
                OnLoadReservationForEdit(sender, reservation.ReservationId);
                reservationView.isEdit = true;
                reservationView.EnableField("Status", true);
                reservationView.EnableField("CustomerName", false);
            }
            else
            {
                reservationView.ReservationId = reservationRepository.GetNextReservationId().ToString();
                reservationView.isEdit = false;
                reservationView.ReservationStatus = "Pending";
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
                var availableRooms = roomRepository.GetAvailableRoomsByType(roomType);
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

            switch (model.ReservationStatus)
            {
                case "Reserved":
                    room.RoomStatus = "Reserved";
                    roomRepository.Edit(room);
                    break;

                case "CheckedIn":
                    room.RoomStatus = "Occupied";
                    roomRepository.Edit(room);
                    break;

                case "Cancelled":
                case "CheckedOut":
                    room.RoomStatus = "Available";
                    roomRepository.Edit(room);
                    break;

                case "Confirmed":
                    
                    if (room.RoomStatus != "Reserved")
                    {
                        room.RoomStatus = "Reserved";
                        roomRepository.Edit(room);
                    }
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