using Roster.Data.Utils;
using System.Collections.Generic;

namespace Roster.Business.Models
{
    public class FlightSchedulesInfo
    {
        public int Id { get; set; }

        public string FlightNo { get; set; }

        public List<string> ProductCode { get; set; }

        public List<WorkInfo> Works { get; set; }

        public FlightSchedulesInfo()
        {

        }
        public FlightSchedulesInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, FlightSchedulesInfo>(srcObject, this);
            }
        }

        public bool HasWork()
        {
            return this.Works.Count > 0;
        }
        
    }
}
