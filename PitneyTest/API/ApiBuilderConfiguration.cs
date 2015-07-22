using System;

namespace PitneyTest.API
{
    public class ApiBuilderConfiguration
    {
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
        public uint? PageNumber { get; set; }
        public uint? PageSize { get; set; }
        public SortField? SortField { get; set; }
        public SortOrder? SortOrder { get; set; }
    }
}