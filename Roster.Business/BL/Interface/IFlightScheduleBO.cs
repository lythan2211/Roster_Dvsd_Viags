using Roster.Business.Models;
using System.Collections.Generic;

namespace Roster.Business.BL
{
    public interface IFlightScheduleBO
    {
        /// <summary>
        /// Search EmailActiveInfo by condition
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IEnumerable<FlightSchedulesInfo> Filter(ConditionSearchFlight condition);

        FlightSchedulesInfo Create(ConditionSearchFlight WorkScheInfo);

        FlightSchedulesInfo Update(int id, FlightSchedulesInfo WorkScheInfo);

        bool Delete(int id);

        FlightSchedulesInfo GetById(int id);

    }
}
