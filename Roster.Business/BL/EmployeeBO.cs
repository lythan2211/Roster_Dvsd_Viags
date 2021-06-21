using Roster.Business.DAO;
using Roster.Common;
using Roster.Data.DBAccessor;
using Roster.Business.Models;
using System.Collections.Generic;
using System.Linq;
using Roster.Business.Extensions;

namespace Roster.Business.BL
{
    public class EmployeeBO : IEmployeeBO
    {
        #region Fields, Properties

        private readonly IFlightScheduleRepository flightScheduleRepository;
        #endregion

        #region Contructor

        public EmployeeBO(IRepositoryFactory repoFactory)
        {
            this.flightScheduleRepository = repoFactory.GetRepository<IFlightScheduleRepository>();
        }

        #endregion

        public List<EmployeerInfo> Filter(ConditionSearchEmployee condition)
        {
            return new List<EmployeerInfo>();
        }

        public EmployeerInfo Create(EmployeerInfo WorkScheInfo)
        {
            throw new System.NotImplementedException();
        }

        public EmployeerInfo Update(int id, EmployeerInfo WorkScheInfo)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }       
    }
}