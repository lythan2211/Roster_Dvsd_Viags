using Roster.Data.DBAccessor;
using Roster.Business.Models;
using System.Collections.Generic;
using System.Linq;
using Roster.Business.Extensions;
using System.Data.SqlClient;
using System.Data;
using System;
namespace Roster.Business.DAO
{
    public class FlightScheduleRepository : GenericRepository<TBL_Flight_Schedule>, IFlightScheduleRepository
    {
        public FlightScheduleRepository(IDbContext context)
            : base(context)
        {
        }
        public IEnumerable<TBL_Flight_Schedule> Filter(ConditionSearchFlight condition)
        {
            var flightsShedule = this.dbSet.Where(p => 1 == 1);
            if (condition.FromDate.HasValue)
            {
                flightsShedule = flightsShedule.Where(p => p.FLIGHT_DAY >= condition.FromDate.Value);
            }

            if (condition.ToDate.HasValue)
            {
                flightsShedule = flightsShedule.Where(p => p.FLIGHT_DAY <= condition.ToDate.Value);
            }

            return flightsShedule;
        }

        public IEnumerable<TBL_Flight_Schedule> GetStoreFlight(ConditionSearchFlight condition)
        {
            var flightsShedule = this.context.ExecWithStoreProcedure<TBL_Flight_Schedule>("ResearchConnection @FromDate, @ToDate",
                 new SqlParameter("FromDate", SqlDbType.Date) { Value = (condition.FromDate ?? (object)DBNull.Value) },
                 new SqlParameter("ToDate", SqlDbType.Date) { Value = (condition.ToDate ?? (object)DBNull.Value) }
                );
            return flightsShedule.ToList();
        }
    }
}

