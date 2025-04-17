using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace Trackery_App.Core
{
    public class BoolToStringConverter : IValueConverter
    {
        public string TrueText { get; set; } = "Yes";
        public string FalseText { get; set; } = "No";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? TrueText : FalseText;

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                if (str.Equals(TrueText, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (str.Equals(FalseText, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
