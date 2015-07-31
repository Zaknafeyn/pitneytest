using System;
using Windows.UI.Xaml.Data;

namespace PitneyTest.Tablet.Converters
{
    public class BooleanInvertedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(bool) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}