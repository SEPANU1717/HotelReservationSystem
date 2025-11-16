using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HotelReservationSystem.Domain.DTOs;
using HotelReservationSystem.Domain.Interface;
using HotelReservationSystem.Domain.Interface.Customer;
using HotelReservationSystem.Domain.Interface.Reservation;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Infrastructure.Security;
using HotelReservationSystem.Presenter.Common;
using HotelReservationSystem.Presenter.Mapper;

namespace HotelReservationSystem.Presenter
{
    public class UserPresenter
    {
        private readonly IUserView userView;
        private readonly IUserRepository repository;
        private readonly IPasswordHasher passwordHasher;
        private readonly BindingSource UserBindingSource;
        private IEnumerable<UserModel> userList;

        private static UserPresenter _lastPresenterInstance;
        public UserPresenter(IUserView userView, IUserRepository repository, IPasswordHasher passwordHasher)
        {
            UserBindingSource = new BindingSource();
            this.userView = userView ?? throw new ArgumentNullException(nameof(userView));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));

            if (_lastPresenterInstance != null)
                _lastPresenterInstance.UnsubscribeFromViewEvents();

            SubscribeToViewEvents();
            _lastPresenterInstance = this;

            this.userView.SetUserListBindingSource(UserBindingSource);
            LoadAllUserList();
        }

        private void SubscribeToViewEvents()
        {
            this.userView.SearchEvent += SearchUser;
            this.userView.AddNewEvent += AddNewUser;
            this.userView.EditEvent += EditUser;
            this.userView.SaveEvent += SaveUser;
            this.userView.CancelEvent += CancelUser;
            this.userView.DeleteEvent += DeleteUser;
        }

        private void UnsubscribeFromViewEvents()
        {
            this.userView.SearchEvent -= SearchUser;
            this.userView.AddNewEvent -= AddNewUser;
            this.userView.EditEvent -= EditUser;
            this.userView.SaveEvent -= SaveUser;
            this.userView.CancelEvent -= CancelUser;
            this.userView.DeleteEvent -= DeleteUser;
        }

        private void LoadAllUserList()
        {
            userList = repository.GetAll() ?? Enumerable.Empty<UserModel>();
            var dtoList = userList.Select(UserMapper.FromUserModel).ToList();


            UserBindingSource.DataSource = dtoList;
            UserBindingSource.ResetBindings(false);
        }

        private void DeleteUser(object sender, EventArgs e)
        {
            try
            {
                var selectedDto = UserBindingSource.Current as UserDto;
                if (selectedDto == null)
                {
                    userView.isSuccessful = false;
                    userView.Message = "No user selected for deletion.";
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to delete user '{selectedDto.FullName}'?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    repository.Delete(selectedDto.UserId);
                    userView.isSuccessful = true;
                    userView.Message = "User deleted successfully";
                    LoadAllUserList();
                }
            }
            catch (Exception ex)
            {
                userView.isSuccessful = false;
                userView.Message = "An error occurred, could not delete user: " + ex.Message;
            }
        }

        private void CancelUser(object sender, EventArgs e) => CleanViewFields();

        private void SaveUser(object sender, EventArgs e)
        {
            var model = UserMapper.FromUserView(userView);

            try
            {
                // Business Rule Validation - Age Check (18+)
                var age = DateTime.Now.Year - model.BirthDate.Year;
                if (model.BirthDate.Date > DateTime.Now.AddYears(-age))
                {
                    age--;
                }

                if (age < 18)
                {
                    userView.isSuccessful = false;
                    userView.Message = $"User must be at least 18 years old to register.\n\nCurrent age: {age} years old\nBirth date: {model.BirthDate:MM/dd/yyyy}";
                    MessageBox.Show(userView.Message, "Age Requirement Not Met", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate username format
                if (!System.Text.RegularExpressions.Regex.IsMatch(model.Username, @"^[a-zA-Z0-9_]+$"))
                {
                    userView.isSuccessful = false;
                    userView.Message = "Username can only contain letters, numbers, and underscores.";
                    MessageBox.Show(userView.Message, "Invalid Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (userView.isEdit)
                {
                    var existingUser = repository.GetById(model.UserId);
                    if (existingUser == null)
                    {
                        userView.isSuccessful = false;
                        userView.Message = "User not found for update.";
                        return;
                    }

                    model.CreatedAt = existingUser.CreatedAt;

                    // Password validation for edit
                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        if (model.Password.Length < 6)
                        {
                            userView.isSuccessful = false;
                            userView.Message = "Password must be at least 6 characters long.";
                            MessageBox.Show(userView.Message, "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        model.PasswordHash = passwordHasher.HashPassword(model.Password);
                    }
                    else
                    {
                        model.PasswordHash = existingUser.PasswordHash;
                        model.Password = "lodgixhotel";
                    }

                    // Standard model validation
                    new ModelDataValidation().Validate(model);

                    if (string.IsNullOrEmpty(userView.Password))
                    {
                        model.Password = null;
                    }

                    repository.Edit(model);
                    userView.Message = "User updated successfully";
                    LoadAllUserList();
                }
                else
                {
                    // New user - password is required
                    if (string.IsNullOrEmpty(model.Password))
                    {
                        userView.isSuccessful = false;
                        userView.Message = "Password is required for new users.";
                        MessageBox.Show(userView.Message, "Password Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (model.Password.Length < 6)
                    {
                        userView.isSuccessful = false;
                        userView.Message = "Password must be at least 6 characters long.";
                        MessageBox.Show(userView.Message, "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    model.PasswordHash = passwordHasher.HashPassword(model.Password);

                    // Standard model validation
                    new ModelDataValidation().Validate(model);

                    // Check for duplicate username
                    var allUsers = repository.GetAll();
                    if (allUsers.Any(u => u.Username.Equals(model.Username, StringComparison.OrdinalIgnoreCase)))
                    {
                        userView.isSuccessful = false;
                        userView.Message = $"Username '{model.Username}' is already taken. Please choose a different username.";
                        MessageBox.Show(userView.Message, "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    repository.Add(model);
                    userView.Message = "User created successfully";
                }

                userView.isSuccessful = true;
                LoadAllUserList();
                CleanViewFields();
                MessageBox.Show(userView.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                userView.isSuccessful = false;
                userView.Message = ex.Message;
                MessageBox.Show(userView.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditUser(object sender, EventArgs e)
        {
            try
            {
                var selectedDto = UserBindingSource.Current as UserDto;
                if (selectedDto == null)
                {
                    userView.isSuccessful = false;
                    userView.Message = "No user selected for editing.";
                    return;
                }

                var user = repository.GetById(selectedDto.UserId);
                if (user == null)
                {
                    userView.isSuccessful = false;
                    userView.Message = "Selected user not found.";
                    return;
                }

                userView.UserId = user.UserId.ToString();
                userView.LastName = user.LastName;
                userView.FirstName = user.FirstName;
                userView.MiddleName = user.MiddleName;
                userView.BirthDate = user.BirthDate;
                userView.Username = user.Username;
                userView.Email = user.Email;
                userView.Gender = user.Gender;
                userView.Role = user.Role;

                userView.Password = user.Password;
                userView.isEdit = true;
            }
            catch (Exception ex)
            {
                userView.isSuccessful = false;
                userView.Message = "Error loading user for editing: " + ex.Message;
            }
        }

        private void AddNewUser(object sender, EventArgs e)
        {
            userView.isEdit = false;
            CleanViewFields();
        }

        private void SearchUser(object sender, EventArgs e)
        {
            try
            {
                bool emptyValue = string.IsNullOrWhiteSpace(userView.SearchValue);
                userList = emptyValue
                    ? repository.GetAll()
                    : repository.GetByValue(userView.SearchValue);

                var dtoList = (userList ?? Enumerable.Empty<UserModel>())
                    .Select(UserMapper.FromUserModel)
                    .ToList();

                UserBindingSource.DataSource = dtoList;
                UserBindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                userView.isSuccessful = false;
                userView.Message = "Error searching users: " + ex.Message;
            }
        }

        private void CleanViewFields() => FieldsCleaner.ClearInputs(userView as Control);
    }
}
