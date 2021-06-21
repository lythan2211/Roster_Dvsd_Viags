using Roster.Business.DAO;
using Roster.Data.DBAccessor;
using Roster.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Roster.Business.BL
{
    public class EmpSkillBO : IEmpSkillBO
    {
        #region Fields, Properties

        private readonly IEmpSkillRepository empSkillRepository;
        #endregion

        #region Contructor

        public EmpSkillBO(IRepositoryFactory repoFactory)
        {
            this.empSkillRepository = repoFactory.GetRepository<IEmpSkillRepository>();
        }

        #endregion

        #region Methods
        public List<EmployeerSkillInfo> Filter(string employeeId)
        {
            var employeesSkill = this.empSkillRepository.Filter(employeeId);
            return employeesSkill.Select(p => new EmployeerSkillInfo(p)).ToList();
        }

        #endregion
    }
}