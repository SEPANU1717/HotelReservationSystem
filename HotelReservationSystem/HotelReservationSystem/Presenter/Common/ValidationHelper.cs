using System;
using System.Windows.Forms;

namespace HotelReservationSystem.Presenter.Common
{
    public static class ValidationHelper
    {
        public static bool TryParseDecimal(string input, out decimal value)
        {
            value = 0m;
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            if (decimal.TryParse(input, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out value))
                return true;
            if (decimal.TryParse(input, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, out value))
                return true;

            return false;
        }

        public static bool TryParseInt(string input, out int value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(input)) return false;
            return int.TryParse(input, out value);
        }

        public static void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
