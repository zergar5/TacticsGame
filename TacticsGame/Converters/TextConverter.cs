using System;
using System.Globalization;
using System.Windows.Data;

namespace TacticsGame.Converters
{
    public class TextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double height = (double)values[0];
            double width = (double)values[1];

            // Вычисляем новый размер шрифта на основе высоты и ширины контейнера
            double fontSize = (height + width) / 20;

            return fontSize;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
