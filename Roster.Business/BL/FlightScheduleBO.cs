using Roster.Business.DAO;
using Roster.Common;
using Roster.Data.DBAccessor;
using Roster.Business.Models;
using System.Collections.Generic;
using System.Linq;
using Roster.Business.Extensions;

namespace Roster.Business.BL
{
    public class FlightScheduleBO : IFlightScheduleBO
    {
        #region Fields, Properties

        private readonly IFlightScheduleRepository flightScheduleRepository;
        #endregion

        #region Contructor

        public FlightScheduleBO(IRepositoryFactory repoFactory)
        {
            this.flightScheduleRepository = repoFactory.GetRepository<IFlightScheduleRepository>();
        }

        #endregion

        public IEnumerable<FlightSchedulesInfo> Filter(ConditionSearchFlight condition)
        {
             var flightShedules = this.flightScheduleRepository.Filter(condition);
             return flightShedules.Select(p => new FlightSchedulesInfo(p)).ToList();
        }
         
        public FlightSchedulesInfo Create(ConditionSearchFlight WorkScheInfo)
        {
            throw new System.NotImplementedException();
        }

        public FlightSchedulesInfo Update(int id, FlightSchedulesInfo WorkScheInfo)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public FlightSchedulesInfo GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}