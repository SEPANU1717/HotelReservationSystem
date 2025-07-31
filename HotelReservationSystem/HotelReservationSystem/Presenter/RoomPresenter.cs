using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationSystem.Interface;
using HotelReservationSystem.Model.Customer;
using System.Windows.Forms;
using HotelReservationSystem.Interface.Rooms;
using HotelReservationSystem.Model.Rooms;
using HotelReservationSystem.Presenter.Common;

namespace HotelReservationSystem.Presenter.Rooms
{
    public class RoomPresenter
    {
        private IRoomView roomView;
        private IRoomRepository repository;
        private BindingSource RoomBindingSource;
        private IEnumerable<RoomModel> roomList;

        public RoomPresenter(IRoomView roomView, IRoomRepository repository)
        {
            RoomBindingSource = new BindingSource();
            this.roomView = roomView;
            this.repository = repository;

            // Subscribe
            this.roomView.SearchEvent += SearchRoom;
            this.roomView.DeleteEvent += DeleteRoom;
            this.roomView.AddNewEvent += AddNewRoom;
            this.roomView.EditEvent += EditRoom;
            this.roomView.SaveEvent += SaveRoom;
            this.roomView.CancelEvent += CancelRoom;

            this.roomView.SetRoomListBindingSource(RoomBindingSource);
            LoadAllRoomList();
            this.roomView.Show();
        }

        private void LoadAllRoomList()
        {
            roomList = repository.GetAll();
            RoomBindingSource.DataSource = roomList;
            RoomBindingSource.ResetBindings(false);
        }

        private void CancelRoom(object sender, EventArgs e) => CleanviewFields();

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
                new ModelDataValidation().Validate(model);

                if (roomView.isEdit)
                {
                    repository.Edit(model);
                    roomView.Message = "Customer edited successfully";
                }
                else
                {
                    repository.Add(model);
                    roomView.Message = "Customer added Successfully";
                }
                roomView.isSuccessful = true;
                LoadAllRoomList();
            }
            catch(Exception ex)
            {
                roomView.isSuccessful = false;
                roomView.Message = ex.Message;
            }
        }

        private void EditRoom(object sender, EventArgs e)
        {
            var room = (RoomModel)RoomBindingSource.Current;
            roomView.RoomId = room.RoomId.ToString();
            roomView.RoomNumber = room.RoomNumber;
            roomView.RoomType = room.RoomType;
            roomView.RoomStatus = room.RoomStatus;
            roomView.RoomPrice = room.RoomPrice.ToString();
            roomView.RoomDescription = room.RoomDescription;
            roomView.RoomGuests = room.RoomGuests.ToString();
            roomView.BedCount = room.BedCount.ToString();

            roomView.isEdit = true;
        }

        private void AddNewRoom(object sender, EventArgs e)
        {
            roomView.isEdit = false;
        }

        private void DeleteRoom(object sender, EventArgs e)
        {
            try
            {
                var room = (RoomModel)RoomBindingSource.Current;
                repository.Delete(room.RoomId);
                roomView.isSuccessful = true;
                roomView.Message = "Room deleted succesfully";
                LoadAllRoomList();
            }
            catch
            {
                roomView.isSuccessful = false;
                roomView.Message = "An error ocurred, could not delete room";
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
        private void CleanviewFields()
        {
            roomView.RoomId = "0";
            roomView.RoomNumber = "";
            roomView.RoomPrice = "0";
            roomView.RoomStatus = "";
            roomView.RoomGuests = "";
            roomView.BedCount = "0";
            roomView.RoomDescription = "";
            roomView.RoomType = "";
        }
    }
}
