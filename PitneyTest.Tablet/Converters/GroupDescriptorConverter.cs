using System;
using Windows.UI.Xaml.Data;
using PitneyTest.Tablet.Model;
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

            switch ((GroupDescriptor) context.Group.Key)
            {
                case GroupDescriptor.Today:
                    return "Today";
                case GroupDescriptor.Yesterday:
                    return "Yesterday";
                case GroupDescriptor.LastWeek:
                    return "Last Week";
                case GroupDescriptor.Older:
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