using Roster.Data.Utils;
using System;
using System.Collections.Generic;

namespace Roster.Business.Models
{
    public class ConditionSearchFlight
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public ConditionSearchFlight(DateTime  fromDate, DateTime toDate)
        {
            this.FromDate = fromDate;
            this.ToDate = ToDate;
        }
        
    }
}
