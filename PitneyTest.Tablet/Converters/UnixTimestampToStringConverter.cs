using System;
using Windows.UI.Xaml.Data;

namespace PitneyTest.Tablet.Converters
{
    public class UnixTimestampToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return new DateTime();

            var unixTimeStamp = (long) value;

            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            while (unixTimeStamp >= int.MaxValue - 1)
            {
                unixTimeStamp -= (int.MaxValue - 1);
                dtDateTime = dtDateTime.AddMilliseconds(int.MaxValue - 1);
            }

            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime.ToString("G");
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}