using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelReservationSystem.Interface.Reservation;
using HotelReservationSystem.Interface.Rooms;
using HotelReservationSystem.Model.Reservation;
using HotelReservationSystem.Presenter.Common;

namespace HotelReservationSystem.Presenter.Reservation
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

        private void CancelReserve(object sender, EventArgs e) => CleanviewFields();

        private void SaveReserve(object sender, EventArgs e)
        {
            var model = new ReservationModel();
            model.ReservationId = int.Parse(reservationView.ReservationId);
            model.CustomerName = reservationView.CustomerName;
            model.CheckInDate = reservationView.CheckInDate;
            model.CheckOutDate = reservationView.CheckOutDate;
            model.TotalPrice = decimal.Parse(reservationView.TotalPrice);
            model.ReservationStatus = reservationView.ReservationStatus;
            model.RoomNumber = reservationView.RoomNumber;

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
            reservationView.CustomerName = reserve.CustomerName.ToString();
            reservationView.CheckInDate = reserve.CheckInDate;
            reservationView.CheckOutDate = reserve.CheckOutDate;
            reservationView.TotalPrice = reserve.TotalPrice.ToString();
            reservationView.ReservationStatus = reserve.ReservationStatus;
            reservationView.RoomNumber = reserve.RoomNumber;


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

        private void CleanviewFields()
        {
            reservationView.ReservationId = "";
            reservationView.CustomerName = "";
            reservationView.RoomNumber = "";
            reservationView.RoomType = "";
            reservationView.Guests = "";
            reservationView.CheckInDate = DateTime.Now;
            reservationView.CheckOutDate = DateTime.Now;
            reservationView.TotalPrice = "";
            reservationView.RoomNumber = "";
        }
    }
}
