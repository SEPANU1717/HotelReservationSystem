using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelReservationSystem.Presenter.Common;

namespace HotelReservationSystem.UserControls
{
    public partial class UCINOUT : UserControl
    {
        public UCINOUT()
        {
            InitializeComponent();
            UserInfoDisplay.UpdateUserInfoDisplay(lblUsername, lblRole, pictureProfile);

        }

        public static void ResetInstance() =>
            UserControlFactory<UCINOUT>.ResetInstance();

        public static UCINOUT GetInstance(Form parentContainer) =>
            UserControlFactory<UCINOUT>.GetInstance(parentContainer);
    }
}
