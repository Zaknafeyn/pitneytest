using System;
using Windows.UI.Xaml.Data;
using Telerik.UI.Xaml.Controls.Grid;

namespace PitneyTest.Tablet.Converters
{
    internal class GroupDescriptorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var context = value as GroupHeaderContext;
            if (context == null)
            {
                return null;
            }

            switch (context.Group.Key.ToString())
            {
                case "1":
                    return "Today";
                case "2":
                    return "Yesterday";
                case "3":
                    return "LastWeek";
                case "4":
                    return "Older";
                default:
                    throw new InvalidOperationException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}