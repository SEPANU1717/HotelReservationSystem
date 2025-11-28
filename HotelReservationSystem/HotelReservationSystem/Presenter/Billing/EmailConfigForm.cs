using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelReservationSystem.Presenter.Billing
{
    public partial class EmailConfigForm : Form
    {
        public EmailConfigForm()
        {
            InitializeComponent();
        }

        public string SmtpServer { get; internal set; }
        public int SmtpPort { get; internal set; }
        public string SenderEmail { get; internal set; }
        public string SenderPassword { get; internal set; }
        public bool EnableSsl { get; internal set; }
    }
}
