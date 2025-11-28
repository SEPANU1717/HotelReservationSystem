using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Interface.Customer;
using HotelReservationSystem.Domain.Interface.Reservation;
using HotelReservationSystem.Domain.Interface.Rooms;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Presenter.Common;

namespace HotelReservationSystem.Presenter
{
    public class RoomPresenter
    {
        private IRoomView roomView;
        private IRoomRepository repository;
        private BindingSource RoomBindingSource;
        private IEnumerable<RoomModel> roomList;
        private static RoomPresenter _lastPresenterInstance;

        public RoomPresenter(IRoomView roomView, IRoomRepository repository)
        {
            RoomBindingSource = new BindingSource();
            this.roomView = roomView;
            this.repository = repository;

            if (_lastPresenterInstance != null)
                _lastPresenterInstance.UnsubscribeFromViewEvents();

            SubscribeToViewEvents();
            _lastPresenterInstance = this;

            this.roomView.SetRoomListBindingSource(RoomBindingSource);
            LoadAllRoomList();
            this.roomView.Show();
        }

        private void SubscribeToViewEvents()
        {
            this.roomView.SearchEvent += SearchRoom;
            this.roomView.DeleteEvent += DeleteRoom;
            this.roomView.AddNewEvent += AddNewRoom;
            this.roomView.EditEvent += EditRoom;
            this.roomView.SaveEvent += SaveRoom;
            this.roomView.CancelEvent += CancelRoom;
            this.roomView.FilterEvent += FilterRooms;
        }

        private void UnsubscribeFromViewEvents()
        {
            this.roomView.SearchEvent -= SearchRoom;
            this.roomView.DeleteEvent -= DeleteRoom;
            this.roomView.AddNewEvent -= AddNewRoom;
            this.roomView.EditEvent -= EditRoom;
            this.roomView.SaveEvent -= SaveRoom;
            this.roomView.CancelEvent -= CancelRoom;
            this.roomView.FilterEvent -= FilterRooms;
        }

        private void FilterRooms(object sender, EventArgs e)
        {
            string filter = roomView.StatusFilter;
            roomList = repository.GetByStatusFilter(filter);
            RoomBindingSource.DataSource = roomList;
            RoomBindingSource.ResetBindings(false);
        }

        private void LoadAllRoomList()
        {
            roomList = repository.GetAll();
            RoomBindingSource.DataSource = roomList;
            RoomBindingSource.ResetBindings(false);
        }

        private void CancelRoom(object sender, EventArgs e)
        {
            CleanViewFields();
            LoadAllRoomList();
        }

        private void SaveRoom(object sender, EventArgs e)
        {
            var model = new RoomModel();
            model.RoomId = int.Parse(roomView.RoomId);
            model.RoomNumber = roomView.RoomNumber;
            model.RoomPrice = roomView.RoomPrice;
            model.RoomDescription = roomView.RoomDescription;
            model.RoomStatus = roomView.RoomStatus;
            model.RoomGuests = roomView.RoomGuests;
            model.RoomType = roomView.RoomType;
            model.BedCount = roomView.BedCount;

            try
            {
                if (!UserSession.IsAdmin)
                {
                    MessageBox.Show(
                        string.Format("{0} cannot {1} rooms. Only administrators can manage room inventory.",
                            UserSession.Role,
                            roomView.isEdit ? "edit" : "add"),
                        "Access Denied",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    roomView.isSuccessful = false;
                    roomView.Message = "Access denied - Administrator privileges required";
                    return;
                }

                new ModelDataValidation().Validate(model);

                if (roomView.isEdit)
                {
                    repository.Edit(model);
                    roomView.Message = "Room updated successfully";
                }
                else
                {
                    repository.Add(model);
                    roomView.Message = "Room added successfully";
                }
                roomView.isSuccessful = true;
                LoadAllRoomList();
            }
            catch (Exception ex)
            {
                roomView.isSuccessful = false;
                roomView.Message = ex.Message;
            }
        }

        private void EditRoom(object sender, EventArgs e)
        {
            if (!UserSession.IsAdmin)
            {
                roomView.isEdit = false;
                return; 
            }

            var room = (RoomModel)RoomBindingSource.Current;
            if (room == null)
            {
                roomView.isEdit = false;
                return;
            }

            roomView.RoomId = room.RoomId.ToString();
            roomView.RoomNumber = room.RoomNumber;
            roomView.RoomType = room.RoomType;
            roomView.RoomStatus = room.RoomStatus;
            roomView.RoomPrice = room.RoomPrice;
            roomView.RoomDescription = room.RoomDescription;
            roomView.RoomGuests = room.RoomGuests;
            roomView.BedCount = room.BedCount;

            roomView.isEdit = true; 
        }

        private void AddNewRoom(object sender, EventArgs e)
        {
            if (!UserSession.IsAdmin)
            {
                return;
            }

            roomView.isEdit = false;
        }

        private void DeleteRoom(object sender, EventArgs e)
        {
            try
            {
                if (!UserSession.IsAdmin)
                {
                    return;
                }

                var room = (RoomModel)RoomBindingSource.Current;
                if (room == null) return;

                var result = MessageBox.Show(
                    string.Format("Are you sure you want to delete room '{0}'?", room.RoomNumber),
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    repository.Delete(room.RoomId);
                    roomView.isSuccessful = true;
                    roomView.Message = "Room deleted successfully";
                    LoadAllRoomList();
                }
            }
            catch
            {
                roomView.isSuccessful = false;
                roomView.Message = "An error occurred, could not delete room";
            }
        }

        private void SearchRoom(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(roomView.SearchValue);
            roomList = emptyValue
                ? repository.GetAll()
                : repository.GetByValue(roomView.SearchValue);

            RoomBindingSource.DataSource = roomList;
            RoomBindingSource.ResetBindings(false);
        }
        
        private void CleanViewFields() => FieldsCleaner.ClearInputs(roomView as Control);
    }
}
