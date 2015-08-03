using System;
using PitneyTest.DataAccess.API;

namespace PitneyTest.DataAccess.Helpers
{
    internal static class Extensions
    {
        public static string ToRequestParameter(this SortOrder value)
        {
            switch (value)
            {
                case SortOrder.Asc:
                    return "asc";
                case SortOrder.Desc:
                    return "desc";
                default:
                    throw new ArgumentOutOfRangeException(value.GetType().Name, value, null);
            }
        }

        public static string ToRequestParameter(this SortField value)
        {
            switch (value)
            {
                case SortField.StartDate:
                    return "startDate";
                case SortField.EndDate:
                    return "endDate";
                case SortField.CreateDate:
                    return "createDate";
                case SortField.ShipmentDate:
                    return "shipmentDate";
                default:
                    throw new ArgumentOutOfRangeException(value.GetType().Name, value, null);
            }
        }
    }
}