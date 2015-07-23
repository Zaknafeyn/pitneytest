using System;
using System.Globalization;
using System.Windows.Data;

namespace PitneyTest.ValueConverters
{
    [ValueConversion(typeof(string), typeof(DateTime))]    
    public class DateConverter : IValueConverter
    {
        // convet UNIX timestamp to DateTime
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new DateTime();

            var unixTimeStamp = (long)value;

            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            while (unixTimeStamp >= int.MaxValue - 1)
            {
                unixTimeStamp -= (int.MaxValue - 1);
                dtDateTime = dtDateTime.AddMilliseconds(int.MaxValue - 1);
            }

            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = (DateTime)value;

            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return dateTime.Subtract(dtDateTime).TotalSeconds;
        }
    }
}