using Roster.Business.Models;
using Roster.Data.DBAccessor;
using Roster.Business.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace Roster.Business.DAO
{
    public interface IFlightScheduleRepository : IRepository<TBL_Flight_Schedule>
    {
        /// <summary>
        /// Get an IEnumerable EmailActive 
        /// </summary>
        /// <returns><c>IEnumerable EmailActive</c> if City not Empty, <c>null</c> otherwise</returns>
        IEnumerable<TBL_Flight_Schedule> Filter(ConditionSearchFlight condition);

        IEnumerable<TBL_Flight_Schedule> GetStoreFlight(ConditionSearchFlight condition);
         
    }
}
