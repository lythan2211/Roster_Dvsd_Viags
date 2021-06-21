using Roster.Data.DBAccessor;
using Roster.Business.Models;
using System.Collections.Generic;

namespace Roster.Business.DAO
{
    public interface IEmpSkillRepository : IRepository<TBL_Emp_Skill>
    {
        /// <summary>
        /// Get an IEnumerable TBL_Emp_Skill 
        /// </summary>
        /// <returns><c>IEnumerable TBL_Emp_Skill</c> if TBL_Emp_Skill not Empty, <c>null</c> otherwise</returns>
        IEnumerable<TBL_Emp_Skill> Filter(string employeeId);


    }
}
