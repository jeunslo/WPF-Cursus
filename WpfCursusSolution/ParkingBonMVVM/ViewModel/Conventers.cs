using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ParkingBonMVVM.ViewModel
{
    public class DecimalToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringGetal = (string)value;
            stringGetal = stringGetal.Replace("€", "");
            decimal.TryParse(stringGetal, out decimal x);
            if (x <= 0)
                return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
