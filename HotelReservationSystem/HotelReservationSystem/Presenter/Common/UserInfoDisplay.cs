using System;
using System.Drawing;
using System.Windows.Forms;

namespace HotelReservationSystem.Presenter.Common
{
    public class UserInfoDisplay
    {
        private static readonly string DefaultMalePhotoPath = "C:\\LodgixHRS Main\\HotelReservationSystem\\HotelReservationSystem\\Resources\\male.png";
        private static readonly string DefaultFemalePhotoPath = "C:\\LodgixHRS Main\\HotelReservationSystem\\HotelReservationSystem\\Resources\\female.png";

        public static void UpdateUserInfoDisplay(Label usernameLabel, Label roleLabel, PictureBox profilePicture = null)
        {
            if (usernameLabel != null)
            {
                Action setUserName = () => usernameLabel.Text = UserSession.Username ?? string.Empty;
                if (usernameLabel.InvokeRequired)
                    usernameLabel.Invoke(setUserName);
                else
                    setUserName();
            }

            if (roleLabel != null)
            {
                Action setRole = () => roleLabel.Text = UserSession.Role ?? string.Empty;
                if (roleLabel.InvokeRequired)
                    roleLabel.Invoke(setRole);
                else
                    setRole();
            }

            if (profilePicture != null)
            {
                Action setPhoto = () =>
                {
                    try
                    {
                        string photoPath = UserSession.IsFemale
                            ? DefaultFemalePhotoPath
                            : DefaultMalePhotoPath;

                        using (var img = Image.FromFile(photoPath))
                        {
                            profilePicture.Image?.Dispose();
                            profilePicture.Image = new Bitmap(img);
                            profilePicture.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                    catch (Exception)
                    {
                        profilePicture.Image = null;
                    }
                };

                if (profilePicture.InvokeRequired)
                    profilePicture.Invoke(setPhoto);
                else
                    setPhoto();
            }
        }
    }
}