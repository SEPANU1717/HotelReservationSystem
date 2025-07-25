using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelReservationSystem.Interface;
using HotelReservationSystem.Interface.Rooms;
using HotelReservationSystem.Model.Customer;
using HotelReservationSystem.Presenter;
using HotelReservationSystem.Repositories;
using HotelReservationSystem.UserControls;

namespace HotelReservationSystem
{
    internal static class Program
    {
        /// <summary>  
        /// The main entry point for the application.  
        /// </summary>  
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;


            ReservationSystem mainForm = new ReservationSystem();
            new MainPresenter(mainForm, sqlConnectionString);

            Application.Run(mainForm);
        }
    }
}
