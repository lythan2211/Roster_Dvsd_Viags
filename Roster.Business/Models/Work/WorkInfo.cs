using System;
using System.Collections.Generic;

namespace Roster.Business.Models
{
    public class WorkInfo
    {
        public int FlightId { get; set; }

        public string AirlineCode { get; set; }

        public string FlightNo { get; set; }

        public string Route { get; set; }

        public string Zone { get; set; }

        public string ProductShortName { get; set; }

        public bool IsCuts { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public WorkInfo()
        {

        }

        public WorkInfo(string employeerId, List<string> cerfiticate)
            : this()
        {
        }
    }
}
