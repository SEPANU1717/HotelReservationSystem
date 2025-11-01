using System;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Interface;
using HotelReservationSystem.Presenter.Common;
using static HotelReservationSystem.Domain.Enums.UserEnums;

namespace HotelReservationSystem.UserControls
{
    public partial class UCSettings : UserControl, IUserView
    {
        public UCSettings()
        {
            InitializeComponent();
            materialTabControl1.TabPages.Remove(tabPage2);
            AssociateAndRaiseEvents();
            cbGender.DataSource = Enum.GetValues(typeof(Gender));
            cbRole.DataSource = Enum.GetValues(typeof(Role));
            UserInfoDisplay.UpdateUserInfoDisplay(lblUsername, lblRole, pictureProfile);
        }

        private void AssociateAndRaiseEvents()
        {
            btnAddNew.Click += delegate
            {
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.TabPages.Remove(tabPage1);
                AddNewEvent?.Invoke(this, EventArgs.Empty);
            };

            btnEdit.Click += delegate
            {
                materialTabControl1.TabPages.Add(tabPage2);
                materialTabControl1.TabPages.Remove(tabPage1);
                EditEvent?.Invoke(this, EventArgs.Empty);
            };

            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            btnSaveUser.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                MessageBox.Show(Message);
            };
            btnCancel.Click += delegate
            {
                materialTabControl1.TabPages.Add(tabPage1);
                materialTabControl1.TabPages.Remove(tabPage2);
                CancelEvent?.Invoke(this, EventArgs.Empty);
            };
            btnDelete.Click += delegate { DeleteEvent?.Invoke(this, EventArgs.Empty); };
        }

        public void SetUserListBindingSource(BindingSource bindingSource)
        {
            dgUserManagement.DataSource = bindingSource;
        }

        public static UCSettings GetInstance(Form parentContainer) => 
            UserControlFactory<UCSettings>.GetInstance(parentContainer);
        public static void ResetInstance() => 
            UserControlFactory<UCSettings>.ResetInstance();

        public string UserId { get; set; }
        public string LastName { get => txtLastname.Texts; set => txtLastname.Texts = value; }
        public string FirstName { get => txtFirstname.Texts; set => txtFirstname.Texts = value; }
        public string MiddleName { get => txtMiddlename.Texts; set => txtMiddlename.Texts = value; }
        public DateTime BirthDate { get => dpBirthdate.Content; set => dpBirthdate.Content = value; }
        public string Username { get => txtUsername.Texts; set => txtUsername.Texts = value; }
        public string Password { get => txtPassword.Texts; set => txtPassword.Texts = value; }
        public string Email { get => txtEmail.Texts; set => txtEmail.Texts = value; }
        public string Gender { get => cbGender.Text; set => cbGender.Text = value; }
        public string Role { get => cbRole.Text; set => cbRole.Text = value; }
        public bool IsActive { get => true; set { } }
        public string SearchValue { get => txtSearch.Texts; set => txtSearch.Texts = value; }
        public bool isEdit { get; set; }
        public bool isSuccessful { get; set; }
        public string Message { get; set; }
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler DeleteEvent;

    }
}
