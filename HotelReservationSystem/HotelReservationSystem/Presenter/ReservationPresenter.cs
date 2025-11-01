using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
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
        private readonly IReservationRepository repository;
        private readonly BindingSource ReservationBindingSource;
        private IEnumerable<ReservationModel> reservationList;

        public ReservationPresenter(IReservationView reservationView, IReservationRepository repository)
        {
            ReservationBindingSource = new BindingSource();
            this.reservationView = reservationView ?? throw new ArgumentNullException(nameof(reservationView));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));

            // Subscribe to events
            this.reservationView.SearchEvent += SearchReservation;
            this.reservationView.AddNewEvent += AddNewReservation;
            this.reservationView.EditEvent += EditReservation;
            this.reservationView.DeleteEvent += DeleteReservation;
            this.reservationView.SaveEvent += SaveReservation;
            this.reservationView.CancelEvent += CancelAction;

            this.reservationView.SetReservationListBindingSource(ReservationBindingSource);
            LoadAllReservationList();
            this.reservationView.Show();
        }

        private void LoadAllReservationList()
        {
            reservationList = repository.GetAll() ?? Enumerable.Empty<ReservationModel>();
            var dtoList = reservationList.Select(ReservationMapper.FromReservationModel).ToList();

            ReservationBindingSource.DataSource = dtoList;
            ReservationBindingSource.ResetBindings(false);
        }

        private void SearchReservation(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(reservationView.SearchValue);
            reservationList = emptyValue
                ? repository.GetAll()
                : repository.GetByValue(reservationView.SearchValue);

            var dtoList = (reservationList ?? Enumerable.Empty<ReservationModel>())
                .Select(ReservationMapper.FromReservationModel)
                .ToList();

            ReservationBindingSource.DataSource = dtoList;
            ReservationBindingSource.ResetBindings(false);
        }

        private void AddNewReservation(object sender, EventArgs e) => reservationView.isEdit = false;

        private void EditReservation(object sender, EventArgs e)
        {
            var dto = (ReservationDto)ReservationBindingSource.Current;
            var model = reservationList.FirstOrDefault(r => r.ReservationId == dto.ReservationId);
            if (model == null) return;

            reservationView.ReservationId = model.ReservationId.ToString();
            reservationView.CustomerName = model.CustomerName;
            reservationView.RoomNumber = model.RoomNumber;
            reservationView.CheckInDate = model.CheckInDate;
            reservationView.CheckOutDate = model.CheckOutDate;
            reservationView.TotalPrice = model.TotalPrice.ToString(CultureInfo.InvariantCulture);
            reservationView.AmountPaid = model.AmountPaid.ToString(CultureInfo.InvariantCulture);
            reservationView.DownPayment = model.DownPayment.ToString(CultureInfo.InvariantCulture);
            reservationView.BalanceDue = model.BalanceDue.ToString(CultureInfo.InvariantCulture);
            reservationView.ReservationStatus = model.ReservationStatus;
            reservationView.PaymentStatus = model.PaymentStatus;
            reservationView.isEdit = true;
        }

        private void DeleteReservation(object sender, EventArgs e)
        {
            try
            {
                var dto = (ReservationDto)ReservationBindingSource.Current;
                repository.Delete(dto.ReservationId);
                reservationView.isSuccessful = true;
                reservationView.Message = "Reservation deleted successfully.";
                LoadAllReservationList();
            }
            catch
            {
                reservationView.isSuccessful = false;
                reservationView.Message = "Error: Could not delete reservation.";
            }
        }

        private void SaveReservation(object sender, EventArgs e)
        {
            var model = ReservationMapper.FromReservationView(reservationView);
            try
            {
                new ModelDataValidation().Validate(model);

                if (reservationView.isEdit)
                {
                    repository.Edit(model);
                    reservationView.Message = "Reservation updated successfully!";
                }
                else
                {
                    repository.Add(model);
                    reservationView.Message = "Reservation added successfully!";
                }

                reservationView.isSuccessful = true;
                LoadAllReservationList();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                reservationView.isSuccessful = false;
                reservationView.Message = ex.Message;
            }
        }

        private void CancelAction(object sender, EventArgs e) => CleanViewFields();

        private void CleanViewFields() => FieldsCleaner.ClearInputs(reservationView as Control);
    }
}
