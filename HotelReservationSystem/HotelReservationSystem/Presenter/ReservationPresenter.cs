using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.Domain.Interface.Customer;
using HotelReservationSystem.Domain.Interface.Reservation;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Presenter.Common;

namespace HotelReservationSystem.Presenter
{
    public class ReservationPresenter
    {
        private IReservationView reservationView;
        private IReservationRepository repository;
        private BindingSource ReservationBindingSource;
        private IEnumerable<ReservationModel> reservationList;

        public ReservationPresenter(IReservationView reservationView, IReservationRepository repository)
        {
            ReservationBindingSource = new BindingSource();
            this.reservationView = reservationView;
            this.repository = repository;

            //Subscribe
            this.reservationView.SearchEvent += SearchReserve;
            this.reservationView.AddNewEvent += AddNewReserve;
            this.reservationView.EditEvent += EditReserve;
            this.reservationView.SaveEvent += SaveReserve;
            this.reservationView.CancelEvent += CancelReserve;
            this.reservationView.DeleteEvent += DeleteReserve;

            this.reservationView.SetReservationListBindingSource(ReservationBindingSource);
            LoadAllReservationList();
            this.reservationView.Show();

        }

        private void LoadAllReservationList()
        {
            reservationList = repository.GetAll();
            ReservationBindingSource.DataSource = reservationList;
            ReservationBindingSource.ResetBindings(false);
        }

        private void DeleteReserve(object sender, EventArgs e)
        {
            try
            {
                var reserve = ReservationBindingSource.Current as ReservationModel;
                if (reserve == null)
                {
                    reservationView.isSuccessful = false;
                    reservationView.Message = "No reservation selected for deletion.";
                    return;
                }
                repository.Delete(reserve.ReservationId);
                reservationView.isSuccessful = true;
                reservationView.Message = "Reservation deleted successfully";
                LoadAllReservationList();
            }
            catch (Exception ex)
            {
                reservationView.isSuccessful = false;
                reservationView.Message = "An error occurred, could not delete reservation: " + ex.Message;
            }
        }

        private void CancelReserve(object sender, EventArgs e) => CleanViewFields();

        private void SaveReserve(object sender, EventArgs e)
        {
            var model = new ReservationModel
            {
                ReservationId = int.Parse(reservationView.ReservationId),
                CustomerName = reservationView.CustomerName,
                CheckInDate = reservationView.CheckInDate,
                CheckOutDate = reservationView.CheckOutDate,
                TotalPrice = decimal.Parse(reservationView.TotalPrice),
                ReservationStatus = reservationView.ReservationStatus,
                RoomNumber = reservationView.RoomNumber
            };

            try
            {
                new ModelDataValidation().Validate(model);
                if (reservationView.isEdit)
                {
                    repository.Edit(model);
                    reservationView.Message = "Reservation edited successfully";
                }
                else
                {
                    repository.Add(model);
                    reservationView.Message = "Reservation added successfully";
                }
                reservationView.isSuccessful = true;
                LoadAllReservationList();
            }
            catch(Exception ex) 
            {
                reservationView.isSuccessful = false;
                reservationView.Message = ex.Message;
            }
        }

        private void EditReserve(object sender, EventArgs e)
        {
            var reserve = (ReservationModel)ReservationBindingSource.Current;
            reservationView.ReservationId = reserve.ReservationId.ToString();
            reservationView.CustomerName = reserve.CustomerName;
            reservationView.CheckInDate = reserve.CheckInDate;
            reservationView.CheckOutDate = reserve.CheckOutDate;
            reservationView.TotalPrice = reserve.TotalPrice.ToString(CultureInfo.InvariantCulture);
            reservationView.ReservationStatus = reserve.ReservationStatus;
            reservationView.RoomNumber = reserve.RoomNumber;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            var roomRepo = new RoomRepository(connectionString);
            var room = roomRepo.GetByNumber(reserve.RoomNumber);
            reservationView.Guests = room != null ? room.RoomGuests : "";

            reservationView.isEdit = true;
        }

        private void AddNewReserve(object sender, EventArgs e) => reservationView.isEdit = false;

        private void SearchReserve(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(reservationView.SearchValue);
            reservationList = emptyValue
                ? repository.GetAll()
                : repository.GetByValue(reservationView.SearchValue);

            ReservationBindingSource.DataSource = reservationList;
            ReservationBindingSource.ResetBindings(false);
        }

        private void CleanViewFields() => FieldsCleaner.ClearInputs(reservationView as Control);
    }
}
