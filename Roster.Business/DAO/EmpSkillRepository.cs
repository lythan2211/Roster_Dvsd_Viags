using Roster.Data.DBAccessor;
using Roster.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Roster.Business.DAO
{
    public class EmpSkillRepository : GenericRepository<TBL_Emp_Skill>, IEmpSkillRepository
    {
        public EmpSkillRepository(IDbContext context)
            : base(context)
        {
        }

        public IEnumerable<TBL_Emp_Skill> Filter(string employeeId)
        {
            return this.dbSet;
        }
    }
}
