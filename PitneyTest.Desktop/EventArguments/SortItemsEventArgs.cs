using System;
using PitneyTest.DataAccess.API;

namespace PitneyTest.EventArguments
{
    public class SortItemsEventArgs : EventArgs
    {
        public SortItemsEventArgs(string sortField, SortOrder sortOrder)
        {
            SortField = sortField;
            SortOrder = sortOrder;
        }

        public string SortField { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}