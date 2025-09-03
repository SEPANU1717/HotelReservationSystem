using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelReservationSystem.Presenter.Common
{
        public class UserControlFactory<T> where T : UserControl, new()
    {
        private static T _instance;
        public static T GetInstance(Form parentContainer)
        {
            if (_instance is null || _instance.IsDisposed) _instance = new T();
            if(_instance.Parent != null && _instance.Parent != parentContainer)
                _instance.Parent.Controls.Remove(_instance);


            var method = typeof(T).GetMethod("UpdateUserInfoDisplay", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            method?.Invoke(_instance, null);

            _instance.Dock = DockStyle.Fill;
            return _instance;
        }

        public static void ResetInstance()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }
    }
}
