using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelReservationSystem.UserControls
{
    public partial class UCReservationRoom : UserControl
    {
        public UCReservationRoom()
        {
            InitializeComponent();
            materialTabControl1.TabPages.Remove(tabPage1);
        }
    }
}
