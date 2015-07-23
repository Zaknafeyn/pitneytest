using System;
using System.Linq;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PitneyTest.ValueConverters
{
    [ValueConversion(typeof(IEnumerable), typeof(Visibility))]
    public class CollectionToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = value as IEnumerable;
            if (collection == null)
                return Visibility.Collapsed;

            if (collection.Cast<object>().Any())
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}