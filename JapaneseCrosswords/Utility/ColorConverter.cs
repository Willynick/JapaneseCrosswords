using System;
using System.Windows.Data;
using System.Windows.Media;

namespace JapaneseCrosswords.Utility
{
    public enum GridState { White, Black }

    public class ColorConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            GridState val = (GridState)value;
            if (val == GridState.White)
                return Colors.White;
            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
