using System;
using System.Collections;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace PitneyTest.Tablet.Converters
{
    internal class EnumerableToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var collection = value as IEnumerable;
            if (collection == null)
                return Visibility.Collapsed;

            if (collection.Cast<object>().Any())
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}