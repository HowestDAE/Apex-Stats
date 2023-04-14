using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ApexStats.View.Converters
{
    internal class RefToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string type = value.ToString();

            if (type == "Apex Coins")
                return "pack://application:,,,/Resources/Images/Apex_Coins.png";
            else
                return "pack://application:,,,/Resources/Images/Legend_Tokens.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
